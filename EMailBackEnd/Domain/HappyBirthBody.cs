using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class HappyBirthBody : ObjetoPersistido
    {
        private const string NAME = "HappyBirthBody";

        public string Body { get; set; }
        public string Locations { get; set; }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
