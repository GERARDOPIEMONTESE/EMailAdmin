using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Domain.External
{
    public class ClienteInfo
    {
        public string apellido { get; set; }
        public string nombre { get; set; }
        public string fecNacimiento { get; set; }
        public int codigo { get; set; }
    }
}
