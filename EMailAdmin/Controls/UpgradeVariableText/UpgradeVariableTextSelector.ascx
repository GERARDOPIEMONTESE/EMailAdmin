<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpgradeVariableTextSelector.ascx.cs" Inherits="EMailAdmin.Controls.UpgradeVariableText.UpgradeVariableTextSelector" %>
<div>
    <fieldset>
        <asp:UpdatePanel runat="server" ID="pnlGrid" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="scrolleableDiv">
                    <asp:GridView 
                        ID="grvUpgradeVariableText" 
                        runat="server" 
                        DataKeyNames="Id"
                        GridLines="Vertical" 
                        AutoGenerateColumns="False"
                        CssClass="tbl-generic m-center"
                        OnRowDataBound="GrvCountriesRowDataBound" 
                        meta:resourcekey="grvUpgradeVariableTextResource1">
                    <Columns>
                        <asp:TemplateField HeaderText=" " 
                            meta:resourcekey="TemplateFieldResource1">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="mycheck" meta:resourcekey="mycheckResource1" />                
                            </ItemTemplate>
        <ControlStyle Width="20px"></ControlStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Code" HeaderText="Code" meta:resourcekey="BoundField2Resource1"/>               
                        <asp:BoundField DataField="Description" 
                            meta:resourcekey="BoundFieldResource1" >
        <ItemStyle Width="480px"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <RowStyle CssClass="gridView_RowStyle" />
                    <PagerStyle CssClass="gridView_Pager_Style_Row" HorizontalAlign="Center" />
                    <SelectedRowStyle CssClass="gridView_Selected_Row" />
                    <HeaderStyle CssClass="gridView_Row_Header" />
                    <EditRowStyle CssClass="gridView_Edit_Row" />
                    <AlternatingRowStyle CssClass="gridView_Alternative_Row_Data" />
                    <EmptyDataTemplate>
                        <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                            Text="No hay paises cargados" meta:resourcekey="lblNoDataResource1"/>
                    </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div>
            <asp:Button runat="server" ID="btnClose" Text="Close" CssClass="button cancel" 
                OnClick="BtnCloseOnClick" meta:resourcekey="btnCloseResource1"/>
        </div>  
    </fieldset>
</div>