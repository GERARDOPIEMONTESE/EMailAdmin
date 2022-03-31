<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportAssociations.aspx.cs" Inherits="EMailAdmin.Reports.Associations.ReportAssociations" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Template List</title>
    <link href="../../CSS/loading.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmMain" runat="server">
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
            <asp:UpdatePanel runat="server" ID="updFIlters">
                <ContentTemplate>
                    <div class="buttonModule">
                        <fieldset>
                            <legend>
                                <h2><asp:Literal ID="ltrFiltroGrupo" runat="server" Text="Grupos" 
                                        meta:resourcekey="ltrFiltroGrupoResource1"></asp:Literal></h2>
                            </legend>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblName" Text="Nombre de grupo" CssClass="label" 
                                        meta:resourcekey="lblNameResource1"  />
                                    <asp:TextBox runat="server" ID="txtName" CssClass="textbox" 
                                        meta:resourcekey="txtNameResource1" />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblType" Text="Tipo de template" CssClass="label" 
                                        meta:resourcekey="lblTypeResource1" />
                                    <asp:DropDownList runat="server" ID="ddlType" CssClass="dropdown" 
                                        DataTextField="Descripcion" DataValueField="Id" 
                                        meta:resourcekey="ddlTypeResource1" />            
                                </td>                               
                                 <td>
                                    <asp:Label runat="server" ID="lblAsociado" Text="Asociacion a templates" 
                                         CssClass="label" meta:resourcekey="lblAsociadoResource1"  />
                                    <asp:DropDownList ID="ddlAsociacion" runat="server" CssClass="dropdown" 
                                        meta:resourcekey="ddlAsociacionResource1"></asp:DropDownList>
                                </td>
                                <td>
                                 <asp:Button ID="btnSearch" runat="server" CssClass="button ok" 
                                         OnClick="BtnSearch_OnClick" Text="Buscar" 
                                        meta:resourcekey="btnSearchResource1" />
                                         </td>
                            </tr>
                        </table>
                        <fieldset>
                            <legend>
                                <h2><asp:Literal ID="ltrDatosCondiciones" runat="server" Text="Condiciones" 
                                        meta:resourcekey="ltrDatosCondicionesResource1"></asp:Literal></h2>
                            </legend>
                            <table>
                            <tr>     
                               <td>
                                    <asp:Label runat="server" ID="lblCountry" Text="Pais" CssClass="label" 
                                        meta:resourcekey="lblCountryResource1"/>
                                    <asp:DropDownList runat="server" ID="ddlCountry" CssClass="dropdown" AutoPostBack="True"
                                        DataTextField="Nombre" DataValueField="IdLocacion" 
                                        OnSelectedIndexChanged="CountryChanged" 
                                        meta:resourcekey="ddlCountryResource1" />     
                                </td>                                                               
                                <td>
                                    <asp:Label runat="server" ID="lblBranch" Text="Sucursal" 
                                        meta:resourcekey="lblBranchResource1"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtAccount" CssClass="textbox" 
                                        meta:resourcekey="txtAccountResource1" />
                                </td>                            
                               
                            </tr>
                            <tr>
                                <td/>
                                <td>
                                    <asp:Label runat="server" ID="lblProduct" Text="Producto" CssClass="label" 
                                        meta:resourcekey="lblProductResource1" />
                                    <asp:DropDownList runat="server" ID="ddlProduct" CssClass="dropdown" 
                                        DataTextField="Descripcion" DataValueField="Id" 
                                        meta:resourcekey="ddlProductResource1" />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblRate" Text="Tarifa" CssClass="label" 
                                        meta:resourcekey="lblRateResource1" />
                                    <asp:TextBox runat="server" ID="txtRate" CssClass="textbox" 
                                        meta:resourcekey="txtRateResource1" />
                                </td>
                            </tr>                            
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel runat="server" ID="updGrid">
                <ContentTemplate>
                    <div class="gridList">
                        <div class="gridFont">
                            <asp:GridView
                                ID="grvTemplate" 
                                runat="server" 
                                AutoGenerateColumns ="False"
                                AllowPaging="True" 
                                Export="Yes" 
                                PageSize = "15" 
                                GridLines="Vertical" 
                                CssClass="tbl-generic m-center"
                                Width="800px"
                                PagerType="DropDownList"
                                OnPageIndexChanging="GrvTemplatePageIndexChanging"
                                OnRowCommand="GrvTemplateRowCommand" ExportPDF="No" 
                                meta:resourcekey="grvTemplateResource1">
                                <Columns>
                                    <asp:BoundField DataField="GroupDescription" HeaderText="Group" 
                                        meta:resourcekey="BoundFieldResource1" >
                                    <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TemplateName" HeaderText="Template" 
                                        meta:resourcekey="BoundFieldResource3" >
                                    <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TemplateType" HeaderText="Tipo" 
                                        meta:resourcekey="BoundFieldResource4" >
                                    <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EffectiveStartDate" HeaderText="Vigencia desde" 
                                        meta:resourcekey="BoundFieldResource5" >
                                    <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EffectiveEndDate" HeaderText="Vigencia hasta" 
                                        meta:resourcekey="BoundFieldResource6" >
                                    <ItemStyle Width="50px" />
                                    </asp:BoundField>
                                </Columns>
                                <RowStyle CssClass="gridView_RowStyle" />
                                <PagerStyle CssClass="gridView_Pager_Style_Row" HorizontalAlign="Center" />
                                <SelectedRowStyle CssClass="gridView_Selected_Row" />
                                <HeaderStyle CssClass="gridView_Row_Header" />
                                <EditRowStyle CssClass="gridView_Edit_Row" />
                                <AlternatingRowStyle CssClass="gridView_Alternative_Row_Data" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="LbCreditoEmpty" runat="server" CssClass="label" 
                                        Text="No hay templates disponibles" 
                                        meta:resourcekey="LbCreditoEmptyResource1" />
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
