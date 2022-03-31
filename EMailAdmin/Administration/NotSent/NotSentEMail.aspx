<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotSentEMail.aspx.cs" Inherits="EMailAdmin.Administration.NotSent.NotSentEMail" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMail Not Sent</title>
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
                <asp:Label runat="server" ID="lblCountry" Text="Country" CssClass="label" 
                    meta:resourcekey="lblCountryResource1" />
                <asp:DropDownList runat="server" ID="ddlCountry" CssClass="dropdown" 
                    DataTextField="Nombre" DataValueField="Codigo" 
                    meta:resourcekey="ddlCountryResource1" />
                <asp:Label runat="server" ID="lblFromDate" CssClass="label" Text="Date From" 
                    meta:resourcekey="lblFromDateResource1" />
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
                    ErrorMessage="*" ValidationGroup="Process" 
                    meta:resourcekey="rfvFromDateResource1"/>
                <asp:Label runat="server" ID="lblFromHour" CssClass="label" Text="Hour From" 
                    meta:resourcekey="lblFromHourResource1" />
                <asp:TextBox ID="txtFromHour" runat="server" CssClass="textbox" 
                    meta:resourcekey="txtFromHourResource1"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvFromHour" ControlToValidate="txtFromHour" 
                    ErrorMessage="*" ValidationGroup="Process" 
                    meta:resourcekey="rfvFromHourResource1"/>

                <asp:Label runat="server" ID="lblFromMinute" CssClass="label" 
                    Text="Minute From" meta:resourcekey="lblFromMinuteResource1" />
                <asp:TextBox ID="txtFromMinute" runat="server" CssClass="textbox" 
                    meta:resourcekey="txtFromMinuteResource1"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvFromMinute" ControlToValidate="txtFromMinute" 
                    ErrorMessage="*" ValidationGroup="Process" 
                    meta:resourcekey="rfvFromMinuteResource1"/>
            </div>
            <div class="buttonModule">
                <asp:Label runat="server" ID="lblToDate" CssClass="label" Text="Date To" 
                    meta:resourcekey="lblToDateResource1" />
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
                    ErrorMessage="*" ValidationGroup="Process" 
                    meta:resourcekey="rfvToDateResource1"/>

                <asp:Label runat="server" ID="lblToHour" CssClass="label" Text="Hour To" 
                    meta:resourcekey="lblToHourResource1" />
                <asp:TextBox ID="txtToHour" runat="server" CssClass="textbox" 
                    meta:resourcekey="txtToHourResource1"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvToHour" ControlToValidate="txtToHour" 
                    ErrorMessage="*" ValidationGroup="Process" 
                    meta:resourcekey="rfvToHourResource1"/>

                <asp:Label runat="server" ID="lblToMinute" CssClass="label" Text="Minute To" 
                    meta:resourcekey="lblToMinuteResource1" />
                <asp:TextBox ID="txtToMinute" runat="server" CssClass="textbox" 
                    meta:resourcekey="txtToMinuteResource1"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvToMinute" ControlToValidate="txtToMinute" 
                    ErrorMessage="*" ValidationGroup="Process" 
                    meta:resourcekey="rfvToMinuteResource1"/>
                <asp:CustomValidator runat="server" ID="cvPeriod" ErrorMessage="Date from must be before date to. Period must be smaller than 12 hours." 
                    ValidationGroup="Process" Display="Dynamic" 
                    OnServerValidate="ValidatePeriod" meta:resourcekey="cvPeriodResource1" /> 
                <asp:Button runat="server" ID="btnProcess" Text="Process" 
                    OnClick="BtnProcessOnClick" CssClass="button ok" ValidationGroup="Process" 
                    meta:resourcekey="btnProcessResource1" />
            </div>
        </div>
    </form>
</body>
</html>
