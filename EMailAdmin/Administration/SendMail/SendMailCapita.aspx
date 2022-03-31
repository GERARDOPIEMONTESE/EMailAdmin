<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="SendMailCapita.aspx.cs" Inherits="EMailAdmin.Administration.SendMail.SendMailCapita" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register TagPrefix="ajx" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register src="~/Controls/Selectors/Capita/CapitaSelector.ascx" tagname="CapitaSelector" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Send Mail Capita</title>
    <link href="../../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/loading.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True" />
    <div class="PaginaPortal">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1">
            <ProgressTemplate>
                <div id="progressBackgroundFilter">
                </div>
                <div id="processMessage" align="center">
                    <p>
                        Loading...</p>
                    <img alt="Loading..." src="../../IMG/loading.gif" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="form medium">
            <fieldset class="m-center">
                <legend>
                    <asp:Label runat="server" ID="lbMailCapita" Text="Send Mail Capita" CssClass="label" 
                        meta:resourcekey="lbMailCapitaResource1" />
                </legend>                
                <div class="form-rows">
                    <div class="row">
                        <asp:Label runat="server" ID="lblNombre" Text="Nombre" CssClass="label" 
                            meta:resourcekey="lblNombreResource1"/>
                        <asp:TextBox runat="server" ID="txtNombre" CssClass="textbox" Width="300px" 
                            meta:resourcekey="txtNombreResource1"/>
                        <asp:RequiredFieldValidator runat="server" ID="rfvNombre" 
                            ControlToValidate="txtNombre" CssClass="error"
                            ErrorMessage="*" ValidationGroup="SendEmailCapita" 
                            meta:resourcekey="rfvNombreResource1"/>
                    </div>
                    <div class="row">
                        <asp:Label runat="server" ID="lblApellido" Text="Apellido" CssClass="label" 
                            meta:resourcekey="lblApellidoResource1"/>
                        <asp:TextBox runat="server" ID="txtApellido" CssClass="textbox" Width="300px" 
                            meta:resourcekey="txtApellidoResource1"/>
                        <asp:RequiredFieldValidator runat="server" ID="rfvApellido" 
                            ControlToValidate="txtApellido" CssClass="error"
                            ErrorMessage="*" ValidationGroup="SendEmailCapita" 
                            meta:resourcekey="rfvApellidoResource1"/>
                    </div>
                    <div class="row">
                        <asp:Label runat="server" ID="lblDNI" Text="Documento" CssClass="label" 
                            meta:resourcekey="lblDNIResource1"/>
                        <asp:DropDownList runat="server" ID="ddlTipoDocumento" DataValueField="Id" 
                            DataTextField="Descripcion" meta:resourcekey="ddlTipoDocumentoResource1"></asp:DropDownList>
                        <asp:TextBox runat="server" ID="txtDocumento" CssClass="textbox" 
                            meta:resourcekey="txtDocumentoResource1"/>
                        <%--<asp:RequiredFieldValidator runat="server" ID="rfvDocumento" 
                            ControlToValidate="txtDocumento" CssClass="error"
                            ErrorMessage="*" ValidationGroup="SendEmailCapita" 
                            meta:resourcekey="rfvDocumentoResource1"/>--%>
                    </div>                                                                          
                    <div class="row">
                        <asp:Label runat="server" ID="lblMailTo" Text="Mail To" CssClass="label" 
                            meta:resourcekey="lblMailToResource1" />
                        <div>
                            <asp:TextBox runat="server" ID="txtMailTo" CssClass="textbox" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVCorreo" CssClass="TextError_Normal" runat="server"
						    ErrorMessage="Correo electrónico." ControlToValidate="txtMailTo" ValidationGroup="SendEmailCapita"
						    Text="*" Width="16px" style="float:right;position: absolute;" 
                                meta:resourcekey="RFVCorreoResource1"/>
                            <asp:RegularExpressionValidator ID="REVCorreo" 
                                ValidationGroup="SendEmailCapita" CssClass="TextError_Normal"
						        runat="server" ErrorMessage="Formato del correo electrónico." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
						        ControlToValidate="txtMailTo" Display="Dynamic"  style="float:right;"
                                meta:resourcekey="REVCorreoResource1"></asp:RegularExpressionValidator>                        
                        </div>
                    </div>
                    <div class="row">
                        <asp:Label runat="server" ID="lblIdioma" Text="Idioma" CssClass="label"/>
                        <asp:DropDownList runat="server" ID="ddlIdioma" DataValueField="Id" 
                            DataTextField="Descripcion"></asp:DropDownList>
                    </div>
                     <div class="row">
                        <asp:Label runat="server" ID="lblCapita" Text="Capita" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtCapitaSel" runat="server" Width="300px" ReadOnly="True"></asp:TextBox>                        
                    </div>
                    <div class="row">
                        <asp:Label runat="server" ID="lblPlan" Text="Plan" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtPlan" runat="server" Width="300px" ReadOnly="True" ></asp:TextBox>
                        <uc1:CapitaSelector runat="server" Id="CapitaSelector" OnCapitaClosed="GetCapitaSel" OnCapitaOpened="BuscarCapita" />                        
                    </div>
                    <div class="row">
                        <asp:Label runat="server" ID="lblCountry" Text="Pais" CssClass="label" />
                        <asp:TextBox ID="txtPais" runat="server" Width="300px" ReadOnly="True" 
                            meta:resourcekey="txtPaisResource1"></asp:TextBox>
                    </div>                       
                </div>
                <div class="btn-cnt">
                    <asp:Button runat="server" ID="btnAccept" OnClick="BtnAcceptOnClick" CssClass="button ok"
                        Text="Accept" ValidationGroup="SendEmailCapita" 
                        meta:resourcekey="btnAcceptResource1" />
                </div>
            </fieldset>
        </div>
        <div id="panels">
            <asp:HiddenField runat="server" ID="hfdPopUp" />
            <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;"
                ID="pnlPopUpMail" meta:resourcekey="pnlPopUpMailResource1">
                <div class="formModule">
                    <fieldset>
                        <div class="module">
                            <asp:Label runat="server" ID="lblPopUp" meta:resourcekey="lblPopUpResource1" />
                        </div>
                        <div class="module">
                            <asp:Button runat="server" ID="btnClose" CssClass="button cancel" Text="Close" 
                                meta:resourcekey="btnCloseResource1" />
                        </div>
                    </fieldset>
                </div>
            </asp:Panel>
            <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" ID="mpePopUpMail" TargetControlID="hfdPopUp"
                PopupControlID="pnlPopUpMail" RepositionMode="None" runat="server" BehaviorID="mpePopUp"
                DynamicServicePath="" Enabled="True" CancelControlID="btnClose"></ajx:ModalPopupExtender>
        </div>
    </div>    
    </form>
</body>
</html>
