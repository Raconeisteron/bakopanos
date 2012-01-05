<%@ Page CodeBehind="NotImplemented.aspx.cs" Language="c#" AutoEventWireup="True"
    Inherits="ASPNET.StarterKit.Portal.NotImplemented" MasterPageFile="~/Portal.Master" %>
<%@ OutputCache Duration="600" VaryByParam="title" %>
<%@ Import Namespace="ASPNET.StarterKit.Portal" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>
<%--
   This page is the target for the fictious links in the sample data.
--%>

<asp:Content ID="Content2" ContentPlaceHolderID="BannerContent" runat="Server">
    <ASPNETPortal:Banner ID="Banner" SelectedTabIndex="0" ShowTabs="false" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <table width="500" border="0">
            <tr>
                <td class="Normal">
                    <br />
                    <br />
                    <br />
                    <br />
                    <span id="title" class="Head" runat="server">Linked Content Not Provided</span>
                    <br />
                    <br />
                    <hr noshade="noshade" size="1" />
                    <br />
                    The link you clicked was provided as a part of the sample data for the <b>Portal Starter
                        Kit</b>. The content for this link is not provided as part of the sample application.
                    <br />
                    <br />
                    <a href="<%=Global.GetApplicationPath(Request)%>/DesktopDefault.aspx">Return to Portal
                        Home</a>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
