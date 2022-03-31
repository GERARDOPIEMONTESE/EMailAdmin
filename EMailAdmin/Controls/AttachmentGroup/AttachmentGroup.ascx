<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachmentGroup.ascx.cs" Inherits="EMailAdmin.Controls.AttachmentGroup.AttachmentGroup" %>
<fieldset>
    <asp:UpdatePanel ID="updGroupAttachment" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hdfIdTemplate" runat="server" />
        <asp:HiddenField ID="hdfIdAttachment" runat="server" />
        <div class="module">
            <asp:Label ID="lblGroupAttachment" runat="server" Text="Group Attachment" CssClass="label" 
                meta:resourcekey="lblGroupAttachmentResource1" />
            <asp:DropDownList ID="ddlGroupAttachment" DataValueField="Id"  runat="server" 
                CssClass="dropdown" DataTextField="GroupName" 
                meta:resourcekey="ddlGroupAttachmentResource1" Width="223px" />
        </div>
        <div class="module" style="padding-top:5px;">
            <asp:Label ID="lblAttachOrder" runat="server" Text="Attach Order" CssClass="label" 
                meta:resourcekey="lblAttachOrderResource1" />
            <asp:TextBox ID="txtAttachOrder" runat="server" CssClass="textBoxNumber_S" 
                MaxLength="5" meta:resourcekey="txtAttachOrderResource1" Width="129px" />
        </div>
        <div class="module">
            <asp:Button runat="server" ID="btnClose" Text="Cerrar" CssClass="button cancel" 
                OnClick="BtnCloseOnClick" meta:resourcekey="btnCloseResource1" />

            <asp:Button runat="server" ID="btnQuitar" Text="Quitar" CssClass="button delete" style=" float: right;"
                meta:resourcekey="btnQuitarResource1" OnClick="btnQuitar_Click" />
        </div>
   </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>