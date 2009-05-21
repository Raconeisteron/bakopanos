using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;

namespace GangOfFour.FactoryMethod
{
    /// <summary>
    /// MainApp startup class for .NET optimized 
    /// Factory Method Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            List<Title> list;
            using(var conn = new SqlCeConnection(@"Data Source=Database1.sdf"))
            {
                conn.Open();
                using(var command = new SqlCeCommand("select * from titles",conn))
                {
                    list = new TitleDataFactory().Map(
                        command.ExecuteReader(CommandBehavior.CloseConnection));
                }
            }

            foreach (var title in list)
            {
                Console.WriteLine(title.Name);
            }

            // Wait for user
            Console.ReadKey();
        }
    }   

    public abstract class DataFactory<T>
    {
        protected List<T> Items = new List<T>();
        public abstract List<T> Map(IDataReader reader);
    }

    public class Title
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TitleDataFactory : DataFactory<Title>
    {
        public override List<Title> Map(IDataReader reader)
        {            
            while (reader.Read())
            {
                int id = (int)reader["id"];
                string name = reader["name"] as string;
                var item = new Title {Id = id, Name = name};
                Items.Add(item);
            }
            return Items;
        }
    }

}
