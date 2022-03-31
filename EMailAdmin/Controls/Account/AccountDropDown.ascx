<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountDropDown.ascx.cs" Inherits="EMailAdmin.Controls.Account.AccountDropDown" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="acc" TagName="AccountSelector" Src="~/Controls/Account/AccountSelector.ascx" %>
<script language="javascript">
    function showAccountPopUp() {
        $find('mpeAccount').show();
    }
</script>
<div class="module">
    <asp:TextBox runat="server" ID="ddlAccount" CssClass="longDropdown" ReadOnly="True"/>
</div>
<div>
    <asp:HiddenField runat="server" ID="hfdAccount"/>
    <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlAccount">
        <acc:AccountSelector runat="server" ID="accAccount" OnGridLoadCompleted="GridLoadedCompleted" OnClosePressed="CloseButtonPressed" OnSearchPressed="SearchButtonPressed"/>
    </asp:Panel>
    <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
         ID="mpeAccount" TargetControlID="hfdAccount"
        PopupControlID="pnlAccount" RepositionMode="None" runat ="server" 
        BehaviorID="mpeAccount" DynamicServicePath="" Enabled="True" />
</div>