<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TableVariableSelector.ascx.cs" Inherits="EMailAdmin.Controls.Selectors.TableVariableText.TableVariableSelector" %>
<div class="formModule">
    <fieldset>
        <div class="module">
            <asp:Label runat="server" ID="lblTableVariableText" Text="TableVariableText" 
                CssClass="labelTitle" meta:resourcekey="lblTableVariableTextResource1" />
            <p/>
            <br/>
        </div>
        <div class="module">
            <asp:DropDownList runat="server" ID="ddlType" CssClass="dropdown" 
                DataValueField="Id" DataTextField="Name" 
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