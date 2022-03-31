<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachmentInformation.aspx.cs" Inherits="EMailAdmin.Attachment.AttachmentInformation" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="grp" TagName="GroupViewer" Src="~/Controls/Group/GroupViewer.ascx" %>
<%@ Register TagPrefix="grc" TagName="GroupControl" Src="~/Controls/Group/GroupControl.ascx" %>
<%@ Register TagPrefix="asc" TagName="AssociationControl" Src="~/Controls/Associations/AssociationsControl.ascx" %>
<%@ Register TagPrefix="acc" TagName="AccountSelector" Src="~/Controls/Account/AccountSelector.ascx" %>
<%@ Register TagPrefix="ctr" TagName="CountrySelector" Src="~/Controls/Country/CountrySelector.ascx" %>
<%@ Register TagPrefix="rte" TagName="RateSelector" Src="~/Controls/Rate/RateSelector.ascx" %>
<%@ Register TagPrefix="pro" TagName="ProductSelector" Src="~/Controls/Product/ProductSelector.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Attachment Information</title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/loading.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
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
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" />
        <div class="PaginaPortal">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" > 
                <ProgressTemplate> 
                    <div id="progressBackgroundFilter"></div> 
                    <div id="processMessage" align="center"><p>Loading...</p>
                        <img alt="Loading..." src="../IMG/loading.gif"  /> 
                    </div> 
                </ProgressTemplate> 
            </asp:UpdateProgress>
            <div class="formModule">
                <fieldset>
                    <div class="module">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblName" Text="Nombre" CssClass="label" Width="100px"
                                    meta:resourcekey="lblNameResource1" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="textbox" MaxLength="49"
                                    meta:resourcekey="txtNameResource1" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" 
                                    ErrorMessage="   *" ForeColor="Red" ValidationGroup="Attachment" />            
                                </td>
                                <td>
                                    <asp:CustomValidator runat="server" ID="ctmName" ErrorMessage="   *" ForeColor="Red" 
                                    ValidationGroup="Attachment" Display="Dynamic" OnServerValidate="CtmNameValidator" 
                                    meta:resourcekey="ctmNameResource1"/>    
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="module">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblType" Text="Tipo" CssClass="label" Width="100px"
                                        meta:resourcekey="lblTypeResource1" />
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlType" DataTextField="Description" OnSelectedIndexChanged="DdlTypeSelectedIndexChanged"
                                        DataValueField="Id" CssClass="dropdown" AutoPostBack="True" meta:resourcekey="ddlTypeResource1" />
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnItems" Text="Items" CssClass="button ok"  
                                        meta:resourcekey="btnItemsResource1" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="module">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblEstrategy" Text="Estrategia" CssClass="label" Width="100px"
                                        meta:resourcekey="lblEstrategyResource1" />
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlEstrategy" DataTextField="Description" 
                                        DataValueField="Id" CssClass="dropdown" 
                                        meta:resourcekey="ddlEstrategyResource1" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="module">
                        <asp:Button runat="server" ID="btnAssociation" CssClass="button ok"  OnClick="BtnAssociationsOnClick" meta:resourcekey="BtnAssociationsResource1"/>
                    </div>
                </fieldset>
                <div class="module">
                    <asp:Button runat="server" ID="btnAccept" Text="Aceptar" CssClass="button ok"  
                        OnClick="BtnAcceptOnClick" meta:resourcekey="btnAcceptResource1" ValidationGroup="Attachment" />
                    <asp:Button runat="server" ID="btnDelete" Text="Eliminar" CssClass="button delete" OnClick="BtnDeleteOnClick"
                        meta:resourcekey="btnDeleteResource1" />
                    <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="button cancel" 
                        OnClick="BtnCancelOnClick" meta:resourcekey="btnCancelResource1" />
                </div>
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdGroup"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlGroup">
                    <div class="formModule">
                        <fieldset>
                            <div class="module">
                                <grp:GroupViewer ID="grpGroup" runat="server" OnButtonAddAttachmentPressed="GrpGroupAddPressed" OnButtonDelAttachmentPressed="GrpGroupDelPressed" />
                                <asp:Button runat="server" ID="btnGroupViewer" CssClass="button ok"  Text="Accept" OnClick="BtnAcceptGroupViewerClick" meta:resourcekey="btnAcceptResource1"/>
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
                <asp:Panel runat="server" ID="pnlGroupControl" CssClass="modalBackgroundRestore" Style="display: none;">
                    <div class="formModule">
                        <fieldset>
                            <div class="module">
                                <grc:GroupControl runat="server" ID="grcGroup" OnFiltersClosed="GrcGroupFiltersClosed" OnFiltersAdded="GrcGroupFiltersAdded" OnDeletePressed="GrcGroupDeletePressed" OnAccountSearchButtonPressed="GrcGroupAccountSearchPressed" OnProductSearchButtonPressed="GrcGroupProductSearchPressed" OnRateSearchButtonPressed="GrcGroupRateSearchPressed" OnFiltersCleared="ClearFilters"/>
                                <asp:Button runat="server" ID="btnAcceptGroup" CssClass="button ok"  Text="Accept" OnClick="BtnAcceptGroupClick" ValidationGroup="Group" meta:resourcekey="btnAcceptResource1"/>
                                <asp:Button runat="server" ID="btnCancelGroup" CssClass="button cancel" Text="Cancel" OnClick="BtnCancelGroupClick" meta:resourcekey="btnCancelResource1"/>
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
                <asp:HiddenField runat="server" ID="hfdDelete"/>
                <asp:Panel ID="pnlDelete" runat="server" CssClass="modalBackgroundRestore" Style="display: none;" >
                    <div class="formModule" style="text-align: left;width:250px;">
                        <fieldset>
                            <div class="module">
                                <asp:Label CssClass="label" ID="lblDelete" runat="server" Text="Desea eliminar?" meta:resourcekey="lblDeleteResource1" />
                            </div><br />
                            <div class="module" style="text-align:right;">
                                <asp:Button ID="btnAcceptDelete" runat="server" CssClass="button ok"  Text="Aceptar" OnClick="BtnAcceptDeleteOnClick" meta:resourcekey="btnAcceptResource1" />
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
            <div>
                <asp:Panel ID="pnlItems" runat="server" CssClass="modalBackgroundRestore" Style="display: none;" >
                    <div class="formModule">
                        <fieldset style="width: 380px">
                            <div class="module">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label CssClass="label" ID="lblFile" runat="server" meta:resourcekey="lblFileResource1" />
                                        </td>
                                        <td>
                                            <asp:FileUpload runat="server" ID="fupFile" CssClass="button"  />
                                        </td>
                                        <td>
                                            <asp:CustomValidator runat="server" ID="ctmFile" ErrorMessage="   *" ForeColor="Red" 
                                            ValidationGroup="Item" Display="Dynamic" OnServerValidate="CtmFileValidator" />
                                        </td>
                                    </tr>
                                </table>

                            </div>
                            <div class="module">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label CssClass="label" ID="lblDescription" runat="server" Text="Descripcion" meta:resourcekey="lblDescriptionResource1" />            
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" CssClass="bigTextbox" ID="txtDescription" TextMode="MultiLine"/>            
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvDescription" ControlToValidate="txtDescription" 
                                                ErrorMessage="   *" ForeColor="Red" ValidationGroup="Item" />             
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="module">
                                <asp:Label runat="server" ID="lblLanguage" Text="Language" CssClass="label" meta:resourcekey="lblLanguageResource1" />            
                                <asp:DropDownList runat="server" ID="ddlLanguage" DataValueField="Id" CssClass="dropdown" />    
                            </div>
                            <div class="module">
                                <asp:Button CssClass="button ok"  ID="btnAdd" runat="server" Text="Agregar" meta:resourcekey="btnAddResource1" OnClick="BtnAddOnClick" ValidationGroup="Item"/>
                            </div>
                            <div>
                                <hr style="width: 300px;" />
                            </div>
                            <div class="module">
                                <asp:UpdatePanel runat="server" ID="uplItems">
                                    <ContentTemplate>
                                        <fieldset style="width: 350px">
                                            <asp:GridView 
                                            ID="grvItems" 
                                            runat="server" 
                                            GridLines="Vertical" 
                                            AutoGenerateColumns="False"
                                            CssClass="tbl-generic m-center"
                                            DataKeyNames="Id"
                                            PagerStyle-HorizontalAlign="Center">
                                                <Columns>
                                                     <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <a href='./FixedAttachmentHandler.ashx?IdAttachment=<%# Eval("IdAttachment") %>&IdLanguage=<%# Eval("Language.Id") %>' target="_blank">
                                                                <asp:Label ID="IDescargar" runat="server" Text="Download" CssClass="label" meta:resourcekey="lnkDescarga"/></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NameToShowOnGrid" HeaderText="File" ItemStyle-Width="150" meta:resourcekey="BoundFieldResource1"/>
                                                    <asp:BoundField DataField="DescriptionToShowOnGrid" HeaderText="Description" ItemStyle-Width="250" meta:resourcekey="BoundFieldResource2"/>
                                                    <asp:BoundField DataField="LanguageDescriptionSpanish" HeaderText="Description" ItemStyle-Width="250" meta:resourcekey="BoundFieldResource3"/>
                                                    <asp:BoundField DataField="LanguageDescriptionEnglish" HeaderText="Description" ItemStyle-Width="250" meta:resourcekey="BoundFieldResource3"/>
                                                    <asp:BoundField DataField="LanguageDescriptionPortuguese" HeaderText="Description" ItemStyle-Width="250" meta:resourcekey="BoundFieldResource3"/>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibnDelete" runat="server" ImageUrl="~/IMG/b_drop.png" 
                                                                ToolTip="Delete" OnClick="IbnDelete_Onclick" meta:resourcekey="ibnDeleteResource1"/>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="False" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle CssClass="gridView_RowStyle" />
                                                <PagerStyle CssClass="gridView_Pager_Style_Row" />
                                                <SelectedRowStyle CssClass="gridView_Selected_Row" />
                                                <HeaderStyle CssClass="gridView_Row_Header" />
                                                <EditRowStyle CssClass="gridView_Edit_Row" />
                                                <AlternatingRowStyle CssClass="gridView_Alternative_Row_Data" />
                                            </asp:GridView>
                                        </fieldset>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        <div class="module">
                            <asp:Button ID="btnCloseItems" runat="server" CssClass="button cancel" Text="Close" meta:resourcekey="btnCloseResource1" />
                        </div>
                        </fieldset>
                    </div>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                 ID="mpeItems" 
                PopupControlID="pnlItems" RepositionMode="None" runat ="server" 
                BehaviorID="ModalBehavior2" CancelControlID="btnCloseItems"
                TargetControlID="btnItems" DynamicServicePath="" Enabled="True" />
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
        </div>
        <div>
            <asp:HiddenField runat="server" ID="hdfTemplates"/>
            <asp:Panel runat="server" ID="pnlTemplates" CssClass="modalBackgroundRestore" Style="display: none;">
                <div class="formModule">
                    <fieldset>
                        <div class="module">
                            <asc:AssociationControl runat="server" ID="AssociationControl1" OnDeletePressed="AscAssociationDeletePressed" OnAddPressed="AscAssociationAddPressed" />
                            <asp:Button runat="server" ID="btnCloseTemplates" CssClass="button cancel" Text="Close" meta:resourcekey="btnCloseResource1"/>
                        </div>
                    </fieldset>
                </div>
            </asp:Panel>
            <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                ID="ModalPopupExtender1" CancelControlID="btnCloseAssociation"
            PopupControlID="pnlAssociations" RepositionMode="None" runat ="server" 
            TargetControlID="hfdAssociations" DynamicServicePath="" Enabled="True" />
        </div>
    </form>
</body>
</html>
