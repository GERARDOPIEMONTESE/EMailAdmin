<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachmentAssociationsList.aspx.cs" Inherits="EMailAdmin.Associations.AttachmentAssociationsList" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Associations List</title>
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
            <div class="buttonModule">
                <asp:Label runat="server" ID="lblName" Text="Tipo" CssClass="label" 
                    meta:resourcekey="lblNameResource1" />
                <asp:TextBox runat="server" ID="txtName" CssClass="textbox" 
                    meta:resourcekey="txtNameResource1"/>
                <asp:Label runat="server" ID="lblType" Text="Tipo" CssClass="label" 
                    meta:resourcekey="lblTypeResource1" />
                <asp:DropDownList runat="server" ID="ddlType" CssClass="dropdown" 
                    DataTextField="Description" DataValueField="Id" 
                    meta:resourcekey="ddlTypeResource1" />
                <asp:Label runat="server" ID="lblEstrategy" Text="Estrategia" CssClass="label" 
                    meta:resourcekey="lblEstrategyResource1" />
                <asp:DropDownList runat="server" ID="ddlEstrategy" CssClass="dropdown" 
                    DataTextField="Description" DataValueField="Id" 
                    meta:resourcekey="ddlEstrategyResource1" />
                <asp:Button runat="server" ID="btnSearch" Text="Buscar" 
                    OnClick="btnSearch_OnClick" CssClass="button ok" 
                    meta:resourcekey="btnSearchResource1" />
                <asp:Button runat="server" ID="btnNew" Text="Nuevo" CssClass="button ok" Visible="False"
                    OnClick="btnNew_OnClick" meta:resourcekey="btnNewResource1" />
            </div>
            <div class="gridList">
                <div class="gridFont">
                    <asp:GridView 
                        ID="grvAttachment" 
                        runat="server" 
                        PageSize="15" 
                        AllowPaging="True"
                        GridLines="Vertical" 
                        AutoGenerateColumns="False"
                        PagerStyle-HorizontalAlign="Center"
                        OnRowCommand="GrvAttachmentRowCommand"
                        OnRowDataBound="GrvAttachmentRowDataBound"
                        OnPageIndexChanging="GrvAttachmentPageIndexChange" meta:resourcekey="grvAttachmentResource1" 
                        CssClass="tbl-generic m-center">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" 
                                meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="Name" HeaderText="Nombre" ItemStyle-Width="300" 
                                meta:resourcekey="BoundFieldResource2" >
                                <ItemStyle Width="300px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="AttachmentTypeDescripcion" 
                                HeaderText="AttachmentTypeDescripcion" ItemStyle-Width="225" 
                                meta:resourcekey="BoundFieldResource3" >
                                <ItemStyle Width="225px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="EstrategyDescription" HeaderText="EstrategyClass" 
                                ItemStyle-Width="225" meta:resourcekey="BoundFieldResource4" >
                                <ItemStyle Width="225px"></ItemStyle>
                            </asp:BoundField>
                            <asp:ButtonField ButtonType="Button" CommandName="Edit" Text="Editar"  ControlStyle-CssClass="cancel small"
                                ItemStyle-Width="60" meta:resourcekey="ButtonFieldResource1" >
                                <ItemStyle Width="50px" CssClass="gridView_Button"/>
                            </asp:ButtonField>
                        </Columns>
                        <RowStyle CssClass="GridView_Row_Data_Normal" />
                        <PagerStyle CssClass="GridView_Pager_Normal" />
                        <SelectedRowStyle CssClass="gridView_Selected_Row" />
                        <HeaderStyle CssClass="gridView_Row_Header" />
                        <EditRowStyle CssClass="gridView_Edit_Row" />
                        <EmptyDataTemplate>
                            <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                                Text="No hay adjuntos cargados" meta:resourcekey="lblNoDataResource1" />
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>