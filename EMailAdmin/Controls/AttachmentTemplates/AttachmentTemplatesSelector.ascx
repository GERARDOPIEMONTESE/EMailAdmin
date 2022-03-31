<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachmentTemplatesSelector.ascx.cs" Inherits="EMailAdmin.Controls.AttachmentTemplates.AttachmentTemplatesSelector" %>
<fieldset>
    <asp:UpdatePanel ID="updTemplatesSelector" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <asp:HiddenField ID="hdfIdTemplate" runat="server" />
        <asp:HiddenField ID="hdfIdAttachment" runat="server" />
        <div class="module">
            <asp:Label ID="lblTemplate" runat="server" Text="Template" CssClass="label" 
                meta:resourcekey="lblTemplateResource1" />
            <asp:DropDownList ID="ddlTemplates" DataValueField="Id"  runat="server" 
                CssClass="dropdown" DataTextField="Name" 
                meta:resourcekey="ddlTemplatesResource1" Width="223px" />
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click"/>
        </div>
        <asp:GridView ID="grvTemplates" runat="server" AutoGenerateColumns="false" OnRowDataBound="grvTemplates_RowDataBound">            
            <Columns>
                <asp:BoundField DataField="TemplateName" HeaderText="TemplateName" ItemStyle-Width="250"/>                                                    
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="ibnDelete" runat="server" CommandArgument='<%# Bind("Id") %>'  ImageUrl="~/IMG/b_drop.png" 
                            ToolTip="Delete" OnClick="IbnDelete_Onclick" meta:resourcekey="ibnDeleteResource1"/>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
            </Columns>            
        </asp:GridView>
        <div class="module">
            <asp:Button runat="server" ID="btnClose" Text="Cerrar" CssClass="button cancel" 
                OnClick="BtnCloseOnClick" meta:resourcekey="btnCloseResource1" />
        </div>
   </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>