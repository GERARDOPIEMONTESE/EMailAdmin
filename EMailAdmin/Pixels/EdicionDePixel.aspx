<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EdicionDePixel.aspx.cs" Inherits="EMailAdmin.Pixels.EdicionDePixel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/default.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function Disable() {
            var validator = document.getElementById('rfvName');
            ValidatorEnable(validator, false);
            var validator = document.getElementById('rfvUrl');
            ValidatorEnable(validator, false);
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">

    <div class="PaginaPortal">

    <table class="m-center"><tr><td>
        <div id="Formulario" class="formModule">
            <fieldset style="width:400px;" >
                <legend class="TextLegend_Normal">
                    <asp:Label ID="lbTitulo" runat="server" Text="Pixel Edit" ForeColor="#4d6185"
                        Font-Bold="true" CssClass="TextBox_Titulo" meta:resourcekey="lbTitulo1"></asp:Label>
                </legend>

                <div class="module">
                    
                    <asp:Label ID="lbName" runat="server" Text="Name: " CssClass="label" ></asp:Label>
                    <asp:TextBox ID="txName" runat="server" Width="300px" CssClass="TextBox_M"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txName" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                </div>

                <div class="module">
                    <asp:Label ID="lbUrl" runat="server" Text="Url: " CssClass="label"></asp:Label>
                    <asp:TextBox ID="txUrl" runat="server" Width="300px" CssClass="TextBox_M"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUrl" ControlToValidate="txUrl" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                </div>
                <div class="module">
                    <asp:Label ID="lblV" runat="server" Text="V: " CssClass="label" ToolTip="Version"></asp:Label>
                    <asp:TextBox ID="txtV" runat="server" Width="300px" CssClass="TextBox_M"></asp:TextBox>
                </div>
                <div class="module">
                    <asp:Label ID="lblT" runat="server" Text="T: " CssClass="label" ToolTip="Evento"></asp:Label>
                    <asp:TextBox ID="txtT" runat="server" Width="300px" CssClass="TextBox_M"></asp:TextBox>
                </div>
               <%-- <div class="module">
                    <asp:Label ID="lblTID" runat="server" Text="TID: " CssClass="label" ToolTip="Id de seguimiento de Universal Analytics"></asp:Label>
                    <asp:TextBox ID="txtTID" runat="server" Width="300px" CssClass="TextBox_M"></asp:TextBox>
                </div>
                <div class="module">
                    <asp:Label ID="lblCID" runat="server" Text="CID: " CssClass="label" ToolTip="Id de cliente"></asp:Label>
                    <asp:TextBox ID="txtCID" runat="server" Width="300px" CssClass="TextBox_M"></asp:TextBox>
                </div>--%>
                <div class="module">
                    <asp:Label ID="lblUID" runat="server" Text="UID: " CssClass="label" ToolTip="Id de usuario"></asp:Label>
                    <asp:TextBox ID="txtUID" runat="server" Width="300px" CssClass="TextBox_M"></asp:TextBox>
                </div>
                <div class="module">
                    <asp:Label ID="lblEC" runat="server" Text="EC: " CssClass="label" ToolTip="Categoria de evento"></asp:Label>
                    <asp:TextBox ID="txtEC" runat="server" Width="300px" CssClass="TextBox_M"></asp:TextBox>
                </div>
                <div class="module">
                    <asp:Label ID="lblEA" runat="server" Text="EA: " CssClass="label" ToolTip="Acción de evento"></asp:Label>
                    <asp:TextBox ID="txtEA" runat="server" Width="300px" CssClass="TextBox_M"></asp:TextBox>
                </div>
                <div class="module">
                    <asp:Label ID="lblEL" runat="server" Text="EL: " CssClass="label" ToolTip="Etiqueta del evento"></asp:Label>
                    <asp:TextBox ID="txtEL" runat="server" Width="300px" CssClass="TextBox_M"></asp:TextBox>
                </div>
                 <div class="module">
                    <asp:Label ID="lblCS" runat="server" Text="CS: " CssClass="label" ToolTip="Fuente de la campaña"></asp:Label>
                    <asp:TextBox ID="txtCS" runat="server" Width="300px" CssClass="TextBox_M"></asp:TextBox>
                </div>
                 <div class="module">
                    <asp:Label ID="lblCM" runat="server" Text="CM: " CssClass="label" ToolTip="Medio de la campaña"></asp:Label>
                    <asp:TextBox ID="txtCM" runat="server" Width="300px" CssClass="TextBox_M"></asp:TextBox>
                </div>
                 <div class="module">
                    <asp:Label ID="lblCN" runat="server" Text="CN: " CssClass="label" ToolTip="Nombre de la campaña"></asp:Label>
                    <asp:TextBox ID="txtCN" runat="server" Width="300px" CssClass="TextBox_M"></asp:TextBox>
                </div>
            </fieldset>
         </div>

         <div id="Botones" class="formModule">
            <asp:Button ID="btnInsertSave" runat="server" onclick="btnInsertSave_Click" CssClass="Button_Normal" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" onclick="btnDelete_Click"  CssClass="Button_Normal"/>
            <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" CssClass="Button_Normal" OnClientClick="Disable();" />
         </div>

          </td></tr></table>

     </div>
    </form>
</body>
</html>
