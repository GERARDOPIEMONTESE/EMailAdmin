<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EMailContactInformation.aspx.cs" Inherits="EMailAdmin.EMailContact.EMailContactInformation" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register TagPrefix="ajx" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  %>
<%@ Register TagPrefix="ctr" TagName="CountryDropDown" Src="~/Controls/Country/CountryDropDown.ascx" %>
<%@ Register TagPrefix="trt" TagName="TabRichTextControl" Src="~/Controls/TabRich/TabRichTextControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMail Contact Information</title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/loading.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
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
                                <asp:Label runat="server" ID="lblName" Text="Nombre" CssClass="label" 
                                    meta:resourcekey="lblNameResource1"/>
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" CssClass="longTextbox" MaxLength="49"
                                    meta:resourcekey="txtNameResource1" />
                            </td>
                            <td>
                                 <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" 
                                    ErrorMessage="   *" ForeColor="Red" ValidationGroup="Contact" />
                            </td>
                            <td>
                                <asp:CustomValidator runat="server" ID="ctmName" ErrorMessage="   *" ForeColor="Red" 
                                ValidationGroup="Contact" Display="Dynamic" OnServerValidate="CtmNameValidator" 
                                meta:resourcekey="ctmNameResource1"/>    
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div class="module">
                    <table>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblCountry" Text="Pais" 
                                    CssClass="label" meta:resourcekey="lblCountryResource1"/>            
                            </td>
                            <td>
                                <ctr:CountryDropDown runat="server" ID="ctrCountry" OnGridLoadCompleted="CsrCountryLoadCompleted" />
                            </td>
                            <td>
                                <asp:CustomValidator runat="server" ID="ctmCountryValidator" ErrorMessage="   *" ForeColor="Red" 
                                ValidationGroup="Contact" Display="Dynamic" OnServerValidate="CtmCountryValidatorComplete" />            
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div class="module">
                    <table>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblEMailContactType" Text="Tipo" CssClass="label" 
                                    meta:resourcekey="lblEMailContactTypeResource1"/>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlEMailContactType" DataSourceID="odsEMailContactType"
                                    DataTextField="Description" DataValueField="Id" 
                                    CssClass="longDropdown" meta:resourcekey="ddlEMailContactTypeResource1"/>
                                <asp:ObjectDataSource ID="odsEMailContactType" runat="server" 
                                    SelectMethod="FindAll" TypeName="EMailAdmin.BackEnd.Home.EMailContactTypeHome"/>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div class="module">
                    <table>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblEMail" Text="EMail" CssClass="label" 
                                    meta:resourcekey="lblEMailResource1"/>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEMail" runat="server" CssClass="longTextbox" MaxLength="149"
                                    meta:resourcekey="txtEMailResource1" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator runat="server" ID="rfvEmail" ControlToValidate="txtEMail" 
                                    ErrorMessage="   *" ForeColor="Red" ValidationGroup="Contact" />
                                <asp:RegularExpressionValidator ID="revEmail" ValidationGroup="Contact" ForeColor="Red"
                                    runat="server" ErrorMessage="   *" ControlToValidate="txtEMail"
                                    ValidationExpression="^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                                    meta:resourcekey="revEmailResource1"/>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div class="module">
                    <table>
                        <tr>
                            <td>
                                <trt:TabRichTextControl runat="server" ID="trtcDescription" />            
                            </td>
                            <td>
                                <asp:CustomValidator runat="server" ID="ctmTabsValidator" ErrorMessage="   *" ForeColor="Red" 
                                Display="Dynamic" OnServerValidate="CtmTabsValidator" ValidationGroup="Contact" 
                                meta:resourcekey="ctmTabsValidatorResource1"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
            <div class="module">
                <asp:Button runat="server" ID="btnAccept" Text="Aceptar" CssClass="button ok"  
                    OnClick="BtnAcceptOnClick" ValidationGroup="Contact" 
                    meta:resourcekey="btnAcceptResource1"/>
                <asp:Button runat="server" ID="btnDelete" Text="Eliminar" CssClass="button delete" 
                    meta:resourcekey="btnDeleteResource1"/>
                <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="button cancel" 
                    OnClick="BtnCancelOnClick" meta:resourcekey="btnCancelResource1"/>
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
                            <asp:Button ID="btnAcceptDelete" runat="server" CssClass="button ok"  Text="Aceptar" OnClick="BtnDeleteOnClick" meta:resourcekey="btnAcceptResource1" />
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
