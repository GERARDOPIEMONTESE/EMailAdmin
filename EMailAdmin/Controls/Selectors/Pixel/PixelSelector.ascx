<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PixelSelector.ascx.cs" Inherits="EMailAdmin.Controls.Selectors.Pixel.PixelSelector" %>
<div class="formModule">
    <fieldset>
        <div class="module">
            <asp:Label runat="server" ID="lblPixel" Text="Pixel" 
                CssClass="labelTitle" />
            <p/>
            <br/>
        </div>
        
        <div class="module">
            <asp:Label runat="server" ID="lblName" Text="Name" CssClass="label" 
                meta:resourcekey="lblNameResource1" />
            <asp:DropDownList runat="server" ID="ddlName" CssClass="dropdown" 
                DataValueField="Id" DataTextField="Name" 
                meta:resourcekey="ddlNameResource1" />
        </div>
        <div class="module">
            <asp:Button runat="server" ID="btnAccept" Text="Aceptar" CssClass="button ok"  
                OnClick="BtnAcceptOnClick" meta:resourcekey="btnAcceptResource1"  />
            <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="button cancel" 
                OnClick="BtnCancelOnClick" meta:resourcekey="btnCancelResource1"  />
        </div>
    </fieldset>
</div>