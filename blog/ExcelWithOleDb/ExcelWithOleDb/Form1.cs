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

        private string _ExcelFilename="template.xls";

        public string GetConnectionString() 
       {
            //if HDR=NO then columns are: F1,F2,... and so on...
           return @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _ExcelFilename + 
                  @";Extended Properties=""Excel 8.0;HDR=YES;"""; 
       }

        private void buttonCreateSheet_Click(object sender, EventArgs e)
        {
           
            //problems
            //ALTER TABLE PDC ADD CONSTRAINT INFOindex0 PRIMARY KEY (NAME)
            //Create table NewTable ( ID int PRIMARY KEY, stringCol nvarchar(255),intCol int )
            //Create table NewTable ( ID int NOT NULL, stringCol nvarchar(255),intCol int )
            OleDbConnection connection = new OleDbConnection(GetConnectionString());
            connection.Open();
            string cmdText = "Create table NewTable ( ID int, stringCol nvarchar(255),intCol int )";
            OleDbCommand command = new OleDbCommand(cmdText, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection(GetConnectionString());
            connection.Open();
            string cmdText = "Insert into [NewTable$]( stringCol,intCol) Values(@stringCol,@intCol)";
            OleDbCommand command = new OleDbCommand(cmdText, connection);

            command.Parameters.AddWithValue("@stringCol", "a");
            command.Parameters.AddWithValue("@intCol", 1);

            command.ExecuteNonQuery();
            connection.Close();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection(GetConnectionString());
            connection.Open();
            string cmdText = "Update [NewTable$] Set stringCol= @stringCol, intCol = @intCol";
            OleDbCommand command = new OleDbCommand(cmdText, connection);

            command.Parameters.AddWithValue("@stringCol", "a");
            command.Parameters.AddWithValue("@intCol", 10);

            command.ExecuteNonQuery();
            connection.Close();
        }

        //problem
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection(GetConnectionString());
            connection.Open();
            string cmdText = "Delete from [NewTable$] Where intCol > @intCol";
            OleDbCommand command = new OleDbCommand(cmdText, connection);

            command.Parameters.AddWithValue("@intCol", 9);

            command.ExecuteNonQuery();
            connection.Close();
        }

        //problem: drops only columns but not data or sheet
        private void buttonDrop_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection(GetConnectionString());
            connection.Open();
            string cmdText = "Drop table NewTable";
            OleDbCommand command = new OleDbCommand(cmdText, connection);

            command.ExecuteNonQuery();
            connection.Close();
        }


        OleDbDataAdapter adapter;
        DataSet ds = new DataSet();

        //problem dynamic commands (OleDbCommandBuilder) not supported...
        private void buttonSave_Click(object sender, EventArgs e)
        {
            new OleDbCommandBuilder(adapter);            
            adapter.Update(ds);
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection(GetConnectionString());
            connection.Open();
            string cmdText = "Select p.id, fname, lname, jobdesc From [Persons$] as p,[Jobs$] as j Where p.jobid=j.id";
            OleDbCommand command = new OleDbCommand(cmdText, connection);
            adapter = new OleDbDataAdapter(command);            
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            connection.Close();
        } 



    }
}
