<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConditionVariableTextList.aspx.cs" Inherits="EMailAdmin.ConditionVariableText.ConditionVariableTextList" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UpgradeVariableText List</title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/loading.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmMain" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"/>
        <div class="PaginaPortal">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" > 
                <%--<ProgressTemplate> 
                    <div id="progressBackgroundFilter"></div> 
                    <div id="processMessage" align="center"><p>Loading...</p>
                        <img alt="Loading..." src="../IMG/loading.gif"  /> 
                    </div> 
                </ProgressTemplate>--%> 
            </asp:UpdateProgress>
            <asp:UpdatePanel runat="server" ID="Up">
            <ContentTemplate>
            <div class="buttonModule">
                <asp:Label runat="server" ID="lblName" Text="Nombre" CssClass="label" 
                    meta:resourcekey="lblNameResource1" />
                <asp:TextBox runat="server" ID="txtName" CssClass="textbox" 
                    meta:resourcekey="txtNameResource1" />
                <asp:Label runat="server" ID="lblVariableText" Text="VariableText" 
                    CssClass="label" meta:resourcekey="lblVariableTextResource1" />
                <asp:DropDownList runat="server" ID="ddlVariableText" CssClass="dropdown" 
                    DataTextField="Name" DataValueField="Id" 
                    meta:resourcekey="ddlVariableTextResource1" />
                <asp:Label runat="server" ID="lblCondicion" Text="Condicion" CssClass="label" />
                <asp:TextBox runat="server" ID="txtCondicion" CssClass="textbox" />
                <asp:Button runat="server" ID="btnSearch" Text="Buscar" 
                    OnClick="btnSearch_OnClick" CssClass="button ok" 
                    meta:resourcekey="btnSearchResource1"/>
                <asp:Button runat="server" ID="btnNew" Text="Nuevo" CssClass="button ok"  
                    OnClick="btnNew_OnClick" meta:resourcekey="btnNewResource1"/>
            </div>
            <div class="gridList">
                <div class="gridFont">
                    <asp:GridView 
                        ID="grvConditionVariableText" 
                        runat="server" 
                        PageSize="15" 
                        AllowPaging="True"
                        GridLines="Vertical" 
                        AutoGenerateColumns="False"
                        CssClass="tbl-generic m-center"
                        OnRowCommand="GrvConditionVariableTextRowCommand"
                        OnRowDataBound="GrvConditionVariableTextRowDataBound"
                        OnPageIndexChanging="GrvConditionVariableTextPageIndexChange" 
                        meta:resourcekey="grvConditionVariableTextResource1">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" 
                                meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="Name" HeaderText="Nombre" 
                                meta:resourcekey="BoundFieldResource2">
                                <ItemStyle Width="250px" />
                            </asp:BoundField>
                            <asp:ButtonField ButtonType="Button" CommandName="Edit" Text="Editar"
                                HeaderText="Edit" meta:resourcekey="ButtonFieldResource1">
                                <ControlStyle CssClass="cancel small" />
                                <ItemStyle Width="50px" CssClass="gridView_Button"/>
                            </asp:ButtonField>
                        </Columns>
                        <RowStyle CssClass="gridView_RowStyle" />
                        <PagerStyle CssClass="gridView_Pager_Style_Row" HorizontalAlign="Center" />
                        <SelectedRowStyle CssClass="gridView_Selected_Row" />
                        <HeaderStyle CssClass="gridView_Row_Header" />
                        <EditRowStyle CssClass="gridView_Edit_Row" />
                        <AlternatingRowStyle CssClass="gridView_Alternative_Row_Data" />
                        <EmptyDataTemplate>
                            <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                                Text="No hay firmas cargadas" meta:resourcekey="lblNoDataResource1"/>
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
