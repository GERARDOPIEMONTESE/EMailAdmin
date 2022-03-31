<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupControl.ascx.cs" Inherits="EMailAdmin.Controls.Group.GroupControl" %>
<%@ Register TagPrefix="pro" TagName="ProductDropDown" Src="~/Controls/Product/ProductDropDown.ascx" %>
<%@ Register TagPrefix="rte" TagName="RateDropDown" Src="~/Controls/Rate/RateDropDown.ascx" %>
<div class="formModule">
    <div class="module m-b">
        <table>
            <tr>
                <td>
                    <asp:Label CssClass="label" ID="lblName" runat="server" Text="Name" 
                        meta:resourcekey="lblNameResource1" />
                </td>
                <td>
                     <asp:TextBox runat="server" CssClass="textbox" ID="txtName" MaxLength="49" 
                         meta:resourcekey="txtNameResource2" />
                </td>
                <td>
                    <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" 
                        ErrorMessage="   *" ValidationGroup="Group" 
                        meta:resourcekey="rfvNameResource2" />
                </td>
                <td>
                    <asp:CustomValidator runat="server" ID="ctmName" ErrorMessage="   *" 
                        ValidationGroup="Group" Display="Dynamic" OnServerValidate="CtmNameValidator" 
                        meta:resourcekey="ctmNameResource1"/>    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label CssClass="label" ID="lblType" runat="server" Text="Type" 
                        meta:resourcekey="lblTypeResource1" />
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlType" DataTextField="Description" 
                        DataValueField="Id" CssClass="dropdown" meta:resourcekey="ddlTypeResource2" />
                </td>
            </tr>
        </table>
    </div>
    <fieldset style="max-width: 500px">        
        <legend>
            <h2><asp:Literal ID="ltrSeleccion" runat="server" Text="Seleccionar" 
                    meta:resourcekey="ltrSeleccionResource1"></asp:Literal></h2>
        </legend>
        <div class="module" >
            <asp:Label runat="server" ID="lblCountry" Text="Countries" CssClass="label" 
                meta:resourcekey="lblCountryResource1"/>
            <asp:TextBox runat="server" ID="ddlCountry" CssClass="longDropdown" 
                ReadOnly="True" meta:resourcekey="ddlCountryResource2" />
            <asp:Label runat="server" ID="lblBranch" Text="Branches" CssClass="label" 
                meta:resourcekey="lblBranchResource1"/>
            <asp:TextBox runat="server" ID="ddlAccount" CssClass="longDropdown" 
                ReadOnly="True" meta:resourcekey="ddlAccountResource2"/>
            <asp:Label runat="server" ID="lblProduct" Text="Products" CssClass="label" 
                meta:resourcekey="lblProductResource1"/>
            <asp:TextBox runat="server" ID="ddlProduct" CssClass="longDropdown" 
                ReadOnly="True" meta:resourcekey="ddlProductResource2"/>
            <asp:Label runat="server" ID="lblRate" Text="Rate" CssClass="label" 
                meta:resourcekey="lblRateResource1"/>
            <asp:TextBox runat="server" ID="ddlRate" CssClass="longDropdown" 
                ReadOnly="True" meta:resourcekey="ddlRateResource2"/>
             <asp:Label runat="server" ID="lblDynamicValue" Text="DynamicValue" CssClass="label" 
                meta:resourcekey="lblDynamicValueResource1"/>
            <asp:TextBox runat="server" ID="ddlDynamicValue" CssClass="longDropdown" 
                ReadOnly="True"/>
        </div>
        <div class="module">
            <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="BtnAddOnClick" 
                CssClass="button ok" meta:resourcekey="btnAddResource1"/>
        </div>
    </fieldset>
    <div class="module" style="height:300px;overflow:auto;">
        <asp:UpdatePanel runat="server" ID="uplItems">
            <ContentTemplate>
                <fieldset style="max-width: 500px">
                    <legend>
                        <h2><asp:Literal ID="ltlCondicionesAgregadas" runat="server" 
                                Text="Condiciones agregadas" 
                                meta:resourcekey="ltlCondicionesAgregadasResource1"></asp:Literal></h2>
                    </legend>
                    <asp:GridView 
                    ID="grvItems" 
                    runat="server" 
                    GridLines="Vertical" 
                    ForeColor="White"
                    AutoGenerateColumns="False"
                    OnRowDataBound="grvItems_RowDataBound"
                    CssClass="tbl-generic m-center"
                    DataKeyNames="Id" meta:resourcekey="grvItemsResource2" 
                        EmptyDataText="No se encontraron datos">
                        <Columns>
                            <asp:BoundField DataField="ConditionTypeText" HeaderText="Type" 
                                meta:resourcekey="BoundFieldResource1">
                            <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VisibleCode" HeaderText="Code" />
                            <asp:TemplateField HeaderText="Value">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("VisibleValue") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="Descripcion" />
                                    <asp:TextBox ID="txtFiltroDescCondAgregada" runat="server" 
                                        CssClass="longDropdown" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("VisibleValue") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                <HeaderTemplate>
                                    <asp:ImageButton ID="ibnFiltrar" runat="server" 
                                        ImageUrl="~/IMG/icon-search-small.gif" 
                                        ToolTip="Filtrar" onclick="ibnFiltrar_Click" />                                    
                                    <asp:ImageButton ID="ibnTodos" runat="server" 
                                        ImageUrl="~/IMG/icon-cancel-search-small.gif" onclick="ibnTodos_Click" 
                                        ToolTip="Ver todos" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibnDelete" runat="server" ImageUrl="~/IMG/b_drop.png" 
                                        ToolTip="Delete" OnClick="IbnDelete_Onclick" 
                                        meta:resourcekey="ibnDeleteResource1" />
                                </ItemTemplate>
                                <ItemStyle Wrap="False" Width="150px" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="GridView_Row_Data_Normal" />
                        <PagerStyle CssClass="GridView_Pager_Normal" />
                        <SelectedRowStyle CssClass="gridView_Selected_Row" />
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" Text="No encontro datos"></asp:Label>
                            <asp:ImageButton ID="ibnTodos" runat="server" 
                                ImageUrl="~/IMG/icon-cancel-search-small.gif" onclick="ibnTodos_Click" 
                                ToolTip="Ver todos" />
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="gridView_Row_Header" />
                        <EditRowStyle CssClass="gridView_Edit_Row" />
                        <EmptyDataRowStyle CssClass="gridView_Row_Header" />
                    </asp:GridView>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>