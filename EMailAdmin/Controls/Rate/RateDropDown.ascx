<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RateDropDown.ascx.cs" Inherits="EMailAdmin.Controls.Rate.RateDropDown" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="rte" TagName="RateSelector" Src="~/Controls/Rate/RateSelector.ascx" %>
<script language="javascript">
    function showRatePopUp() {
        $find('mpeRate').show();
    }
</script>
<div class="module">
    <asp:TextBox runat="server" ID="ddlRate" CssClass="longDropdown" ReadOnly="True"/>
</div>
<div>
    <asp:HiddenField runat="server" ID="hfdRate"/>
    <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlRate">
        <rte:RateSelector runat="server" ID="rteRate" OnGridLoadCompleted="GridLoadedCompleted" 
        OnCloseRatePressed="CloseButtonPressed" OnSearchPressed="SearchButtonPressed" OnCountryChangedCompleted="CountryChanged"/>
    </asp:Panel>
    <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
         ID="mpeRate" TargetControlID="hfdRate"
        PopupControlID="pnlRate" RepositionMode="None" runat ="server" 
        BehaviorID="mpeRate" DynamicServicePath="" Enabled="True" />
</div>