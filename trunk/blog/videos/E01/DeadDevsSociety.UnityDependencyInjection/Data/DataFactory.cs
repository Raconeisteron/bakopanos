
using System.Collections.Generic;
using System.Data;

namespace DeadDevsSociety.UnityDependencyInjection.Data
{
    public abstract class DataFactory<T>
        where T : class, new()
    {
        protected readonly IDbCommand Command;

        protected DataFactory(IDbCommand command)
        {
            Command = command;
        }

        public IList<T> List()
        {
            IList<T> list = new List<T>();
            Command.Connection.Open();
            IDataReader reader = Command.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                var item = Map(reader);                
                list.Add(item);                
            }

            return list;
        }

        public abstract T Map(IDataRecord record);
    }
}
