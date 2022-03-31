<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrwEmailPrepurchase.aspx.cs" Inherits="EMailAdmin.Administration.EmailLogsPrecompra.FrwEmailPrepurchase" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register TagPrefix="ajx" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/loading.css" rel="stylesheet" type="text/css" />
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
                    <h2>
                        Reenvio de Email de Precompra
                    </h2>
                </legend>
                <div class="form-rows">
                    <div class="row">
                        <asp:Label runat="server" ID="Label1" Text="Tipo de email:" CssClass="label" 
                            meta:resourcekey="Label1Resource1"/>                
                        <asp:DropDownList ID="ddlTipoEmailPrepurchase" runat="server" 
                            DataTextField="Descripcion" DataValueField="Codigo" 
                            meta:resourcekey="ddlTipoEmailPrepurchaseResource1" ></asp:DropDownList>
                    </div>
                     <div class="row">                    
                        <asp:Label runat="server" ID="lblCodePaxBox" Text="Codigo Box:" 
                            CssClass="label" meta:resourcekey="lblCodePaxBoxResource1"/>                
                        <asp:TextBox ID="txtCodigoPaxBox" runat="server" 
                            meta:resourcekey="txtCodigoPaxBoxResource1"></asp:TextBox>
                    </div>
                    <div class="row">
                        <asp:Label runat="server" ID="lblPais" Text="Pais:" CssClass="label" 
                            meta:resourcekey="lblPaisResource1"/>                
                        <asp:DropDownList ID="ddlPais" runat="server" DataTextField="Nombre" 
                            DataValueField="Codigo" meta:resourcekey="ddlPaisResource1" ></asp:DropDownList>
                    </div>
                    <div class="row">
                         <asp:Label runat="server" ID="lblCodigoVerif" Text="Codigo de Verificacion:" 
                             CssClass="label" meta:resourcekey="lblCodigoVerifResource1"/>                
                        <asp:TextBox ID="txtCodigoVerif" runat="server" 
                             meta:resourcekey="txtCodigoVerifResource1"></asp:TextBox>
                    </div>
                    <div class="row">                    
                        <asp:Label runat="server" ID="lblVoucherGroup" Text="Voucher group:" 
                            CssClass="label" meta:resourcekey="lblVoucherGroupResource1"/>                
                        <asp:TextBox ID="txtVoucherGroup" runat="server" 
                            meta:resourcekey="txtVoucherGroupResource1"></asp:TextBox>
                    </div>               
                    <div class="row">                    
                        <asp:Label runat="server" ID="lblMailTo" Text="Mail To" CssClass="label" />
                        <asp:TextBox runat="server" ID="txtMailTo" CssClass="textbox" />
                        <asp:CheckBox runat="server" ID="cbMailTo" />
                        <asp:CustomValidator runat="server" ForeColor="Red" ID="ctmMailTo" Display="Dynamic"
                            ValidationGroup="Voucher" ControlToValidate="txtMailTo" OnServerValidate="CtmMailToOnValidate" />
                    </div>
                    <div class="btn-cnt">
                        <asp:Button runat="server" ID="btnAccept" OnClick="BtnAcceptOnClick" CssClass="button ok"
                            Text="Accept" ValidationGroup="PaxBox" 
                            meta:resourcekey="btnAcceptResource1" />
                    </div>
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
                            <asp:Label runat="server" ID="lblPopUp" meta:resourceKey="lblPopUpResource1" />
                        </div>
                        <div class="module">
                            <asp:Button runat="server" ID="btnClose" CssClass="button cancel" Text="Close" 
                                meta:resourceKey="btnCloseResource1" />
                        </div>
                    </fieldset>
                </div>
            </asp:Panel>
            <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" ID="mpePopUpMail" TargetControlID="hfdPopUp"
                PopupControlID="pnlPopUpMail" RepositionMode="None" runat="server" BehaviorID="mpePopUp"
                DynamicServicePath="" Enabled="True" CancelControlID="btnClose" />
        </div>
    </div>
    </form>
</body>
</html>
