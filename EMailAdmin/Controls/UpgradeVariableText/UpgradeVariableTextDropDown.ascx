<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpgradeVariableTextDropDown.ascx.cs" Inherits="EMailAdmin.Controls.UpgradeVariableText.UpgradeVariableTextDropDown" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="uvts" TagName="UpgradeVariableTextSelector" Src="~/Controls/UpgradeVariableText/UpgradeVariableTextSelector.ascx" %>

<script language="javascript">
    function ShowUpgradeVariableTextUpgradePopUp() {
        $find('mpeUpgrade').show();
    }
</script>
<div class="module">
    <asp:TextBox runat="server" ID="ddl" CssClass="longDropdown" 
        ReadOnly="True" meta:resourcekey="ddlResource1"/>
</div>
<div>
    <asp:HiddenField runat="server" ID="hfd"/>
    <asp:Panel runat="server" CssClass="modalBackgroundRestore" 
        Style="display: none;" ID="pnl" meta:resourcekey="pnlResource1">
        <uvts:UpgradeVariableTextSelector runat="server" ID="uvtsUpgradeVariableText" OnUpgradeVariableTextGridLoadCompleted="GridUpgradeVariableTextLoadedCompleted" OnCloseUpgradeVariableTextPressed="CloseButtonPressed"/>
    </asp:Panel>
    <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
         ID="mpeUpgrade" TargetControlID="hfd"
        PopupControlID="pnl" RepositionMode="None" runat ="server" 
        BehaviorID="mpeUpgrade" DynamicServicePath="" Enabled="True" />
</div>