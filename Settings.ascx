<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="FortyFingers.UserSelector.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<table width="550" cellspacing="0" cellpadding="4" border="0" width=100%>
    <tr>
        <td class="SubHead" width="150"><dnn:label id="AllowAdminAnySwitchLabel" controlname="chkAllowAdminAnySwitch" runat="server" Text="admin can switch freely" suffix=":" /></td>
        <td><asp:CheckBox runat="server" ID="chkAllowAdminAnySwitch"/></td>
    </tr>
    <tr>
        <td class="SubHead" width="150" valign="top"><dnn:label id="plAllowedRoles" controlname="lstAvailableRoles" runat="server" Text="available roles" suffix=":" /></td>
        <td valign="top">
            <table runat="server" id="AvailableRolesTable">
                <tr>
                    <td valign="top">
                        <asp:Label runat="server" ID="AvailableRolesLabel"></asp:Label><br/>
                        <asp:ListBox runat="server" ID="lstAvailableRoles" SelectionMode="Multiple" Width="300px" />
                    </td>
                    <td valign="top">
                        <asp:Button runat="server" ID="btnAddRoles" Text=">" OnClick="btnAddRoles_Click" /><br/>
                        <asp:Button runat="server" ID="btnRemoveRoles" Text="<" OnClick="btnRemoveRoles_Click"/>
                    </td>
                    <td valign="top">
                        <asp:Label runat="server" ID="AllowedRolesLabel"></asp:Label><br/>
                        <asp:ListBox runat="server" ID="lstAllowedRoles" SelectionMode="Multiple" Width="300px" />
                    </td>
                </tr>
            </table>

        </td>
    </tr>
    <tr>
        <td class="SubHead" width="150" valign="top"><dnn:label id="plAvailableUsers" controlname="lstAvailableUsers" runat="server" Text="available users" suffix=":" /></td>
        <td valign="top">
            <table runat="server" id="AvailableUsersTable">
                <tr>
                    <td valign="top">
                        <asp:ListBox runat="server" ID="lstAllUsers" SelectionMode="Multiple" Width="300px" Height="200px" />
                    </td>
                    <td valign="top">
                        <asp:Button runat="server" ID="btnAddUsers" Text=">" OnClick="btnAddUsers_Click" /><br/>
                        <asp:Button runat="server" ID="btnRemoveUsers" Text="<" OnClick="btnRemoveUsers_Click"/>
                    </td>
                    <td valign="top">
                        <asp:ListBox runat="server" ID="lstSelectedUsers" SelectionMode="Multiple" Width="300px" Height="200px" />
                    </td>
                </tr>
            </table>

        </td>
    </tr>
</table>

