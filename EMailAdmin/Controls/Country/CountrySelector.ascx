<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CountrySelector.ascx.cs" Inherits="EMailAdmin.Controls.Country.CountrySelector" %>
<div>
    <fieldset>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="scrolleableDiv">
                    <asp:GridView 
                        ID="grvCountries" 
                        runat="server" 
                        DataKeyNames="IdLocacion"
                        GridLines="Horizontal" 
                        AutoGenerateColumns="False"
                        PagerStyle-HorizontalAlign="Center"
                        OnRowDataBound="GrvCountriesRowDataBound" 
                        meta:resourcekey="grvCountriesResource1"
                        CssClass="tbl-generic m-center">
                    <Columns>
                        <asp:TemplateField HeaderText=" " ControlStyle-Width="20px" 
                            meta:resourcekey="TemplateFieldResource1">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="mycheck" OnCheckedChanged="ChkChanged" AutoPostBack="True" meta:resourcekey="mycheckResource1" />                
                            </ItemTemplate>
                            <ControlStyle Width="20px"/>
                        </asp:TemplateField>               
                        <asp:BoundField DataField="Nombre" ItemStyle-Width="210px" 
                            meta:resourcekey="BoundFieldResource1" >
                            <ItemStyle Width="230px"/>
                        </asp:BoundField>
                    </Columns>
                    <RowStyle CssClass="GridView_Row_Data_Normal" />
                    <PagerStyle CssClass="GridView_Pager_Normal" />
                    <SelectedRowStyle CssClass="gridView_Selected_Row" />
                    <HeaderStyle CssClass="gridView_Row_Header" />
                    <EditRowStyle CssClass="gridView_Edit_Row" />
                    <EmptyDataTemplate>
                        <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                            Text="No hay paises cargados" meta:resourcekey="lblNoDataResource1"/>
                    </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="formModule">
            <div class="module">
                <asp:Button runat="server" ID="btnClose" Text="Close" CssClass="button cancel" 
                    OnClick="BtnCloseOnClick" meta:resourcekey="btnCloseResource1"/>
            </div>  
        </div>
    </fieldset>
</div>