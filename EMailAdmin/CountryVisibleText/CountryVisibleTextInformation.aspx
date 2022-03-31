<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CountryVisibleTextInformation.aspx.cs" Inherits="EMailAdmin.CountryVisibleText.CountryVisibleTextInformation" meta:resourcekey="PageResource1" culture="auto" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="ctr" TagName="CountryDropDown" Src="~/Controls/Country/CountryDropDown.ascx" %>
<%@ Register TagPrefix="snt" TagName="CountryVisibleTextTypeDropDown" Src="~/Controls/CountryVisibleTextType/CountryVisibleTextTypeDropDown.ascx" %>
<%@ Register TagPrefix="trt" TagName="TabRichTextControl" Src="~/Controls/TabRich/TabRichTextControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CountryVisibleText Information</title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/loading.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmMain" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
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
                                    <asp:Label runat="server" ID="lblName" Text="Nombre" CssClass="label" meta:resourcekey="lblNameResource1" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="longTextbox" MaxLength="29" 
                                        meta:resourcekey="txtNameResource1" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" 
                                        ErrorMessage="   *" ValidationGroup="CountryVisibleText" 
                                        meta:resourcekey="rfvNameResource1" />
                                </td>
                                <td>
                                    <asp:CustomValidator runat="server" ID="ctmName" ErrorMessage="   *" 
                                    ValidationGroup="CountryVisibleText" Display="Dynamic" OnServerValidate="CtmNameValidator" 
                                    meta:resourcekey="ctmNameResource1"/>    
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="module">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblCountry" Text="Pais" CssClass="label" meta:resourcekey="lblCountryResource1" />            
                                </td>
                                <td>
                                    <ctr:CountryDropDown runat="server" ID="ctrCountry" OnGridLoadCompleted="CsrCountryLoadCompleted" />            
                                </td>
                                <td>
                                    <asp:CustomValidator runat="server" ID="ctmCountryValidator" 
                                        ErrorMessage="   *" ValidationGroup="CountryVisibleText" Display="Dynamic" 
                                        OnServerValidate="CtmCountryValidatorComplete" 
                                        meta:resourcekey="ctmCountryValidatorResource1" />
                                </td>
                            </tr>
                        </table>                     
                    </div>
                    <div class="module">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblCountryVisibleTextType" Text="Tipo" CssClass="label" meta:resourcekey="lblCountryVisibleTextTypeResource1" />
                                </td>
                                <td>
                                    <snt:CountryVisibleTextTypeDropDown runat="server" ID="sntCountryVisibleTextType" OnGridCountryVisibleTextLoadCompleted="SntCountryVisibleTextTypeLoadCompleted"/>
                                </td>
                                <td>
                                    <asp:CustomValidator runat="server" ID="ctmCountryVisibleTextTypeValidator" 
                                        ErrorMessage="   *" ValidationGroup="CountryVisibleText" Display="Dynamic" 
                                        OnServerValidate="CtmCountryVisibleTextTypeValidatorComplete" 
                                        meta:resourcekey="ctmCountryVisibleTextTypeValidatorResource1" />            
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="module">
                        <table>
                            <tr>
                                <td>
                                    <trt:TabRichTextControl runat="server" ID="trtDescription" />            
                                </td>
                                <td>
                                    <asp:CustomValidator runat="server" ID="ctmTabsValidator" ErrorMessage="   *" 
                                    Display="Dynamic" OnServerValidate="CtmTabsValidator" ValidationGroup="CountryVisibleText" 
                                    meta:resourcekey="ctmTabsValidatorResource1"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
                <div class="module">
                    <asp:Button runat="server" ID="btnAccept" Text="Aceptar" CssClass="button ok" OnClick="BtnAcceptOnClick" ValidationGroup="CountryVisibleText" meta:resourcekey="btnAcceptResource1" />
                    <asp:Button runat="server" ID="btnDelete" Text="Eliminar" CssClass="button delete" meta:resourcekey="btnDeleteResource1" />
                    <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="button cancel" OnClick="BtnCancelOnClick" meta:resourcekey="btnCancelResource1" />
                </div>
            </div>
            <div>
                <asp:Panel ID="pnlDelete" runat="server" CssClass="modalBackgroundRestore" 
                    Style="display: none;" meta:resourcekey="pnlDeleteResource1" >
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
                TargetControlID="btnDelete" DynamicServicePath="" Enabled="True" />
            </div>
        </div>
    </form>
</body>
</html>
