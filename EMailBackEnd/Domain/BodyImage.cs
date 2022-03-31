using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Domain
{
    public class BodyImage : ObjetoNegocio
    {
        private const string NAME = "BodyImage";

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoBodyImage();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
