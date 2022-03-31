using System;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class EMailContactLocation : ObjetoNegocio
    {
        private const string NAME = "EMailContact_R_Location";

        #region Attributes

        #endregion

        #region Properties

        public int IdEMailContact { get; set; }

        public int IdLocation { get; set; }

        public string Name { get; set; }

        #endregion

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            throw new NotImplementedException();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}