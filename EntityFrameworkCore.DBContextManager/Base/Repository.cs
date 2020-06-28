using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using EntityFrameworkCore.DBContextManager.Config;
using EntityFrameworkCore.DBContextManager.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EntityFrameworkCore.DBContextManager.Base
{
    /// <summary>
    /// Base class for repositories 
    /// </summary>
    /// <typeparam name="TDbContext">Context</typeparam>
    public class Repository<TDbContext> where TDbContext : DbContext
    {
        protected readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;
        protected readonly IOptions<DataBaseConfiguration> _options;
        private static object syncRoot = new Object();
        //you need the syncContext to avoid duplicate db connections.
        protected object syncContext = new Object();
        private static volatile ConcurrentDictionary<string, List<PropertyInfo>> spProperties;
        public static ConcurrentDictionary<string, List<PropertyInfo>> SpProperties
        {
            get
            {
                if (spProperties == null)
                {
                    lock (syncRoot)
                    {
                        if (spProperties == null)
                            spProperties = new ConcurrentDictionary<string, List<PropertyInfo>>();
                    }
                }

                return spProperties;
            }
        }

        public Repository(IAmbientDbContextLocator ambientDbContextLocator, IDbContextScopeFactory dbContextScopeFactory, IOptions<DataBaseConfiguration> options)
        {
            _ambientDbContextLocator = ambientDbContextLocator;
            _dbContextScopeFactory = dbContextScopeFactory;
            _options = options;
        }

        /// <summary>
        /// Get all properties to set the query's result.
        /// </summary>
        /// <typeparam name="T">Type of an item of the list result</typeparam>
        /// <param name="spName">Stored procedure name</param>
        /// <param name="columns">list of column names</param>
        /// <returns>List<PropertyInfo></returns>
        private List<PropertyInfo> GetPropertiesOfResult<T>(string spName, List<string> columns) where T : new()
        {
            List<PropertyInfo> listProperties;
            if (SpProperties.TryGetValue(spName, out listProperties))
                return listProperties;

            T resultItem = new T();
            listProperties = new List<PropertyInfo>();
            foreach (string item in columns)
            {
                listProperties.Add(resultItem.GetType().GetProperty(item, BindingFlags.Public | BindingFlags.Instance));
            }
            SpProperties.TryAdd(spName, listProperties);
            return listProperties;
        }

        /// <summary>
        /// Open connection and configure dbCommand.
        /// </summary>
        /// <typeparam name="T">Type of a derived class of BDParameter</typeparam>
        /// <param name="spName">Stored procedure name</param>
        /// <param name="parameters">List of parameters</param>
        /// <param name="timeout">time before a timeout</param>
        /// <param name="cmdType">CommandType: This value can be Text or Stored Procedure.</param>
        /// <returns>DbCommand</returns>
        private DbCommand ExecuteSqlCommandBase<T>(string spName, List<T> parameters, int timeout = 7200, CommandType cmdType = CommandType.StoredProcedure) where T : DbParameter
        {
            var cmd = AmbientDbContext.Database.GetDbConnection().CreateCommand();
            cmd.CommandTimeout = timeout;
            cmd.CommandText = spName;
            cmd.CommandType = cmdType;

            parameters.ForEach(param => cmd.Parameters.Add(param));

            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();

            return cmd;
        }

        /// <summary>
        /// Execute stored procedure with a result.
        /// </summary>
        /// <typeparam name="T">Type of an item of the list result</typeparam>
        /// <typeparam name="U">Type of a derived class of BDParameter</typeparam>
        /// <param name="spName">Stored procedure name</param>
        /// <param name="parameters">List of parameters</param>
        /// <param name="timeout">time before a timeout</param>
        /// <returns>List<T></returns>
        protected List<T> ExecuteProcedure<T, U>(string spName, List<U> parameters, int timeout = 7200) where T : new() where U : DbParameter
        {
            lock (syncContext)
            {
                using (var dbContextScope = _dbContextScopeFactory.Create())
                {
                    var cmd = ExecuteSqlCommandBase(spName, parameters, timeout);
                    List<T> result = new List<T>();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            var columns = Enumerable.Range(0, reader.FieldCount).Select(i => reader.GetName(i)).ToList();
                            List<PropertyInfo> listProperties = GetPropertiesOfResult<T>(spName, columns);

                            while (reader.Read())
                            {
                                T resultItem = new T();
                                for (int i = 0; i < columns.Count; i++)
                                {
                                    object item;
                                    if (reader.GetFieldType(i) == listProperties[i].PropertyType)
                                        item = reader.IsDBNull(i) ? null : reader[columns[i]];
                                    else
                                    {
                                        Type t = Nullable.GetUnderlyingType(listProperties[i].PropertyType) ?? listProperties[i].PropertyType;
                                        item = reader.IsDBNull(i) ? Activator.CreateInstance(listProperties[i].PropertyType) : Convert.ChangeType(reader[columns[i]], t);
                                    }


                                    listProperties[i].SetValue(resultItem, item);
                                }
                                result.Add(resultItem);
                            }
                        }
                    }

                    return result;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Type of a derived class of BDParameter</typeparam>
        /// <param name="spName">Stored procedure name</param>
        /// <param name="parameters">List of parameters</param>
        /// <param name="timeout">time before a timeout</param>
        /// <param name="cmdType">CommandType: This value can be Text or Stored Procedure.</param>
        /// <returns>result of ExecuteNonQuery</returns>
        protected int ExecuteProcedureNonQuery<T>(string spName, List<T> parameters, int timeout = 7200) where T : DbParameter
        {
            lock (syncContext)
            {
                using (var dbContextScope = _dbContextScopeFactory.Create())
                {
                    var cmd = ExecuteSqlCommandBase(spName, parameters, timeout, CommandType.StoredProcedure);
                    return cmd.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Type of a derived class of BDParameter</typeparam>
        /// <param name="sqlText">Stored procedure name or Sql sentence</param>
        /// <param name="parameters">List of parameters</param>
        /// <param name="timeout">time before a timeout</param>
        /// <param name="cmdType">CommandType: This value can be Text or Stored Procedure.</param>
        /// <returns>Returns all parameter sent. This includes output parameters.</returns>
        protected DbParameterCollection ExecuteSqlCommand<T>(string sqlText, List<T> parameters, int timeout = 7200, CommandType cmdType = CommandType.Text) where T : DbParameter
        {
            lock (syncContext)
            {
                using (var dbContextScope = _dbContextScopeFactory.Create())
                {
                    var cmd = ExecuteSqlCommandBase(sqlText, parameters, timeout, cmdType);
                    cmd.ExecuteNonQuery();
                    return cmd.Parameters;
                }
            }
        }


        protected TDbContext AmbientDbContext
        {
            get
            {
                lock (syncContext)
                {
                    var dbContext = _ambientDbContextLocator.Get<TDbContext>(_options);
                    if (dbContext == null)
                        dbContext = _dbContextScopeFactory.Create().DbContexts.Get<TDbContext>(_options);
                    return dbContext;
                }
            }
        }

    }
}
