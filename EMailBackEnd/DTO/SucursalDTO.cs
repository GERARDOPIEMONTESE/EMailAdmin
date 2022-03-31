using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaNegocioDatos.CapaNegocio;

namespace EMailAdmin.BackEnd.DTO
{
    public class SucursalDTO
    {
        public string Domicilio { get; set; }
        public string Provincia { get; set; }
        public string Telefono { get; set; }
        public string Distrito { get; set; }
        public string Departamento { get; set; }
        public string Fax { get; set; }
        
        public SucursalDTO() 
        { 
        }

        public SucursalDTO(Sucursal branch)
        {
            if (branch.PersonaJuridica.Direcciones.Count > 0)
            {
                this.Domicilio = branch.PersonaJuridica.Direcciones[0].Domicilio;
                this.Provincia = branch.PersonaJuridica.Direcciones[0].Provincia;
                this.Telefono = branch.PersonaJuridica.Direcciones[0].Telefono;
                this.Distrito = branch.PersonaJuridica.Direcciones[0].Localidad;
                this.Fax = branch.PersonaJuridica.Direcciones[0].Fax;
            }
            this.Domicilio = "";
            this.Provincia = "";
            this.Telefono = "";
            this.Distrito = "";
            this.Fax = "";
        }
    }
}
