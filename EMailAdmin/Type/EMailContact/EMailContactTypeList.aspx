<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EMailContactTypeList.aspx.cs" Inherits="EMailAdmin.Type.EMailContact.EMailContactTypeList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMailContact Type List</title>
    <link href="../../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/loading.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmMain" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"/>
        <div class="PaginaPortal">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" > 
                <ProgressTemplate> 
                    <div id="progressBackgroundFilter"></div> 
                    <div id="processMessage" align="center"><p>Loading...</p>
                        <img alt="Loading..." src="../../IMG/loading.gif"  /> 
                    </div> 
                </ProgressTemplate> 
            </asp:UpdateProgress>
            <asp:UpdatePanel runat="server" ID="Up">
            <ContentTemplate>
            <div class="buttonModule">
                <asp:Label runat="server" ID="lblName" Text="Nombre" CssClass="label" />
                <asp:TextBox runat="server" ID="txtDescription" CssClass="textbox" />
                <asp:Button runat="server" ID="btnSearch" Text="Buscar" 
                    OnClick="BtnSearchOnClick" CssClass="button ok"  />
                <asp:Button runat="server" ID="btnNew" Text="Nuevo" CssClass="button ok"  
                    OnClick="BtnNewOnClick" />
            </div>
            <div class="gridList">
                <div class="gridFont">
                    <asp:GridView 
                        ID="grvEMailContactType" 
                        runat="server" 
                        PageSize="20" 
                        AllowPaging="True"
                        GridLines="Vertical"                         
                        AutoGenerateColumns="False"
                        CssClass="tbl-generic m-center"
                        OnRowCommand="GrvEMailContactTypeRowCommand"
                        OnRowDataBound="GrvEMailContactTypeRowDataBound"
                        OnPageIndexChanging="GrvEMailContactTypePageIndexChange" >
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id"  />
                            <asp:BoundField DataField="Code" HeaderText="Code" ItemStyle-Width="100px" />
                            <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="700px"/>
                        </Columns>
                        <RowStyle CssClass="gridView_RowStyle" />
                        <PagerStyle CssClass="gridView_Pager_Style_Row" HorizontalAlign="Center" />
                        <SelectedRowStyle CssClass="gridView_Selected_Row" />
                        <HeaderStyle CssClass="gridView_Row_Header" />
                        <EditRowStyle CssClass="gridView_Edit_Row" />
                        <AlternatingRowStyle CssClass="gridView_Alternative_Row_Data" />
                        <EmptyDataTemplate>
                            <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                                Text="No hay tipos de contactos cargados" />
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
