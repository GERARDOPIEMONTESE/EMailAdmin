<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CountryVisibleTextTypeDropDown.ascx.cs" Inherits="EMailAdmin.Controls.CountryVisibleTextType.CountryVisibleTextTypeDropDown" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="sgn" TagName="CountryVisibleTextTypeSelector" Src="~/Controls/CountryVisibleTextType/CountryVisibleTextTypeSelector.ascx" %>

<script language="javascript">
    function ShowCountryVisibleTextPopUp() {
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
        <sgn:CountryVisibleTextTypeSelector runat="server" ID="sgnCountryVisibleTextType" OnCountryVisibleTextGridLoadCompleted="GridCountryVisibleTextLoadedCompleted" OnCloseCountryVisibleTextPressed="CloseButtonPressed"/>
    </asp:Panel>
    <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
         ID="mpeType" TargetControlID="hfdType"
        PopupControlID="pnlType" RepositionMode="None" runat ="server" 
        BehaviorID="mpeType" DynamicServicePath="" Enabled="True" />
</div>