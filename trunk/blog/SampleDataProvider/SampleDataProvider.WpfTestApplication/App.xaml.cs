using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using DotNetDataProviderTemplate;

namespace SampleDataProvider.WpfTestApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //temporary until I get some gui...
            testReader();
            testAdapter();
        }

        static void testReader()
        {
            TemplateConnection conn = new TemplateConnection();
            conn.Open();

            TemplateCommand cmd = new TemplateCommand("select * from customers", conn);

            IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.Write(reader.GetInt32(0) + "\t");
                Console.Write(reader.GetString(1) + "\t");
                Console.Write(reader.GetInt32(2));
                Console.WriteLine();
            }
            reader.Close();

            conn.Close();
        }

        static void testAdapter()
        {
            TemplateConnection conn = new TemplateConnection();
            TemplateDataAdapter adapter = new TemplateDataAdapter();

            adapter.SelectCommand = new TemplateCommand("select * from customers", conn);

            adapter.UpdateCommand = new TemplateCommand("update name, orderid values(@name, @orderid) where id = @id", conn);
            adapter.UpdateCommand.Parameters.Add("@name", DbType.String);
            adapter.UpdateCommand.Parameters.Add("@orderid", DbType.Int32);
            adapter.UpdateCommand.Parameters.Add("@id", DbType.Int32);

            DataSet ds = new DataSet();
            adapter.Fill(ds, "Customers");

            Console.WriteLine("------------------------");
            Console.WriteLine("Results of adapter.Fill");
            printDataSet(ds);
            Console.WriteLine("------------------------");

            ds.Tables["Customers"].Rows[2]["orderid"] = 4199;
            adapter.Update(ds, "Customers");

            Console.WriteLine("------------------------");
            Console.WriteLine("Results of adapter.Update");
            printDataSet(ds);
            Console.WriteLine("------------------------");
        }

        static void printDataSet(DataSet ds)
        {
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataColumn col in table.Columns)
                    Console.Write(col.ColumnName + "\t\t");
                Console.WriteLine();
                foreach (DataRow row in table.Rows)
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                        Console.Write(row[i] + "\t\t");
                    Console.WriteLine("");
                }
            }
        }
    }
}