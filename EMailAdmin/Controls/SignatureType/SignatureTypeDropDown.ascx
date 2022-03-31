<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SignatureTypeDropDown.ascx.cs" Inherits="EMailAdmin.Controls.SignatureType.SignatureTypeDropDown" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="sgn" TagName="SignatureTypeSelector" Src="~/Controls/SignatureType/SignatureTypeSelector.ascx" %>

<script language="javascript">
    function ShowSignaturePopUp() {
        $find('mpeType').show();
    }
</script>
<div class="module">
    <asp:TextBox runat="server" ID="ddlType" CssClass="longDropdown" ReadOnly="True"/>
</div>
<div>
    <asp:HiddenField runat="server" ID="hfdType"/>
    <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlType">
        <sgn:SignatureTypeSelector runat="server" ID="sgnSignatureType" OnSignatureGridLoadCompleted="GridSignatureLoadedCompleted" OnCloseSignaturePressed="CloseButtonPressed"/>
    </asp:Panel>
    <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
         ID="mpeType" TargetControlID="hfdType"
        PopupControlID="pnlType" RepositionMode="None" runat ="server" 
        BehaviorID="mpeType" DynamicServicePath="" Enabled="True" />
</div>