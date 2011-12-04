<%@ Page Language="c#" CodeBehind="DesktopDefault.aspx.cs" AutoEventWireup="True"
    Inherits="ASPNET.StarterKit.Portal.DesktopDefault" MasterPageFile="~/Portal.Master" %>
<%--

   The DesktopDefault.aspx page is used to load and populate each Portal View.  It accomplishes
   this by reading the layout configuration of the portal from the Portal Configuration
   system, and then using this information to dynamically instantiate portal modules
   (each implemented as an ASP.NET User Control), and then inject them into the page.

--%>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <table width="100%" cellspacing="0" cellpadding="4" border="0">
        <tr height="*" valign="top">
            <td width="5">
                &nbsp;
            </td>
            <td id="LeftPane" visible="false" width="170" runat="server">
            </td>
            <td width="1">
            </td>
            <td id="ContentPane" visible="false" width="*" runat="server">
            </td>
            <td id="RightPane" visible="false" width="230" runat="server">
            </td>
            <td width="10">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
