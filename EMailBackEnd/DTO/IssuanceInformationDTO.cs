using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO.EkitBenefits;
using System.Collections;
using System.Runtime.Serialization;

namespace EMailAdmin.BackEnd.DTO
{
     [DataContract(Name = "VOUCHERALL")]
    public class IssuanceInformationDTO
    {
        [DataMember(Name = "datosVoucher")]
        public VoucherInformationDTO VoucherInformationDTO { get; set; }

        public GrupoClausulaDTO GrupoClausulaDTO { get; set; }
    }

    public class VoucherInformationDTO
    {
        public int idModalidad { get; set; }

        public ProductDTO producto { get; set; }

        public VoucherDTO voucher { get; set; }

        public ClientDTO cliente { get; set; }             
    }

    public class ProductDTO
    {
        public int pais { get; set; }

        public string codigo { get; set; }

        public string rubro { get; set; }

        public string nombre { get; set; }

        public string leyendaImpresion { get; set; }

        public string subfijoAImprimir { get; set; }

        public string area { get; set; }

        public string debitoAutomatico { get; set; }

        public string roadAssistance { get; set; }

        public string medicinaPrepaga { get; set; }

        public string programaMillage { get; set; }

        public int grupoFamiliar { get; set; }

        public string observaciones { get; set; }

        public string corporativo { get; set; }

        public string prefijoAImp { get; set; }

        public string tipoPasajero { get; set; }

        public int activaSeguro { get; set; }

        public string individual { get; set; }

        public string codigoSabre { get; set; }

        public string monedaImpresion { get; set; }

        public string monedaFactura { get; set; }

        public bool darBaja { get; set; }

        public int categoriaProducto { get; set; }

        public int tipoProducto { get; set; }
    }

    public class VoucherDTO
    {
        public int pais { get; set; }

        public int codigo { get; set; }

        public string tipoPaxVoucher { get; set; }

        public string sufijoVoucher { get; set; }

        public DateTime fechaEmision { get; set; }

        public int cliente { get; set; }

        public string agencia { get; set; }

        public string promotor { get; set; }

        public string producto { get; set; }

        public string codTarifa { get; set; }

        public int cantDias { get; set; }

        public float tarifaEmitida { get; set; }

        public float taxEmitida { get; set; }

        public float remesaEmitida { get; set; }

        public float tarifaFull { get; set; }

        public float taxFull { get; set; }

        public float remesaFull { get; set; }

        public float tarifaImpresa { get; set; }

        public float taxImpresa { get; set; }

        public float remesaImpresa { get; set; }

        public float tarifaFactura { get; set; }

        public float taxFactura { get; set; }

        public float remesaFactura { get; set; }

        public float cambioDolar { get; set; }

        public string monedaEmision { get; set; }

        public string codBonificacion { get; set; }

        public float porcBonificacion { get; set; }

        public string nroFactura { get; set; }

        public DateTime fecVigInic { get; set; }

        public DateTime fecVifFin { get; set; }

        public string area { get; set; }

        public string datosAdicionales { get; set; }

        public string codMedPrepaga { get; set; }

        public string planFamilia { get; set; }

        public string codAutResoluc { get; set; }

        public int tipoUsuario { get; set; }

        public string usuario { get; set; }

        public DateTime fecBaja { get; set; }

        public string nroTarjetaCred { get; set; }

        public string codVerificador { get; set; }

        public long grupoVoucher { get; set; }

        public string postfijo { get; set; }

        public int sucAgencia { get; set; }

        public IList tarifasVoucher { get; set; }

        public int paisCom { get; set; }

        public string tipoTarjetaCredito { get; set; }

        public float fee { get; set; }

        public string nombreTaker { get; set; }

        public string direccionTaker { get; set; }

        public string originalVoucher { get; set; }

        public string capita { get; set; }

        public int free { get; set; }

        public IList upgrades { get; set; }

        public string leyenda { get; set; }

        public int precompra { get; set; }

        public string datoAdic1 { get; set; }

        public string datoAdic2 { get; set; }

        public DateTime fecAlta { get; set; }

        public string tipoCarga { get; set; }

        public string codigoCounter { get; set; }

        public string sabrePnr { get; set; }

        public string ordenFacturacion { get; set; }

        public string precompraAgencia { get; set; }

        public int idPromocion { get; set; }

        public int primeroDeGrupo { get; set; }

        public string monedaImpresion { get; set; }

        public string monedaFactura { get; set; }

        public float cambioImpresion { get; set; }

        public float cambioFactura { get; set; }

        public int canalEmisor { get; set; }

        public int paisTriangulado { get; set; }

        public string agenciaTriangulada { get; set; }

        public int sucursalTriangulada { get; set; }

        public int dniCounter { get; set; }

        public string destinationCode { get; set; }

        public string service { get; set; }

        public string provider { get; set; }

        public string hotel { get; set; }

        public int effectiveDays { get; set; }

        public string amadeusUniqueId { get; set; }

        public int tarjeta { get; set; }

        public double costoViaje { get; set; }

        public float cargoEnvio { get; set; }

        public float incrementoInteresFinanciero { get; set; }

        public int idTarjetaCredito { get; set; }

        public int cuotasPagoTarjeta { get; set; }
    }

    public class ClientDTO
    {
        public int pais { get; set; }

        public int codigo { get; set; }

        public string apellido { get; set; }

        public string nombre { get; set; }

        public DateTime fecNacimiento { get; set; }

        public string sexo { get; set; }

        public int estadoCivil { get; set; }

        public string pasaporte { get; set; }

        public int tipoDocumento { get; set; }

        public string nroDocumento { get; set; }

        public string domCalle { get; set; }

        public string domNro { get; set; }

        public string domPiso { get; set; }

        public string domPuerta { get; set; }

        public string domCp { get; set; }

        public string domCiudad { get; set; }

        public string domProvincia { get; set; }

        public int domPais { get; set; }

        public int nacionalidad { get; set; }

        public string telParticular { get; set; }

        public string telComercial { get; set; }

        public string fax { get; set; }

        public string email { get; set; }

        public string emergContacto { get; set; }

        public string emergCalle { get; set; }

        public string emergNro { get; set; }

        public string emergPiso { get; set; }

        public string emergPuerta { get; set; }

        public string emergCp { get; set; }

        public string emergCiudad { get; set; }

        public string emergProv { get; set; }

        public int emergPais { get; set; }

        public string emergTel1 { get; set; }

        public string emergTel2 { get; set; }

        public string emergEmail { get; set; }

        public string datosAdicionales { get; set; }

        public int nroSocioMillage { get; set; }

        public string automovilMarca { get; set; }

        public string automovilModelo { get; set; }

        public string automovilPatente { get; set; }

        public string medPrepagaNroRecibo { get; set; }

        public DateTime medPrepagaFecRecibo { get; set; }

        public int nroCertificado { get; set; }

        public int edad { get; set; }

        public bool exist { get; set; }

        public int idCuenta { get; set; }

        public int codigoTarjeta { get; set; }
    }
}
