using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Diagnostics;

namespace DeadDevsSociety.UnityDependencyInjection
{
    internal static class Program
    {
        private static readonly Presentation _presentation = new Presentation();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            _presentation.Show();

            Console.ReadLine();
        }
    }

    public class Presentation
    {
        private readonly AppServices _appServices = new AppServices();

        public void Show()
        {
            foreach (Product item in _appServices.Products("."))
            {
                Console.WriteLine("{0}, {1}",item.FirstName, item.LastName);
            }
        }
    }


    public class AppServices
    {
        private readonly Repository _repository = new Repository();

        public IEnumerable<Product> Products(string filter)
        {
            return _repository.List();
        }
    }

    public class Repository : DataFactory<Product>
    {
        protected override IDbCommand Command()
        {
            var connection = new SqlCeConnection(@"Data Source=Database\Database1.sdf");
            return new SqlCeCommand("select * from products", connection);
        }

        protected override Product Mapp(IDataRecord record)
        {
            var item = new Product
                           {
                               FirstName = record["FirstName"] as string,
                               LastName = record["LastName"] as string
                           };
            return item;
        }
    }

    public class Product
    {
        public string FirstName{ get; set;}
        public string LastName{ get; set;}
    }

    public class Logger
    {
        public void Information(string message)
        {
            Console.WriteLine("Information: {0}", message);
        }
    }

    public abstract class DataFactory<T>
        where T:class,new()
    {
        protected abstract IDbCommand Command();

        public IList<T> List()
        {
            var command = Command();
            IList<T> list = new List<T>();
            command.Connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {                               
                list.Add(Mapp(reader));
            }
            return list;
        }

        protected abstract T Mapp(IDataRecord record);
    }
}