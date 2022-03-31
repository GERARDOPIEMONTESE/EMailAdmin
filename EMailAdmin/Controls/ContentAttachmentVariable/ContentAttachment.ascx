<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContentAttachment.ascx.cs" Inherits="EMailAdmin.Controls.ContentAttachmentVariable.ContentAttachment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="trt" TagName="TabRichTextControl" Src="~/Controls/TabRich/TabRichTextControl.ascx" %>
<%@ Register TagPrefix="vtx" TagName="VariableTextSelector" Src="~/Controls/VariableText/VariableTextSelector.ascx" %>

<%@ Register src="../Selectors/VariableText/VariableTextSelector.ascx" tagname="VariableTextSelector" tagprefix="uc1" %>

<div>
    <fieldset>
        <asp:UpdatePanel ID="updContentAttachment" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdfIdTemplate" runat="server" />
                <asp:HiddenField ID="hdfIdAttachment" runat="server" />
                <div class="module">
                    <asp:Label ID="lblAttachment" runat="server" Text="Attachment" CssClass="label" 
                        meta:resourcekey="lblAttachmentResource1" />
                    <asp:Label ID="lblAttachmentName" runat="server" CssClass="title"></asp:Label>
                </div>
                <div class="module">
                    <asp:Label ID="lblTypeContent" runat="server" Text="Type" CssClass="label" 
                        meta:resourcekey="lblTypeContentResource1" />
                    <asp:DropDownList ID="ddlType" runat="server"></asp:DropDownList>
                </div>
                <div class="module">
                    <table>
                        <tr>
                            <td>
                                <trt:TabRichTextControl runat="server" ID="trtDescription" />            
                            </td>
                            <td>
                                <asp:CustomValidator runat="server" ID="ctmTabsValidator" ErrorMessage="   *" 
                                Display="Dynamic" OnServerValidate="CtmTabsValidator" ValidationGroup="UpgradeVariableText" 
                                meta:resourcekey="ctmTabsValidatorResource1"/>
                            </td>
                            <td>
                                <asp:Panel runat="server" ID="pnlCAVariableText">
                                    <uc1:VariableTextSelector ID="ctrlVariableTextSelector" runat="server" Visible="false" OnVariableTextUploadedCompleted="VariableTextCompleted" OnVariableTextCanceled="VariableTextCanceled" />
                    
                                    <asp:Button runat="server" ID="btnVariableTextSel" Text="Variable text" CssClass="button" 
                                        OnClick="VariableTextUploadButtonPressed" 
                                        meta:resourcekey="btnVariableTextSelResource1" />
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="module">
                    <asp:Button runat="server" ID="btnClose" Text="Cerrar" CssClass="button cancel" 
                        OnClick="BtnCloseOnClick" meta:resourcekey="btnCloseResource1" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
</div>