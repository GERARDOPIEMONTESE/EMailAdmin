<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EMailProcessLogList.aspx.cs" Inherits="EMailAdmin.Administration.EmailLogs.EMailProcessLogList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <title>EMail Log List</title>
    <link href="../../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/loading.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True" 
            EnableScriptGlobalization="True"/>
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
                <asp:RequiredFieldValidator runat="server" ID="rfvFromDate" ControlToValidate="txtFromDate" 
                    ErrorMessage="*" ValidationGroup="Search" />

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
                <asp:RequiredFieldValidator runat="server" ID="rfvToDate" ControlToValidate="txtToDate" 
                    ErrorMessage="*" ValidationGroup="Search" />
                <asp:Button runat="server" ID="btnSearch" Text="Search" ValidationGroup="Search"
                    OnClick="BtnSearchOnClick" CssClass="button ok" />
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
                        CssClass="tbl-generic m-center" >
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="StartDate" HeaderText="Start" />
                            <asp:BoundField DataField="EndDate" HeaderText="End" />
                            <asp:BoundField DataField="ProcessTypeDescription" HeaderText="Type" ItemStyle-Width="300px" />
                            <asp:BoundField DataField="IdLote" HeaderText="Lote" />
                        </Columns>
                        <RowStyle CssClass="GridView_Row_Data_Normal" />
                        <PagerStyle CssClass="GridView_Pager_Normal" />
                        <SelectedRowStyle CssClass="gridView_Selected_Row" />
                        <HeaderStyle CssClass="gridView_Row_Header" />
                        <EditRowStyle CssClass="gridView_Edit_Row" />
                        <EmptyDataTemplate>
                            <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                                Text="No logs available."/>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
