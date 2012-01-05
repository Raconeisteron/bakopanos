<%@ Page CodeBehind="AccessDenied.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="ASPNET.StarterKit.Portal.AccessDeniedPage"
    MasterPageFile="~/Portal.Master" %>

<%@ OutputCache Duration="36000" VaryByParam="none" %>

<%@ Import Namespace="ASPNET.StarterKit.Portal" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BannerContent" runat="Server">
    <ASPNETPortal:Banner ID="Banner" SelectedTabIndex="0" ShowTabs="false" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <table width="500" border="0">
            <tr>
                <td class="Normal">
                    <br />
                    <br />
                    <br />
                    <br />
                    <span class="Head">Access Denied</span>
                    <br />
                    <br />
                    <hr noshade="noshade" size="1pt" />
                    <br />
                    Either you are not currently logged in, or you do not have access to this tab page
                    within the portal. Please contact the portal administrator to obtain access.
                    <br />
                    <br />
                    <a href="<%=Global.GetApplicationPath(Request)%>/DesktopDefault.aspx">Return to ASP.NET
                        Portal Starter Kit Home</a>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
