<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CountryDropDown.ascx.cs" Inherits="EMailAdmin.Controls.Country.CountryDropDown" %>
<%@ Register TagPrefix="ctr" TagName="CountrySelector" Src="~/Controls/Country/CountrySelector.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<script language="javascript">
    function showPopUp()
    {
        $find('mpeCountry').show();
    }
</script>
<div class="module">
    <asp:TextBox runat="server" ID="ddlCountry" CssClass="longDropdown" ReadOnly="True"/>
</div>
<div>
    <asp:HiddenField runat="server" ID="hfdCountry"/>
    <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlCountry">
        <ctr:CountrySelector runat="server" ID="ctrCountry" OnGridLoadCompleted="GridLoadedCompleted" OnClosePressed="CloseButtonPressed"/>
    </asp:Panel>
    <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
         ID="mpeCountry" TargetControlID="hfdCountry"
        PopupControlID="pnlCountry" RepositionMode="None" runat ="server" 
        BehaviorID="mpeCountry" DynamicServicePath="" Enabled="True" />
</div>