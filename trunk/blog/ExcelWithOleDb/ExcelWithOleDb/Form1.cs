using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace ExcelWithOleDb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection(GetConnectionString());
            connection.Open();
            string cmdText = "Select p.id, fname, lname, jobdesc From [Persons$] as p,[Jobs$] as j Where p.jobid=j.id";
            OleDbCommand command = new OleDbCommand(cmdText, connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource=ds.Tables[0];
            connection.Close();
        }

        private string _ExcelFilename="template.xls";

        public string GetConnectionString() 
       {
           return @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _ExcelFilename + 
                  @";Extended Properties=""Excel 8.0;HDR=YES;"""; 
       } 



    }
}
