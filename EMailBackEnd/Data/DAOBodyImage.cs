using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data.Interfaces;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOBodyImage : DAOObjetoNegocio<BodyImage>, IDAOBodyImage
    {
        protected override FrameworkDAC.Parametro.Parametros ParametrosCrear(BodyImage ObjetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosModificar(BodyImage ObjetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosEliminar(BodyImage ObjetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override void Completar(BodyImage ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            throw new NotImplementedException();
        }

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }
    }
}
