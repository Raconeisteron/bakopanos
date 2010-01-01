using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text.RegularExpressions;

namespace DeadDevsSociety.UnityDependencyInjection
{
    internal static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var presentation = new ProductView();

            presentation.Show();

            Console.ReadLine();
        }
    }
    
    public class ProductView
    {
        private readonly ProductServices _appServices = new ProductServices();

        public void Show()
        {
            foreach (Product item in _appServices.Products("."))
            {
                if (item.DateOfBirth.HasValue)
                {
                    Console.WriteLine("{0}, {1} ({2})", 
                        item.FirstName, item.LastName, item.DateOfBirth.Value.ToShortDateString());                   
                }
                else
                {
                    Console.WriteLine("{0}, {1}", 
                        item.FirstName, item.LastName);                    
                }
            }
        }
    }    

    public class ProductServices
    {
        private readonly ProductRepository _repository = new ProductRepository();

        public IEnumerable<Product> Products(string filter)
        {
            return from item in _repository.List()
                   where (Regex.IsMatch(item.FirstName, filter) | 
                        Regex.IsMatch(item.LastName, filter))
                   select item;
        }
    }
    
    public class ProductRepository : DataFactory<Product>
    {        
        public ProductRepository() :
            base(new SqlCeCommand("select * from products", 
                new SqlCeConnection(@"Data Source=Database\Database1.sdf")))
        {
        }

        private const string Id = "Id";
        private const string FirstName = "FirstName";
        private const string LastName = "LastName";
        private const string DateOfBirth = "DateOfBirth";

        protected override Product Mapp(IDataRecord record)
        {
            var item = new Product
                           {
                               //required allow null == false
                               Id = (int) record[Id],
                               FirstName = record[FirstName] as string,
                               LastName = record[LastName] as string                 
                           };
            //optional allow null == true
            if (record[DateOfBirth] != DBNull.Value)
            {
                item.DateOfBirth = Convert.ToDateTime(record[DateOfBirth]);
            }
            return item;
        }
    }    
    
    public class Product
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    } 
    

    public class Logger
    {
        public void Information(string message)
        {
            Console.WriteLine("Information: {0}", message);
        }
    }

    public abstract class DataFactory<T>
        where T : class, new()
    {
        private readonly IDbCommand _command;

        protected DataFactory(IDbCommand command)
        {
            _command = command;
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