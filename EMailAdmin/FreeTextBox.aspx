<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="FreeTextBox.aspx.cs" Inherits="EMailAdmin.FreeTextBox" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Prueba</title>
</head>
<body>
    <form id="form2" runat="server">
    <div>
        <FTB:FreeTextBox id="FreeTextBox1" runat="Server" />
    </div>
    </form>
</body>
</html>
