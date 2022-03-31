<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailLogsCapita.aspx.cs" Inherits="EMailAdmin.Administration.EmailLogsCapita.EmailLogsCapita" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMail Log Capita List</title>
    <link href="../../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/loading.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True" 
            EnableScriptGlobalization="True"/>
        <div class="PaginaPortal">
            <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" > 
                <ProgressTemplate> 
                    <div id="progressBackgroundFilter"></div> 
                    <div id="processMessage" align="center"><p>Loading...</p>
                        <img alt="Loading..." src="../../IMG/loading.gif"  /> 
                    </div> 
                </ProgressTemplate> 
            </asp:UpdateProgress>--%>
            <div class="buttonModule">
                <div>
                <asp:Label runat="server" ID="lblApellido" Text="Apellido" CssClass="label" 
                    meta:resourcekey="lblApellidoResource1"/>                
                <asp:TextBox ID="txtApellido" runat="server" 
                    meta:resourcekey="txtApellidoResource1"></asp:TextBox>
                <asp:Label runat="server" ID="lblNombre" Text="Nombre" CssClass="label" 
                    meta:resourcekey="lblNombreResource1"/>                
                <asp:TextBox ID="txtNombre" runat="server" 
                    meta:resourcekey="txtNombreResource1"></asp:TextBox>
                <asp:Label runat="server" ID="lblDocumento" Text="Documento:" CssClass="label" 
                    meta:resourcekey="lblDocumentoResource1"/>                
                <asp:TextBox ID="txtDocumento" runat="server" 
                    meta:resourcekey="txtDocumentoResource1"></asp:TextBox>
                <asp:Label runat="server" ID="lblCapita" Text="Capita:" CssClass="label" 
                    meta:resourcekey="lblCapitaResource1"/>                
                <asp:TextBox ID="txtCapita" runat="server" 
                    meta:resourcekey="txtCapitaResource1"></asp:TextBox>
                <asp:Label runat="server" ID="lblPlan" Text="Plan:" CssClass="label" 
                    meta:resourcekey="lblPlanResource1"/>                
                <asp:TextBox ID="txtPlan" runat="server" meta:resourcekey="txtPlanResource1"></asp:TextBox>
                </div>
                <div>
                <asp:Label runat="server" ID="lblPais" Text="Pais:" CssClass="label" 
                    meta:resourcekey="lblPaisResource1"/>                
                <asp:DropDownList ID="ddlPais" runat="server" DataTextField="Nombre" 
                    DataValueField="Codigo" meta:resourcekey="ddlPaisResource1"></asp:DropDownList>
                <asp:Label runat="server" ID="lblEnvioLinks" Text="Envio Links:" CssClass="label" 
                    meta:resourcekey="lblEnvioLinksResource1"/>                
                <asp:DropDownList ID="ddlEnvioLinks" runat="server" 
                    meta:resourcekey="ddlEnvioLinksResource1"></asp:DropDownList>
                <asp:Label runat="server" ID="lblFromDate" CssClass="label" Text="Date From:" 
                    meta:resourcekey="lblFromDateResource1"/>
                <asp:TextBox ID="txtFromDate" CssClass="textbox" runat="server" 
                    meta:resourcekey="txtFromDateResource1" />
                <ajx:CalendarExtender ID="cerFromDate" runat="server" 
                    TargetControlID="txtFromDate" PopupPosition="Right" CssClass="calendarStyle" 
                    Enabled="True" />
                <ajx:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                    MaskType="Date" Mask="99/99/9999" CultureAMPMPlaceholder="" 
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"/>
				<asp:Label runat="server" ID="lblToDate" CssClass="label" Text="Date To:" 
                    meta:resourcekey="lblToDateResource1"/>
                <asp:TextBox ID="txtToDate" CssClass="textbox" runat="server" 
                    meta:resourcekey="txtToDateResource1" />
                <ajx:CalendarExtender ID="cerToDate" runat="server" TargetControlID="txtToDate" 
                    PopupPosition="Right" CssClass="calendarStyle" Enabled="True" />
                <ajx:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                    MaskType="Date" Mask="99/99/9999" CultureAMPMPlaceholder="" 
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"/>
                <asp:Button runat="server" ID="btnSearch" Text="Search" 
                    OnClick="BtnSearchOnClick" CssClass="button ok" 
                    meta:resourcekey="btnSearchResource1"/>                
                </div>
            </div>
            <div>
                <asp:GridView ID="grvLogs" runat="server" AutoGenerateColumns="False"
                    PageSize="15" 
                    AllowPaging="True"
                    GridLines="Vertical" 
                    PagerStyle-HorizontalAlign="Center"
                    CssClass="tbl-generic m-center" onrowdatabound="grvLogs_RowDataBound" 
                    onpageindexchanging="grvLogs_PageIndexChanging" 
                    meta:resourcekey="grvLogsResource1">
                    <Columns>
                        <asp:BoundField HeaderText="Estado" meta:resourcekey="BoundFieldResource1" />                        
                        <asp:BoundField DataField="MailTo" HeaderText="MailTo" 
                            meta:resourcekey="BoundFieldResource2" />
                        <asp:BoundField DataField="StartDate" HeaderText="Fecha" 
                            meta:resourcekey="BoundFieldResource3" />
                        <asp:BoundField DataField="PaisNombre" HeaderText="Pais" 
                            meta:resourcekey="BoundFieldResource4" />
                        <asp:BoundField DataField="ApellidoNombre" HeaderText="Apellido y Nombre" 
                            meta:resourcekey="BoundFieldResource5" />
                        <asp:BoundField DataField="TipoDocumentoDescripcion" HeaderText="Tipo Doc" 
                            meta:resourcekey="BoundFieldResource6" />
                        <asp:BoundField DataField="Documento" HeaderText="Documento" 
                            meta:resourcekey="BoundFieldResource7" />
                        <asp:BoundField DataField="ProductName" HeaderText="Capita" 
                            meta:resourcekey="BoundFieldResource8" />
                        <asp:BoundField DataField="RateName" HeaderText="Plan" 
                            meta:resourcekey="BoundFieldResource9" />
                    </Columns>
                    <RowStyle CssClass="GridView_Row_Data_Normal" />
                    <PagerStyle CssClass="GridView_Pager_Normal" />
                    <SelectedRowStyle CssClass="gridView_Selected_Row" />
                    <HeaderStyle CssClass="gridView_Row_Header" />
                    <EditRowStyle CssClass="gridView_Edit_Row" />
                    <EmptyDataTemplate>
                        <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                            Text="No logs available." meta:resourcekey="lblNoDataResource1"/>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>        
    </form>
</body>
</html>
