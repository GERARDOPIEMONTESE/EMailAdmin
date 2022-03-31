<%@ Control Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="TemplateControl.ascx.cs" Inherits="EMailAdmin.Controls.Template.TemplateControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<div class="formModule">
     <div class="module">
        <asp:Label runat="server" ID="lblSubject" Text="Subject" CssClass="label" 
             meta:resourcekey="lblSubjectResource1" />
        <asp:TextBox ID="txtSubject" runat="server" CssClass="longTextbox" MaxLength="250"
             meta:resourcekey="txtSubjectResource1" />
    </div>
    <div class="module">    
        <table>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblHeader" Text="Header" CssClass="label" 
                        meta:resourcekey="lblHeaderResource1" />
                </td>
                <td>
                    <ajx:AsyncFileUpload runat="server" OnUploadedComplete="HeaderLoaded" ID="fupHeader" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnDeleteHeader" OnClick="BtnDeleteHeaderOnClick" 
                            meta:resourcekey="btnDeleteHeaderResource1" CssClass="button delete small" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label runat="server" ID="lblHeaderName" CssClass="text_Normal"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblHeaderPDF" Text="Header Pdf" CssClass="label" 
                        meta:resourcekey="lblHeaderPDFResource1" />
                </td>
                <td>
                    <ajx:AsyncFileUpload runat="server" OnUploadedComplete="HeaderPDFLoaded" ID="fupHeaderPDF" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnDeleteHeaderPDF" OnClick="BtnDeleteHeaderPDFOnClick" 
                            meta:resourcekey="btnDeleteHeaderPDFResource1" CssClass="button delete small" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label runat="server" ID="lblHeaderPDFName" CssClass="text_Normal"/>
                    
                </td>
            </tr> 
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblColor" Text="Color" CssClass="label" 
                        meta:resourcekey="lblColorResource1" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtColor" CssClass="jscolor {onFineChange:'update(this)'}"
                        meta:resourcekey="txtNameResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblEVoucherName" Text="EVoucherName" CssClass="label" 
                        meta:resourcekey="lblEVoucherNameResource1" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtEVoucherName" 
                        meta:resourcekey="txtEVoucherNameResource1"></asp:TextBox>
                </td>
            </tr>
            <tr><td colspan="3">
                <asp:Label runat="server" ID="lblBody" Text="Body" CssClass="label" 
                        meta:resourcekey="lblBodyResource1" />
                <asp:HiddenField ID="hfdLanguage" runat="server" Value="0"/>            
            </td></tr>        
            <tr>
            <td colspan="5">                
                <FTB:FreeTextBox id="txtRichText" runat="Server" Width="570px" 
                    AllowHtmlMode="False" AssemblyResourceHandlerPath="" AutoConfigure="" 
                    AutoGenerateToolbarsFromString="True" AutoHideToolbar="True" 
                    AutoParseStyles="True" BackColor="158, 190, 245" BaseUrl="" 
                    BreakMode="Paragraph" ButtonDownImage="False" ButtonFileExtention="gif" 
                    ButtonFolder="Images" ButtonHeight="20" ButtonImagesLocation="InternalResource" 
                    ButtonOverImage="False" ButtonPath="" ButtonSet="Office2003" ButtonWidth="21" 
                    ClientSideTextChanged="" ConvertHtmlSymbolsToHtmlCodes="False" 
                    DesignModeBodyTagCssClass="" DesignModeCss="" DisableIEBackButton="False" 
                    DownLevelCols="50" DownLevelMessage="" DownLevelMode="TextArea" 
                    DownLevelRows="10" EditorBorderColorDark="128, 128, 128" 
                    EditorBorderColorLight="128, 128, 128" EnableHtmlMode="True" EnableSsl="False" 
                    EnableToolbars="True" Focus="False" FormatHtmlTagsToXhtml="True" 
                    GutterBackColor="129, 169, 226" GutterBorderColorDark="128, 128, 128" 
                    GutterBorderColorLight="255, 255, 255" Height="280px" HelperFilesParameters="" 
                    HelperFilesPath="" HtmlModeCss="" HtmlModeDefaultsToMonoSpaceFont="True" 
                    InstallationErrorMessage="InlineMessage" JavaScriptLocation="InternalResource" 
                    Language="en-US" PasteMode="Text" ReadOnly="False"
                    RemoveScriptNameFromBookmarks="True" RemoveServerNameFromUrls="True" 
                    RenderMode="NotSet" ScriptMode="External" ShowTagPath="False" SslUrl="/." 
                    StartMode="DesignMode" StripAllScripting="False" 
                    SupportFolder="/aspnet_client/FreeTextBox/" TabIndex="-1" 
                    TabMode="InsertSpaces" Text="" TextDirection="LeftToRight" 
                    ToolbarBackColor="Transparent" ToolbarBackgroundImage="True" 
                    ToolbarImagesLocation="InternalResource" 
                    ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;Cut,Copy,Paste;Undo,Redo" 
                    ToolbarStyleConfiguration="NotSet" UpdateToolbar="True" 
                    UseToolbarBackGroundImage="True" />
            </td>
            <td colspan="5">
                <table>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnImage" Text="IMagen" CssClass="button cancel" Width="120px"
                                OnClick="BtnImageOnClick" meta:resourcekey="btnImageResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnVariableText" Text="TExto variable" Width="120px"
                                CssClass="button cancel" OnClick="BtnVariableTextOnClick" meta:resourcekey="btnVariableTextResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnLink" Text="Link" CssClass="button cancel" Width="120px"
                                OnClick="BtnLinkOnClick" meta:resourcekey="btnLinkResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnSignature" Text="Signature" CssClass="button cancel" Width="120px"
                                OnClick="BtnSignatureOnClick" meta:resourcekey="btnSignatureResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnContact" Text="Contacto" CssClass="button cancel" Width="120px"
                                OnClick="BtnContactOnClick" meta:resourcekey="btnContactResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnCountryVisibleText" Text="Country Visible Text" CssClass="button cancel" Width="120px"
                                OnClick="BtnCountryVisibleTextOnClick" meta:resourcekey="btnCountryVisibleTextResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnUpgradeVariableText" Text="Upgrade Variable Text" CssClass="button cancel" Width="120px"
                                OnClick="BtnUpgradeVariableTextOnClick" meta:resourcekey="btnUpgradeVariableTextResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnTable" Text="Table" CssClass="button cancel" Width="120px"
                                OnClick="BtnTableOnClick" meta:resourcekey="btnTableResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnConditionVariableText" Text="Condition" CssClass="button cancel" Width="120px"
                                OnClick="BtnConditionVariableTextOnClick" meta:resourcekey="btnConditionVariableTextResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnPixel" Text="Pixel" CssClass="button cancel" Width="120px"
                                OnClick="BtnPixelOnClick" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnClausule" Text="Clausule" CssClass="button cancel" Width="120px"
                                OnClick="BtnClausuleOnClick" />
                        </td>
                    </tr>
                </table>                        
            </td>
        </tr>
        </table>
    </div>
    <div class="module">
        <table>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblFooter" Text="Foter" CssClass="label" 
                        meta:resourcekey="lblFooterResource1" />
                </td>
                <td>
                    <ajx:AsyncFileUpload runat="server" OnUploadedComplete="FooterLoaded" ID="fupFooter" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnDeleteFooter" OnClick="BtnDeleteFooterOnClick" 
                        meta:resourcekey="btnDeleteFooterResource1" CssClass="delete small" />
                </td>
                </tr>
                <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label runat="server" ID="lblFooterName" CssClass="text_Normal"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblFooterPDF" Text="Footer PDF" CssClass="label" 
                        meta:resourcekey="lblFooterPDFResource1" />
                </td>
                <td>
                    <ajx:AsyncFileUpload runat="server" OnUploadedComplete="FooterPDFLoaded" ID="fupFooterPDF" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnDeleteFooterPDF" OnClick="BtnDeleteFooterPDFOnClick" 
                        meta:resourcekey="btnDeleteFooterPDFResource1" CssClass="button delete small" />
                </td>
                </tr>
                <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label runat="server" ID="lblFooterPDFName" CssClass="text_Normal"/>
                </td>
            </tr>
        </table>
    </div>
</div>