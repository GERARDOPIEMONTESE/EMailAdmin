<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailLogList.aspx.cs" Inherits="EMailAdmin.Administration.EmailLogs.EmailLogList" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>EMail Log List</title>
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
                <asp:Label runat="server" ID="lblCountry" Text="Country" CssClass="label" 
                    meta:resourcekey="lblCountryResource1" />
                <asp:DropDownList runat="server" ID="ddlCountry" CssClass="dropdown" 
                    DataTextField="Nombre" DataValueField="Id" 
                    meta:resourcekey="ddlCountryResource1" />
                <asp:Label runat="server" ID="lblVoucher" Text="Voucher" CssClass="label" 
                    meta:resourcekey="lblVoucherResource1" />
                <asp:TextBox runat="server" ID="txtVoucher" CssClass="textbox" 
                    meta:resourcekey="txtVoucherResource1" />
                <asp:Label runat="server" ID="lblStatus" Text="Status" CssClass="label" 
                    meta:resourcekey="lblStatusResource1" />
                <asp:DropDownList runat="server" ID="ddlStatus" CssClass="dropdown" 
                    meta:resourcekey="ddlStatusResource1" />
                <asp:Label runat="server" ID="lblFromDate" CssClass="label" Text="Date From" 
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
				<%--<ajx:MaskedEditValidator ID="mevFromDate" runat="server" Display="None" InitialValue=" / / "
					ControlToValiFromDate="txtFromDate" MinimumValue="01/01/1850" ControlExtender="meeFromDate"
					IsValidEmpty="False"  Text="*" />--%>
                <asp:Label runat="server" ID="lblToDate" CssClass="label" Text="Date To" 
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
				<%--<ajx:MaskedEditValidator ID="mevToDate" runat="server" Display="None" InitialValue=" / / "
					ControlToValiToDate="txtToDate" MinimumValue="01/01/1850" ControlExtender="meeToDate"
					IsValidEmpty="False"  Text="*" />--%>
                <asp:Button runat="server" ID="btnSearch" Text="Search" 
                    OnClick="BtnSearchOnClick" CssClass="button ok" 
                    meta:resourcekey="btnSearchResource1" />
                <asp:Button runat="server" ID="btnReprocess" Text="Reprocess All" CssClass="button cancel" 
                    OnClick="BtnReprocess_OnClick" meta:resourcekey="btnReprocessResource1" />
            </div>
            <div class="gridList">
                <div class="gridFont">
                    <asp:GridView 
                        ID="grvEMailLog" 
                        runat="server" 
                        PageSize="15" 
                        AllowPaging="True"
                        GridLines="Vertical" 
                        AutoGenerateColumns="False"
                        PagerStyle-HorizontalAlign="Center"
                        OnRowCommand="GrvEMailLogRowCommand"
                        OnRowDataBound="GrvEMailLogRowDataBound"
                        OnPageIndexChanging="GrvEMailLogPageIndexChange" 
                        meta:resourcekey="grvEMailLogResource1" 
                        CssClass="tbl-generic m-center">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" 
                                meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="CountryCode" HeaderText="Country" 
                                            ItemStyle-Width="100px" meta:resourcekey="BoundFieldResource2" />                
                            <asp:BoundField DataField="VoucherCode" HeaderText="Voucher Code" 
                                            ItemStyle-Width="150px" meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField DataField="ErrorMessageForGrid" HeaderText="Error" 
                                            ItemStyle-Width="300px" meta:resourcekey="BoundFieldResource4" />
                            <asp:ButtonField ButtonType="Button" CommandName="View" Text="View" ControlStyle-CssClass="ok small"
                                             HeaderText="View" ItemStyle-Width="60px" meta:resourcekey="ButtonFieldResource2" />
                            <asp:ButtonField ButtonType="Button" CommandName="Reprocess" Text="Reprocess" 
                                             HeaderText="Reprocess" ItemStyle-Width="60px" ControlStyle-CssClass="cancel small"
                                             meta:resourcekey="ButtonFieldResource1" />
                        </Columns>
                        <RowStyle CssClass="GridView_Row_Data_Normal" />
                        <PagerStyle CssClass="GridView_Pager_Normal" />
                        <SelectedRowStyle CssClass="gridView_Selected_Row" />
                        <HeaderStyle CssClass="gridView_Row_Header" />
                        <EditRowStyle CssClass="gridView_Edit_Row" />
                        <EmptyDataTemplate>
                            <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                                Text="No hay emails a reprocesar." meta:resourcekey="lblNoDataResource1" />
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
            <div id="panels">
                <asp:HiddenField runat="server" ID="hfdError"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="display: none;" ID="pnlError">
                    <div class="formModule">
                        <fieldset>
                            <div class="module cnt-stack">
                                <asp:Label runat="server" ID="lblError" class="txt-stack" />
                            </div>
                            <div class="module ta-right">
                                <asp:Button runat="server" ID="btnClose" CssClass="button cancel" Text="Close" meta:resourcekey="btnCloseResource1"/>
                            </div>
                        </fieldset>     
                    </div>              
                </asp:Panel>
                <ajx:ModalPopupExtender BackgroundCssClass="modalBackground" 
                     ID="mpeError" TargetControlID="hfdError"
                    PopupControlID="pnlError" RepositionMode="None" runat ="server" 
                    BehaviorID="mpeError" DynamicServicePath="" Enabled="True" CancelControlID="btnClose" />
            </div>
        </div>
    </form>
</body>
</html>
