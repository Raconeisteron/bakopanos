<%@ Control Language="c#" Inherits="ASPNET.StarterKit.Portal.Discussion" CodeBehind="Discussion.ascx.cs"
    AutoEventWireup="True" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="~/DesktopModuleTitle.ascx" %>
<%@ Import Namespace="ASPNET.StarterKit.Portal" %>
<ASPNETPortal:Title ID="Title1" runat="server" EditTarget="_new" EditUrl="~/DesktopModules/DiscussDetails.aspx"
    EditText="Add New Thread"></ASPNETPortal:Title>
<%-- discussion list --%>
<asp:DataList ID="TopLevelList" runat="server" DataKeyField="Parent" ItemStyle-CssClass="Normal"
    Width="98%">
    <ItemTemplate>
        <asp:ImageButton ID="btnSelect" ImageUrl='<%# NodeImage((int)DataBinder.Eval(Container.DataItem, "ChildCount")) %>'
            CommandName="select" runat="server" />
        <asp:HyperLink Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>' NavigateUrl='<%# FormatUrl((int)DataBinder.Eval(Container.DataItem, "ItemID")) %>'
            Target="_new" runat="server" ID="Hyperlink1" />, from
        <%# DataBinder.Eval(Container.DataItem,"CreatedByUser") %>
        , posted
        <%# DataBinder.Eval(Container.DataItem,"CreatedDate", "{0:g}") %>
    </ItemTemplate>
    <SelectedItemTemplate>
        <asp:ImageButton ID="btnCollapse" ImageUrl="~/images/minus.gif" runat="server" CommandName="collapse" />
        <asp:HyperLink Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>' NavigateUrl='<%# FormatUrl((int)DataBinder.Eval(Container.DataItem, "ItemID")) %>'
            Target="_new" runat="server" ID="Hyperlink2" />, from
        <%# DataBinder.Eval(Container.DataItem,"CreatedByUser") %>
        , posted
        <%# DataBinder.Eval(Container.DataItem,"CreatedDate", "{0:g}") %>
        <asp:DataList ID="DetailList" ItemStyle-CssClass="Normal" DataSource="<%# GetThreadMessages() %>"
            runat="server">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "Indent") %>
                <img src="<%=Global.GetApplicationPath(Request)%>/images/1x1.gif" height="15">
                <asp:HyperLink Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>' NavigateUrl='<%# FormatUrl((int)DataBinder.Eval(Container.DataItem, "ItemID")) %>'
                    Target="_new" runat="server" ID="Hyperlink3" />, from
                <%# DataBinder.Eval(Container.DataItem,"CreatedByUser") %>
                , posted
                <%# DataBinder.Eval(Container.DataItem,"CreatedDate", "{0:g}") %>
            </ItemTemplate>
        </asp:DataList>
    </SelectedItemTemplate>
</asp:DataList>
