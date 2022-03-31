<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EdicionDeLinks.aspx.cs" Inherits="EMailAdmin.Links.EdicionDeLinks" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            <fieldset style="width:800px">
                <legend class="TextLegend_Normal">
                    <asp:Label ID="lbTitulo" runat="server" Text="Link Edit" ForeColor="#4d6185"
                        Font-Bold="true" CssClass="TextBox_Titulo" meta:resourcekey="lbTitulo1"></asp:Label>
                </legend>

                <div class="module">
                    <div class="labelStyle">
                    <asp:Label ID="lbLinkType" runat="server" Text="Link Type: " CssClass="label" ></asp:Label>
                        </div>
                    <asp:DropDownList ID="ddlLinkType" runat="server" DataValueField="Codigo" DataTextField="Descripcion" 
                    Width="200px" CssClass="DropDownList_X" Enabled="true">
                    </asp:DropDownList>
                </div>              
                
                <div class="module">
                    <div class="labelStyle">
                    <asp:Label ID="lbName" runat="server" Text="Nombre: " CssClass="label" ></asp:Label>
                    </div>
                    <asp:TextBox ID="txName" runat="server" MaxLength="50" Width="400px" CssClass="TextBox_M"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txName" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                </div>

                <div class="module">
                    <asp:Label ID="lbUrl" runat="server" Text="Url: " CssClass="label"></asp:Label>
                    <asp:TextBox ID="txUrl" runat="server" CssClass="TextBox_M" Width="600px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUrl" ControlToValidate="txUrl" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                </div>

                 <div class="module">
                    <asp:Label ID="lblDisplayText_ES" runat="server" Text="Texto en español: " CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtDisplayText_ES" runat="server" Width="600px" MaxLength="255" CssClass="TextBox_M"></asp:TextBox>                    
                </div>

                <div class="module">
                    <asp:Label ID="lblDisplayText_EN" runat="server" Text="Texto en inglés: " CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtDisplayText_EN" runat="server" Width="600px" MaxLength="255" CssClass="TextBox_M"></asp:TextBox>                    
                </div>

                <div class="module">
                    <asp:Label ID="lblDisplayText_PT" runat="server" Text="Texto en portugués: " CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtDisplayText_PT" runat="server" Width="600px" MaxLength="255" CssClass="TextBox_M"></asp:TextBox>                    
                </div>

                <div class="module">
                    <asp:Label ID="lblStyle" runat="server" Text="Estilo CSS: " CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtStyle" runat="server" Width="600px" CssClass="TextBox_M" 
                        Height="70px" Rows="5" TextMode="MultiLine"></asp:TextBox>                    
                </div>

                <div class="module">
                    <asp:Label ID="lblImageName" runat="server" Text="Imagen: " CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtImageName" runat="server" Width="600px" MaxLength="255" CssClass="TextBox_M"></asp:TextBox>                    
                </div>

                 <div class="module">
                    <div class="labelStyle">
                        <asp:Label ID="lbEnabledDeepLink" runat="server" Text="Convertir a DeepLink: " CssClass="label" ></asp:Label>
                    </div>
                    <asp:CheckBox ID="chkEnabledDeepLink" runat="server" Checked="false" />
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
