<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VariableTextSelector.ascx.cs" Inherits="EMailAdmin.Controls.VariableText.VariableTextSelector" %>
<script type="text/javascript">
    function ShowDynamic(sel) {
        debugger;
        if (sel.options[sel.selectedIndex].text == 'DynamicValue') {
            document.getElementById('ddlVariableText_VariableTextSelector1_txtDynamicName').style.visibility = "visible";
        }
        else {
            document.getElementById('ddlVariableText_VariableTextSelector1_txtDynamicName').style.visibility = "hidden";
        }
    }
</script>
<div>
    <fieldset>
        <asp:UpdatePanel runat="server" ID="pnlGrid" UpdateMode="Conditional">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="Label2" Text="VariableText" CssClass="label" 
                                meta:resourcekey="Label2Resource1" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlVariableText" runat="server" CssClass="dropdown" 
                                DataValueField="Id" DataTextField="Name" 
                                meta:resourcekey="ddlVariableTextResource1" onchange="return ShowDynamic(this)"></asp:DropDownList>                        
                            <asp:TextBox ID="txtDynamicName" runat="server" MaxLength="1000"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="Label1" Text="Operador" CssClass="label" 
                                meta:resourcekey="Label1Resource1" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlOperador" runat="server" CssClass="dropdown" 
                                meta:resourcekey="ddlOperadorResource1"></asp:DropDownList>
                        </td>                
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblCondition" Text="Condicion" CssClass="label" 
                                meta:resourcekey="lblConditionResource1" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtCondition" runat="server" MaxLength="1000" 
                                meta:resourcekey="txtConditionResource1"></asp:TextBox>
                            <asp:CustomValidator ID="ctvCondition" runat="server" 
                                ControlToValidate="txtCondition" Display="Dynamic" 
                                onservervalidate="ctvCondition_ServerValidate" ValidationGroup="Validar" 
                                meta:resourcekey="ctvConditionResource1" SetFocusOnError="True"></asp:CustomValidator>
                            <%--<asp:RequiredFieldValidator ID="rfvCondicion" runat="server" 
                                ControlToValidate="txtCondition" Display="Dynamic" SetFocusOnError="True" 
                                ValidationGroup="Validar" meta:resourcekey="rfvCondicionResource1"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div>
            <asp:Button runat="server" ID="btnAgregar" Text="Agregar" CssClass="button add" 
                OnClick="BtnAgregarOnClick" ValidationGroup="Validar" 
                meta:resourcekey="btnAgregarResource1" />
            <asp:Button runat="server" ID="btnCloseOnClick" Text="Cancel" CssClass="button cancel" 
                OnClick="BtnCloseOnClick" meta:resourcekey="btnCloseOnClickResource1" />
        </div>  
    </fieldset>
</div>