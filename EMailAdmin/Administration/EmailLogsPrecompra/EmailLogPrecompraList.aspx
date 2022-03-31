<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailLogPrecompraList.aspx.cs" Inherits="EMailAdmin.Administration.EmailLogsPrecompra.EmailLogPrecompraList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMail Log Prepurchase List</title>
    <link href="../../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/loading.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True" 
            EnableScriptGlobalization="True"/>
        <div class="PaginaPortal">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" > 
                <ProgressTemplate> 
                    <div id="progressBackgroundFilter"></div> 
                    <div id="processMessage" align="center"><p>Loading...</p>
                        <img alt="Loading..." src="../../IMG/loading.gif"  /> 
                    </div> 
                </ProgressTemplate> 
            </asp:UpdateProgress>
            <div class="buttonModule">
                <asp:Label runat="server" ID="lblCodigoVerif" Text="Codigo de Verificacion:" CssClass="label"/>                
                <asp:TextBox ID="txtCodigoVerif" runat="server"></asp:TextBox>
                <asp:Label runat="server" ID="Label1" Text="Codigo Precompra:" CssClass="label"/>                
                <asp:TextBox ID="txtCodPaxBox" runat="server"></asp:TextBox>
                <asp:Label runat="server" ID="lblVoucherGroup" Text="Voucher group:" CssClass="label"/>                
                <asp:TextBox ID="txtVoucherGroup" runat="server"></asp:TextBox>
                <asp:Label runat="server" ID="lblPais" Text="Pais:" CssClass="label"/>                
                <asp:DropDownList ID="ddlPais" runat="server" DataTextField="Nombre" DataValueField="Codigo"></asp:DropDownList>
                <asp:Button runat="server" ID="btnSearch" Text="Search" 
                    OnClick="BtnSearchOnClick" CssClass="button ok"/>                
            </div>
            <div>
                <asp:GridView ID="grvLogs" runat="server" AutoGenerateColumns="False"
                    PageSize="15" 
                    AllowPaging="True"
                    GridLines="Vertical" 
                    PagerStyle-HorizontalAlign="Center"
                    CssClass="tbl-generic m-center" onrowdatabound="grvLogs_RowDataBound" 
                    onpageindexchanging="grvLogs_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="TemplateName" HeaderText="Tipo" />
                        <asp:BoundField DataField="MailTo" HeaderText="MailTo" />
                        <asp:BoundField DataField="StartDate" HeaderText="Fecha" />
                        <asp:BoundField DataField="CodigoPaxBox" HeaderText="CodigoPaxBox" />
                        <asp:BoundField DataField="CodigoVerif" HeaderText="CodigoVerif" />
                        <asp:BoundField DataField="VoucherGroup" HeaderText="VoucherGroup" />
                        <asp:BoundField HeaderText="Estado" />
                        <asp:BoundField DataField="PaisNombre" HeaderText="Pais" />
                    </Columns>
                    <RowStyle CssClass="GridView_Row_Data_Normal" />
                    <PagerStyle CssClass="GridView_Pager_Normal" />
                    <SelectedRowStyle CssClass="gridView_Selected_Row" />
                    <HeaderStyle CssClass="gridView_Row_Header" />
                    <EditRowStyle CssClass="gridView_Edit_Row" />
                    <EmptyDataTemplate>
                        <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                            Text="No logs available."/>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>        
    </form>
</body>
</html>
