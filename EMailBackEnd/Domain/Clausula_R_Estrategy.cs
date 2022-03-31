using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class Clausula_R_Estrategy : ObjetoPersistido
    {
        private const string NAME = "Clausula_R_Estrategy";

        public int CodigoPais { get; set; }
        public int IdEstrategy { get; set; }
        public int IdTipoClausula { get; set; }
        public string ClausulaCode { get; set; }
        public bool Excluye { get; set; }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
