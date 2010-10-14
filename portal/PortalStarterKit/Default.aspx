<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="PortalStarterKit._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table cellspacing="0" cellpadding="4" width="100%" border="0">
        <tbody>
            <tr valign="top">
                <td width="5">
                    &nbsp;
                </td>
                <td id="LeftPane" width="170" runat="server" visible="false">
                </td>
                <td width="1">
                </td>
                <td id="ContentPane" width="*" runat="server" visible="false">
                </td>
                <td id="RightPane" width="230" runat="server" visible="false">
                </td>
                <td width="10">
                    &nbsp;
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
