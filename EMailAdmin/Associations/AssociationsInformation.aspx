<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssociationsInformation.aspx.cs" Inherits="EMailAdmin.Associations.AssociationsInformation" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register TagPrefix="grp" TagName="GroupViewer" Src="~/Controls/Group/GroupViewer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="asc" TagName="AssociationControl" Src="~/Controls/Associations/AssociationsControl.ascx" %>
<%@ Register TagPrefix="grc" TagName="GroupControl" Src="~/Controls/Group/GroupControl.ascx" %>
<%@ Register TagPrefix="acc" TagName="AccountSelector" Src="~/Controls/Account/AccountSelector.ascx" %>
<%@ Register TagPrefix="ctr" TagName="CountrySelector" Src="~/Controls/Country/CountrySelector.ascx" %>
<%@ Register TagPrefix="rte" TagName="RateSelector" Src="~/Controls/Rate/RateSelector.ascx" %>
<%@ Register TagPrefix="pro" TagName="ProductSelector" Src="~/Controls/Product/ProductSelector.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Associations Information</title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/loading.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function showAccountPopUp() {
            $find('mpeGroupControl').hide();
            $find('mpeAccount').show();
        }

        function showCountryPopUp() {
            $find('mpeGroupControl').hide();
            $find('mpeCountry').show();
        }

        function showProductPopUp() {
            $find('mpeGroupControl').hide();
            $find('mpeProduct').show();
        }

        function showRatePopUp() {
            $find('mpeGroupControl').hide();
            $find('mpeRate').show();
        }
    </script>
</head>
<body>
    <form id="frmMain" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="PaginaPortal">
            <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" > 
                <ProgressTemplate> 
                    <div id="progressBackgroundFilter"></div> 
                    <div id="processMessage" align="center"><p>Loading...</p>
                        <img alt="Loading..." src="../IMG/loading.gif"  /> 
                    </div> 
                </ProgressTemplate> 
            </asp:UpdateProgress>--%>
            <div class="formModule">
                <asc:AssociationControl runat="server" ID="ascAssociation" OnDeletePressed="AscAssociationDeletePressed" OnAddPressed="ShowGroupAssociations"/>
                <div class="module">
                    <asp:Button runat="server" ID="btnAccept" Text="Aceptar" CssClass="button ok" 
                        OnClick="BtnAcceptOnClick" meta:resourcekey="btnAcceptResource1" />
                    <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="button cancel" 
                        OnClick="BtnCancelOnClick" meta:resourcekey="btnCancelResource1" />
                </div>
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdGroup"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlGroup" ScrollBars="Vertical" Height="550px">
                    <div class="formModule">
                        <fieldset>
                            <div class="module">
                                <grp:GroupViewer ID="grpGroup" runat="server" OnButtonAddGroupPressed="AddGroupAssociations" OnButtonDelGroupPressed = "DelGroupAssociations" />
                                <asp:Button runat="server" ID="btnGroupViewer" CssClass="button ok" Text="Accept" OnClick="BtnAcceptGroupViewerClick" meta:resourcekey="btnAcceptResource1"/>
                                <asp:Button runat="server" ID="btnCancelGroupViewer" CssClass="button cancel" Text="Cancel" OnClick="BtnCancelGroupViewerClick" meta:resourcekey="btnCancelResource1"/>
                            </div>
                        </fieldset>     
                    </div>              
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeGroup" TargetControlID="hfdGroup"
                    PopupControlID="pnlGroup" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeGroup" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdGroupControl"/>
                <asp:Panel runat="server" ID="pnlGroupControl" 
                    CssClass="modalBackgroundRestore" Style="display: none;" 
                    meta:resourcekey="pnlGroupControlResource1">
                    <div class="formModule">
                        <fieldset>
                            <div class="module">
                                <grc:GroupControl runat="server" ID="grcGroup" OnFiltersClosed="ShowGroupControl" OnFiltersAdded="ShowGroupControl" OnDeletePressed="ShowGroupControl" OnAccountSearchButtonPressed="ShowGroupControl" OnFiltersCleared="ClearFilters"/>
                                <asp:Button runat="server" ID="btnAcceptGroup" CssClass="button ok" Text="Accept" 
                                    OnClick="BtnAcceptGroupClick" ValidationGroup="Group" 
                                    meta:resourcekey="btnAcceptGroupResource1"/>
                                <asp:Button runat="server" ID="btnCancelGroup" CssClass="button cancel" Text="Cancel" 
                                    OnClick="BtnCancelGroupClick" meta:resourcekey="btnCancelGroupResource1"/>
                            </div>
                        </fieldset>
                    </div>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                 ID="mpeGroupControl" CancelControlID="hfdGroupControl"
                PopupControlID="pnlGroupControl" RepositionMode="None" runat ="server" 
                TargetControlID="hfdGroupControl" DynamicServicePath="" Enabled="True" />
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
                <asp:HiddenField runat="server" ID="hfdDelete"/>
                <asp:Panel ID="pnlDelete" runat="server" CssClass="modalBackgroundRestore" Style="display: none;" >
                    <div class="formModule" style="text-align: left;width:250px;">
                        <fieldset>
                            <div class="module">
                                <asp:Label CssClass="label" ID="lblDelete" runat="server" Text="Desea eliminar?" meta:resourcekey="lblDeleteResource1" />
                            </div><br />
                            <div class="module" style="text-align:right;">
                                <asp:Button ID="btnAcceptDelete" runat="server" CssClass="button ok" Text="Aceptar" OnClick="BtnAcceptDeleteOnClick" meta:resourcekey="btnAcceptResource1" />
                                <asp:Button ID="btnCancelDelete" runat="server" CssClass="button cancel" Text="Cancelar" meta:resourcekey="btnCancelResource1" />
                            </div>
                        </fieldset>
                    </div>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                 ID="mpeDelete" 
                PopupControlID="pnlDelete" RepositionMode="None" runat ="server" 
                BehaviorID="ModalBehavior1" CancelControlID="btnCancelDelete"
                TargetControlID="hfdDelete" DynamicServicePath="" Enabled="True" />
            </div>
        </div>
    </form>
</body>
</html>
