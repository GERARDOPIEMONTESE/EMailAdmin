<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignatureInformation.aspx.cs" Inherits="EMailAdmin.Signature.SignatureInformation" meta:resourcekey="PageResource1" culture="auto" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="ctr" TagName="CountryDropDown" Src="~/Controls/Country/CountryDropDown.ascx" %>
<%@ Register TagPrefix="snt" TagName="SignatureTypeDropDown" Src="~/Controls/SignatureType/SignatureTypeDropDown.ascx" %>
<%@ Register TagPrefix="trt" TagName="TabRichTextControl" Src="~/Controls/TabRich/TabRichTextControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signature Information</title>
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
                                    <asp:TextBox ID="txtName" runat="server" CssClass="longTextbox" MaxLength="29" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" 
                                        ErrorMessage="   *" ForeColor="Red" ValidationGroup="Signature" />
                                </td>
                                <td>
                                    <asp:CustomValidator runat="server" ID="ctmName" ErrorMessage="   *" ForeColor="Red" 
                                    ValidationGroup="Signature" Display="Dynamic" OnServerValidate="CtmNameValidator" 
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
                                    <asp:CustomValidator runat="server" ID="ctmCountryValidator" ErrorMessage="   *" ForeColor="Red" ValidationGroup="Signature" Display="Dynamic" OnServerValidate="CtmCountryValidatorComplete" />
                                </td>
                            </tr>
                        </table>                     
                    </div>
                    <div class="module">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblSignatureType" Text="Tipo" CssClass="label" meta:resourcekey="lblSignatureTypeResource1" />
                                </td>
                                <td>
                                    <snt:SignatureTypeDropDown runat="server" ID="sntSignatureType" OnGridSignatureLoadCompleted="SntSignatureTypeLoadCompleted"/>
                                </td>
                                <td>
                                    <asp:CustomValidator runat="server" ID="ctmSignatureTypeValidator" ErrorMessage="   *" ForeColor="Red" ValidationGroup="Signature" Display="Dynamic" OnServerValidate="CtmSignatureTypeValidatorComplete" />            
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
                                    <asp:CustomValidator runat="server" ID="ctmTabsValidator" ErrorMessage="   *" ForeColor="Red" 
                                    Display="Dynamic" OnServerValidate="CtmTabsValidator" ValidationGroup="Signature" 
                                    meta:resourcekey="ctmTabsValidatorResource1"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
                <div class="module">
                    <asp:Button runat="server" ID="btnAccept" Text="Aceptar" CssClass="button ok"  OnClick="BtnAcceptOnClick" ValidationGroup="Signature" meta:resourcekey="btnAcceptResource1" />
                    <asp:Button runat="server" ID="btnDelete" Text="Eliminar" CssClass="button delete" meta:resourcekey="btnDeleteResource1" />
                    <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="button cancel" OnClick="BtnCancelOnClick" meta:resourcekey="btnCancelResource1" />
                </div>
            </div>
            <div>
                <asp:Panel ID="pnlDelete" runat="server" CssClass="modalBackgroundRestore" Style="display: none;" >
                    <div class="formModule" style="text-align: left;width:250px;">
                        <fieldset>
                            <div class="module" >
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
