<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpgradeVariableTextTypeDropDown.ascx.cs" Inherits="EMailAdmin.Controls.UpgradeVariableTextType.UpgradeVariableTextTypeDropDown" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="sgn" TagName="UpgradeVariableTextTypeSelector" Src="~/Controls/UpgradeVariableTextType/UpgradeVariableTextTypeSelector.ascx" %>

<script language="javascript">
    function ShowUpgradeVariableTextPopUp() {
        $find('mpeType').show();
    }
</script>
<div class="module">
    <asp:TextBox runat="server" ID="ddlType" CssClass="longDropdown" 
        ReadOnly="True" meta:resourcekey="ddlTypeResource1"/>
</div>
<div>
    <asp:HiddenField runat="server" ID="hfdType"/>
    <asp:Panel runat="server" CssClass="modalBackgroundRestore" 
        Style="display: none;" ID="pnlType" meta:resourcekey="pnlTypeResource1">
        <sgn:UpgradeVariableTextTypeSelector runat="server" ID="sgnUpgradeVariableTextType" OnUpgradeVariableTextGridLoadCompleted="GridUpgradeVariableTextLoadedCompleted" OnCloseUpgradeVariableTextPressed="CloseButtonPressed"/>
    </asp:Panel>
    <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
         ID="mpeType" TargetControlID="hfdType"
        PopupControlID="pnlType" RepositionMode="None" runat ="server" 
        BehaviorID="mpeType" DynamicServicePath="" Enabled="True" />
</div>