<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteInfo.ascx.cs" Inherits="PortalStarterKit.DesktopModules.SiteInfo" %>

<asp:DataList ID="tabList" GridLines="Both" runat="server">
    <HeaderTemplate>
        <strong>Tabs</strong>
    </HeaderTemplate>
    <ItemTemplate>
        <%# DataBinder.Eval(Container.DataItem,"TabId") %>,
        <%# DataBinder.Eval(Container.DataItem,"TabName") %>
    </ItemTemplate>
    <FooterTemplate>        
    </FooterTemplate>
</asp:DataList>