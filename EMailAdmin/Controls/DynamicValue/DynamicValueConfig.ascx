<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DynamicValueConfig.ascx.cs" Inherits="EMailAdmin.Controls.DynamicValue.DynamicValueConfig" %>
<div>
    <fieldset>       
        <div class="formModule">
            <fieldset style="width: 450px;">
                 <legend>
                    <h2><asp:Literal ID="ltrDynamicData" runat="server" Text="Dato dinámico" 
                            meta:resourcekey="ltrDynamicDataResource1"></asp:Literal></h2>
                </legend>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDynamicName" runat="server" CssClass="label" Text="Nombre del dato" meta:resourcekey="lblDynamicNameResource1" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDynamicName" runat="server" CssClass="longTextbox" MaxLength="4000" Width="302px" meta:resourcekey="txtDynamicNameResource1" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDynamicValue" runat="server" CssClass="label" Text="Valor del dato" meta:resourcekey="lblDynamicValueResource1" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDynamicValue" runat="server" MaxLength="4000" CssClass="longTextbox"
                                       Width="300px" meta:resourcekey="txtDynamicValueResource1"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
        </div>
        <div class="formModule">
            <div class="module">
                <asp:Button runat="server" ID="btnClose" Text="Cerrar" CssClass="button cancel" 
                    OnClick="BtnCloseOnClick" meta:resourcekey="btnCloseResource1" />
            </div>  
        </div>
    </fieldset>
</div>