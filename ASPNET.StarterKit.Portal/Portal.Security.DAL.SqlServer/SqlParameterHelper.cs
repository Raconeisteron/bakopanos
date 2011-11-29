using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.Security.DAL.SqlServer
{
    internal class SqlParameterHelper
    {
        public static SqlParameter InputItemId(int itemId)
        {
            return new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};
        }

        public static SqlParameter ReturnValueItemId()
        {
            return new SqlParameter("@ItemID", SqlDbType.Int, 4) {Direction = ParameterDirection.ReturnValue};            
        }
        
        public static SqlParameter InputParentId(int parentId)
        {
            return new SqlParameter("@ParentID", SqlDbType.Int, 4) { Value = parentId };
        }

        public static SqlParameter InputModuleId(int moduleId)
        {
            return new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};            
        }

        public static SqlParameter InputPortalId(int portalId)
        {
            return new SqlParameter("@PortalID", SqlDbType.Int, 4) {Value = portalId};            
        }

        public static SqlParameter InputUserId(int userId)
        {
            return new SqlParameter("@UserID", SqlDbType.Int, 4) {Value = userId};            
        }

        public static SqlParameter InputViewOrder(int viewOrder)
        {
            return new SqlParameter("@ViewOrder", SqlDbType.Int, 4) { Value = viewOrder};
        }

        public static SqlParameter ReturnValueUserId()
        {
            return new SqlParameter("@UserID", SqlDbType.Int, 4) {Direction = ParameterDirection.ReturnValue};                        
        }

        public static SqlParameter InputRoleId(int roleId)
        {
            return new SqlParameter("@RoleID", SqlDbType.Int, 4) {Value = roleId};            
        }

        public static SqlParameter ReturnValueRoleId()
        {
            return new SqlParameter("@RoleID", SqlDbType.Int, 4) {Direction = ParameterDirection.ReturnValue};            
        }

        public static SqlParameter InputUserName(string userName)
        {
            return new SqlParameter("@UserName", SqlDbType.NVarChar, 100) {Value = userName};            
        }

        public static SqlParameter InputName(string name)
        {
            return new SqlParameter("@Name", SqlDbType.NVarChar, 100) {Value = name};            
        }

        public static SqlParameter InputTitle(string title)
        {
            return new SqlParameter("@Title", SqlDbType.NVarChar, 100) {Value = title};            
        }

        public static SqlParameter InputDescription(string description)
        {
            return new SqlParameter("@Description", SqlDbType.NVarChar, 100) {Value = description};            
        }

        public static SqlParameter InputMoreLink(string moreLink)
        {
            return new SqlParameter("@MoreLink", SqlDbType.NVarChar, 150) {Value = moreLink};            
        }

        public static SqlParameter InputMobileMoreLink(string mobileMoreLink)
        {
            return new SqlParameter("@MobileMoreLink", SqlDbType.NVarChar, 150) {Value = mobileMoreLink};            
        }
        
        public static SqlParameter InputUrl(string url)
        {
            return new SqlParameter("@Url", SqlDbType.NVarChar, 100) { Value = url };
        }

        public static SqlParameter InputMobileUrl(string mobileUrl)
        {
            return new SqlParameter("@MobileUrl", SqlDbType.NVarChar, 100) { Value = mobileUrl };
        }

        public static SqlParameter InputWhereWhen(string whereWhen)
        {
            return new SqlParameter("@WhereWhen", SqlDbType.NVarChar, 100) { Value = whereWhen };
        }

        public static SqlParameter InputExpireDate(DateTime expireDate)
        {
            return new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8) {Value = expireDate};            
        }

        public static SqlParameter InputContact1(string contact1)
        {
            return new SqlParameter("@Contact1", SqlDbType.NVarChar, 100) {Value = contact1};            
        }

        public static SqlParameter InputContact2(string contact2)
        {
            return new SqlParameter("@Contact2", SqlDbType.NVarChar, 100) {Value = contact2};            
        }

        public static SqlParameter InputBody(string body)
        {
            return new SqlParameter("@Body", SqlDbType.NVarChar, 3000) { Value = body };
        }

        public static SqlParameter InputDesktopHtml(string desktopHtml)
        {
            return new SqlParameter("@DesktopHtml", SqlDbType.NText) { Value = desktopHtml };
        }

        public static SqlParameter InputMobileSummary(string mobileSummary)
        {
            return new SqlParameter("@MobileSummary", SqlDbType.NText) { Value = mobileSummary };
        }

        public static SqlParameter InputMobileDetails(string mobileDetails)
        {
            return new SqlParameter("@MobileDetails", SqlDbType.NText) { Value = mobileDetails };
        }

        public static SqlParameter InputParent(string parent)
        {
            return new SqlParameter("@Parent", SqlDbType.NVarChar, 750) { Value = parent };
        }

        public static SqlParameter InputEmail(string email)
        {
            return new SqlParameter("@Email", SqlDbType.NVarChar, 100) {Value = email};            
        }

        public static SqlParameter InputRole(string role)
        {
            return new SqlParameter("@Role", SqlDbType.NVarChar, 100) {Value = role};            
        }
    }
}