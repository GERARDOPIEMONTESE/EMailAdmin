<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EMailListManager.aspx.cs" Inherits="EMailAdmin.Administration.EMailLists.EMailListManager" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/loading.css" rel="stylesheet" type="text/css" />
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
            <div class="buttonModule">
                    <asp:Label runat="server" ID="lblPais" Text="Pais" CssClass="label" 
                        meta:resourcekey="lblPaisResource1" />
                    <asp:DropDownList runat="server" ID="ddlPais" CssClass="dropdown" 
                        DataTextField="Nombre" DataValueField="IdLocacion" 
                        meta:resourcekey="ddlPaisResource1"/>                
                    <asp:CustomValidator runat="server" ID="ddlPaisValidator" ErrorMessage="   *" 
                        ValidationGroup="EMailList" Display="Dynamic" 
                        OnServerValidate="ddlPaisValidatorComplete" 
                        meta:resourcekey="ddlPaisValidatorResource1" />
                    <asp:Label runat="server" ID="lblEmailListType" Text="Tipo de Email" 
                        CssClass="label" meta:resourcekey="lblEmailListTypeResource1" />
                    <asp:DropDownList runat="server" ID="ddlEmailListType" CssClass="dropdown" 
                        DataTextField="Description" DataValueField="Id" 
                        meta:resourcekey="ddlEmailListTypeResource1" />
                    <asp:CustomValidator runat="server" ID="ddlEmailListTypeValidator" 
                        ErrorMessage="   *" ValidationGroup="EMailList" Display="Dynamic" 
                        OnServerValidate="ddlEmailListTypeValidatorComplete" 
                        meta:resourcekey="ddlEmailListTypeValidatorResource1" />
                    <asp:Label runat="server" ID="lblCorreoelectronico" Text="Correo electronico" CssClass="label"/>
                    <asp:TextBox ID="txtCorreoelectronico" runat="server"></asp:TextBox>                    
                    <asp:Button runat="server" ID="btnSearch" Text="Buscar" 
                        OnClick="btnSearch_OnClick" CssClass="button ok" 
                        meta:resourcekey="btnSearchResource1" />
                    <asp:Button runat="server" ID="btnNew" Text="Agregar" CssClass="button ok" 
                        OnClick="BtnAddOnClick" ValidationGroup="EMailList" 
                        meta:resourcekey="btnNewResource1"/>
            </div>
            <div class="gridList">
                
                <asp:UpdatePanel runat="server" ID="uplItems" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="gridFont">
                            <asp:GridView 
                            ID="grvEmailListUsus" 
                            runat="server" 
                            GridLines="Vertical" 
                            ForeColor="White"
                            AutoGenerateColumns="False"
                            CellPadding="4" 
                            DataKeyNames="Id" meta:resourcekey="grvEmailListUsusResource1" CssClass="tbl-generic m-center">                                
                                <Columns>
                                    <asp:BoundField DataField="Pais" HeaderText="Pais" 
                                        meta:resourcekey="BoundFieldResource1" >
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Code" HeaderText="Codigo" 
                                        meta:resourcekey="BoundFieldResource2" >                                    
                                        <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmailListType" HeaderText="Lista" 
                                        meta:resourcekey="BoundFieldResource3" >
                                        <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" 
                                        meta:resourcekey="BoundFieldResource4" >                       
                                        <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                                        meta:resourcekey="BoundFieldResource5" >
                                        <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CorreoElectronico" HeaderText="EMail" 
                                        meta:resourcekey="BoundFieldResource6" >
                                        <ItemStyle Width="250px" />
                                    </asp:BoundField>
                                    <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnDelete" runat="server" ImageUrl="~/IMG/b_drop.png" 
                                                ToolTip="Delete" OnClick="IbnDelete_Onclick" 
                                                CommandArgument='<%# Bind("IdEmailList") %>' 
                                                meta:resourcekey="ibnDeleteResource1" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle CssClass="gridView_Edit_Row" />
                                <EmptyDataTemplate>
                                    <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                                        Text="No hay usuarios en la lista de emails" 
                                        meta:resourcekey="lblNoDataResource1" />
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
            <div id="panels">
            <asp:HiddenField runat="server" ID="hfdUsuarios"/>
            <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" 
                    ID="pnlUsuarios" meta:resourcekey="pnlUsuariosResource1">
                        <div class="formUsuarios">
                         <fieldset>
                                <div class="buttonModule">
                            <asp:Label runat="server" ID="lblNombre" Te xt="Nombre" CssClass="label" 
                                        meta:resourcekey="lblNombreResource1" />
                            <asp:TextBox ID="txtNombre" runat="server" meta:resourcekey="txtNombreResource1"></asp:TextBox>
                            <asp:Label runat="server" ID="lblApellido" Text="Apellido" CssClass="label" 
                                        meta:resourcekey="lblApellidoResource1" />
                            <asp:TextBox ID="txtApellido" runat="server" meta:resourcekey="txtApellidoResource1"></asp:TextBox>
                            <asp:Label runat="server" ID="lblEMail" Text="EMail" CssClass="label" 
                                        meta:resourcekey="lblEMailResource1" />
                            <asp:TextBox ID="txtEMail" runat="server" meta:resourcekey="txtEMailResource1"></asp:TextBox>
                            <asp:Button runat="server" ID="btnSearchUsu" Text="Buscar" 
                                OnClick="btnSearchUsu_OnClick" CssClass="button ok" 
                                        meta:resourcekey="btnSearchUsuResource1"  />   
                             <asp:Button runat="server" ID="btnCancel" CssClass="button cancel" Text="Cancelar" 
                                    meta:resourcekey="btnCancelResource1"/>                         
                        </div>
                        <div class="modUsuario">
                        </div>
                        
                        <div class="module">
                            <asp:UpdatePanel ID="uplUsus" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="gridFont">
                                    <asp:GridView ID="grvUsuarios" runat="server" AutoGenerateColumns="False" 
                                        CellPadding="4" DataKeyNames="IdUsuario" GridLines="Vertical"
                                        OnPageIndexChanging="grvUsuariosPageIndexChange" PageSize="15"
                                        AllowPaging="True" meta:resourcekey="grvUsuariosResource1" CssClass="tbl-generic m-center" style="width: 100%;">
                                        <Columns>
                                            <asp:TemplateField HeaderText=" " meta:resourcekey="TemplateFieldResource2">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="mycheck" runat="server" AutoPostBack="True" 
                                                        meta:resourcekey="mycheckResource1" OnCheckedChanged="ChkChanged" />
                                                </ItemTemplate>
                                                <controlstyle width="20px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IdUsuario" HeaderText="Id" 
                                                meta:resourcekey="BoundFieldResource7">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Apellido" HeaderText="Apellido" 
                                                meta:resourcekey="BoundFieldResource8">
                                                <ItemStyle Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                                                meta:resourcekey="BoundFieldResource9">
                                                <ItemStyle Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CorreoElectronico" HeaderText="EMail" 
                                                meta:resourcekey="BoundFieldResource10">
                                                <ItemStyle Width="250px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <EditRowStyle CssClass="gridView_Edit_Row" />
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblNoData" runat="server" CssClass="text_Normal" 
                                                meta:resourcekey="lblNoDataResource2" Text="No se encontraron usuarios"></asp:Label>
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="gridView_Row_Header" />
                                        <PagerStyle CssClass="GridView_Pager_Normal" />
                                        <RowStyle CssClass="GridView_Row_Data_Normal" />
                                        <SelectedRowStyle CssClass="gridView_Selected_Row" />
                                    </asp:GridView>                                    
                                    </div>
                                    <div class="ta-right">
                                        <asp:Button runat="server" ID="btnSave" CssClass="button ok" Text="Agregar" 
                                            onclick="btnSave_Click" meta:resourcekey="btnSaveResource1"/>
                                    </div>
                                </ContentTemplate>
                                <triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSearchUsu" EventName="Click" />
                                </triggers>
                            </asp:UpdatePanel>
                        </div>
                    </fieldset>     
                </div>              
            </asp:Panel>
            <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                 ID="mpeUsuarios" TargetControlID="hfdUsuarios"
                PopupControlID="pnlUsuarios" RepositionMode="None" runat ="server" 
                    DynamicServicePath="" Enabled="True" CancelControlID="btnCancel" />
            </div>
        </div>
        
    </form>
</body>
</html>
