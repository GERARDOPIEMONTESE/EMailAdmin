<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateInformation.aspx.cs" Inherits="EMailAdmin.Template.TemplateInformation" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" ValidateRequest="false" enableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="tcl" TagName="TemplateControl" Src="~/Controls/Template/TabTemplateControl.ascx" %>
<%@ Register TagPrefix="img" TagName="ImageSelector" Src="~/Controls/Selectors/Image/ImageSelector.ascx" %>
<%@ Register TagPrefix="lnk" TagName="LinkSelector" Src="~/Controls/Selectors/Link/LinkSelector.ascx" %>
<%@ Register TagPrefix="vtx" TagName="VariableTextSelector" Src="~/Controls/Selectors/VariableText/VariableTextSelector.ascx" %>
<%@ Register TagPrefix="sgr" TagName="SignatureSelector" Src="~/Controls/Selectors/Signature/SignatureSelector.ascx" %>
<%@ Register TagPrefix="cvt" TagName="CouuntryVisibleTextSelector" Src="~/Controls/Selectors/CountryVisibleText/CountryVisibleTextSelector.ascx" %>
<%@ Register TagPrefix="uvt" TagName="UpgradeVariableTextSelector" Src="~/Controls/Selectors/UpgradeVariableText/UpgradeVariableTextSelector.ascx" %>
<%@ Register TagPrefix="ctc" TagName="ContactSelector" Src="~/Controls/Selectors/Contact/ContactSelector.ascx" %>
<%@ Register TagPrefix="att" TagName="AttachmentSelector" Src="~/Controls/Selectors/Attachment/AttachmentSelector.ascx" %>
<%@ Register TagPrefix="prv" TagName="Preview" Src="~/Controls/Preview/Preview.ascx" %>
<%@ Register TagPrefix="grp" TagName="GroupViewer" Src="~/Controls/Group/GroupViewer.ascx" %>
<%@ Register TagPrefix="grc" TagName="GroupControl" Src="~/Controls/Group/GroupControl.ascx" %>
<%@ Register TagPrefix="asc" TagName="AssociationControl" Src="~/Controls/Associations/AssociationsControl.ascx" %>
<%@ Register TagPrefix="acc" TagName="AccountSelector" Src="~/Controls/Account/AccountSelector.ascx" %>
<%@ Register TagPrefix="ctr" TagName="CountrySelector" Src="~/Controls/Country/CountrySelector.ascx" %>
<%@ Register TagPrefix="rte" TagName="RateSelector" Src="~/Controls/Rate/RateSelector.ascx" %>
<%@ Register TagPrefix="pro" TagName="ProductSelector" Src="~/Controls/Product/ProductSelector.ascx" %>
<%@ Register tagprefix="tab" tagname="TableVariableSelector" src="~/Controls/Selectors/TableVariableText/TableVariableSelector.ascx" %>
<%@ Register tagprefix="cdvt" tagname="ConditionVariableTextSelector" src="~/Controls/Selectors/ConditionVariableText/ConditionVariableTextSelector.ascx"%>
<%@ Register TagPrefix="pxl" TagName="PixelSelector" Src="~/Controls/Selectors/Pixel/PixelSelector.ascx" %>
<%@ Register TagPrefix="cla" TagName="ClausuleSelector" Src="~/Controls/Selectors/Clausule/ClausuleSelector.ascx" %>

