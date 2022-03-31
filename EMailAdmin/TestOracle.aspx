<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestOracle.aspx.cs" Inherits="EMailAdmin.TestOracle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Pais<asp:TextBox ID="txtPais" runat="server"></asp:TextBox>
        Voucher<asp:TextBox ID="txtDato" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Buscar voucher" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Buscar tarifa" />
    </div>
        resultado<asp:TextBox ID="txtRst" runat="server" Columns="20" Rows="20" TextMode="MultiLine" ></asp:TextBox>        
    </form>
</body>
</html>
