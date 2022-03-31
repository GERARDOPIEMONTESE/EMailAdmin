<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CapitaSelector.ascx.cs" Inherits="EMailAdmin.Controls.Selectors.Capita.CapitaSelector" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<asp:HiddenField runat="server" ID="hfdPais"/>    
<asp:ImageButton runat="server" ID="btnSearch" Text="Cerrar" 
    ImageUrl="~/IMG/icon-search-small.gif" onclick="btnSearch_Click" 
    meta:resourcekey="btnSearchResource1" />
<div>
    <asp:HiddenField runat="server" ID="hfdCapita"/>
    <asp:Panel runat="server" CssClass="modalBackgroundRestore" 
        Style="display: none;" ID="pnlCapita" 
        meta:resourcekey="pnlCapitaResource1">
        <div class="form medium">
        <asp:HiddenField ID="hfIdCapita" runat="server" />
        <div class="form-rows">
            <div class="row">
                <asp:Label runat="server" ID="lblCountry" Text="Country" CssClass="label" 
                    meta:resourcekey="lblCountryResource1"/>
                <asp:DropDownList runat="server" ID="ddlCountry" CssClass="drp-medium" DataTextField="Nombre"
                    DataValueField="Codigo" meta:resourcekey="ddlCountryResource1"></asp:DropDownList>                        
            </div>
            <div class="row">
                <asp:Label runat="server" ID="lblCapita" Text="Capita" CssClass="label" 
                    meta:resourcekey="lblCapitaResource1" />
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" 
                    meta:resourcekey="txtDescripcionResource1" ></asp:TextBox>                        
                </div>
                <div class="row">
                <asp:Label runat="server" ID="lblPlan" Text="Plan" CssClass="label" 
                        meta:resourcekey="lblPlanResource1" />
                <asp:TextBox ID="txtPlan" runat="server" CssClass="textbox" 
                        meta:resourcekey="txtPlanResource1" ></asp:TextBox>
                <asp:ImageButton ID="btnFiltrar" runat="server" 
                    ImageUrl="~/IMG/icon-search-small.gif" onclick="btnFiltrar_Click" 
                        meta:resourcekey="btnFiltrarResource1" />
                </div>
                <div class="row" style="width:500px;height:200px;overflow:auto">
                <asp:GridView ID="grvCapitas" runat="server" Caption="Capitas" 
                    Width="100%" DataKeyNames="Id,Codigo,Descripcion,PlanId,PlanCodigo,PlanDescripcion"
                    CssClass="tbl-generic m-center" AutoGenerateColumns="False" 
                        onrowdatabound="grvCapitas_RowDataBound" 
                        meta:resourcekey="grvCapitasResource1">
                    <Columns>                               
                        <asp:TemplateField ShowHeader="False" meta:resourcekey="TemplateFieldResource1">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnSelect" runat="server" CausesValidation="False" 
                                    CommandArgument='<%# Container.DataItemIndex %>' CommandName="Select" 
                                    ImageUrl="~/IMG/ok.gif" oncommand="btnSelect_Command" Text="Select" 
                                    ToolTip="Seleccionar" meta:resourcekey="btnSelectResource1"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PlanId" HeaderText="IdPlan" 
                            meta:resourcekey="BoundFieldResource1" />                               
                        <asp:BoundField DataField="PlanCodigo" HeaderText="Cod Plan" 
                            meta:resourcekey="BoundFieldResource2" />
                        <asp:BoundField DataField="PlanDescripcion" HeaderText="Plan" 
                            meta:resourcekey="BoundFieldResource3"/>
                        <asp:BoundField DataField="Id" HeaderText="IdCapita" 
                            meta:resourcekey="BoundFieldResource4" />
                        <asp:BoundField DataField="Codigo" HeaderText="Cod Capita" 
                            meta:resourcekey="BoundFieldResource5" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Capita" 
                            meta:resourcekey="BoundFieldResource6"/>
                    </Columns>
                    <EditRowStyle CssClass="gridView_Edit_Row" />
                        <EmptyDataTemplate>
                            <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" Text="" ></asp:label>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="gridView_Row_Header" />
                        <PagerStyle CssClass="GridView_Pager_Normal" />
                        <RowStyle CssClass="GridView_Row_Data_Normal" />
                        <SelectedRowStyle CssClass="gridView_Selected_Row" />
                </asp:GridView>
            </div>  
            <asp:Button runat="server" ID="btnClose" onclick="btnClose_Click" 
                CssClass="button cancel" Text="Cerrar" 
                meta:resourcekey="btnCloseResource1" />
        </div>
    </div>
    </asp:Panel>
    <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
            ID="mpeCapita" TargetControlID="hfdCapita"
        PopupControlID="pnlCapita" RepositionMode="None" runat ="server" 
        BehaviorID="mpeCapita" DynamicServicePath="" Enabled="True"></ajx:ModalPopupExtender>
</div>  