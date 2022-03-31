<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConditionVariableTextInformation.aspx.cs" Inherits="EMailAdmin.ConditionVariableText.ConditionVariableTextInformation" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="snt" TagName="UpgradeVariableTextTypeDropDown" Src="~/Controls/UpgradeVariableTextType/UpgradeVariableTextTypeDropDown.ascx" %>
<%@ Register TagPrefix="trt" TagName="TabRichTextControl" Src="~/Controls/TabRich/TabRichTextControl.ascx" %>
<%@ Register TagPrefix="uvt" TagName="UpgradeVariableTextDropDown" Src="~/Controls/UpgradeVariableText/UpgradeVariableTextDropDown.ascx" %>
<%@ Register src="../Controls/VariableText/VariableTextDropDown.ascx" tagname="VariableTextDropDown" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                                <td style="margin-left: 40px">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="longTextbox" 
                                        meta:resourcekey="txtNameResource1" MaxLength="30" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" 
                                        ErrorMessage="   *" ValidationGroup="UpgradeVariableText" 
                                        meta:resourcekey="rfvNameResource1" />
                                </td>
                                <td>
                                    <asp:CustomValidator runat="server" ID="ctmName" ErrorMessage="   *" 
                                    ValidationGroup="UpgradeVariableText" Display="Dynamic" 
                                    OnServerValidate="CtmNameValidator" meta:resourcekey="ctmNameResource1" 
                                        ControlToValidate="txtName" />    
                                </td>
                            </tr>
                        </table>
                    </div>                    
                    <div class="module">                    
                        <table>
                            <tr>
                                <td style="margin-left: 80px">
                                    <asp:Label runat="server" ID="lblVariableText" Text="VariableText" 
                                        CssClass="label" meta:resourcekey="lblVariableTextResource1" />            
                                </td>
                                <td>
                                    <uc1:VariableTextDropDown ID="ddlVariableText" runat="server"  OnGridLoadCompleted="ddlVariableTextGridLoadCompleted" OnGridVariableTextCancelCompleted="ddlVariableTextGridCancelCompleted"></uc1:VariableTextDropDown>                                                                    
                                </td>
                                <td>
                                    <asp:CustomValidator runat="server" ID="ctmVariableTextValidator" 
                                        ErrorMessage="   *" ValidationGroup="vgVariableText" Display="Dynamic" 
                                        OnServerValidate="ctmVariableTextValidatorComplete" 
                                        meta:resourcekey="ctmVariableTextValidatorResource1"/>                          
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
                            <div class="module" style="display:inline-block; text-align:right; width:100%">
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
