<%@ Control Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="RichTextControl.ascx.cs" Inherits="EMailAdmin.Controls.TabRich.RichTextControl" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<div id="TabItems">
    <asp:HiddenField ID="hfdLanguage" runat="server" Value="0"/>
    <FTB:FreeTextBox id="txtRichText" runat="Server" Width="575px" Height="275px" />
</div>