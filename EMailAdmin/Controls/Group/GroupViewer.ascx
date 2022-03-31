<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupViewer.ascx.cs" Inherits="EMailAdmin.Controls.Group.GroupViewer" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit"  TagPrefix="ajx"%>
<div class="formModule">    
    <asp:UpdatePanel runat="server" ID="pnlTemplate" UpdateMode="Always">
        <ContentTemplate>
            <div class="formModule">         
                <fieldset>
                    <legend>
                        <h2><asp:Literal ID="ltrSelGrupo" runat="server" Text="Seleccionar grupo"></asp:Literal></h2>
                    </legend>
                    <div class="module">  
                    <table><tr><td>
                        <td>
                        <asp:Label runat="server" ID="lblGroup" CssClass="label" Text="Group Name" ></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlGroup" CssClass="dropdown" 
                        DataTextField="Name" DataValueField="Id" ></asp:DropDownList>
                        </td>
                        <td>
                        <asp:Label CssClass="label" ID="lblModule" runat="server" Text="Module" 
                            meta:resourcekey="lblModuleResource1" />
                        <asp:DropDownList runat="server" ID="ddlModule" CssClass="dropdown" 
                        DataTextField="Nombre" DataValueField="Nombre"
                            meta:resourcekey="ddlModuleResource1" />
                        </td>
                        <td>
                        <asp:Label runat="server" ID="lblRecive" CssClass="label" Text="Recive" 
                            meta:resourcekey="lblReciveResource1"/>
                        <asp:CheckBox runat="server" ID="chkRecive" Checked="True" 
                            meta:resourcekey="chkReciveResource1"/>
                        </td>
                        <td>
                        <asp:Button runat="server" ID="btnAddGroup" Text="Agregar" CssClass="button ok" 
                            OnClick="btnAddGroup_Click" meta:resourcekey="btnAddResource1"/>    
                        </td></tr></table>
                    </div>
                 </fieldset>
                 <fieldset>
                    <legend>
                        <h2><asp:Literal ID="Literal1" runat="server" Text="Grupo agregados"></asp:Literal></h2>
                    </legend>                 
                    <asp:GridView ID="gvGruposAgregados" runat="server" AutoGenerateColumns="False"
                        GridLines="Vertical" 
                        CssClass="tbl-generic m-center" 
                         onrowdatabound="gvGruposAgregados_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" />
                            <asp:BoundField DataField="GroupName" HeaderText="Grupo" />
                            <asp:BoundField DataField="ModuleName" HeaderText="Modulo" />
                            <asp:TemplateField HeaderText="Recive">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkRecive" runat="server" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRecive" runat="server" Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelGroup" runat="server" 
                                        CommandArgument='<%# Container.DataItemIndex %>' ImageUrl="~/IMG/delete.gif" 
                                        oncommand="btnDelGroup_Command" />
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
                                Text="No hay templates cargados" meta:resourcekey="lblNoDataResource1" />
                        </EmptyDataTemplate>
                    </asp:GridView>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>