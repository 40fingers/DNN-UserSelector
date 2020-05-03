<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="FortyFingers.UserSelector.View" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<div runat="server" id="SelectUserDiv" class="dnnForm">
    <asp:Label runat="server" ID="NotAllowedLabel" resourcekey="NotAllowedLabel" CssClass="dnnFormMessage dnnFormInfo"></asp:Label>
    <asp:Label runat="server" ID="ErrorLabel" Visible="False" EnableViewState="False" CssClass="dnnFormMessage dnnFormWarning"></asp:Label>
    <fieldset>
        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="UserLabel" resourcekey="UserLabel"></dnn:Label>
            <asp:DropDownList runat="server" AutoPostBack="True" OnSelectedIndexChanged="UserDropDown_SelectedIndexChanged" ID="UserDropDown"></asp:DropDownList>
        </div>
        <div class="dnnFormItem" runat="server" id="UserNamePanel">
            <dnn:Label runat="server" ID="UserNameLabel" resourcekey="UserNameLabel"></dnn:Label>
            <div>
                <asp:TextBox runat="server" ID="UserNameTextBox"></asp:TextBox>
                <asp:LinkButton runat="server" Text="Switch" ID="SwitchToUserNameButton" CssClass="dnnPrimaryAction" OnClick="SwitchToUserNameButton_Click"></asp:LinkButton></li>
            </div>
        </div>
        <div class="dnnFormItem" runat="server" id="SuperUsersPanel">
            <dnn:Label runat="server" ID="SuperUserLabel" resourcekey="SuperUserLabel"></dnn:Label>
            <asp:LinkButton runat="server" ID="SuperUserButton" OnClick="SuperUserButton_Click"></asp:LinkButton>
        </div>
    </fieldset>
    <ul class="dnnActions dnnClear">
        <li>
            <asp:LinkButton runat="server" ID="BackToInitialUserButton" CssClass="dnnPrimaryAction" OnClick="BackToInitialUserButton_Click"></asp:LinkButton></li>
    </ul>
</div>
