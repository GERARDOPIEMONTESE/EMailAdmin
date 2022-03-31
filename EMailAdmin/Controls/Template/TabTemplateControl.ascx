<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabTemplateControl.ascx.cs" Inherits="EMailAdmin.Controls.Template.TabTemplateControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>

<table>
    <tr>
        <td>
            <ajx:TabContainer ID="tbcTemplate" runat="server" Style="text-align: left" 
            Width="750px" />            
        </td>
        <td>
            <asp:CustomValidator runat="server" ID="ctmTabsValidator" ErrorMessage="   *" ForeColor="Red" 
            Display="Dynamic" OnServerValidate="CtmTabsValidator" 
                ValidationGroup="Template" meta:resourcekey="ctmTabsValidatorResource1" />            
        </td>
    </tr>
</table>