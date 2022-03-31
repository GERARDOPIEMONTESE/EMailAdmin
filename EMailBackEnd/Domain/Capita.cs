using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using CapaNegocioDatos.CapaNegocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class Capita:ObjetoPersistido
    {        
        private const string NAME = "Capita";

        public string Codigo { get; set; }
        public String Descripcion { get; set; }
        public Plan Plan { get; set; }
        public Pais Pais { get; set; }

        public Capita()
        {
            Plan = new Plan();
        }
     
        public override string ObtenerNombre()
        {
            return NAME;
        }

        public int PlanId { get { return Plan.Id; } }
        public string PlanCodigo { get { return Plan.Codigo; } }
        public string PlanDescripcion { get { return Plan.Descripcion; } }        
    }

    public class Plan
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
}
