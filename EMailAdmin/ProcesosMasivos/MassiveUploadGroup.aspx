<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MassiveUploadGroup.aspx.cs" Inherits="EMailAdmin.ProcesosMasivos.MassiveUploadGroup" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit"  TagPrefix="ajx"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Carga Masiva de Grupo de asociaciones</title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/loading.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="PaginaPortal">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" > 
            <ProgressTemplate> 
                <div id="progressBackgroundFilter"></div> 
                <div id="processMessage" align="center">
                    <p>Loading...</p>
                    <img alt="Loading..." src="../IMG/loading.gif"  /> 
                </div> 
            </ProgressTemplate> 
        </asp:UpdateProgress>
        <div id="loader" runat="server" style="display: none;"> 
            <div id="progressBackgroundFilter"></div> 
            <div id="processMessage" align="center">
                <p>Loading...</p>
                <img alt="Loading..." src="../IMG/loading.gif"  /> 
            </div> 
        </div> 
        <div class="form m-center medium">
        <fieldset>
            <legend>
                <h2>
                    <asp:Literal ID="ltrTitle" runat="server" Text="Alta masiva de grupos" meta:resourcekey="ltrTitle"></asp:Literal>
                </h2>
            </legend>
            <p class="desc">
                <asp:Literal ID="ltrDescription" runat="server" Text="Complete los datos y suba el archivo excel con el formato"
                    meta:resourcekey="ltrDescription"></asp:Literal>
                <asp:HyperLink ID="lnkSpecified" runat="server" Text=" especificado." meta:resourcekey="lnkSpecified"
                    NavigateUrl="~/examples/ejemploNuevoGrupo.xlsx" Target="_blank"></asp:HyperLink><br />
                <asp:Literal ID="ltrInstructions" runat="server" Text="Para completarlo siga "
                    meta:resourcekey="ltrInstructions"></asp:Literal>
                <asp:LinkButton ID="lnkInstructions" runat="server" Text=" las instrucciones." meta:resourcekey="lnkInstructions"
                    NavigateUrl="~/examples/ejemploNuevoGrupo.xlsx" Target="_blank"></asp:LinkButton>
            </p>
            <div class="form-rows">
                <div class="row">
                    <asp:Label ID="lblTemplate" runat="server" Text="Template" meta:resourcekey="lblTemplate"></asp:Label>
                    <asp:DropDownList ID="ddlTemplate" runat="server" DataTextField="Name" DataValueField="Id">
                    </asp:DropDownList>
                </div>
                <div class="row">
                    <asp:Label ID="lblModule" runat="server" Text="Modulo" meta:resourcekey="lblModule"></asp:Label>
                    <asp:DropDownList ID="ddlModule" runat="server" DataTextField="Nombre" DataValueField="Nombre">
                    </asp:DropDownList>
                </div>
                <div class="row">
                    <asp:Label ID="lblReceive" runat="server" Text="Recibe" meta:resourcekey="lblReceive"></asp:Label>
                    <asp:CheckBox ID="chkReceive" runat="server" ></asp:CheckBox>
                </div>
                <div class="row">
                    <asp:Label ID="lblName" runat="server" Text="Nombre" meta:resourcekey="lblName"></asp:Label>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </div>
                <div class="row">
                    <asp:Label ID="lblType" runat="server" Text="Tipo" meta:resourcekey="lblType"></asp:Label>
                    <asp:DropDownList ID="ddlType" runat="server" DataTextField="Description" DataValueField="Id">
                    </asp:DropDownList>
                </div>
                <div class="row">
                    <asp:Label ID="lblFile" runat="server" Text="Archivo" meta:resourcekey="lblFile"></asp:Label>
                    <asp:FileUpload ID="fuExcelFile" runat="server" />
                </div>
                <div class="btn-cnt">
                    <asp:Button runat="server" ID="btnAccept" Text="Aceptar" CssClass="ok tall" ValidationGroup="Group"
                        OnClick="BtnAcceptOnClick" meta:resourcekey="btnAccept" OnClientClick="document.getElementById('loader').style.display = 'block';" />
                    <asp:Button runat="server" ID="btnCancel" Text="Cancelar" CssClass="cancel tall" OnClick="BtnCancelOnClick"
                        meta:resourcekey="btnCancel" />
                </div>
                <div class="msg-cnt">
                    <asp:Literal ID="ltrMessage" runat="server"></asp:Literal>
                </div>
            </div>
            <asp:Panel ID="cntGroup" runat="server" class="lst" Visible="false">
                <div class="row">
                    <div class="title">
                        <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div>
                        <asp:Label ID="lblCountry" runat="server" Text="Paises" meta:resourcekey="lblCountry"></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblCountries" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div>
                        <asp:Label ID="lblRate" runat="server" Text="Tarífas" meta:resourcekey="lblRate"></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblRates" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div>
                        <asp:Label ID="lblProduct" runat="server" Text="Productos" meta:resourcekey="lblProduct"></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblProducts" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div>
                        <asp:Label ID="lblBranch" runat="server" Text="Sucursales" meta:resourcekey="lblBranch"></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblBranches" runat="server"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
            <asp:HiddenField ID="hdnModal" runat="server" Value=" " />
            <ajx:ModalPopupExtender BackgroundCssClass="modalBackground"
                 ID="mpeOk" CancelControlID="BtnCloseInstrucctions" 
                PopupControlID="pnlInsrtuctions" RepositionMode="None" runat ="server"
                TargetControlID="lnkInstructions" DynamicServicePath="" Enabled="True" />
            <asp:Panel runat="server" ID="pnlInsrtuctions"  CssClass="modalBackgroundRestore m-center form medium" style="display: none;">
                <p>
                    <asp:Literal ID="ltrInstructionsText" runat="server" meta:resourcekey="ltrInstructionsText" 
                    Text="Antes de empezar descarge el archivo de prueba que se encuentra en esta misma pagina.&lt;br&gt; 
                    Vera que el archivo tiene la extension xlsx, pero podra subir éste tanto como una version de excel 2003 (xls) o incluso un archivo separado por comas (csv). &lt;br&gt;
                    En la primera fila vera que se encuentran los nombres de las columnas que debera completar; esta fila nunca debe ser borrarda. &lt;br&gt;
                    La primera columna hace referencia al tipo de condicion que este grupo tendra. Los posibles tipos de condiciones son 4 y las abrebiaturas 
                    que se utilizaran en el archivo para hacer referencia a ellos seran estas: &lt;br&gt; &lt;br&gt;
                    CTR (refiere a codigo de Pais), ACC (refiere a sucursal), PRO (refiere a producto), RTE (refiere a tarifa) &lt;br&gt; &lt;br&gt;
                    Las siguientes columnas (tantas como tipos de condiciones) indican donde ud debera ubicar el valor el cual indica la primera columna. &lt;br&gt;
                    Los valores deberan respetar los siguientes formatos para cada tipo: &lt;br&gt; &lt;br&gt;
                    CTR - debera ser simplemente el codigo del pais ej: 540 (argentina) &lt;br&gt;
                    ACC - esta conformado de del codigo de pais / codigo de cuenta / codigo de sucursal ej: 540/10/0 (alguna sucursal de alguna cuenta en argentina) &lt;br&gt;
                    PRO - esta compuesto por codigo de pais / codigo de producto ej: 540/R3 (Privileged argentina) &lt;br&gt;
                    RTE - compuesto por codigo de pais / codigo de producto /codigo de tarifa ej: 540/R3/10036 (alguna tarifa del producto privileged de argentina) &lt;br&gt; &lt;br&gt;
                    Debera agregar tantas filas como condiciones crea necesario hacen al grupo."></asp:Literal>   
                </p>
                <div class="btn-cnt">
                    <asp:Button runat="server" ID="BtnCloseInstrucctions" Text="Ok" CssClass="button ok" meta:resourcekey="btnAcceptOkResource1" />
                </div>    
            </asp:Panel>
            </fieldset>
        </div>
    </div>
    </form>
</body>
</html>
