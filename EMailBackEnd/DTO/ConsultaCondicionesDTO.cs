using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.DTO
{
    public class ConsultaCondicionesDTO
    {
        public int CodigoPais { get; set; }
        public bool Anual { get; set; }
        public int Edad { get; set; }
        public int IdTipoPlan { get; set; }
        public int IdTipoModalidad { get; set; }
        public int Categoria { get; set; }
        public string CodigoProducto { get; set; }
        public string CodigoTarifa { get; set; }
        public int Agencia { get; set; }
        public int Sucursal { get; set; }
        public ConsultaPadre ConsultaPadre { get; set; }
        public Upgrades Upgrades { get; set; }
    }

    public class ConsultaPadre
    {
        public int CodigoPais { get; set; }
        public string CodigoProducto { get; set; }
        public string CodigoTarifa { get; set; }
        public int Anual { get; set; }
        public int Edad { get; set; }
        public int IdTipoPlan { get; set; }
        public int IdTipoModalidad { get; set; }

        public ConsultaPadre()
        {
            Edad = 30;
        }
    }
    public class Upgrades
    {
        public ConsultaCondicionesUpgradeDTO ConsultaCondicionesUpgradeDTO { get; set; }
    }

    public class ConsultaCondicionesUpgradeDTO
    {
        public int CodigoUpgrade { get; set; }
        public int CodigoTarifa { get; set; }
        public int Categoria { get; set; }
    }
}
