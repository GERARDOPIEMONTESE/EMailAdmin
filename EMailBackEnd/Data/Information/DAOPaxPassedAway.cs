using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain.Information;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data.Information
{
    public class DAOPaxPassedAway : DAOObjetoNegocio<PaxPassedAway>, IDAOPaxPassedAway      
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosCrear(PaxPassedAway objetoNegocio)
        {
            Parametros parameters = new Parametros();

            parameters.AgregarParametro("CountryCode", objetoNegocio.CountryCode);
            parameters.AgregarParametro("VoucherCode", objetoNegocio.VoucherCode);
            parameters.AgregarParametro("NationalId", objetoNegocio.NationalId);
            parameters.AgregarParametro("IsDead", objetoNegocio.IsDead);

            return parameters;
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosModificar(PaxPassedAway objetoNegocio)
        {
            Parametros parameters = new Parametros();

            parameters.AgregarParametro("CountryCode", objetoNegocio.CountryCode);
            parameters.AgregarParametro("VoucherCode", objetoNegocio.VoucherCode);
            parameters.AgregarParametro("NationalId", objetoNegocio.NationalId);
            parameters.AgregarParametro("IsDead", objetoNegocio.IsDead);

            return parameters;
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosEliminar(PaxPassedAway ObjetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override void Completar(PaxPassedAway objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdPaxPassedAway"]);
            objetoPersistido.CountryCode = Convert.ToInt32(dr["CountryCode"]);
            objetoPersistido.VoucherCode = dr["VoucherCode"].ToString();
            objetoPersistido.NationalId = dr["NationalId"].ToString();
            objetoPersistido.IsDead = Convert.ToBoolean(dr["IsDead"]);
        }

        #region IDAOPaxPassedAway Members

        public PaxPassedAway Get(int id)
        {
            return Obtener(id);
        }

        public PaxPassedAway Get(int countryCode, string voucherCode, string nationalId)
        {
            Parametros parameters = new Parametros();
            parameters.AgregarParametro("CountryCode", countryCode);
            parameters.AgregarParametro("VoucherCode", voucherCode);
            parameters.AgregarParametro("NationalId", nationalId);

            return Obtener(new Filtro(parameters, "dbo.PaxPassedAway_Tx_Parameters"));
        }

        #endregion
    }
}
