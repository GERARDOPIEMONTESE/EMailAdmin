<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessManager.aspx.cs" Inherits="EMailAdmin.Administration.Configuration.ProcessManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Configurador</title>  
    <link href="../../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/loading.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="PaginaPortal">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" > 
            <ProgressTemplate> 
                <div id="progressBackgroundFilter"></div> 
                <div id="processMessage" align="center"><p>Loading...</p>
                    <img alt="Loading..." src="../../IMG/loading.gif"  /> 
                </div> 
            </ProgressTemplate> 
        </asp:UpdateProgress>
        <asp:UpdatePanel runat="server" ID="Up" UpdateMode="Always">
            <ContentTemplate>
            <div class="buttonModule">
                <asp:Label runat="server" Text="Código:" ID="lblValue" />
                <asp:DropDownList ID="ddlConfigurationValues" runat="server" DataTextField="Codigo" DataValueField="Id" ></asp:DropDownList>
                <asp:Button ID="btnSearch" runat="server" Text="Buscar" OnClick="btnSearch_OnClick" CssClass="button ok" />
                <asp:Button ID="btnAdd" runat="server" Text="Agregar" OnClick="btnAdd_OnClick" CssClass="button ok"  />                
            </div>       

        <div class="gridList">
                <div class="gridFont">
            <asp:GridView ID="grvConfiguration" runat="server" AllowPaging="True" AllowSorting="True"
                Width="100%" GridLines="Vertical" ForeColor="White" AutoGenerateColumns="False"
                CellPadding="4" PageSize="20" PagerStyle-HorizontalAlign="Center" DataKeyNames="Id"
                OnPageIndexChanging="grvConfiguration_OnPageIndexChanging" OnRowCommand="grvConfiguration_OnRowCommand">
                <Columns>
                    <asp:BoundField DataField="Codigo" HeaderText="Código" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Descripción" ItemStyle-Width="600" DataField="Descripcion" />
                    <asp:BoundField HeaderText="Period" ItemStyle-Width="600" DataField="Period" />
                    <asp:BoundField HeaderText="PeriodHours" ItemStyle-Width="600" DataField="PeriodHours" />
                    <asp:BoundField HeaderText="CheckLote" ItemStyle-Width="600" DataField="CheckLote" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px">
                        <ItemTemplate>
                            <asp:Button runat="server" ID="lnkEdit" CommandName="Editar"
                                Text="Editar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                ControlStyle-CssClass="button ok"  />
                           <%-- <asp:Button runat="server" ID="lnkDelete" CommandName="Eliminar" Text="Eliminar"
                                OnClientClick="Confirm();" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                ControlStyle-CssClass="button delete" />--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle CssClass="gridView_Edit_Row" />
                <AlternatingRowStyle CssClass="gridView_Alternative_Row_Data" />
                <HeaderStyle CssClass="gridView_Row_Header" />
                <PagerStyle CssClass="GridView_Pager_Normal" />
                <RowStyle CssClass="GridView_Row_Data_Normal" />
                <SelectedRowStyle CssClass="gridView_Selected_Row" />

                <EmptyDataTemplate>
                    <asp:Label ID="lbNoRecords" runat="server" Text="No se encontraron registros." ForeColor="Gray" />
                </EmptyDataTemplate>
                </asp:GridView>
                </div>
                </div>
                <asp:Label runat="server" ID="lblErrorMessage" ForeColor="Red" Font-Size="Smaller"></asp:Label>
                      
                </ContentTemplate>
            </asp:UpdatePanel>

        <asp:HiddenField ID="hdfConfiguration" runat="server" />
        <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" DropShadow="True" ID="mpeConfiguration"
            runat="server" PopupControlID="pnlConfiguration" TargetControlID="hdfConfiguration" Enabled="True" />
        <asp:Panel runat="server" ID="pnlConfiguration" CssClass="modalBackgroundRestore" Style="display: none; height: 400px; width: 500px;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                 <div class="formModule">
                        <fieldset>
                        <legend>
                            <h2><asp:Literal ID="ltrConfigValor" runat="server" Text="">Configuracion de valor</asp:Literal></h2>
                        </legend> 
                        <div class="module">
                            <div class="labelStyle">
                                <asp:Label runat="server" Text="Código:" ID="lblCode" Style="margin-left: -5px;"/>
                            </div>
                            <asp:TextBox ID="txtCode" runat="server" MaxLength="50" CssClass="textBoxStyle" Style="margin-left: 35px; Width:300px;" />
                        </div>
                        <div class="module">
                            <div class="labelStyle">
                                <asp:Label runat="server" Text="Descripción:" ID="lblDescription" Style="margin-left: -5px;"/>
                            </div>
                            <asp:TextBox ID="txtDescription" runat="server" MaxLength="50" CssClass="textBoxStyle" Style="margin-left: 35px; Width:300px;" />
                        </div>
                        <div class="module">
                            <div class="labelStyle">
                                <asp:Label runat="server" Text="Period:" ID="lblPeriod" Style="margin-left: -5px;"/>
                            </div>
                            <asp:TextBox ID="txtPeriod" runat="server" MaxLength="255" CssClass="textBoxStyle" Style="margin-left: 35px; Width:300px;" />
                        </div>
                        <div class="module">
                            <div class="labelStyle">
                                <asp:Label runat="server" Text="Period Hours:" ID="lblPeriodHours" Style="margin-left: -5px;"/>
                            </div>
                            <asp:TextBox ID="txtPeriodHours" runat="server" MaxLength="255" CssClass="textBoxStyle" Style="margin-left: 35px; Width:300px;" />
                        </div>
                        <div class="module">
                            <asp:CheckBox ID="ChkCheckLote" runat="server" Text="Check Lote:" TextAlign="Left" />
                        </div>
                         <div class="module">
                        <div class="buttons" style="text-align: right; margin-right: 15px;">
                            <asp:Button ID="btnSaveConfiguration" CssClass="button ok" runat="server" Text="Grabar" OnClick="btnSaveConfiguration_OnClick" Width="75px"  />
                            <asp:Button ID="btnCancel" CssClass="button cancel" runat="server" Text="Cancelar" Width="75px" OnClick="btnCancel_OnClick"/>
                        </div>
                        </div>      
                    </fieldset>
                </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>

    </div>
    </form>
</body>
</html>
