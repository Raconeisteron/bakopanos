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
            List<Title> list = new TitleDataFactory().List();
                        
            foreach (var title in list)
            {
                Console.WriteLine(title.Name);
            }

            // Wait for user
            Console.ReadKey();
        }
    }   

    public abstract class DataFactory<T>
        where T : class, new()
    {
        public abstract List<T> List();

        protected List<T> List(string cmd)
        {            
            var items = new List<T>();
            
            using (var conn = new SqlCeConnection(@"Data Source=Database1.sdf"))
            {
                conn.Open();
                using (var command = new SqlCeCommand(cmd, conn))
                {
                    var reader=command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        items.Add(Map(reader));
                    }
                }
            }
            return items;
        }

        public abstract T Map(IDataRecord reader);            
    }

    public class Title
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TitleDataFactory : DataFactory<Title>
    {
        public override List<Title> List()
        {
            return List("select * from titles");
        }

        public override Title Map(IDataRecord reader)            
        {
            int id = (int) reader["id"];
            string name = reader["name"] as string;
            var item = new Title {Id = id, Name = name};

            return item;
        }
    }

}
