<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotSendEmailAgencia.aspx.cs" Inherits="EMailAdmin.Administration.NotSentEmailAgencia.NotSendEmailAgencia" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="acc" TagName="AccountSelector" Src="~/Controls/Account/AccountSelector.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/loading.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function showAccountPopUp() {
            $find('mpeAccount').show();
        }
    </script>
</head>
<body>
   <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"/>
        <div class="PaginaPortal">
           <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" > 
                <ProgressTemplate> 
                    <div id="progressBackgroundFilter"></div> 
                    <div id="processMessage" align="center"><p>Loading...</p>
                        <img alt="Loading..." src="../../IMG/loading.gif"  /> 
                    </div> 
                </ProgressTemplate> 
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="upNotSendEmailAgencies" runat="server">
            <ContentTemplate>
            <div class="buttonModule">
                    <asp:Label runat="server" ID="lblPais" Text="Pais" CssClass="label"></asp:Label>
                    <asp:DropDownList runat="server" ID="ddlPais" CssClass="dropdown" 
                        DataTextField="Nombre" DataValueField="Codigo"></asp:DropDownList>  
                    <%--<asp:CustomValidator runat="server" ID="ddlPaisValidator" ErrorMessage="   *" 
                        ValidationGroup="EMailList" Display="Dynamic" 
                        OnServerValidate="ddlPaisValidatorComplete"></asp:CustomValidator>--%>
                    <asp:Label runat="server" ID="lblEmailListType" Text="Codigo" CssClass="label"></asp:Label>
                    <asp:TextBox runat="server" ID="txtCodigo"></asp:TextBox>
                    <asp:Label runat="server" ID="lblCorreoelectronico" Text="Razon social" CssClass="label"/>
                    <asp:TextBox ID="txtRazonSocial" runat="server"></asp:TextBox>          
                    <asp:Button runat="server" ID="btnSearch" Text="Buscar" 
                        OnClick="btnSearch_OnClick" CssClass="button ok" />
                    <asp:Button runat="server" ID="btnNew" Text="Agregar" CssClass="button ok" 
                        OnClick="BtnAddOnClick" ValidationGroup="EMailList" />
            </div>
            <div class="gridList">                
                <asp:UpdatePanel runat="server" ID="uplItems" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="gridFont">
                            <asp:GridView 
                            ID="grvCuentas" 
                            runat="server" 
                            GridLines="Vertical" 
                            ForeColor="White"
                            AutoGenerateColumns="False"
                            CellPadding="4" 
                            DataKeyNames="Id" 
                            CssClass="tbl-generic m-center">                                
                                <Columns>
                                    <asp:BoundField DataField="CountryCode" HeaderText="CountryCode">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pais" HeaderText="Country">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AgencyCode" HeaderText="Code">                            
                                        <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Branch" HeaderText="Branch">                       
                                        <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FullName" HeaderText="Account">
                                        <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnDelete" runat="server" ImageUrl="~/IMG/b_drop.png" 
                                                ToolTip="Delete" 
                                                CommandArgument='<%# Bind("Id") %>' oncommand="ibnDelete_Command"/>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle CssClass="gridView_Edit_Row" />
                                <EmptyDataTemplate>
                                    <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                                        Text="No hay cuentas en la lista de excluidos" ></asp:label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="gridView_Row_Header" />
                                <PagerStyle CssClass="GridView_Pager_Normal" />
                                <RowStyle CssClass="GridView_Row_Data_Normal" />
                                <SelectedRowStyle CssClass="gridView_Selected_Row" />
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hfdAccount"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlAccount">
                    <acc:AccountSelector runat="server" ID="accAccount" OnSearchPressed="AccAccountSelectorOnSearch" OnClosePressed="AccAccountSelectorOnClose" />
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeAccount" TargetControlID="hfdAccount"
                    PopupControlID="pnlAccount" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeAccount" DynamicServicePath="" Enabled="True" />
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>        
    </form>
</body>
</html>
