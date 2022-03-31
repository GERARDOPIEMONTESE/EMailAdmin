<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpgradeVariableTextInformation.aspx.cs" Inherits="EMailAdmin.UpgradeVariableText.UpgradeVariableTextInformation" meta:resourcekey="PageResource1" culture="auto" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="snt" TagName="UpgradeVariableTextTypeDropDown" Src="~/Controls/UpgradeVariableTextType/UpgradeVariableTextTypeDropDown.ascx" %>
<%@ Register TagPrefix="trt" TagName="TabRichTextControl" Src="~/Controls/TabRich/TabRichTextControl.ascx" %>
<%@ Register TagPrefix="uvt" TagName="UpgradeVariableTextDropDown" Src="~/Controls/UpgradeVariableText/UpgradeVariableTextDropDown.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upgrade Variable Text Information</title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/loading.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function OvrdSubmit() {
            var ftbSubmit = document.forms[0].onsubmit;
            if (typeof (ftbSubmit) == 'function') {
                document.forms[0].onsubmit = function () {
                    try { ftbSubmit(); }
                    catch (ex) { }
                }
            }

            // We are ok
            return true;
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
                                        ErrorMessage="   *" ValidationGroup="UpgradeVariableText" 
                                        meta:resourcekey="rfvNameResource1" />
                                </td>
                                <td>
                                    <asp:CustomValidator runat="server" ID="ctmName" ErrorMessage="   *" 
                                    ValidationGroup="UpgradeVariableText" Display="Dynamic" OnServerValidate="CtmNameValidator" 
                                    meta:resourcekey="ctmNameResource1"/>    
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="module">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblCountry" Text="Nombre" 
                                    CssClass="label" meta:resourcekey="lblCountryResource1" />
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlCountry" 
                                    DataTextField="Nombre" DataValueField="Codigo" 
                                    OnSelectedIndexChanged="DdlCountrySelectedIndexChanged" 
                                    AutoPostBack="True"/>
                                </td>
                            </tr>
                        </table>    
                    </div>
                    <div class="module">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblUpgrades" Text="Pais" CssClass="label" meta:resourcekey="lblUpgradeResource1" />            
                                </td>
                                <td>
                                    <uvt:UpgradeVariableTextDropDown runat="server" ID="uvtUpgrades" OnGridLoadCompleted="UvtUpgradesLoadCompleted" />            
                                </td>
                                <td>
                                    <asp:CustomValidator runat="server" ID="ctmProductValidator" 
                                        ErrorMessage="   *" ValidationGroup="UpgradeVariableText" Display="Dynamic" 
                                        OnServerValidate="CtmProductValidatorComplete" 
                                        meta:resourcekey="ctmProductValidatorResource1" />
                                </td>
                            </tr>
                        </table>                     
                    </div>
                    <div class="module">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblUpgradeVariableTextType" Text="Tipo" CssClass="label" meta:resourcekey="lblUpgradeVariableTextTypeResource1" />
                                </td>
                                <td>
                                    <snt:UpgradeVariableTextTypeDropDown runat="server" ID="sntUpgradeVariableTextType" OnGridUpgradeVariableTextLoadCompleted="SntUpgradeVariableTextTypeLoadCompleted"/>
                                </td>
                                <td>
                                    <asp:CustomValidator runat="server" ID="ctmUpgradeVariableTextTypeValidator" 
                                        ErrorMessage="   *" ValidationGroup="UpgradeVariableText" Display="Dynamic" 
                                        OnServerValidate="CtmUpgradeVariableTextTypeValidatorComplete" 
                                        meta:resourcekey="ctmUpgradeVariableTextTypeValidatorResource1" />            
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
                                    Display="Dynamic" OnServerValidate="CtmTabsValidator" ValidationGroup="UpgradeVariableText" 
                                    meta:resourcekey="ctmTabsValidatorResource1"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
                <div class="module">
                    <asp:Button runat="server" ID="btnAccept" Text="Aceptar" CssClass="button ok"  OnClick="BtnAcceptOnClick" ValidationGroup="UpgradeVariableText" meta:resourcekey="btnAcceptResource1" />
                    <asp:Button runat="server" ID="btnDelete" Text="Eliminar" CssClass="button delete" meta:resourcekey="btnDeleteResource1" />
                    <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="button cancel" OnClick="BtnCancelOnClick" meta:resourcekey="btnCancelResource1" />
                </div>
            </div>
            <div>
                <asp:Panel ID="pnlDelete" runat="server" CssClass="modalBackgroundRestore" 
                    Style="display: none;" meta:resourcekey="pnlDeleteResource1" >
                    <div class="formModule" style="text-align: left;width:250px;">
                        <fieldset>
                            <div class="module" >
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
                TargetControlID="btnDelete" DynamicServicePath="" Enabled="True" />
            </div>
        </div>
    </form>
</body>
</html>
