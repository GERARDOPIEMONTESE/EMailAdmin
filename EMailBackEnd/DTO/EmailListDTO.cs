using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using System.Collections.Generic;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data.Interfaces;

namespace EMailAdmin.BackEnd.DTO
{
    public class EMailListDTO : ObjetoPersistido
    {
        #region Constant

        private const string NAME = "EMailList";

        #endregion Constant

        #region Properties
        public int IdEmailList { get; set; }
        public string Pais { get; set; }
        public string EmailListType { get; set; }
        public string Code { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }

        #endregion
        
        public override string ObtenerNombre()
        {
            return NAME;
        }
    }

    
    public class EMailListUsuarioDTO : ObjetoPersistido
    {
        #region Constant

        private const string NAME = "EMailList";

        #endregion Constant

        #region Properties

        public int IdUsuario { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public Idioma Idioma { get; set; }
        
        #endregion

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
