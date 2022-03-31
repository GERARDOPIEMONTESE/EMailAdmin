<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulkLoad.aspx.cs" Inherits="EMailAdmin.Associations.BulkLoad" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit"  TagPrefix="ajx"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Bulk Load</title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/loading.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
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
    <br/>
    <div class="formModule">
        <fieldset>
            <div class="module">
                <asp:Label runat="server" ID="lblDescription" Text="Seleccione el archivo" 
                    meta:resourcekey="lblDescriptionResource1"/>
                <asp:FileUpload runat="server" ID="fupBulk" CssClass="button" 
                    meta:resourcekey="fupBulkResource1" />
                <asp:RequiredFieldValidator ID="rfvValid" runat="server" Display="Dynamic" ControlToValidate="fupBulk"
                        ErrorMessage="*" CssClass="label"  ValidationGroup="Bulk" 
                    meta:resourcekey="rfvValidResource1" />
            </div>
            <div class="module">
                <asp:Button runat="server" ID="btnAccept" Text="Load" 
                    OnClick="BtnAcceptOnClick" CssClass="button ok" ValidationGroup="Bulk" 
                    meta:resourcekey="btnAcceptResource1" />
                <asp:Button runat="server" ID="btnInstrucctions" Text="VER INSTRUCTIVO" 
                    CssClass="button cancel" meta:resourcekey="btnInstrucctionsResource1" />
            </div>
        </fieldset>
    </div>
    <div class="formModule">
        <div class="module">
            <asp:Panel ID="pnlInstructions" runat="server" CssClass="modalBackgroundRestore" 
                Style="display: none;" Width="800px" meta:resourcekey="pnlInstructionsResource1">
                <table>
                    <tr>
                        <td>
                            <fieldset style="width: 780px;">
                                <table width="760px;">
                                    <tr>
                                        <td>
                                            <asp:Label CssClass="labelTitle" ID="lblTitle" runat="server" 
                                                Text="Instructivo" meta:resourcekey="lblTitleResource1"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblInstructions" runat="server" Width="745px" 
                                               meta:resourcekey="lblInstructionsResource1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Button ID="btnAcceptInstructions" runat="server" CssClass="button ok" 
                                                Text="Aceptar" meta:resourcekey="btnAcceptInstructionsResource1" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajx:ModalPopupExtender BackgroundCssClass="modalBackground"
             ID="mpeInstructions" CancelControlID="btnAcceptInstructions"
            PopupControlID="pnlInstructions" RepositionMode="None" runat ="server" 
            TargetControlID="btnInstrucctions" DynamicServicePath="" Enabled="True" />
        </div>
    </div>
    <div class="formModule">
        <div class="module">
            <asp:Panel runat="server" ID="pnlOk"  CssClass="modalBackgroundRestore" 
                Style="display: none;" meta:resourcekey="pnlOkResource1">
                <fieldset>
                    <table>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblOk" CssClass="label" runat="server"
                                    Text="Se han insertado todas las asociaciones en forma correcta." 
                                    meta:resourcekey="lblOkResource1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="btnAcceptOk" Text="Accept" CssClass="button ok"  
                                    meta:resourcekey="btnAcceptOkResource1"/>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hfdOk"/>
            <ajx:ModalPopupExtender BackgroundCssClass="modalBackground"
             ID="mpeOk" CancelControlID="btnAcceptOk"
            PopupControlID="pnlOk" RepositionMode="None" runat ="server" 
            TargetControlID="hfdOk" DynamicServicePath="" Enabled="True" />
        </div>
    </div>
    <div class="formModule">
        <div class="module">
            <asp:Panel runat="server" ID="pnlErrors"  CssClass="modalBackgroundRestore" 
                Style="display: none;">
                <fieldset>
                    <table>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblErrorTitle" CssClass="labelTitle" Text="Errors" meta:resourcekey="lblErrorTitleResource"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel runat="server" ID="uplErrors" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <fieldset style="width: 350px">
                                            <asp:GridView 
                                            ID="grvErrors" 
                                            runat="server" 
                                            GridLines="Vertical" 
                                            AutoGenerateColumns="False"
                                            CssClass="tbl-generic m-center"
                                            PagerStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:BoundField DataField="LineNumber" HeaderText="LineNumber" ItemStyle-Width="150" meta:resourcekey="LineNumber"/>
                                                    <asp:BoundField DataField="ErrorToShow" HeaderText="ErrorToShow" ItemStyle-Width="250" meta:resourcekey="Errors"/>
                                                </Columns>
                                                <RowStyle CssClass="gridView_RowStyle" />
                                                <PagerStyle CssClass="gridView_Pager_Style_Row" />
                                                <SelectedRowStyle CssClass="gridView_Selected_Row" />
                                                <HeaderStyle CssClass="gridView_Row_Header" />
                                                <EditRowStyle CssClass="gridView_Edit_Row" />
                                                <AlternatingRowStyle CssClass="gridView_Alternative_Row_Data" />
                                            </asp:GridView>
                                        </fieldset>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="btnAcceptErrors" Text="Accept" CssClass="button ok" 
                                    meta:resourcekey="btnAcceptOkResource1"/>        
                            </td>                            
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hfdErrors"/>
            <ajx:ModalPopupExtender BackgroundCssClass="modalBackground"
             ID="mpeErrors" CancelControlID="btnAcceptErrors"
            PopupControlID="pnlErrors" RepositionMode="None" runat ="server" 
            TargetControlID="hfdErrors" DynamicServicePath="" Enabled="True" />
        </div>
    </div>
    </div>
    </form>
</body>
</html>