<%@ Register src="../Controls/ContentAttachmentVariable/ContentAttachment.ascx" tagname="ContentAttachment" tagprefix="uc1" %>
<%@ Register src="../Controls/AttachmentGroup/AttachmentGroup.ascx" tagname="AttachmentGroup" tagprefix="cag" %>
<%@ Register src="../Controls/AttachmentTemplates/AttachmentTemplatesSelector.ascx" tagname="AttachmentTemplatesSelector" tagprefix="ats" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Signature Information</title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/loading.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/ColorPicker/jscolor.js" type="text/javascript"></script>
    <script type="text/javascript">
        function OvrdSubmit()
        {
            var ftbSubmit=document.forms[0].onsubmit;
            if (typeof(ftbSubmit) == 'function')
            {
                document.forms[0].onsubmit = function()
                {
                    try{ftbSubmit();}
                    catch(ex){}
                }
            }

            // We are ok
            return true;
        }
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
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" EnableViewState="true" EnableScriptGlobalization="True" EnableScriptLocalization="True"/>
        <div class="PaginaPortal">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" > 
                <ProgressTemplate> 
                    <div id="progressBackgroundFilter"></div> 
                    <div id="processMessage" align="center"><p>Loading...</p>
                        <img alt="Loading..." src="../IMG/loading.gif"  /> 
                    </div> 
                </ProgressTemplate> 
            </asp:UpdateProgress>
        <asp:UpdatePanel runat="server" ID="UpButton">
        <ContentTemplate>
            <div class="formModule">
                <fieldset>
                    <div class="module">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblName" Text="Nombre" CssClass="label" meta:resourcekey="lblNameResource1" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="textbox" MaxLength="149"/>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" ErrorMessage=" *" 
                                        ForeColor="Red" ValidationGroup="Template" meta:resourcekey="rfvNameResource1"/>
                                </td>
                                <td>
                                    <asp:CustomValidator runat="server" ID="ctmName" ErrorMessage="   *" ForeColor="Red" 
                                    ValidationGroup="Template" Display="Dynamic" OnServerValidate="CtmNameValidator" 
                                    meta:resourcekey="ctmNameResource1"/>    
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblType" Text="Tipo" CssClass="label" meta:resourcekey="lblTypeResource1" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="dropdown" DataTextField="Descripcion" DataValueField="Id" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="module" style="z-index: 9999;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblFromDate" Text="Fecha Desde" CssClass="label" meta:resourcekey="lblFromDateResource1" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFromDate" CssClass="textbox" runat="server" />
                                    <ajx:CalendarExtender ID="cerFromDate" runat="server" TargetControlID="txtFromDate" PopupPosition="Right" CssClass="calendarStyle" />
                                    <ajx:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                        MaskType="Date" OnInvalidCssClass="MaskedEditError" OnFocusCssClass="MaskedEditFocus"
                                        MessageValidatorTip="true" Mask="99/99/9999"/>
					                <ajx:MaskedEditValidator ID="mevFromDate" runat="server" Display="None" InitialValue=" / / "
						                ControlToValidate="txtFromDate" MinimumValue="01/01/1850" ControlExtender="meeFromDate"
						                IsValidEmpty="False"  Text="*" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvFromDate" ControlToValidate="txtFromDate" ErrorMessage="   *" ForeColor="Red" ValidationGroup="Template" />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblToDate" Text="Fecha hasta" CssClass="label" meta:resourcekey="lblToDateResource1" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToDate" CssClass="textbox" runat="server" />
                                    <ajx:CalendarExtender ID="cerToDate" runat="server" TargetControlID="txtToDate" PopupPosition="Right" CssClass="calendarStyle" />
                                    <ajx:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                        MaskType="Date" OnInvalidCssClass="MaskedEditError" OnFocusCssClass="MaskedEditFocus"
                                        MessageValidatorTip="true" Mask="99/99/9999"/>
					                <ajx:MaskedEditValidator ID="mevToDate" runat="server" Display="None" InitialValue=" / / "
						                ControlToValidate="txtToDate" MinimumValue="01/01/1850" EmptyValueMessage="Fecha inválida."
						                IsValidEmpty="False" ControlExtender="meeToDate" Text="*" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvToDate" ControlToValidate="txtToDate" ErrorMessage="   *" ForeColor="Red" ValidationGroup="Template" />
                                    <asp:CustomValidator runat="server" ID="cvDates" ErrorMessage="   *" ForeColor="Red" 
                                    ValidationGroup="Template" Display="Dynamic" OnServerValidate="CvDatesValidator" 
                                    meta:resourcekey="cvDatesResource1"/>    

                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="module">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="LFromAddress" Text="Jerarquia" CssClass="label" 
                                        meta:resourcekey="lblFromAddressResource1" />            
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFromAddress" runat="server" CssClass="dropdown" 
                                        DataTextField="FullName" DataValueField="Id" DataSourceID="odsFromAddress"/>
                                    <asp:ObjectDataSource ID="odsFromAddress" runat="server" 
                                        SelectMethod="FindAll" TypeName="EMailAdmin.BackEnd.Home.EMailAddressHome"/>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblHierarchy" Text="Jerarquia" CssClass="label" 
                                        meta:resourcekey="lblHierarchyResource1" />            
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHierarchy" runat="server" CssClass="textBoxNumber_XS" 
                                        MaxLength="2"/>            
                                </td>
                                <td>
                                    <asp:RegularExpressionValidator ID="revHierarchy" runat="server" 
                                        ControlToValidate="txtHierarchy" ErrorMessage="   *" 
                                        ForeColor="Red" ValidationGroup="Template" 
                                        ValidationExpression="^\d+$" meta:resourcekey="revHierarchy1"/>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvHierarchy" 
                                        ControlToValidate="txtHierarchy" 
                                        ErrorMessage="   *" ForeColor="Red" ValidationGroup="Template" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="module">
                        <table>
                            <tr>
                                <td>                                    
                                    <%--<asp:CheckBox runat="server" ID="chkMergeAttachs" Checked="false"
                                        Text="Agrupa los archivos adjuntos en un solo PDF" TextAlign="Left" meta:resourcekey="chkMergeAttachs" />--%>
                                    <asp:Label runat="server" ID="lblTypeAttachsWithEkit" Text="Configuración de adjuntos" CssClass="label" 
                                        meta:resourcekey="lblTypeAttachsWithEkitResource1" />     
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTypeAttachsWithEkit" runat="server" CssClass="dropdown" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTemplatePDF" runat="server" CssClass="dropdown" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="module">
                        <tcl:TemplateControl runat="server" ID="tclTemplates" 
                        OnImageUploadButton="ImageUploadButtonPressed" 
                        OnLinkUploadButton="LinkUploadButtonPressed" 
                        OnVariableTextUploadButton="VariableTextUploadButtonPressed" 
                        OnContactUploadButton="ContactUploadButtonPressed"
                        OnSignatureUploadButton="SignatureUploadButtonPressed"
                        OnCountryVisibleTextUploadButton="CountryVisibleTextUploadButtonPressed"
                        OnUpgradeVariableTextUploadButton="UpgradeVariableTextUploadButtonPressed"
                        OnTableUploadButton = "TableUploadButtonPressed"
                        OnConditionVariableTextUploadButton="ConditionVariableTextButtonPressed"
                        OnPixelUploadButton="PixelButtonPressed"
                        OnClausuleUploadButton="ClausuleButtonPressed"/>
                    </div>
                    <div class="module">
                        <asp:Button runat="server" ID="btnAssociations" Text="Associations" CssClass="button cancel" 
                             meta:resourcekey="btnAssociationsResource1" OnClick="BtnAssociationsOnClick" />
                        <asp:Button runat="server" ID="btnAttachment" Text="Attachment" CssClass="button cancel" 
                            OnClick="BtnAttachmentOnClick" meta:resourcekey="btnAttachmentResource1" />
                        <asp:Button runat="server" ID="btnPreview" Text="Preview" CssClass="button ok" ValidationGroup="Template"
                            OnClick="BtnPreviewOnClick" meta:resourcekey="btnPreviewResource1" />
                    </div>
                </fieldset>
                <div class="module">
                    <asp:Button runat="server" ID="btnAccept" Text="Aceptar" CssClass="button ok" 
                        OnClick="BtnAcceptOnClick" meta:resourcekey="btnAcceptResource1" ValidationGroup="Template"/>
                    <asp:Button runat="server" ID="btnDelete" Text="Eliminar" CssClass="button delete" OnClick="BtnDeleteOnClick"
                        meta:resourcekey="btnDeleteResource1" />
                    <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="button cancel" 
                        OnClick="BtnCancelOnClick" meta:resourcekey="btnCancelResource1" />
                </div>
                
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdDelete"/>
                <asp:Panel ID="pnlDelete" runat="server" CssClass="modalBackgroundRestore" 
                    Style="display: none;" meta:resourcekey="pnlDeleteResource1" >
                    <div class="formModule" style="text-align: left;width:250px;">
                        <fieldset>
                            <div class="module" >
                                <asp:Label CssClass="label" ID="lblDelete" runat="server"
                                    Text="Desea eliminar?" meta:resourcekey="lblDeleteResource1" />
                            </div><br />                         
                            <div class="module" style="text-align:right;">
                                <asp:Button ID="btnAcceptDelete" runat="server" CssClass="button ok" 
                                    Text="Aceptar" OnClick="BtnAcceptDeleteOnClick" 
                                    meta:resourcekey="btnAcceptDeleteResource1" />
                                <asp:Button ID="btnCancelDelete" runat="server" CssClass="button cancel" OnClick="BtnCancelDelete"
                                    Text="Cancelar" meta:resourcekey="btnCancelDeleteResource1" />
                            </div>
                        </fieldset>
                    </div>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                 ID="mpeDelete" 
                PopupControlID="pnlDelete" RepositionMode="None" runat ="server" 
                BehaviorID="mpeDelete" CancelControlID="hfdDelete"
                TargetControlID="hfdDelete" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdImage"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlImage">
                    <img:ImageSelector runat="server" ID="imgSelector" OnImageUploadedCompleted="ImageUploadedCompleted" OnImageCanceled="ImageCanceled"/>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeImage" TargetControlID="hfdImage"
                    PopupControlID="pnlImage" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeImage" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
            <asp:Panel runat="server" ID="MyPanel">
                <asp:HiddenField runat="server" ID="hfdLink"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlLink">
                    <lnk:LinkSelector runat="server" ID="lnkSelector" OnLinkUploadedCompleted="LinkUploadedCompleted" OnLinkCanceled="LinkCanceled"/>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeLink" TargetControlID="hfdLink"
                    PopupControlID="pnlLink" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeLink" DynamicServicePath="" Enabled="True" />
            </asp:Panel>
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdVariableText"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlVariableText">
                    <vtx:VariableTextSelector runat="server" ID="vtxVariableText" OnVariableTextUploadedCompleted="VariableTextCompleted" OnVariableTextCanceled="VariableTextCanceled" />
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeVariableText" TargetControlID="hfdVariableText"
                    PopupControlID="pnlVariableText" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeVariableText" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdSignature"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlSignature">
                    <sgr:SignatureSelector ID="sgrSignature" runat="server" OnSignatureUploadedCompleted="SignatureCompleted" OnSignatureCanceled="SignatureCanceled"/>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeSignature" TargetControlID="hfdSignature"
                    PopupControlID="pnlSignature" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeSignature" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdCountryVisibleText"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlCountryVisibleText">
                    <cvt:CouuntryVisibleTextSelector ID="cvtCountryVisibleText" runat="server" OnCountryVisibleTextUploadedCompleted="CountryVisibleTextCompleted" OnCountryVisibleTextCanceled="CountryVisibleTextCanceled"/>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeCountryVisibleText" TargetControlID="hfdCountryVisibleText"
                    PopupControlID="pnlCountryVisibleText" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeCountryVisibleText" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdUpgradeVariableText"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlUpgradeVariableText">
                    <uvt:UpgradeVariableTextSelector ID="uvtUpgradeVariableText" runat="server" OnUpgradeVariableTextUploadedCompleted="UpgradeVariableTextCompleted" OnUpgradeVariableTextCanceled="UpgradeVariableTextCanceled"/>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeUpgradeVariableText" TargetControlID="hfdUpgradeVariableText"
                    PopupControlID="pnlUpgradeVariableText" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeUpgradeVariableText" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdContact"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlContact">
                    <ctc:ContactSelector ID="ctcContact" runat="server" OnContactUploadedCompleted="ContactCompleted" OnContactCanceled="ContactCanceled"/>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeContact" TargetControlID="hfdContact"
                    PopupControlID="pnlContact" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeContact" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdAttachment"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlAttachment">
                    <att:AttachmentSelector ID="attAttachment" runat="server" OnAttachmentClosed="AttachmentClose"
                    OnAttachmentSearched="AttachmentSearched" OnAttachmentPageIndexChanged="AttachmentPageIndexChanged"
                    OnAttachmentContentEdited="AttachmentEditedContent"
                    OnAttachmentContentClosed="ContentAttachmentClose"
                    OnAttachmentGroupEdited="AttachmentEditedGroup"
                    OnAttachmentGroupClosed="AttachmentClosedGroup"
                    OnAttachmentTemplatesEdited="AttachmentTemplatesEdited"/>    
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeAttachment" TargetControlID="hfdAttachment"
                    PopupControlID="pnlAttachment" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeAttachment" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdPreview"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none; width:950px;" ID="pnlPreview">
                    <prv:Preview ID="prvPreview" runat="server" OnPreviewClosed="PrvPreviewOnClosed" OnPreviewShowed="PrvPreviewOnShow" />
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpePreview" TargetControlID="hfdPreview"
                    PopupControlID="pnlPreview" RepositionMode="None" runat ="server" 
                    BehaviorID="mpePreview" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdGroup"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlGroup">
                    <div class="formModule">
                        <fieldset>
                            <div class="module">
                                <grp:GroupViewer ID="grpGroup" runat="server" OnButtonAddGroupPressed="AddGroupAssociations" OnButtonDelGroupPressed = "DelGroupAssociations" />
                                <asp:Button runat="server" ID="btnGroupViewer" CssClass="button ok" Text="Accept" OnClick="BtnAcceptGroupViewerClick" meta:resourcekey="btnAcceptDeleteResource1"/>
                                <asp:Button runat="server" ID="btnCancelGroupViewer" CssClass="button cancel" Text="Cancel" OnClick="BtnCancelGroupViewerClick" meta:resourcekey="btnCancelDeleteResource1"/>
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
                <asp:Panel runat="server" ID="pnlGroupControl" CssClass="modalBackgroundRestore" Style="display: none;" >
                    <div class="formModule">
                        <fieldset>
                            <div class="module">
                                <grc:GroupControl runat="server" ID="grcGroup" OnFiltersClosed="GrcGroupFiltersClosed" OnFiltersAdded="GrcGroupFiltersAdded" OnDeletePressed="GrcGroupDeletePressed" OnAccountSearchButtonPressed="GrcGroupAccountSearchPressed" OnProductSearchButtonPressed="GrcGroupProductSearchPressed" OnRateSearchButtonPressed="GrcGroupRateSearchPressed" OnFiltersCleared="ClearFilters"/>
                                <asp:Button runat="server" ID="btnAcceptGroup" CssClass="button ok" Text="Accept" OnClick="BtnAcceptGroupClick" ValidationGroup="Group" meta:resourcekey="btnAcceptDeleteResource1"/>
                                <asp:Button runat="server" ID="btnCancelGroup" CssClass="button cancel" Text="Cancel" OnClick="BtnCancelGroupClick" meta:resourcekey="btnCancelDeleteResource1"/>
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
                <asp:HiddenField runat="server" ID="hfdAssociations"/>
                <asp:Panel runat="server" ID="pnlAssociations" CssClass="modalBackgroundRestore" Style="display: none;">
                    <div class="formModule">
                        <fieldset>
                            <div class="module">
                                <asc:AssociationControl runat="server" ID="ascAssociation" OnDeletePressed="AscAssociationDeletePressed" OnAddPressed="AscAssociationAddPressed" />
                                <asp:Button runat="server" ID="btnCloseAssociation" CssClass="button cancel" Text="Close" meta:resourcekey="btnCloseResource1"/>
                            </div>
                        </fieldset>
                    </div>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                 ID="mpeAssociation" CancelControlID="btnCloseAssociation"
                PopupControlID="pnlAssociations" RepositionMode="None" runat ="server" 
                TargetControlID="hfdAssociations" DynamicServicePath="" Enabled="True" />
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
                <asp:HiddenField runat="server" ID="hfdTable"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlTable">
                    <tab:TableVariableSelector ID="TableVariableSelector1" runat="server" OnTableVariableTextUploadedCompleted="TableVariableTextCompleted" OnTableVariableTextCanceled="TableVariableTextCanceled"></tab:TableVariableSelector>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeTable" TargetControlID="hfdTable"
                    PopupControlID="pnlTable" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeTable" DynamicServicePath="" Enabled="True" />
            </div>
             <div>
                <asp:HiddenField runat="server" ID="hfdConditionVariableText"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlConditionVariableText">
                    <cdvt:ConditionVariableTextSelector ID="ConditionVariableTextSelector1" runat="server"  OnConditionVariableTextUploadedCompleted="ConditionVariableTextCompleted" OnConditionVariableTextCanceled="ConditionVariableTextCanceled"></cdvt:ConditionVariableTextSelector>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeConditionVariableText" TargetControlID="hfdConditionVariableText"
                    PopupControlID="pnlConditionVariableText" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeConditionVariableText" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hdfContentAttachments"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlContentAttachments">
                    <uc1:ContentAttachment ID="ContentAttachment1" runat="server"
                    OnContentAttachmentClosed="ContentAttachmentClosed"
                    OnContentAttachmentOpened = "ContentAttachmentOpened"  />
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground"
                     ID="mpeContentAttachments" TargetControlID="hdfContentAttachments"
                    PopupControlID="pnlContentAttachments" RepositionMode="None" runat ="server"
                    BehaviorID="mpeContentAttachments" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hdfPixel"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlPixel">
                    <pxl:PixelSelector ID="PixelSelector1" runat="server"  OnPixelUploadedCompleted="PixelCompleted" OnPixelCanceled="PixelCanceled" />
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpePixelSelector" TargetControlID="hdfPixel"
                    PopupControlID="pnlPixel" RepositionMode="None" runat ="server" 
                    BehaviorID="mpePixelSelector" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hdfClausule"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlClausule">
                    <cla:ClausuleSelector ID="ClausuleSelector1" runat="server"  OnClausuleSelectorUploadedCompleted="ClausuleCompleted" OnClausuleSelectorCanceled="ClausuleCanceled" />
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeClausuleSelector" TargetControlID="hdfClausule"
                    PopupControlID="pnlClausule" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeClausuleSelector" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hdfAttachmentGroup"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlAttachmentGroup">
                    <cag:AttachmentGroup ID="AttachmentGroup1" runat="server" 
                        OnAttachmentGroupOpened="AttachmentGroupOpened" 
                        OnAttachmentGroupClosed="AttachmentGroupClosed"/>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground"
                     ID="mpeAttachmentGroup" TargetControlID="hdfAttachmentGroup"
                    PopupControlID="pnlAttachmentGroup" RepositionMode="None" runat ="server"
                    BehaviorID="mpeAttachmentGroup" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hdfAttachmentTemplatesSelector"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlAttachmentTemplatesSelector">
                    <ats:AttachmentTemplatesSelector ID="AttachmentTemplatesSelector1" runat="server" 
                        OnAttachmentTemplatesOpened="AttachmentTemplatesOpened" 
                        OnAttachmentTemplatesClosed="AttachmentTemplatesClosed"/>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground"
                     ID="mpeAttachmentTemplatesSelector" TargetControlID="hdfAttachmentTemplatesSelector"
                    PopupControlID="pnlAttachmentTemplatesSelector" RepositionMode="None" runat ="server"
                    BehaviorID="mpeAttachmentTemplatesSelector" DynamicServicePath="" Enabled="True" />
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        </div>        
    </form>
</body>
</html>
