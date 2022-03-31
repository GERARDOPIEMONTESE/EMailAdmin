<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductDropDown.ascx.cs" Inherits="EMailAdmin.Controls.Product.ProductDropDown" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="pro" TagName="ProductSelector" Src="~/Controls/Product/ProductSelector.ascx" %>
<script language="javascript">
    function showProductPopUp() {
        $find('mpeProduct').show();
    }
</script>
<div class="module">
    <asp:TextBox runat="server" ID="ddlProduct" CssClass="longDropdown" ReadOnly="True"/>
</div>
<div>
    <asp:HiddenField runat="server" ID="hfdProduct"/>
    <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlProduct">
        <pro:ProductSelector runat="server" ID="proProduct" OnGridLoadCompleted="GridLoadedCompleted" OnCloseProductPressed="CloseButtonPressed" OnSearchPressed="SearchButtonPressed"/>
    </asp:Panel>
    <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
         ID="mpeProduct" TargetControlID="hfdProduct"
        PopupControlID="pnlProduct" RepositionMode="None" runat ="server" 
        BehaviorID="mpeProduct" DynamicServicePath="" Enabled="True" />
</div>