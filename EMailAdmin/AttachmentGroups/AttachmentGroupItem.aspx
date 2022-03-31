<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachmentGroupItem.aspx.cs" Inherits="EMailAdmin.AttachmentGroups.AttachmentGroupItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

    <div class="PaginaPortal">

    <table class="m-center"><tr><td>
        <div id="Formulario" class="formModule">
            <fieldset style="width:800px">
                <legend class="TextLegend_Normal">
                    <asp:Label ID="lbTitulo" runat="server" Text="Attachment Group Edit" ForeColor="#4d6185"
                        Font-Bold="true" CssClass="TextBox_Titulo" meta:resourcekey="lbTitulo1"></asp:Label>
                </legend>                
                <div class="module">                    
                    <asp:Label ID="lbName" runat="server" Text="Nombre: " CssClass="label" ></asp:Label>
                    <asp:TextBox ID="txName" runat="server" MaxLength="50" Width="400px" CssClass="TextBox_M"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txName" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                </div>
                 <div class="module">
                    <asp:Label ID="lblDisplayText_ES" runat="server" Text="Texto en español: " CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtDisplayText_ES" runat="server" Width="600px" MaxLength="255" CssClass="TextBox_M"></asp:TextBox>                    
                    <asp:RequiredFieldValidator ID="rfvDisplayText_ES" ControlToValidate="txtDisplayText_ES" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                </div>
                <div class="module">
                    <asp:Label ID="lblDisplayText_EN" runat="server" Text="Texto en inglés: " CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtDisplayText_EN" runat="server" Width="600px" MaxLength="255" CssClass="TextBox_M"></asp:TextBox>                    
                    <asp:RequiredFieldValidator ID="rfvDisplayText_EN" ControlToValidate="txtDisplayText_EN" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                </div>
                <div class="module">
                    <asp:Label ID="lblDisplayText_PT" runat="server" Text="Texto en portugués: " CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtDisplayText_PT" runat="server" Width="600px" MaxLength="255" CssClass="TextBox_M"></asp:TextBox>                    
                    <asp:RequiredFieldValidator ID="rfvDisplayText_PT" ControlToValidate="txtDisplayText_PT" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                </div>
                <div class="module">
                    <asp:Label ID="lblAttachOrder" runat="server" Text="Orden: " CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtAttachOrder" runat="server" Width="60px" MaxLength="3" CssClass="TextBox_S"></asp:TextBox>                    
                </div>
                <div id="divInUseTemplates" class="module" runat="server">
                    <asp:Label ID="lblInUseTemplates" runat="server" Text="Templates asociados:" CssClass="label"></asp:Label>
                    <asp:Label ID="lblInUseTemplatesName" runat="server" Text="" CssClass="label"></asp:Label>
                </div>
            </fieldset>
         </div>

         <div id="Botones" class="formModule">
            <asp:Button ID="btnInsertSave" runat="server" onclick="btnInsertSave_Click" CssClass="Button_Normal" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" onclick="btnDelete_Click"  CssClass="Button_Normal"/>
            <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" CssClass="Button_Normal" OnClientClick="Disable();" />
         </div>
        </td></tr>
    </table>

     </div>
    </form>
</body>
</html>
