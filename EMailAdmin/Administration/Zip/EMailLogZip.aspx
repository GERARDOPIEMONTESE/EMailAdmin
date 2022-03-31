<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EMailLogZip.aspx.cs" Inherits="EMailAdmin.Administration.Zip.EMailLogZip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMail Log Zip</title>
    <link href="../../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/loading.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
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
            <div class="formModule">
                <div class="module">
                    <asp:Label runat="server" ID="lblPending" CssClass="label" />
                    <asp:Button runat="server" ID="btnProcess" OnClick="BtnProcessOnClick" 
                        CssClass="button cancel" Text="Process"/>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
