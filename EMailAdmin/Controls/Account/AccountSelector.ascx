<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountSelector.ascx.cs" Inherits="EMailAdmin.Controls.Account.AccountSelector" %>
<fieldset>
    <div class="formModule">
        <fieldset style="max-width: 450px;">
            <div class="module">
                <asp:Label ID="lblCountry" runat="server" Text="Country" CssClass="label" 
                    meta:resourcekey="lblCountryResource1" />
                <asp:DropDownList ID="ddlCountry" DataValueField="IdLocacion"  runat="server" 
                    CssClass="dropdown" DataTextField="Nombre" 
                    meta:resourcekey="ddlCountryResource1" />
            </div>
            <div class="module">
                <asp:Label ID="lblCode" runat="server" Text="Code" CssClass="label" 
                    meta:resourcekey="lblCodeResource1" />
                <asp:TextBox ID="txtCode" runat="server" CssClass="textBoxNumber_S" 
                    MaxLength="5" meta:resourcekey="txtCodeResource1" />
            </div>
            <div class="module">
                <asp:Label ID="lblFirmName" runat="server" Text="Firm Name" CssClass="label" 
                    meta:resourcekey="lblFirmNameResource1" />
                <asp:TextBox ID="txtFirmName" runat="server" CssClass="textbox" 
                    meta:resourcekey="txtFirmNameResource1"  />
            </div>
            <div class="module">
                <asp:Label ID="lblName" runat="server" Text="Name" CssClass="label" 
                    meta:resourcekey="lblNameResource1" />
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox" 
                    meta:resourcekey="txtNameResource1" />
            </div>
            <div class="module">
                <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="button ok" 
                    OnClick="BtnSearchOnClick" meta:resourcekey="btnSearchResource1"/>
            </div>
        </fieldset>
    </div>
    <div class="formModule">
        <fieldset style="max-width: 450px;">
            <div class="scrolleableDivWidth">
                <asp:GridView 
                    ID="grvAccounts" 
                    runat="server" 
                    DataKeyNames="Id"
                    GridLines="Vertical" 
                    AutoGenerateColumns="False"
                    PagerStyle-HorizontalAlign="Center"
                    OnRowDataBound="GrvAccountsRowDataBound" 
                    meta:resourcekey="grvAccountsResource1"
                    CssClass="tbl-generic m-center">
                <Columns>
                    <asp:TemplateField HeaderText=" " ControlStyle-Width="20px" 
                        meta:resourcekey="TemplateFieldResource1" >
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="mycheck" meta:resourcekey="mycheckResource1" />
                        </ItemTemplate>
                        <ControlStyle Width="20px"/>
                    </asp:TemplateField>               
                    <asp:BoundField DataField="CodigoDeCuenta" ItemStyle-Width="70px" 
                        meta:resourcekey="BoundFieldResource1" >
                        <ItemStyle Width="70px"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="NumeroSucursal" ItemStyle-Width="50px" 
                        meta:resourcekey="BoundFieldResource2" >
                        <ItemStyle Width="50px"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="RazonSocial" ItemStyle-Width="120px" 
                        meta:resourcekey="BoundFieldResource3" >
                        <ItemStyle Width="300px"/>
                    </asp:BoundField>
                </Columns>
                <RowStyle CssClass="GridView_Row_Data_Normal" />
                <PagerStyle CssClass="GridView_Pager_Normal" />
                <SelectedRowStyle CssClass="gridView_Selected_Row" />
                <HeaderStyle CssClass="gridView_Row_Header" />
                <EditRowStyle CssClass="gridView_Edit_Row" />
                <EmptyDataTemplate>
                    <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                        Text="No hay sucursales cargadas" meta:resourcekey="lblNoDataResource1" />
                </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </fieldset>
        <div class="formModule">
            <div class="module">
                <asp:Button runat="server" ID="btnClose" Text="Close" CssClass="button cancel" 
                    OnClick="BtnCloseOnClick" meta:resourcekey="btnCloseResource1" />
            </div>  
        </div>
    </div>
</fieldset>