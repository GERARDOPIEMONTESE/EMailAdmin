<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductSelector.ascx.cs" Inherits="EMailAdmin.Controls.Product.ProductSelector" %>
<div>
    <fieldset>
        <div class="formModule">
            <fieldset style="width: 450px;">
                <div class="module">
                    <asp:Label ID="lblCountry" runat="server" Text="Country" CssClass="label" 
                        meta:resourcekey="lblCountryResource1" />
                    <asp:DropDownList ID="ddlCountry" DataValueField="Codigo"  runat="server" 
                        CssClass="dropdown" DataTextField="Nombre" 
                        meta:resourcekey="ddlCountryResource1" />
                </div>
                <div class="module">
                    <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="button ok" 
                        OnClick="BtnSearchOnClick" meta:resourcekey="btnSearchResource1"/>
                </div>
            </fieldset>
        </div>
        <div class="formModule">
            <fieldset style="width: 450px;">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="scrolleableDivWidth">
                            <asp:GridView 
                                ID="grvProduct" 
                                runat="server" 
                                DataKeyNames="Id"
                                GridLines="Vertical" 
                                AutoGenerateColumns="False"
                                PagerStyle-HorizontalAlign="Center"
                                OnRowDataBound="GrvProductRowDataBound" 
                                meta:resourcekey="grvProductResource1"
                                CssClass="tbl-generic m-center" >
                            <Columns>
                                <asp:TemplateField HeaderText=" " ControlStyle-Width="20px" 
                                    meta:resourcekey="TemplateFieldResource1" >
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" OnCheckedChanged="ChkChanged" AutoPostBack="True" ID="mycheck" meta:resourcekey="mycheckResource1" />
                                    </ItemTemplate>
                                    <ControlStyle Width="20px"/>
                                </asp:TemplateField>               
                                <asp:BoundField DataField="Code" ItemStyle-Width="480" HeaderText="Codigo" >
                                <ItemStyle Width="480px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Description" ItemStyle-Width="480" 
                                    HeaderText="Descripcion" >
                                <ItemStyle Width="480px" />
                                </asp:BoundField>
                            </Columns>
                            <RowStyle CssClass="GridView_Row_Data_Normal" />
                            <PagerStyle CssClass="GridView_Pager_Normal" />
                            <SelectedRowStyle CssClass="gridView_Selected_Row" />
                            <HeaderStyle CssClass="gridView_Row_Header" />
                            <EditRowStyle CssClass="gridView_Edit_Row" />
                            <EmptyDataTemplate>
                                <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                                    Text="No hay productos cargados" meta:resourcekey="lblNoDataResource1" />
                            </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
        </div>
        <div class="formModule">
            <div class="module">
                <asp:Button runat="server" ID="btnClose" Text="Close" CssClass="button cancel" 
                    OnClick="BtnCloseOnClick" meta:resourcekey="btnCloseResource1"/>
            </div>  
        </div>
    </fieldset>
</div>