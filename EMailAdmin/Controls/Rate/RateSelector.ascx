<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RateSelector.ascx.cs" Inherits="EMailAdmin.Controls.Rate.RateSelector" %>
<div>
    <fieldset>
        <div class="formModule">
            <fieldset style="width: 450px;">
                <div class="module">
                    <asp:UpdatePanel runat="server" ID="pnlFilters" >
                        <ContentTemplate>
                            <div class="formModule">
                                <div class="module">
                                    <asp:Label runat="server" ID="lblCountry"  Text="Country" CssClass="label" />
                                    <asp:DropDownList  runat="server" ID="ddlCountry" CssClass="dropdown"  AutoPostBack="True"
                                         DataTextField="Nombre" DataValueField="Codigo"  OnSelectedIndexChanged="CountryChanged" />
                                </div>
                                <div class="module">
                                    <asp:Label ID="lblProduct" runat="server" Text="Product" CssClass="label"  />
                                    <asp:DropDownList runat="server" ID="ddlProduct" CssClass="dropdown"
                                         DataTextField="FullDescription" DataValueField="Id" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="module">
                    <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="button ok" 
                        OnClick="BtnSearchOnClick" />
                </div>
            </fieldset>
        </div>
        <div class="formModule">
            <fieldset style="width: 450px;">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="scrolleableDivWidth">
                            <asp:GridView 
                                ID="grvRate" 
                                runat="server" 
                                DataKeyNames="Id"
                                GridLines="Vertical" 
                                AutoGenerateColumns="False"
                                PagerStyle-HorizontalAlign="Center"
                                OnRowDataBound="GrvRateRowDataBound"
                                CssClass="tbl-generic m-center" >
                            <Columns>
                                <asp:TemplateField HeaderText=" " ControlStyle-Width="20px"  >
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" OnCheckedChanged="ChkChanged" AutoPostBack="True" ID="mycheck" />
                                    </ItemTemplate>
                                    <ControlStyle Width="20px"/>
                                </asp:TemplateField>               
                                <asp:BoundField DataField="Code" ItemStyle-Width="480" HeaderText="Codigo"  >
                                    <ItemStyle Width="480px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="Description" ItemStyle-Width="480" 
                                    HeaderText="Descripcion"  >
                                    <ItemStyle Width="480px"/>
                                </asp:BoundField>                                
                            </Columns>
                            <RowStyle CssClass="GridView_Row_Data_Normal" />
                            <PagerStyle CssClass="GridView_Pager_Normal" />
                            <SelectedRowStyle CssClass="gridView_Selected_Row" />
                            <HeaderStyle CssClass="gridView_Row_Header" />
                            <EditRowStyle CssClass="gridView_Edit_Row" />
                            <EmptyDataTemplate>
                                <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                                    Text="No hay rates cargados" />
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
                    OnClick="BtnCloseOnClick" />
            </div>  
        </div>
    </fieldset>
</div>