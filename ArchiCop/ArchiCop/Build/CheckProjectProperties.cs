using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Data;

namespace ArchiCop.Build
{
    public class CheckProjectProperties : CheckVisualStudioProjectList
    {
        [Required]
        public string ConnectionString { get; set; }
        
        [Required]
        public string ProviderName { get; set; }

        [Required]
        public string Table { get; set; }

        public override void CheckProject(VisualStudioProject project)
        {            
            DataSet ds = new DataSet(); 

            OleDbConnection connection = new OleDbConnection(ConnectionString);
            connection.Open();
            string cmdText = "Select PropertyName, PropertyPattern From [" + Table + "$]";
            OleDbCommand command = new OleDbCommand(cmdText, connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            adapter.Fill(ds);            
            connection.Close();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string name = row["PropertyName"] as string;
                string pattern = row["PropertyPattern"] as string;

                string value = DataMapper.GetPropertyValue(project, name).ToString();
                bool retValue = Regex.IsMatch(value, pattern);
                if (retValue == false)
                {
                    Log.LogError("{0} doesn't match regex pattern {1}", name, pattern);
                }                      
            }            
        }

    }
}
