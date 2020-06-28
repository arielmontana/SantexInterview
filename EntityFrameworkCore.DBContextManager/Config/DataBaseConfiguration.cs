using System;
using EntityFrameworkCore.DBContextManager.Enums;

namespace EntityFrameworkCore.DBContextManager.Config
{
    /// <summary>
    /// Class used to database configuration.
    /// </summary>
    public class DataBaseConfiguration
    {
        public string ConnectionString { get; set; }
        public string ServerEngine { get; set; }
        public DBServerEngine ServerEngineValue { get { return (DBServerEngine)Enum.Parse(typeof(DBServerEngine), ServerEngine); }  }
    }
}