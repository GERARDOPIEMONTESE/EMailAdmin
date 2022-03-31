<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CountryVisibleTextSelector.ascx.cs" Inherits="EMailAdmin.Controls.Selectors.CountryVisibleText.CountryVisibleTextSelector" %>

<div class="formModule">
    <fieldset>
        <div class="module">
            <asp:Label runat="server" ID="lblCountryVisibleText" Text="CountryVisibleText" 
                CssClass="labelTitle" meta:resourcekey="lblCountryVisibleTextResource1" />
            <p/>
            <br/>
        </div>
        <div class="module">
            <asp:Label runat="server" ID="lblType" Text="Type" CssClass="label" 
                meta:resourcekey="lblTypeResource1" />
            <asp:DropDownList runat="server" ID="ddlType" CssClass="dropdown" 
                DataValueField="Id" DataTextField="Description" 
                meta:resourcekey="ddlTypeResource1" />
        </div>
        <div class="module">
            <asp:Button runat="server" ID="btnAccept" Text="Aceptar" CssClass="button ok"  
                OnClick="BtnAcceptOnClick" meta:resourcekey="btnAcceptResource1" />
            <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="button cancel" 
                OnClick="BtnCancelOnClick" meta:resourcekey="btnCancelResource1" />
        </div>
    </fieldset>
</div>