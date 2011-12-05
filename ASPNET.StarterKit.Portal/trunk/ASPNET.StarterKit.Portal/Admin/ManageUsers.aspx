<%@ Page Language="c#" CodeBehind="ManageUsers.aspx.cs" AutoEventWireup="True" Inherits="ASPNET.StarterKit.Portal.ManageUsers"
	MasterPageFile="~/Portal.Master" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Banner" Src="~/DesktopPortalBanner.ascx" %>

<%--
	The SecurityRoles.aspx page is used to create and edit security roles within
	the Portal application.
--%>

<asp:Content ID="Content2" ContentPlaceHolderID="BannerContent" runat="Server">
    <ASPNETPortal:Banner ID="Banner" SelectedTabIndex="0" ShowTabs="false" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
	<table width="450" cellspacing="0" cellpadding="4" border="0">
		<tr height="*" valign="top">
			<td colspan="2">
				<table width="100%" cellspacing="0" cellpadding="0">
					<tr>
						<td align="left">
							<span id="title" class="Head" runat="server">Manage User</span>
						</td>
					</tr>
					<tr>
						<td>
							<hr noshade="noshade" size="1"/>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="Normal">
				Email (or Windows domain name):
			</td>
			<td>
				<asp:TextBox ID="Email" Width="200" CssClass="NormalTextBox" runat="server" />
			</td>
		</tr>
		<tr>
			<td class="Normal">
				Password:
			</td>
			<td>
				<asp:TextBox ID="Password" Width="200" CssClass="NormalTextBox" runat="server" TextMode="Password" />
				<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
					ControlToValidate="Password" CssClass="NormalRed" Display="Dynamic"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<td class="Normal">
				Confirm Password:
			</td>
			<td>
				<asp:TextBox ID="ConfirmPassword" Width="200" CssClass="NormalTextBox" runat="server"
					TextMode="Password" />
				<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
					ControlToValidate="ConfirmPassword" CssClass="NormalRed" Display="Dynamic"></asp:RequiredFieldValidator>
				<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ControlToValidate="ConfirmPassword"
					ControlToCompare="Password" CssClass="NormalRed" Display="Dynamic"></asp:CompareValidator>
			</td>
		</tr>
		<tr>
			<td colspan="3">
				<asp:LinkButton Text="Apply Name and Password Changes" CssClass="CommandButton" runat="server"
					ID="UpdateUserBtn" OnClick="UpdateUser_Click" />
				<br>
				<br>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:DropDownList ID="allRoles" DataTextField="RoleName" DataValueField="RoleID"
					runat="server" />
				&nbsp;<asp:LinkButton ID="addExisting" CssClass="CommandButton" Text="Add user to this role"
					runat="server" CausesValidation="False" OnClick="AddRole_Click">
								Add user to this role</asp:LinkButton>
			</td>
		</tr>
		<tr valign="top">
			<td>
				&nbsp;
			</td>
			<td>
				<asp:DataList ID="userRoles" RepeatColumns="2" DataKeyField="RoleId" runat="server">
					<ItemStyle Width="225" />
					<ItemTemplate>
						&nbsp;&nbsp;
						<asp:ImageButton ImageUrl="~/images/delete.gif" CommandName="delete" AlternateText="Remove user from this role"
							runat="server" ID="Imagebutton1" />
						<asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "RoleName") %>' CssClass="Normal"
							runat="server" ID="Label1" />
					</ItemTemplate>
				</asp:DataList>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<hr noshade="noshade" size="1"/>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:LinkButton ID="saveBtn" class="CommandButton" Text="Save User Changes" runat="server"
					CausesValidation="False" OnClick="Save_Click">
								Save User Changes</asp:LinkButton>
			</td>
		</tr>
	</table>
</asp:Content>
