using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocioDatos.CapaHome;
using ControlMenu;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;
using CapaNegocioDatos.Servicios;

namespace EMailAdmin.Administration.SendMail
{
    public partial class TestPointsReport : CustomPage
    {
        #region Constants

        private const string ModuleCode = "ACNET";
        private const string UserACNETService = "mailservice@assist-card.com";
        private const string PassACNETService = "123456";

        #endregion Constants

        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            /*var dto = new PointsReportDTO
            {
                CountryCode = 540, //Convert.ToInt32(Request.QueryString["country"]), 
                //VoucherCode = Request.QueryString["voucher"],
                IdLanguage = 1,
                ModuleCode = "EMailAdmin", //ModuleCode,
                ReportDate = DateTime.Now
            };*/

            //var outBuf = ServiceLocator.Instance().GetSendMailService().GetAttachments(dto);
            //ServiceLocator.Instance().GetSendMailService().SendMailPoints(dto);

            /*ACCOMQuoteDTO dto = new ACCOMQuoteDTO();
            dto.ModuleCode = "ACCOM";
            dto.IdLanguage = 1;

            ServiceLocator.Instance().GetSendMailService().SendMailACCOMQuote(dto);*/


            ServicioCuentaACNet s = new ServicioCuentaACNet();
            s.InvocarWebMethodData("<replicar><AGENCIAS xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><Pais>560</Pais><Codigo>4672</Codigo><CodigoSuc>0</CodigoSuc><RazonSocial>A TI VIAJES Y TMO. FRANCISCO G. OJEDA PINO E.I.R.L</RazonSocial><Promotor>72</Promotor><NombreComercial>ATI VIAJES</NombreComercial><Domicilio>AV 11 DE SEPTIEMBRE 1945 OF 60</Domicilio><Ciudad>Santiago</Ciudad><Provincia>Region Metropolitana</Provincia><Cp>1</Cp><Telefono>2053660</Telefono><Contacto /><Fax> </Fax><Cuit>76.391.550-6</Cuit><TipoCliente>2</TipoCliente><TipoBonificacion>2</TipoBonificacion><CondicionPago>CSH</CondicionPago><PorcBonificacion>20</PorcBonificacion><LimiteCredito>1000</LimiteCredito><CodAgenciaPpal>4672</CodAgenciaPpal><Sucursal>0</Sucursal><NroCalle /><Piso /><Puerta /><Email>test@test.com</Email><Emisor>EMI</Emisor><CondImpositiva>RI</CondImpositiva><Iata /><PaginaWeb /><FechaAlta>04/01/2012</FechaAlta><FechaBaja>09/03/2015</FechaBaja><DatosAdicionales>N</DatosAdicionales><RespNombre /><RespEmail /><RespTel /><RespFecNac>NULL</RespFecNac><GteVtasNombre /><GteVtasEmail /><GteVtasTel /><GteVtasFecNac>NULL</GteVtasFecNac><GteAdmNombre /><GteAdmEmail /><GteAdmTel /><GteAdmFecNac>NULL</GteAdmFecNac><PagoNombre /><PagoEmail /><PagoTel /><PagoFecNac>NULL</PagoFecNac><Zona>H01</Zona><Saldo>1000</Saldo><Gsa>0</Gsa><Mayorista>0</Mayorista><ShowTtr>1</ShowTtr><ShowTtrReloaded>1</ShowTtrReloaded><PromotorNombre>DAFNE MORA</PromotorNombre><Usuario>-3</Usuario><TipoAgencia>1</TipoAgencia><Seprit>N</Seprit><IdAgenciaPadre>4672</IdAgenciaPadre><IdSucursalPadre>0</IdSucursalPadre><IdTipoFacturacion>0</IdTipoFacturacion><Idioma>1</Idioma><CodigoCanal>8</CodigoCanal><codigoSusep /><domZona /><ventaDirecta>0</ventaDirecta><emisionRetroactiva>0</emisionRetroactiva><markupFijo>0</markupFijo><Categoria>AgvAC</Categoria></AGENCIAS><AGENCIAS_PDF_NEW xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><pais>560</pais><codigo>4672</codigo><codigoSuc>0</codigoSuc><strategy>NewSinCamposConTarifaStrategy</strategy></AGENCIAS_PDF_NEW><PRINT_WITH_JASPER xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><pais>560</pais><agencia>4672</agencia><agenciaSuc>0</agenciaSuc><muestraCodigoBarra>0</muestraCodigoBarra></PRINT_WITH_JASPER><AGENCIA_HABILITACION_PAGO xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><Pais>560</Pais><Agencia>4672</Agencia><codSuc>0</codSuc><Tipo>POSTNET</Tipo><Habilitado>0</Habilitado></AGENCIA_HABILITACION_PAGO></replicar>");

            
            /*Response.Expires = 0;
            Response.Buffer = true;
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=emisiones.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/ms-excel";
            Response.BinaryWrite(outBuf);
            Response.End();*/

        }

        #endregion
    }
}