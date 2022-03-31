<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailAddressInformation.aspx.cs" Inherits="EMailAdmin.EmailAddress.EmailAddressInformation" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Signature Information</title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/loading.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmMain" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="PaginaPortal">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" > 
                <ProgressTemplate> 
                    <div id="progressBackgroundFilter"></div> 
                    <div id="processMessage" align="center"><p>Loading...</p>
                        <img alt="Loading..." src="../IMG/loading.gif"  /> 
                    </div> 
                </ProgressTemplate> 
            </asp:UpdateProgress>
            <div class="formModule">
                <fieldset>
                    <div class="module">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblName" Text="Name" CssClass="label" 
                                        meta:resourcekey="lblNameResource1"  />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="longTextbox" MaxLength="99" 
                                        meta:resourcekey="txtNameResource1" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" 
                                        ErrorMessage="   *" ValidationGroup="EmailAddress" 
                                        meta:resourcekey="rfvNameResource1" />
                                </td>
                                <td>
                                    <asp:CustomValidator runat="server" ID="ctmName" ErrorMessage="   *" 
                                    ValidationGroup="EmailAddress" Display="Dynamic" 
                                        OnServerValidate="CtmNameValidator" meta:resourcekey="ctmNameResource1" />    
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="module">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblEmail" Text="Email" 
                                        CssClass="label" meta:resourcekey="lblEmailResource1" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="longTextbox" 
                                        MaxLength="150" meta:resourcekey="txtEmailResource1" />
                                </td>
                                <td>
                                    <asp:RegularExpressionValidator ID="revEmail" ValidationGroup="EmailAddress"
                                    runat="server" ErrorMessage="   *" ControlToValidate="txtEMail"
                                    
                                        ValidationExpression="^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$" 
                                        meta:resourcekey="revEmailResource1"/>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvEmail" ControlToValidate="txtEmail" 
                                        ErrorMessage="   *" ValidationGroup="EmailAddress" 
                                        meta:resourcekey="rfvEmailResource1" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
                <div class="module">
                    <asp:Button runat="server" ID="btnAccept" Text="Aceptar" CssClass="button ok" 
                        OnClick="BtnAcceptOnClick" ValidationGroup="EmailAddress" 
                        meta:resourcekey="btnAcceptResource1"   />
                    <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="button cancel" 
                        OnClick="BtnCancelOnClick" meta:resourcekey="btnCancelResource1"/>
                </div>
            </div>
        </div>
    </form>
</body>
</html>