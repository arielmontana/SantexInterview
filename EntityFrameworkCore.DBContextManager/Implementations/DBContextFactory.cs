using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using Blueshift.EntityFrameworkCore.MongoDB.Infrastructure;
using EntityFrameworkCore.DBContextManager.Interfaces;
using EntityFrameworkCore.DBContextManager.Config;
using EntityFrameworkCore.DBContextManager.Enums;

namespace EntityFrameworkCore.DBContextManager.Implementations
{
    /// <summary>
    /// Factory class for MySQLDBContext, OracleDBContext and SqlServerDBContext
    /// </summary>
    public class DBContextFactory : IDbContextFactory
    {
        public TDbContext CreateDbContext<TDbContext>(IOptions<DataBaseConfiguration> options) where TDbContext : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            switch (options.Value.ServerEngineValue)
            {
                case DBServerEngine.SqlServer:
                    optionsBuilder.UseSqlServer(options.Value.ConnectionString);
                    break;
                case DBServerEngine.Oracle:
                    optionsBuilder.UseOracle(options.Value.ConnectionString);
                    break;
                case DBServerEngine.MongoDB:
                    optionsBuilder.UseMongoDb(options.Value.ConnectionString);
                    break;
                default: //MySql
                    optionsBuilder.UseMySql(options.Value.ConnectionString);
                    break;
            }

            var context = (TDbContext)Activator.CreateInstance(typeof(TDbContext), new object[] { optionsBuilder.Options });
            //Ensure database creation
            context.Database.EnsureCreated();

            return context;
        }
    }
}