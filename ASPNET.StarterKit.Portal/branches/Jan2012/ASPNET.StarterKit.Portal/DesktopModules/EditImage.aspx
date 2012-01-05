<%@ Page Language="c#" CodeBehind="EditImage.aspx.cs" AutoEventWireup="True" Inherits="ASPNET.StarterKit.Portal.EditImage"
    MasterPageFile="~/Portal.Master" %>

<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BannerContent" runat="Server">
    <ASPNETPortal:Banner ID="Banner" SelectedTabIndex="0" ShowTabs="false" runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <table width="98%" cellspacing="0" cellpadding="4" border="0">
        <tr valign="top">
            <td width="150">
                &nbsp;
            </td>
            <td width="*">
                <table width="500" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" class="Head">
                            Image Settings
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr noshade="noshade" size="1" />
                        </td>
                    </tr>
                </table>
                <table width="500" cellspacing="0" cellpadding="0">
                    <tr valign="top">
                        <td width="100" class="SubHead">
                            Src Location:
                        </td>
                        <td rowspan="3">
                            &nbsp;
                        </td>
                        <td class="Normal">
                            <asp:TextBox ID="Src" CssClass="NormalTextBox" Width="390" Columns="30" runat="server" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="SubHead">
                            Image Width:
                        </td>
                        <td>
                            <asp:TextBox ID="Width" CssClass="NormalTextBox" Width="390" Columns="30" runat="server" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="SubHead">
                            Image Height:
                        </td>
                        <td>
                            <asp:TextBox ID="Height" CssClass="NormalTextBox" Width="390" Columns="30" runat="server" />
                        </td>
                    </tr>
                </table>
                <p>
                    <asp:LinkButton ID="updateButton" Text="Update" runat="server" class="CommandButton"
                        BorderStyle="none" OnClick="UpdateBtn_Click" />
                    &nbsp;
                    <asp:LinkButton ID="cancelButton" Text="Cancel" CausesValidation="False" runat="server"
                        class="CommandButton" BorderStyle="none" OnClick="CancelBtn_Click" />
                </p>
            </td>
        </tr>
    </table>
</asp:Content>
