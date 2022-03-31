<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssociationsControl.ascx.cs" Inherits="EMailAdmin.Controls.Associations.AssociationsControl" %>
<div class="formModule">
    <fieldset style="width: 600px">
        <div class="module">
            <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="BtnAddOnClick" 
                CssClass="button ok" meta:resourcekey="btnAddResource1" />
        </div>
    </fieldset>
    <div class="module" style="overflow:scroll; height:450px;">
        <asp:UpdatePanel runat="server" ID="uplItems" UpdateMode="Conditional">
            <ContentTemplate>
                <fieldset style="width: 500px">
                    <asp:GridView 
                    ID="grvAssociations" 
                    runat="server" 
                    GridLines="Vertical" 
                    AutoGenerateColumns="False"
                    DataKeyNames="Id" meta:resourcekey="grvAssociationsResource1"
                    CssClass="tbl-generic m-center">
                        <Columns>
                            <asp:BoundField DataField="GroupName" HeaderText="GroupName" 
                                meta:resourcekey="BoundFieldResource1">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ModuleName" HeaderText="ModuleName" 
                                meta:resourcekey="BoundFieldResource2" >
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ReceiveDescription" HeaderText="ReceiveDescription" 
                                meta:resourcekey="BoundFieldResource3">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibnDelete" runat="server" ImageUrl="~/IMG/b_drop.png" 
                                        ToolTip="Delete" OnClick="IbnDelete_Onclick" 
                                        meta:resourcekey="ibnDeleteResource1" />
                                </ItemTemplate>
                                <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="GridView_Row_Data_Normal" />
                        <PagerStyle CssClass="GridView_Pager_Normal" />
                        <SelectedRowStyle CssClass="gridView_Selected_Row" />
                        <HeaderStyle CssClass="gridView_Row_Header" />
                        <EditRowStyle CssClass="gridView_Edit_Row" />
                    </asp:GridView>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>