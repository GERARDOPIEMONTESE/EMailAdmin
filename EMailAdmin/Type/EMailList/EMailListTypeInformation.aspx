﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EMailListTypeInformation.aspx.cs" Inherits="EMailAdmin.Type.EMailList.EMailListTypeInformation" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMailList Information</title>
    <link href="../../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/loading.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmMain" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
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
                <fieldset>
                    <div class="module">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblCode" Text="Code" CssClass="label" 
                                        meta:resourcekey="lblCodeResource1"/>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCode" runat="server" CssClass="longTextbox" MaxLength="10" 
                                        meta:resourcekey="txtCodeResource1"/>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvCode" ControlToValidate="txtCode" 
                                        ErrorMessage="   *" ValidationGroup="EMailListType" 
                                        meta:resourcekey="rfvCodeResource1"/>
                                </td>
                                <td>
                                    <asp:CustomValidator runat="server" ID="ctmCode" ErrorMessage="   *" 
                                    ValidationGroup="EMailListType" Display="Dynamic" 
                                        OnServerValidate="CtmCodeValidator" meta:resourcekey="ctmCodeResource1"/>    
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="module">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblDescription" Text="Description" 
                                        CssClass="label" meta:resourcekey="lblDescriptionResource1" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="longTextbox" 
                                        MaxLength="30" meta:resourcekey="txtDescriptionResource1"/>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvDescription" ControlToValidate="txtDescription" 
                                        ErrorMessage="   *" ValidationGroup="EMailListType" 
                                        meta:resourcekey="rfvDescriptionResource1" />
                                </td>                                
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan=2>
                                    <div class="buttonModule" runat="server" id="divMensaje">
                                        <asp:Literal ID="litMensaje" runat="server"  meta:resourcekey="litMensajeResource1"></asp:Literal>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
                <div class="module">
                    <asp:Button runat="server" ID="btnAccept" Text="Aceptar" CssClass="button ok" 
                        OnClick="BtnAcceptOnClick" ValidationGroup="EMailListType" 
                        meta:resourcekey="btnAcceptResource1" />
                    <asp:Button runat="server" ID="btnDelete" Text="Eliminar" CssClass="button delete" 
                        OnClick="BtnAcceptDeleteOnClick" meta:resourcekey="btnDeleteResource1" />
                    <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="button cancel" 
                        OnClick="BtnCancelOnClick" meta:resourcekey="btnCancelResource1" />
                    
                </div>
            </div>
        </div>
    </form>
</body>