<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendMail.aspx.cs" Inherits="EMailAdmin.Administration.SendMail.SendMail" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register TagPrefix="ajx" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Send Mail</title>
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
                        Mail Send
                    </h2>
                </legend>
                <div class="form-rows">
                <div class="row">
                    <asp:Label runat="server" ID="lblCountry" Text="Country" CssClass="label" 
                        meta:resourcekey="lblCountryResource1" />
                    <asp:DropDownList runat="server" ID="ddlCountry" CssClass="drp-medium" DataTextField="Nombre"
                        DataValueField="Codigo" meta:resourcekey="ddlCountryResource1" />
                </div>
                <div class="row">
                    <asp:Label runat="server" ID="lblVoucher" Text="Voucher" CssClass="label" 
                        meta:resourcekey="lblVoucherResource1" />
                    <asp:TextBox runat="server" ID="txtVoucher" CssClass="textbox" 
                        meta:resourcekey="txtVoucherResource1" />
                    <asp:RequiredFieldValidator runat="server" ID="rfvVoucher" 
                        ControlToValidate="txtVoucher" CssClass="error"
                        ErrorMessage="*" ValidationGroup="Voucher" 
                        meta:resourcekey="rfvVoucherResource1" />
                    <asp:CustomValidator runat="server" ID="ctmVoucher" Display="Dynamic" CssClass="error"
                        ValidationGroup="Voucher" ControlToValidate="txtVoucher" 
                        OnServerValidate="CtmVoucherOnValidate" 
                        meta:resourcekey="ctmVoucherResource1" />
                </div>
                <div class="row">                    
                    <asp:Label runat="server" ID="lblMailTo" Text="Mail To" CssClass="label" 
                        meta:resourcekey="lblMailToResource1" />
                    <asp:TextBox runat="server" ID="txtMailTo" CssClass="textbox" 
                        meta:resourcekey="txtMailToResource1" />
                    <asp:CheckBox runat="server" ID="cbMailTo" 
                        meta:resourcekey="cbMailToResource1" />
                    <asp:CustomValidator runat="server" ID="ctmMailTo" Display="Dynamic" CssClass="error"
                        ValidationGroup="Voucher" ControlToValidate="txtMailTo" 
                        OnServerValidate="CtmMailToOnValidate" meta:resourcekey="ctmMailToResource1" />
                </div>
                <div class="btn-cnt">
                    <asp:Button runat="server" ID="btnAccept" OnClick="BtnAcceptOnClick" CssClass="button ok"
                        Text="Accept" ValidationGroup="Voucher" 
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
                DynamicServicePath="" Enabled="True" CancelControlID="btnClose" />
        </div>
    </div>
    </form>
</body>
</html>
