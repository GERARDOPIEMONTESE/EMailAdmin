<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateList.aspx.cs" Inherits="EMailAdmin.Template.TemplateList" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Template List</title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/loading.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmMain" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" EnableViewState="true"/>
        <div class="PaginaPortal">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" > 
                <ProgressTemplate> 
                    <div id="progressBackgroundFilter"></div> 
                    <div id="processMessage" align="center"><p>Loading...</p>
                        <img alt="Loading..." src="../IMG/loading.gif"  /> 
                    </div> 
                </ProgressTemplate> 
            </asp:UpdateProgress>
            <asp:UpdatePanel runat="server" ID="Up">
            <ContentTemplate>
            <div class="buttonModule">
                <asp:Label runat="server" ID="lblName" Text="Nombre" CssClass="label" 
                    meta:resourcekey="lblNameResource1" />
                <asp:TextBox runat="server" ID="txtName" CssClass="textbox" 
                    meta:resourcekey="txtNameResource1"/>
                <asp:Label runat="server" ID="lblType" Text="Tipo" CssClass="label" 
                    meta:resourcekey="lblTypeResource1"/>
                <asp:DropDownList runat="server" ID="ddlType" CssClass="dropdown" 
                    DataTextField="Descripcion" DataValueField="Id" 
                    meta:resourcekey="ddlTypeResource1" />
                <asp:Button runat="server" ID="btnSearch" Text="Buscar" 
                    OnClick="btnSearch_OnClick" CssClass="button ok"  
                    meta:resourcekey="btnSearchResource1" />
                <asp:Button runat="server" ID="btnNew" Text="Nuevo" CssClass="button ok"  
                    OnClick="btnNew_OnClick" meta:resourcekey="btnNewResource1" />
            </div>
            <div class="gridList">
                <div class="gridFont">
                    <asp:GridView 
                        ID="grvTemplate" 
                        runat="server" 
                        PageSize="30" 
                        AllowPaging="True"
                        GridLines="Vertical" 
                        AutoGenerateColumns="False"
                        CssClass="tbl-generic m-center"
                        PagerStyle-HorizontalAlign="Center"
                        OnRowCommand="GrvTemplateRowCommand"
                        OnRowDataBound="GrvTemplateRowDataBound"
                        OnPageIndexChanging="GrvTemplatePageIndexChange" meta:resourcekey="grvTemplateResource1" 
                        >
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" 
                                meta:resourcekey="BoundFieldResource1"  />
                            <asp:BoundField DataField="Name" HeaderText="Nombre" ItemStyle-Width="200" 
                                meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField DataField="TemplateTypeDescription" HeaderText="TemplateTypeDescription" ItemStyle-Width="200" 
                                meta:resourcekey="BoundFieldResource3" />
                                <asp:BoundField DataField="EffectiveStartDateDescription" HeaderText="EffectiveStartDateDescription" ItemStyle-Width="150" 
                                meta:resourcekey="BoundFieldResource4" />
                                <asp:BoundField DataField="EffectiveEndDateDescription" HeaderText="EffectiveEndDateDescription" ItemStyle-Width="150" 
                                meta:resourcekey="BoundFieldResource5" />
                                <asp:BoundField DataField="HierarchyDescription" HeaderText="HierarchyDescription" ItemStyle-Width="50" 
                                meta:resourcekey="BoundFieldResource6" />
                            <asp:ButtonField ButtonType="Button" CommandName="Edit" Text="Editar"  ControlStyle-CssClass="cancel small"
                                HeaderText="Edit" ItemStyle-Width="60" 
                                meta:resourcekey="ButtonFieldResource1" >
                                <ItemStyle Width="50px" CssClass="gridView_Button"/>
                            </asp:ButtonField>
                            <asp:ButtonField ButtonType="Button" CommandName="Copy" Text="Copiar"  ControlStyle-CssClass="cancel small"
                                HeaderText="" ItemStyle-Width="60" 
                                meta:resourcekey="ButtonFieldResource7" >
                                <ItemStyle Width="50px" CssClass="gridView_Button"/>
                            </asp:ButtonField>
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
                </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
