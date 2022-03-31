using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Reports.Objects
{
    public class VoucherReportObject
    {
        #region Properties

        public string Moneda { get; set; }
        public string NumeroVoucher { get; set; }
        public string Agencia { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Modalidad { get; set; }
        public string FechaEmision { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string NombreCompleto { get; set; }
        public string Edad { get; set; }
        public string Pasaporte { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public string ContactoNombre { get; set; }
        public string ContactoDireccion { get; set; }
        public string ContactoPais { get; set; }
        public string ContactoTelefono { get; set; }
        public string ContactoTelefonoAlt { get; set; }
        public string Destino { get; set; }
        public decimal Tarifa { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }

        public string EmergenciaPais { get; set; }
        public string EmergenciaTelefono { get; set; }
        public string EmergenciaTelGrat { get; set; }
        public string EmergenciaCentReg { get; set; }

        #endregion

    }
}
