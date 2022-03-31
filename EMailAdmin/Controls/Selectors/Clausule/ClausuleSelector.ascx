<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClausuleSelector.ascx.cs" Inherits="EMailAdmin.Controls.Selectors.Clausule.ClausuleSelector" %>
<div class="formModule">
    <fieldset>
        <div class="module">
            <asp:Label runat="server" ID="lblClausuleSelectorTitle" Text="ClausuleSelectorTitle" 
                CssClass="labelTitle" 
                meta:resourcekey="lblClausuleSelectorTitleResource1" />
            <p/>
            <br/>
        </div>
        <div class="module">
            <asp:Label runat="server" ID="lblClausuleSelector" Text="ClausuleSelector" CssClass="label" 
                meta:resourcekey="lblClausuleSelectorResource1" />
            <asp:TextBox runat="server" ID="txtClausuleCode"></asp:TextBox>
        </div>
        <div class="module">
            <asp:Button runat="server" ID="btnAccept" Text="Aceptar" CssClass="button ok"  
                OnClick="BtnAcceptOnClick" meta:resourcekey="btnAcceptResource1" />
            <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="button cancel" 
                OnClick="BtnCancelOnClick" meta:resourcekey="btnCancelResource1" />
        </div>
    </fieldset>
</div>