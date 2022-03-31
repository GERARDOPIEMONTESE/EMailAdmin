<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LegalList.aspx.cs" Inherits="EMailAdmin.Legal.LegalList" culture="auto" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>Legal List</title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/loading.css" rel="stylesheet" type="text/css" />
 
     <style type="text/css">
        pre {
            display: block;
            font-family: monospace;
            white-space: pre;
            margin: 1em 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"/>
        <div class="PaginaPortal">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" > 
                <ProgressTemplate> 
                    <div id="progressBackgroundFilter"></div> 
                    <div id="processMessage" align="center"><p>Loading...</p>
                        <img alt="Loading..." src="../IMG/loading.gif"  /> 
                    </div> 
                </ProgressTemplate> 
            </asp:UpdateProgress>
            <div class="buttonModule">
                <asp:Label runat="server" ID="lblCountry" Text="Country" CssClass="label" />
                <asp:TextBox runat="server" ID="txtCountryCode" CssClass="textbox" />
                <asp:Label runat="server" ID="lblVoucher" Text="Voucher" CssClass="label"  />
                <asp:TextBox runat="server" ID="txtVoucher" CssClass="textbox" />
                <asp:Label runat="server" ID="lblEmail" Text="Email" CssClass="label"  />
                <asp:TextBox runat="server" ID="txtEmail" CssClass="textbox"  />
                <asp:Label runat="server" ID="lblTemplate" Text="Template" CssClass="label"></asp:Label>
                <asp:DropDownList runat="server" ID="ddlTemplate" CssClass="dropdown" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                <asp:Button runat="server" ID="btnSearch" Text="Search"  OnClick="BtnSearchOnClick" CssClass="button ok" />
                <asp:Label runat="server" ID="lblErrorMessage" Text="* Complete Country or Template to Search" Visible="false" ForeColor="Red"></asp:Label>
            </div>
            <div class="gridList">
                <div class="gridFont">
                    <asp:GridView 
                        ID="grvLegal" 
                        runat="server" 
                        PageSize="15" 
                        AllowPaging="True"
                        GridLines="Vertical" 
                        AutoGenerateColumns="False"
                        CssClass="tbl-generic m-center"
                        PagerStyle-HorizontalAlign="Center"
                        OnRowCommand="GrvLegalRowCommand"
                        OnRowDataBound="GrvLegalRowDataBound"
                        OnPageIndexChanging="GrvLegalPageIndexChange"  >
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" />
                            <asp:BoundField DataField="CountryCode" HeaderText="Country" 
                                ItemStyle-Width="70px" />                
                            <asp:BoundField DataField="VoucherCode" HeaderText="Voucher Code" 
                                ItemStyle-Width="150px" />
                            <asp:BoundField DataField="PaxName" HeaderText="Pax Name" 
                                ItemStyle-Width="100px" />
                            <asp:BoundField DataField="EmissionDateForGrid" HeaderText="Emission Date" 
                                ItemStyle-Width="100px" />
                            <asp:BoundField DataField="SentDateForGrid" HeaderText="Sent Date" 
                                ItemStyle-Width="100px" DataFormatString />
                            <asp:BoundField DataField="Email" HeaderText="Email" 
                                ItemStyle-Width="100px" />
                            <asp:BoundField DataField="IdLote" HeaderText="Lote" 
                                ItemStyle-Width="100px" />
                            <asp:BoundField DataField="ErrorMessageForGrid" HeaderText="Error" 
                                            ItemStyle-Width="150px" />
                            <asp:BoundField DataField="ErrorDateForGrid" HeaderText="Error Date" 
                                ItemStyle-Width="100px" />
                            <asp:BoundField DataField="Process" HeaderText="Process Status" ItemStyle-Width="100px"/>
                            <asp:BoundField DataField="TemplateName" HeaderText="Template" ItemStyle-Width="100px"/>
                            <asp:ButtonField ButtonType="Button" CommandName="View" Text="View" ControlStyle-CssClass="ok small"
                                             HeaderText="View" ItemStyle-Width="60px" />
                            <asp:ButtonField ButtonType="Button" CommandName="ViewXml" Text="Xml" ControlStyle-CssClass="cancel small"
                                             HeaderText="Xml" ItemStyle-Width="60px" />
                            <asp:ButtonField ButtonType="Button" CommandName="ViewBody" Text="Body" ControlStyle-CssClass="cancel small"
                                             HeaderText="Body" ItemStyle-Width="60px" />
                        </Columns>
                        <RowStyle CssClass="gridView_RowStyle" />
                        <PagerStyle CssClass="gridView_Pager_Style_Row" />
                        <SelectedRowStyle CssClass="gridView_Selected_Row" />
                        <HeaderStyle CssClass="gridView_Row_Header" />
                        <EditRowStyle CssClass="gridView_Edit_Row" />
                        <AlternatingRowStyle CssClass="gridView_Alternative_Row_Data" />
                        <EmptyDataTemplate>
                            <asp:label ID="lblNoData" runat="server" CssClass="text_Normal" 
                                Text="No hay registros." />
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
            <div id="panels">
                <asp:HiddenField runat="server" ID="hfdError"/>
                <asp:Panel runat="server" CssClass="modalBackgroundRestore" Style="width:750px;height:600px;display:none;overflow:scroll;" ID="pnlError">
                    <div class="formModule">
                        <fieldset>
                            <div class="module">
                                <asp:Label runat="server" ID="lblError" Width="700px"/>
                            </div>
                            <div class="module">
                                <asp:Button runat="server" ID="btnClose" CssClass="button cancel" Text="Close" />
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
