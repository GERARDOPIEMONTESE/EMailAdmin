<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachmentSelector.ascx.cs" Inherits="EMailAdmin.Controls.Selectors.Attachment.AttachmentSelector" %>

<div class="formModule">
    <fieldset>
        <div class="module">
            <asp:Label runat="server" ID="lblAttachment" Text="Attachment" 
                CssClass="labelTitle" meta:resourcekey="lblAttachmentResource1" />
            <p/>
            <br/>
        </div>
        <div>
            <fieldset>
                <div class="module">
                    <asp:CheckBox ID="chkSoloAsociados" runat="server" Text="Ver solo asociados" 
                        Checked="True" TextAlign="Left" />
                </div>
                <div class="module">
                    <asp:Label runat="server" ID="lblName" Text="Name" CssClass="label" 
                        meta:resourcekey="lblNameResource1" />
                    <asp:TextBox runat="server" ID="txtName" CssClass="smalltextbox"  />
                </div>
                <div class="module">
                    <asp:Label runat="server" ID="lblType" Text="Type" CssClass="label" 
                        meta:resourcekey="lblTypeResource1" />
                    <asp:DropDownList runat="server" ID="ddlType" CssClass="smalldropdown" 
                        DataValueField="Id" DataTextField="Description"  />
                </div>
                <div class="module">
                    <asp:Button runat="server" ID="btnSearch" Text="Aceptar" CssClass="button ok"  
                        OnClick="BtnSearchOnClick" meta:resourcekey="btnSearchResource1" />
                </div>
            </fieldset>
            <br/>    
        </div>
        <div class="gridList">
            <div class="gridFont">
                <asp:GridView 
                    ID="grvAttachment" 
                    runat="server" 
                    AllowPaging="True"
                    GridLines="Vertical" 
                    AutoGenerateColumns="False"
                    CssClass="tbl-generic m-center"
                    PagerStyle-HorizontalAlign="Center"
                    OnRowDataBound="GrvAttachmentRowDataBound"
                    OnPageIndexChanging="GrvAttachmentPageIndexChange" 
                    meta:resourcekey="grvAttachmentResource1">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" 
                            meta:resourcekey="BoundFieldResource1" />
                        <asp:TemplateField HeaderText=" " ControlStyle-Width="20px" 
                            meta:resourcekey="TemplateFieldResource1">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="mycheck" meta:resourcekey="mycheckResource1" />                
                            </ItemTemplate>
                            <ControlStyle Width="20px"/>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Nombre" ItemStyle-Width="100"
                            meta:resourcekey="BoundFieldResource2" >
<ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="AttachmentTypeDescripcion" HeaderText="Tipo" ItemStyle-Width="80"
                            meta:resourcekey="BoundFieldResource3" >
<ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnContentAttachment" runat="server" CausesValidation="False" 
                                    CommandArgument='<%# Bind("Id") %>' Text="Content PDF" Visible="False"
                                    oncommand="btnContentAttachment_Command"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnContentAttachmentFooter" runat="server" CausesValidation="False" CommandArgument='<%# Bind("Id") %>' oncommand="btnContentAttachmentFooter_Command" Text="Content PDF Footer" Visible="False"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" HeaderText="Grupo">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnAttachmentGroup" runat="server" CausesValidation="False" CommandArgument='<%# Bind("Id") %>' oncommand="btnAttachmentGroup_Command" Text="Group"></asp:LinkButton>
                                <asp:HiddenField ID="hdfIdGroupAttachment" runat="server" />
                                <asp:HiddenField ID="hdfAttachOrder" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" HeaderText="Templates">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnAttachmentTemplates" runat="server" CausesValidation="False" CommandArgument='<%# Bind("Id") %>' oncommand="btnAttachmentTemplates_Command" Text="templates"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="gridView_RowStyle" />
                    <PagerStyle CssClass="gridView_Pager_Style_Row" />
                    <SelectedRowStyle CssClass="gridView_Selected_Row" />
                    <HeaderStyle CssClass="gridView_Row_Header" />
                    <EditRowStyle CssClass="gridView_Edit_Row" />
                    <AlternatingRowStyle CssClass="gridView_Alternative_Row_Data" />
                    <EmptyDataTemplate>
                        <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                            Text="No hay attachments cargados" meta:resourcekey="lblNoDataResource1" />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>        
        </div>
        <div class="module">
            <asp:Button runat="server" ID="btnClose" Text="Cerrar" CssClass="button cancel" 
                OnClick="BtnCloseOnClick" meta:resourcekey="btnCloseResource1" />
        </div>
    </fieldset>
</div>