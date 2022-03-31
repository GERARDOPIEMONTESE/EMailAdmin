<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Preview.ascx.cs" Inherits="EMailAdmin.Controls.Preview.Preview" %>

<script type="text/javascript">
    function SetLanguage() {
        var idLanguage = document.getElementById('prvPreview_ddlLanguage').value;
        document.getElementById("lnkViewPDF").href = document.getElementById("lnkViewPDF").href + idLanguage;
        document.getElementById("lnkViewPDF").click();
    }
</script>

<div class="formModule">
    <fieldset>
        <div class="module">
            <asp:Label runat="server" ID="lblPreview" Text="Preview" CssClass="labelTitle" 
                meta:resourcekey="lblPreviewResource1"  />
        </div>
        <div class="module" align="right">
            <asp:DropDownList runat="server" ID="ddlLanguage" CssClass="dropdown" 
                DataValueField="Id" DataTextField="Descripcion"
                meta:resourcekey="ddlLanguageResource1"/>
        </div>
        <div class="module" align="right">
            <asp:Button ID="btnView" runat="server" CssClass="button ok" 
                OnClick="BtnViewOnClick" Text="View" 
                OnClientClick="parent.frames[FrameID].window.location.reload();" 
                meta:resourcekey="btnViewResource1" />
        </div>
        <div class="module" align="right">      
            <asp:Button ID="btnViewPDF" runat="server" CssClass="button" 
               Text="PDF" OnClientClick="SetLanguage()"/>
             <a id="lnkViewPDF" target="_blank" href="../../Attachment/TestAttachmentEkitPDF.ashx?IdLanguage=" style="display:none;">PDF</a>
        </div>
        <p/>
        <br/>
        <div id="divContent">
            <iframe src="/Controls/Preview/pdf.aspx" runat="server" width="900px" height="400px" name="frame" id="frame" visible="false"/>
        </div>
        <div class="module">
            <asp:Button runat="server" ID="btnDownload" Text="Descargar" CssClass="button cancel" OnClick="BtnDownloadOnClick" Visible="false" />
            <asp:Button runat="server" ID="btnClose" Text="Cerrar" CssClass="button cancel" OnClick="BtnCloseOnClick" />
        </div>
    </fieldset>
</div>