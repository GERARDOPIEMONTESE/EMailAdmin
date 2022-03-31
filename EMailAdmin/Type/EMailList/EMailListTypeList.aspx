<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EMailListTypeList.aspx.cs" Inherits="EMailAdmin.Type.EMailList.EMailListTypeList" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMailList Type List</title>
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
                <asp:Label runat="server" ID="lblName" Text="Nombre" CssClass="label" 
                    meta:resourcekey="lblNameResource1" />
                <asp:TextBox runat="server" ID="txtDescription" CssClass="textbox" 
                    meta:resourcekey="txtDescriptionResource1" />
                <asp:Button runat="server" ID="btnSearch" Text="Buscar" 
                    OnClick="BtnSearchOnClick" CssClass="button ok"  
                    meta:resourcekey="btnSearchResource1" />
                <asp:Button runat="server" ID="btnNew" Text="Nuevo" CssClass="button ok"  
                    OnClick="BtnNewOnClick" meta:resourcekey="btnNewResource1" />
            </div>
            <div class="gridList">
                <div class="gridFont">
                    <asp:GridView 
                        ID="grvEMailListType" 
                        runat="server" 
                        PageSize="20" 
                        AllowPaging="True"
                        GridLines="Vertical"                         
                        AutoGenerateColumns="False"
                        CssClass="tbl-generic m-center"
                        OnRowCommand="GrvEMailListTypeRowCommand"
                        OnRowDataBound="GrvEMailListTypeRowDataBound" 
                        meta:resourcekey="grvEMailListTypeResource1"> 
                        <AlternatingRowStyle CssClass="gridView_Alternative_Row_Data" />
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" 
                                meta:resourcekey="BoundFieldResource1"  />
                            <asp:BoundField DataField="Code" HeaderText="Code" 
                                meta:resourcekey="BoundFieldResource2" >
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Description" HeaderText="Description" 
                                meta:resourcekey="BoundFieldResource3">
                                <ItemStyle Width="700px" />
                            </asp:BoundField>
                            <asp:ButtonField ButtonType="Button" CommandName="Edit" Text="Editar"  ControlStyle-CssClass="cancel small"
                                HeaderText="Edit" meta:resourcekey="ButtonFieldResource1" >
                                <ItemStyle Width="50px" CssClass="gridView_Button"/>
                            </asp:ButtonField>
                        </Columns>
                        <EditRowStyle CssClass="gridView_Edit_Row" />
                        <EmptyDataTemplate>
                            <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                                Text="No hay tipos de listas de mails cargadas" 
                                meta:resourcekey="lblNoDataResource1" />
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="gridView_Row_Header" />
                        <PagerStyle CssClass="gridView_Pager_Style_Row" HorizontalAlign="Center" />
                        <RowStyle CssClass="gridView_RowStyle" />
                        <SelectedRowStyle CssClass="gridView_Selected_Row" />
                    </asp:GridView>
                </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
