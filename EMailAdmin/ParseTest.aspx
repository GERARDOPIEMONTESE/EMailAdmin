<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParseTest.aspx.cs" Inherits="EMailAdmin.ParseTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblPais" Text="Pais: " runat="server" />
        <asp:TextBox runat="server" ID="txtPais"/>
        <asp:Label ID="lblVoucher" Text="Voucher: " runat="server" />
        <asp:TextBox ID="txtVoucher" runat="server"/>
        <asp:Button runat="server" ID="btnAceptar" OnClick="BtnAceptarOnClick" Text="Enviar"/>
        <asp:Button runat="server" ID="btnTestACNet" OnClick="BtnTestACNetOnClick" Text="Test ACNet"/>        
        <asp:Button runat="server" ID="btnReprocess" OnClick="BtnReprocessClick" Text="Reprocesar" />
    </div>
    </form>
</body>
</html>
