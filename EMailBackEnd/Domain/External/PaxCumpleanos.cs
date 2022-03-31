using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain.External
{
    public class PaxCumpleanos : ObjetoPersistido
    {
        public int  COUNTRYCODE{ get; set; }
        public string LANGUAGE { get; set; }
        public string NATIONALID { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string EMAIL { get; set; }
        public string CELLPHONE { get; set; }
        public DateTime BIRTHDATE { get; set; }
        public bool debug { get; set; }
        public Nullable<int> IDCLIENTEUNICO { get; set; }

        public override string ObtenerNombre()
        {
            return "PaxCumpleanos";
        }

       
    }
}
