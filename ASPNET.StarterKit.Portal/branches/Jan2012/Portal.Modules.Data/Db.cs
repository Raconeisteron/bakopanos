using System;
using System.Configuration;
using System.Data;
using Microsoft.Practices.Unity;

namespace Portal.Modules.Data
{
    public abstract class Db : IDb
    {
        protected string ConnectionString { get; private set; }

        #region IDb Members

        public abstract IDataReader GetList(int moduleId);

        public abstract IDataReader GetSingle(int itemId);

        public abstract void Delete(int itemId);

        #endregion

        [InjectionMethod]
        public void Initialize(ConnectionStringSettings connectionStringSettings)
        {
            ConnectionString = connectionStringSettings.ConnectionString;
        }
    }
}