<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="PortalStarterKit._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
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
