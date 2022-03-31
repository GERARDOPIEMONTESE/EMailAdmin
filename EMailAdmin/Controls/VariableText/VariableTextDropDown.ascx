<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VariableTextDropDown.ascx.cs" Inherits="EMailAdmin.Controls.VariableText.VariableTextDropDown" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register src="VariableTextSelector.ascx" tagname="VariableTextSelector" tagprefix="uc1" %>

<script language="javascript">
    function ShowSelectorPopUp() {
        $find('mpeSelector').show();
    }
</script>
<div class="module">
    <asp:TextBox runat="server" ID="ddlVariableText" CssClass="longTextbox" 
        ReadOnly="True" meta:resourcekey="ddlVariableTextResource1"/>
</div>
<div>
    <asp:HiddenField runat="server" ID="hfdType"/>
    <asp:Panel runat="server" CssClass="modalBackgroundRestore" 
        Style="display: none;" ID="pnlSelector" meta:resourcekey="pnlSelectorResource1">
        <div class="module">
            <uc1:VariableTextSelector ID="VariableTextSelector1" runat="server"  OnVariableTextGridLoadCompleted="GridVariableTextLoadedCompleted" OnAgregarVariableTextPressed="AgregarButtonPressed" OnCloseVariableTextPressed="CloseButtonPressed"/>
        </div>
    </asp:Panel>
    <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
         ID="mpeSelector" TargetControlID="hfdType"
        PopupControlID="pnlSelector" RepositionMode="None" runat ="server" 
        BehaviorID="mpeSelector" DynamicServicePath="" Enabled="True" />
</div>

