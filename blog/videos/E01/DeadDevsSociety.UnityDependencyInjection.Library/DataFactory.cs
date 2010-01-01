using System.Collections.Generic;
using System.Data;

namespace DeadDevsSociety.UnityDependencyInjection.Library
{

    public abstract class DataFactory<T>
        where T : class, new()
    {
        private readonly IDbCommand _command;

        protected DataFactory(IDbCommand command)
        {
            _command = command;
            LoggerSingleton.Instance.Information("CommandText=" + command.CommandText, "DataFactory");
        }

        public IList<T> List()
        {
            IList<T> list = new List<T>();
            _command.Connection.Open();
            IDataReader reader = _command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                list.Add(Mapp(reader));
            }
            return list;
        }

        protected abstract T Mapp(IDataRecord record);
    } 

}