<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupInformation.aspx.cs" Inherits="EMailAdmin.Group.GroupInformation" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register TagPrefix="grp" TagName="GroupControl" Src="~/Controls/Group/GroupControl.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="acc" TagName="AccountSelector" Src="~/Controls/Account/AccountSelector.ascx" %>
<%@ Register TagPrefix="ctr" TagName="CountrySelector" Src="~/Controls/Country/CountrySelector.ascx" %>
<%@ Register TagPrefix="rte" TagName="RateSelector" Src="~/Controls/Rate/RateSelector.ascx" %>
<%@ Register TagPrefix="pro" TagName="ProductSelector" Src="~/Controls/Product/ProductSelector.ascx" %>
<%@ Register Tagprefix="dvc" Tagname="DynamicValueConfig" src="~/Controls/DynamicValue/DynamicValueConfig.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Group Information</title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/loading.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function showAccountPopUp() {
            $find('mpeAccount').show();
        }

        function showCountryPopUp() {
            $find('mpeCountry').show();
        }

        function showProductPopUp() {
            $find('mpeProduct').show();
        }

        function showRatePopUp() {
            $find('mpeRate').show();
        }

        function showDynamicValuePopUp() {
            $find('mpeDynamicValue').show();
        }
    </script>
</head>
<body>
    <form id="frmMain" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="PaginaPortal">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" > 
                <ProgressTemplate> 
                    <div id="progressBackgroundFilter"></div> 
                    <div id="processMessage" align="center">
                        <p>Loading...</p>
                        <img alt="Loading..." src="../IMG/loading.gif"  /> 
                    </div> 
                </ProgressTemplate> 
            </asp:UpdateProgress>
            <div class="form medium">
                <fieldset>
                    <legend>
                        <h2><asp:Literal ID="ltrGroupConditions" runat="server" Text="Grupo Condiciones" meta:resourcekey="ltrGroupConditions"></asp:Literal></h2>
                    </legend>
                    <grp:GroupControl runat="server" ID="grpGroup"/>
                </fieldset>
                <div class="module btn-cnt">
                    <asp:Button runat="server" ID="btnAccept" Text="Aceptar" CssClass="button ok" ValidationGroup="Group"
                        OnClick="BtnAcceptOnClick" meta:resourcekey="btnAcceptResource1" />
                    <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="button cancel" 
                        OnClick="BtnCancelOnClick" meta:resourcekey="btnCancelResource1" />
                </div>
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdMissing"/>
                <asp:Panel ID="pnlMissing" runat="server" CssClass="modalBackgroundRestore" 
                    Style="display: none;" meta:resourcekey="pnlMissingResource1" >
                    <div class="formModule">
                        <fieldset style="width: 300px;">                            
                            <div class="module">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label CssClass="label" ID="lblMissing" runat="server" 
                                                Text="Missing Information" meta:resourcekey="lblMissingResource1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnAcceptMissing" runat="server" CssClass="button ok" 
                                                Text="Accept" meta:resourcekey="btnAcceptMissingResource1" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </fieldset>
                    </div>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                 ID="mpeMissing" 
                PopupControlID="pnlMissing" RepositionMode="None" runat ="server" CancelControlID="btnAcceptMissing"
                TargetControlID="hfdMissing" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdAccount"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlAccount">
                    <acc:AccountSelector runat="server" ID="accAccount" OnSearchPressed="AccAccountSelectorOnSearch" OnClosePressed="AccAccountSelectorOnClose" />
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeAccount" TargetControlID="hfdAccount"
                    PopupControlID="pnlAccount" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeAccount" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdCountry"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlCountry">
                    <ctr:CountrySelector runat="server" ID="ctrCountry" OnClosePressed="CtrCountryOnClose" OnChkPressed="CtrCountryOnChkPressed"/>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeCountry" TargetControlID="hfdCountry"
                    PopupControlID="pnlCountry" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeCountry" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdProduct"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlProduct">
                    <pro:ProductSelector runat="server" ID="proProduct" OnCloseProductPressed="ProProductSelectorOnClose" OnSearchPressed="ProProductSelectorOnSearch" OnChkPressed="ProProductOnChkPressed"/>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeProduct" TargetControlID="hfdProduct"
                    PopupControlID="pnlProduct" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeProduct" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdRate"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlRate">
                    <rte:RateSelector runat="server" ID="rteRate" 
                    OnCloseRatePressed="RteRateSelectorOnClose" OnSearchPressed="RteRateSelectorOnSearch" OnCountryChangedCompleted="RteRateSelectorOnCountryChanged" OnChkPressed="RteRateOnChkPressed"/>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeRate" TargetControlID="hfdRate"
                    PopupControlID="pnlRate" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeRate" DynamicServicePath="" Enabled="True" />
            </div> 
            <div>                
                <asp:HiddenField runat="server" ID="hfdDynamicValue"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlDynamicValue">
                    <dvc:DynamicValueConfig ID="dvcDynamicValueConfig" runat="server" 
                    OnCloseDynamicValuePressed="DynamicValueOnClose" OnSearchPressed="DynamicValueOnSearch" OnChkPressed="DynamicValueOnChkPressed"/>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeDynamicValue" TargetControlID="hfdDynamicValue"
                    PopupControlID="pnlDynamicValue" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeDynamicValue" DynamicServicePath="" Enabled="True" />
            </div>
        </div>
    </form>
</body>
</html>
