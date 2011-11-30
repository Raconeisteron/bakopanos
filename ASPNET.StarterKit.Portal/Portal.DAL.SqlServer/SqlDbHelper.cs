using System;
using System.Data;
using System.Data.SqlClient;

namespace Portal.Modules.DAL.SqlServer
{
    internal class SqlDbHelper
    {
        internal string ConnectionString { get; set; }

        protected int CreateItem(string commandText, SqlParameter returnValueParameter, params SqlParameter[] parameters)
        {
            using (var myConnection = new SqlConnection(ConnectionString))
            {
                using (var myCommand = new SqlCommand(commandText, myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add(returnValueParameter);
                    myCommand.Parameters.AddRange(parameters);
                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
            return (int) returnValueParameter.Value;
        }

        protected IDataReader GetItems(string commandText, params SqlParameter[] parameters)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand(commandText, myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.AddRange(parameters);

            // Execute the command
            myConnection.Open();
            IDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        protected IDataReader GetSingleItem(string commandText, int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand(commandText, myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(InputItemId(itemId));

            // Execute the command
            myConnection.Open();
            IDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        protected void ExecuteNonQuery(string commandText, params SqlParameter[] parameters)
        {
            using (var myConnection = new SqlConnection(ConnectionString))
            {
                using (var myCommand = new SqlCommand(commandText, myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddRange(parameters);
                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
        }

        #region Parameters

        protected static SqlParameter InputItemId(int itemId)
        {
            return new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};
        }

        protected static SqlParameter OutputItemId()
        {
            return new SqlParameter("@ItemID", SqlDbType.Int, 4) {Direction = ParameterDirection.Output};
        }

        protected static SqlParameter InputParentId(int parentId)
        {
            return new SqlParameter("@ParentID", SqlDbType.Int, 4) {Value = parentId};
        }

        protected static SqlParameter InputModuleId(int moduleId)
        {
            return new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};
        }

        protected static SqlParameter InputViewOrder(int viewOrder)
        {
            return new SqlParameter("@ViewOrder", SqlDbType.Int, 4) {Value = viewOrder};
        }

        protected static SqlParameter InputUserName(string userName)
        {
            return new SqlParameter("@UserName", SqlDbType.NVarChar, 100) {Value = userName};
        }

        protected static SqlParameter InputName(string name)
        {
            return new SqlParameter("@Name", SqlDbType.NVarChar, 100) {Value = name};
        }

        protected static SqlParameter InputTitle(string title)
        {
            return new SqlParameter("@Title", SqlDbType.NVarChar, 100) {Value = title};
        }

        protected static SqlParameter InputDescription(string description)
        {
            return new SqlParameter("@Description", SqlDbType.NVarChar, 100) {Value = description};
        }

        protected static SqlParameter InputMoreLink(string moreLink)
        {
            return new SqlParameter("@MoreLink", SqlDbType.NVarChar, 150) {Value = moreLink};
        }

        protected static SqlParameter InputMobileMoreLink(string mobileMoreLink)
        {
            return new SqlParameter("@MobileMoreLink", SqlDbType.NVarChar, 150) {Value = mobileMoreLink};
        }

        protected static SqlParameter InputUrl(string url)
        {
            return new SqlParameter("@Url", SqlDbType.NVarChar, 100) {Value = url};
        }

        protected static SqlParameter InputMobileUrl(string mobileUrl)
        {
            return new SqlParameter("@MobileUrl", SqlDbType.NVarChar, 100) {Value = mobileUrl};
        }

        protected static SqlParameter InputWhereWhen(string whereWhen)
        {
            return new SqlParameter("@WhereWhen", SqlDbType.NVarChar, 100) {Value = whereWhen};
        }

        protected static SqlParameter InputExpireDate(DateTime expireDate)
        {
            return new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8) {Value = expireDate};
        }

        protected static SqlParameter InputContact1(string contact1)
        {
            return new SqlParameter("@Contact1", SqlDbType.NVarChar, 100) {Value = contact1};
        }

        protected static SqlParameter InputContact2(string contact2)
        {
            return new SqlParameter("@Contact2", SqlDbType.NVarChar, 100) {Value = contact2};
        }

        protected static SqlParameter InputBody(string body)
        {
            return new SqlParameter("@Body", SqlDbType.NVarChar, 3000) {Value = body};
        }

        protected static SqlParameter InputDesktopHtml(string desktopHtml)
        {
            return new SqlParameter("@DesktopHtml", SqlDbType.NText) {Value = desktopHtml};
        }

        protected static SqlParameter InputMobileSummary(string mobileSummary)
        {
            return new SqlParameter("@MobileSummary", SqlDbType.NText) {Value = mobileSummary};
        }

        protected static SqlParameter InputMobileDetails(string mobileDetails)
        {
            return new SqlParameter("@MobileDetails", SqlDbType.NText) {Value = mobileDetails};
        }

        protected static SqlParameter InputParent(string parent)
        {
            return new SqlParameter("@Parent", SqlDbType.NVarChar, 750) {Value = parent};
        }

        protected static SqlParameter InputEmail(string email)
        {
            return new SqlParameter("@Email", SqlDbType.NVarChar, 100) {Value = email};
        }

        protected static SqlParameter InputRole(string role)
        {
            return new SqlParameter("@Role", SqlDbType.NVarChar, 100) {Value = role};
        }

        protected static SqlParameter InputFileFriendlyName(string fileFriendlyName)
        {
            return new SqlParameter("@FileFriendlyName", SqlDbType.NVarChar, 150) {Value = fileFriendlyName};
        }

        protected static SqlParameter InputFileNameUrl(string fileNameUrl)
        {
            return new SqlParameter("@FileNameUrl", SqlDbType.NVarChar, 250) {Value = fileNameUrl};
        }

        protected static SqlParameter InputCategory(string category)
        {
            return new SqlParameter("@Category", SqlDbType.NVarChar, 50) {Value = category};
        }

        protected static SqlParameter InputContent(byte[] content)
        {
            return new SqlParameter("@Content", SqlDbType.Image) {Value = content};
        }

        protected static SqlParameter InputContentType(string contentType)
        {
            return new SqlParameter("@ContentType", SqlDbType.NVarChar, 50) {Value = contentType};
        }

        protected static SqlParameter InputContentSize(int contentSize)
        {
            return new SqlParameter("@ContentSize", SqlDbType.Int) {Value = contentSize};
        }

        #endregion
    }
}