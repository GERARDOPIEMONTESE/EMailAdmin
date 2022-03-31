using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    class DAOGetReceivedConditions : DAOObjetoNegocio<ReceivedConditions>, IDAOGetReceivedConditions
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(ReceivedConditions objetoPersistido, SqlDataReader dr)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosCrear(ReceivedConditions objetoNegocio)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTemplate", objetoNegocio.IdTemplate);
            parameters.AgregarParametro("CountryCode", objetoNegocio.CountryCode);
            parameters.AgregarParametro("VoucherCode", objetoNegocio.VoucherCode);

            return parameters;
        }

        protected override Parametros ParametrosModificar(ReceivedConditions objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(ReceivedConditions objetoNegocio)
        {
            throw new NotImplementedException();
        }
    }
}
