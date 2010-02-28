using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using System.Configuration;
using System.Data.OleDb;

namespace ArchiCop
{       
    public static class DataHelper
    {
        public static List<T> GetData<T>(string providerName, string connectionString, string cmdText)
            where T : new()
        {
            //bool isProviderAvailable = (from item in DbProviderFactories.GetFactoryClasses().AsEnumerable()
            //         where item.Field<string>("InvariantName")==providerName
            //         select item).Count()==1;

            //if (isProviderAvailable==false)
            //{
            //    string message = string.Format("{0} is not installed.", providerName);
            //    throw new ApplicationException(message);
            //}

            var list = new List<T>();
            var ds = new DataSet();
            DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
            DbConnection connection = factory.CreateConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            DbCommand command = factory.CreateCommand();
            command.CommandText = cmdText;
            command.Connection = connection;
            DbDataAdapter adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(ds);
            connection.Close();
            
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                T item = new T();
                PropertyInfo[] props = DataMapper.GetSourceProperties(typeof(T));
                foreach (PropertyInfo property in props)
                {
                    DataMapper.SetPropertyValue(item, property.Name, row[property.Name]);
                }
                list.Add(item);
            }
            return list;
        }
    }
}