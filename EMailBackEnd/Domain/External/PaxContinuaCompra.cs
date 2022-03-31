using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain.External
{
    public class PaxContinuaCompra : ObjetoPersistido
    {
        public int LISTA_ID { get; set; }
        public int IDQUOTELOG { get; set; }
        public string ISO2CODE { get; set; }
        public string LANGUAGE { get; set; }
        public string FULLNAME { get; set; }
        public string EMAIL { get; set; }
        public string URLBASE64 { get; set; }
        public Nullable<int> IDCLIENTEUNICO { get; set; }

        public override string ObtenerNombre()
        {
            return "PaxContinuaCompra";
        }
    }
}