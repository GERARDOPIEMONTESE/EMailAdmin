<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupList.aspx.cs" Inherits="EMailAdmin.Group.GroupList" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>

<%@ Register src="../Controls/Group/GroupControl.ascx" tagname="GroupControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    function ConfirmDeleteCondicion(ID) {
        var CurrentBackColour = ID.parentElement.parentElement.style.backgroundColor;
        ID.parentElement.parentElement.style.backgroundColor = 'yellow';
        var result = confirm('Delete?');
        if (result == false) {
            ID.parentElement.parentElement.style.backgroundColor = CurrentBackColour;
        }
        return result;
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Group List</title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/loading.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmMain" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"/>
        <div class="PaginaPortal">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" > 
                <ProgressTemplate> 
                    <div id="progressBackgroundFilter"></div> 
                    <div id="processMessage" align="center"><p>Loading...</p>
                        <img alt="Loading..." src="../IMG/loading.gif"  />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel runat="server" ID="Up" UpdateMode="Always">
            <ContentTemplate>
            <div class="buttonModule">
                <asp:Label runat="server" ID="lblName" Text="Nombre" CssClass="label" 
                    meta:resourcekey="lblNameResource1" />
                <asp:TextBox runat="server" ID="txtName" CssClass="textbox" 
                    meta:resourcekey="txtNameResource1" />
                <asp:Label runat="server" ID="lblType" Text="Type" CssClass="label" 
                    meta:resourcekey="lblTypeResource1" />
                <asp:DropDownList runat="server" ID="ddlType" DataTextField="Description" 
                    DataValueField="Id" CssClass="dropdown" meta:resourcekey="ddlTypeResource1" />
                <asp:Button runat="server" ID="btnSearch" Text="Buscar" 
                    OnClick="btnSearch_OnClick" CssClass="button ok" 
                    meta:resourcekey="btnSearchResource1" />
                <asp:Button runat="server" ID="btnNew" Text="Nuevo" CssClass="button ok" 
                    OnClick="btnNew_OnClick" meta:resourcekey="btnNewResource1" />
            </div>
            <div class="gridList">
                <div class="gridFont">
                    <asp:GridView 
                        ID="grvGroup" 
                        runat="server" 
                        PageSize="30" 
                        AllowPaging="True"
                        GridLines="Vertical" 
                        AutoGenerateColumns="False"
                        CellPadding="4"
                        OnRowDataBound="GrvGroupRowDataBound"
                        OnPageIndexChanging="GrvGroupPageIndexChange" 
                        OnRowCommand="GrvGroupRowCommand"
                        OnRowDeleting="GrvGroupRowDeleting"
                        meta:resourcekey="grvGroupResource1"
                        CssClass="tbl-generic m-center" >
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" 
                                meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="Name" HeaderText="Nombre" 
                                meta:resourcekey="BoundFieldResource2"  >
                            <ItemStyle Width="550px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GroupTypeDescription" 
                                HeaderText="GroupTypeDescription" meta:resourcekey="BoundFieldResource3"  >
                            <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:ButtonField ButtonType="Button" CommandName="View" Text="View" 
                                meta:resourcekey="ButtonFieldResource2">
                                <ControlStyle CssClass="ok small" />
                                <ItemStyle Width="50px" CssClass="gridView_Button"/>
                            </asp:ButtonField>
                            <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Delete" 
                                meta:resourcekey="ButtonFieldResource1" >
                                <ControlStyle CssClass="cancel small" />
                                <ItemStyle Width="50px" CssClass="gridView_Button"/>
                            </asp:ButtonField>
                        </Columns>
                        <RowStyle CssClass="GridView_Row_Data_Normal" />
                        <PagerStyle CssClass="GridView_Pager_Normal" HorizontalAlign="Center" />
                        <SelectedRowStyle CssClass="gridView_Selected_Row" />
                        <HeaderStyle CssClass="gridView_Row_Header" />
                        <EditRowStyle CssClass="gridView_Edit_Row" />
                        <EmptyDataTemplate>
                            <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                                Text="No hay group cargados" meta:resourcekey="lblNoDataResource1"/>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hdfDelete"/>
                <asp:Panel ID="pnlDelete" runat="server" CssClass="modalBackgroundRestore" 
                    Style="display: none;" meta:resourcekey="pnlDeleteResource1" >
                    <div class="formModule" style="text-align: left;width:250px;">
                        <fieldset>
                            <table><tr><td>
                            <div class="module">
                                <asp:Label CssClass="label" ID="lblDelete" runat="server" Text="Desea eliminar el grupo {0}?" meta:resourcekey="lblDeleteResource1" />
                            </div>
                            </td></tr>
                            <tr><td>
                            <div class="module" style="text-align:center;">
                                <asp:Button ID="btnAcceptDelete" runat="server" CssClass="button ok" Text="Aceptar" OnClick="BtnAcceptDeleteOnClick" meta:resourcekey="btnAcceptResource1" />
                                <asp:Button ID="btnCancelDelete" runat="server" CssClass="button cancel" Text="Cancelar" meta:resourcekey="btnCancelResource1" />
                            </div>
                            </td></tr></table>
                        </fieldset>
                    </div>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" ID="mpeDelete" 
                PopupControlID="pnlDelete" RepositionMode="None" runat ="server" CancelControlID="btnCancelDelete"
                TargetControlID="hdfDelete" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hdfCantDelete"/>
                <asp:Panel ID="pnlCantDelete" runat="server" CssClass="modalBackgroundRestore" 
                    Style="display: none;" meta:resourcekey="pnlCantDeleteResource1" >
                    <div class="formModule">
                        <fieldset>
                            <legend>
                                <h2><asp:Literal ID="ltrGroupDelete" runat="server" Text=""></asp:Literal></h2>
                            </legend>
                            <div class="module" >
                                <table Width="350px">
                                    <tr>
                                        <td align="center">
                                            <asp:Label CssClass="label" ID="lblMessage" runat="server" Text="No es posible eliminar." Width="300px" meta:resourcekey="lblCantDeleteResource1" />            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnCantDelete" runat="server" CssClass="button cancel" Text="Aceptar" meta:resourcekey="btnAcceptResource1" />
                                        </td>
                                    </tr>
                                </table
                            </div>
                        </fieldset>
                    </div>
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                ID="mpeCantDelete"  PopupControlID="pnlCantDelete" RepositionMode="None" 
                    runat ="server" CancelControlID="btnCantDelete"
                TargetControlID="hdfCantDelete" DynamicServicePath="" Enabled="True" />
            </div>
            <div>
	            <asp:HiddenField runat="server" ID="hdfConditions"/>
	            <asp:Panel ID="pnlConditions" runat="server" CssClass="modalBackgroundRestore" 
		            Style="display: none;" meta:resourcekey="pnlConditionsResource1" >
		                 <fieldset>
                            <legend>
                                <h2><asp:Literal ID="ltrGroup" runat="server" Text=""></asp:Literal></h2>
                            </legend>
				            <div class="module">
					            <asp:UpdatePanel runat="server" ID="uplItems" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:GridView 
                                            ID="grvConditions" 
                                            runat="server" 
                                            GridLines="Vertical"
                                            AutoGenerateColumns="False"
                                            CellPadding="4" 
                                            PageSize="15"
                                            AllowPaging="True"   
                                            OnRowDataBound="GrvConditionsRowDataBound"
                                            OnPageIndexChanging="GrvConditionsPageIndexChange"
                                            CssClass="tbl-generic m-center" meta:resourcekey="grvConditionsResource1" >
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
                                                        <asp:ImageButton ID="bDeleteCondition" runat="server" ImageUrl="~/IMG/b_drop.png" 
                                                            CommandArgument='<%# Bind("Id") %>'
                                                            ToolTip="Delete" OnCommand="bDeleteCondition_Command" 
                                                            meta:resourcekey="bDeleteConditionResource1" />
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
				            </div>
				            <div class="module">                                
					            <asp:Button runat="server" ID="btnEditar" CssClass="button ok" 
                                    onclick="btnEditar_Click" meta:resourcekey="btnEditarResource1" style="float:left;"/>
                                <asp:Button runat="server" ID="btnClose" CssClass="button cancel" OnClick="BtnClose_OnClick" meta:resourcekey="btnClose" style="float:right;" />
                                
				            </div>
			            </fieldset>
	            </asp:Panel>
	            <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
	             ID="mpeConditions" 
	            PopupControlID="pnlConditions" RepositionMode="None" runat ="server" CancelControlID="btnClose"
	            TargetControlID="hdfConditions" DynamicServicePath="" Enabled="True" />         
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
