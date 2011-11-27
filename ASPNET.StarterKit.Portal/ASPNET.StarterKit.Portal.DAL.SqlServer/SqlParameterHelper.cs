using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    internal static class SqlParameterHelper
    {
        public static void AddParameterItemId(this SqlCommand command, int itemId)
        {
            var parameter = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};
            command.Parameters.Add(parameter);
        }

        public static SqlParameter AddParameterItemId(this SqlCommand command)
        {
            var parameter = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Direction = ParameterDirection.ReturnValue};
            command.Parameters.Add(parameter);
            return parameter;
        }

        public static void AddParameterModuleId(this SqlCommand command, int moduleId)
        {
            var parameter = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};
            command.Parameters.Add(parameter);
        }

        public static void AddParameterUserName(this SqlCommand command, string userName)
        {
            var parameter = new SqlParameter("@UserName", SqlDbType.NVarChar, 100) {Value = userName};
            command.Parameters.Add(parameter);
        }

        public static void AddParameterName(this SqlCommand command, string name)
        {
            var parameter = new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = name };
            command.Parameters.Add(parameter);
        }

        public static void AddParameterTitle(this SqlCommand command, string title)
        {
            var parameter = new SqlParameter("@Title", SqlDbType.NVarChar, 100) { Value = title };
            command.Parameters.Add(parameter);
        }

        public static void AddParameterDescription(this SqlCommand command, string description)
        {
            var parameter = new SqlParameter("@Description", SqlDbType.NVarChar, 100) { Value = description };
            command.Parameters.Add(parameter);
        }

        public static void AddParameterMoreLink(this SqlCommand command, string moreLink)
        {
            var parameter = new SqlParameter("@MoreLink", SqlDbType.NVarChar, 150) { Value = moreLink };
            command.Parameters.Add(parameter);
        }

        public static void AddParameterMobileMoreLink(this SqlCommand command, string mobileMoreLink)
        {
            var parameter = new SqlParameter("@MobileMoreLink", SqlDbType.NVarChar, 150) { Value = mobileMoreLink };
            command.Parameters.Add(parameter);
        }

        public static void AddParameterExpireDate(this SqlCommand command, DateTime expireDate)
        {
            var parameter = new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8) { Value = expireDate };
            command.Parameters.Add(parameter);
        }

        public static void AddParameterContact1(this SqlCommand command, string contact1)
        {
            var parameter = new SqlParameter("@Contact1", SqlDbType.NVarChar, 100) { Value = contact1 };
            command.Parameters.Add(parameter);
        }

        public static void AddParameterContact2(this SqlCommand command, string contact2)
        {
            var parameter = new SqlParameter("@Contact2", SqlDbType.NVarChar, 100) { Value = contact2 };
            command.Parameters.Add(parameter);
        }

        public static void AddParameterEmail(this SqlCommand command, string email)
        {
            var parameter = new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = email };
            command.Parameters.Add(parameter);
        }

        public static void AddParameterRole(this SqlCommand command, string role)
        {
            var parameter = new SqlParameter("@Role", SqlDbType.NVarChar, 100) { Value = role };
            command.Parameters.Add(parameter);
        }
    }
}
