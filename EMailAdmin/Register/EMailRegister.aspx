<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EMailRegister.aspx.cs" Inherits="EMailAdmin.Register.EMailRegister" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assist-Card</title>
    <link href="/CSS/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <%--<div style="text-align:center;padding-top:20px;">
            <asp:Label runat="server" ID="lblConfirmation" 
                Text="Confirmación recibida. Muchas gracias!" CssClass="text_Title" 
                meta:resourcekey="lblConfirmationResource1"></asp:Label>    
        </div>--%>
        <div class="page-thanks modal-bkg form-thanks">
            <img src="/IMG/logo-small.jpg" alt="small logo" class="small-logo"/>
            <img src="/IMG/firulete.jpg" alt="firulete" class="firulete"  />
            <h3>
                <asp:Literal ID="ltlTahnksTitle" runat="server" Text="Muchas Gracias!" meta:resourcekey="ltlTahnksTitleResource1"></asp:Literal>
            </h3>
            <p>
                <asp:Literal ID="ltlTahnksParagraph" runat="server" Text="Su confirmación fue recibida correctamente" 
                    meta:resourcekey="ltlTahnksParagraphResource1"></asp:Literal>
            </p>
        </div>
    </form>
</body>
</html>
