<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageSelector.ascx.cs" Inherits="EMailAdmin.Controls.Selectors.Image.ImageSelector" %>
<%@ Register TagPrefix="ajx" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<div class="formModule">
    <fieldset>
        <div class="module">
            <asp:Label runat="server" ID="lblImage" Text="Imagen" CssClass="labelTitle" 
                meta:resourcekey="lblImageResource1"/>
            <p/>
            <br/>
        </div>
        <div class="module">
            <asp:Label runat="server" ID="lblName" Text="Name" CssClass="label" 
                meta:resourcekey="lblNameResource1"/>
            <ajx:AsyncFileUpload runat="server" OnUploadedComplete="FupImageOnUploadedComplete" ID="fupImage" />
        </div>
        <div class="module">
            <asp:Button runat="server" ID="btnAccept" Text="Aceptar" CssClass="button ok"  
                OnClick="BtnAcceptOnClick" meta:resourcekey="btnAcceptResource1" />
            <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="button cancel" 
                OnClick="BtnCancelOnClick" meta:resourcekey="btnCancelResource1" />
        </div>
    </fieldset>
</div>