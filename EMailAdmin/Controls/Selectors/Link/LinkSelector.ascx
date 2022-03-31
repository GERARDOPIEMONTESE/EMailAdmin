<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LinkSelector.ascx.cs" Inherits="EMailAdmin.Controls.Selectors.Link.LinkSelector" %>

<script type="text/javascript">
    function FilterStatus() {
        if (document.getElementById('lnkSelector_ddlLinkType').value == 'FIXED') {
            document.getElementById('lnkSelector_ddlLinkName').style.visibility = "hidden";
            document.getElementById('lnkSelector_ddlLinkQR').style.visibility = "hidden";
            document.getElementById('lnkSelector_ddlLinkFixedName').style.visibility = "visible";
        }
        else if (document.getElementById('lnkSelector_ddlLinkType').value == 'QR') {
            document.getElementById('lnkSelector_ddlLinkName').style.visibility = "hidden";
            document.getElementById('lnkSelector_ddlLinkQR').style.visibility = "visible";
            document.getElementById('lnkSelector_ddlLinkFixedName').style.visibility = "hidden";
        } else {
            document.getElementById('lnkSelector_ddlLinkName').style.visibility = "visible";
            document.getElementById('lnkSelector_ddlLinkQR').style.visibility = "hidden";
            document.getElementById('lnkSelector_ddlLinkFixedName').style.visibility = "hidden";
        }
        SetDivNewFixed();
    }

    function SetDivNewFixed() {
        if (document.getElementById('lnkSelector_ddlLinkType').value == 'FIXED' &&
            document.getElementById('lnkSelector_ddlLinkFixedName').value == '0') {
            document.getElementById('lnkSelector_divNewLink').style.visibility = "visible";
        }
        else {
            document.getElementById('lnkSelector_divNewLink').style.visibility = "hidden";
        }
    }
</script>

<div class="formModule">
    <fieldset>   
        <asp:UpdatePanel runat="server" ID="pnlLink">
            <ContentTemplate>
                <div class="module">
                    <asp:Label runat="server" ID="lblLink" Text="Link" CssClass="labelTitle" 
                        meta:resourcekey="lblLinkResource1"/>
                    <p/>
                    <br/>
                </div>
                <div class="module">
                    <asp:Label runat="server" ID="lblLinkType" Text="Type" CssClass="label" 
                        meta:resourcekey="lblTypeResource1"/>
                    <asp:DropDownList runat="server" ID="ddlLinkType" CssClass="dropdown"
                        DataValueField="Code" DataTextField="Description" onchange="return FilterStatus()"/>
                </div>
                <div class="module">                   
                    <asp:DropDownList runat="server" ID="ddlLinkFixedName" CssClass="dropdown" 
                        DataValueField="Id" DataTextField="Name" style="visibility:visible;" 
                        onchange="return SetDivNewFixed()" /> 
                    <br/>  
                    <asp:DropDownList runat="server" ID="ddlLinkQR" CssClass="dropdown" 
                        DataValueField="Id" DataTextField="Name" style="visibility:hidden;" 
                        onchange="return SetDivNewFixed()" /> 
                    <br/>                           
                    <asp:DropDownList runat="server" ID="ddlLinkName" CssClass="dropdown" 
                        DataValueField="Id" DataTextField="Name" style="visibility:hidden;" />
                </div>
                <div runat="server" id="divNewLink" style="visibility:visible;">
                    <div class="module">
                         <asp:Label runat="server" ID="lblLinkName" Text="Name" CssClass="label" 
                            meta:resourcekey="lblNameResource1"/>
                         <asp:TextBox runat="server" ID="txtName" CssClass="textbox" 
                            meta:resourcekey="txtNameResource1" />              
                    </div>
                    <div class="module">
                        <asp:Label runat="server" ID="lblUrl" Text="URL" CssClass="label" 
                            meta:resourcekey="lblUrlResource1"/>
                        <asp:TextBox runat="server" ID="txtUrl" CssClass="textbox" 
                            meta:resourcekey="txtUrlResource1" />
                    </div>
                </div>
                <div class="module">
                    <asp:Button runat="server" ID="btnAccept" Text="Aceptar" CssClass="button ok"  
                        OnClick="BtnAcceptOnClick" meta:resourcekey="btnAcceptResource1" />
                    <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="button cancel" 
                        OnClick="BtnCancelOnClick" meta:resourcekey="btnCancelResource1" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>    
    </fieldset>
</div>
