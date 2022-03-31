<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConditionVariableTextSelector.ascx.cs" Inherits="EMailAdmin.Controls.Selectors.ConditionVariableText.ConditionVariableTextSelector" %>
<div class="formModule">
    <fieldset>
        <div class="module">
            <asp:Label runat="server" ID="lblConditionVariableText" Text="ConditionVariableText" 
                CssClass="labelTitle" 
                meta:resourcekey="lblConditionVariableTextResource1" />
            <p/>
            <br/>
        </div>
        <div class="module">
            <asp:Label runat="server" ID="lblCondition" Text="Condition" CssClass="label" 
                meta:resourcekey="lblConditionResource1" />
            <asp:DropDownList ID="ddlConditionVT" runat="server" DataValueField="ID" 
                DataTextField="Name" meta:resourcekey="ddlConditionVTResource1" >
            </asp:DropDownList>
        </div>
        <div class="module">
            <asp:Button runat="server" ID="btnAccept" Text="Aceptar" CssClass="button ok"  
                OnClick="BtnAcceptOnClick" meta:resourcekey="btnAcceptResource1" />
            <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="button cancel" 
                OnClick="BtnCancelOnClick" meta:resourcekey="btnCancelResource1" />
        </div>
    </fieldset>
</div>