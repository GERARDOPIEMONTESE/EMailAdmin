<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListaDePixeles.aspx.cs" Inherits="EMailAdmin.Pixels.ListaDePixeles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../CSS/default.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>

           <div class="buttonModule">
                <asp:Label runat="server" ID="lblName" Text="Name: " CssClass="label" />
                <asp:TextBox runat="server" ID="txtName" CssClass="textbox" />
                <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_OnClick" CssClass="Button_Normal"  />
                <asp:Button runat="server" ID="btnNew" Text="New" CssClass="Button_Normal" OnClick="btnNew_OnClick" />
            </div>
            <br />
        <asp:GridView ID="grvPixels" runat="server" HorizontalAlign="Center" AutoGenerateColumns="false" DataKeyNames="Id" OnRowCommand="OnRowCommand_EditItem"
        PagerStyle-CssClass="GridView_Pager_Normal" BorderWidth="0px" HeaderStyle-CssClass="GridView_Row_Header_Normal"
        RowStyle-CssClass="GridView_Row_Data_Normal" CssClass="tbl-generic">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                <asp:BoundField DataField="Name" HeaderText="Name"/>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Edit" CssClass="EditarGridButton2"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>    

    </div>
    </form>
</body>
</html>