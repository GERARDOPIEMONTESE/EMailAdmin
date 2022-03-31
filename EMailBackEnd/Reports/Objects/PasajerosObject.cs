using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Reports.Objects
{
    public class PasajerosObject
    {
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string FecNacimiento { get; set; }

        public PasajerosObject() { }

        public PasajerosObject(string apellido, string nombre, string fecNacimiento)
        {
            Apellido = apellido;
            Nombre = nombre;
            FecNacimiento = fecNacimiento;
        
        }
    }


}
