using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.ExternalServices.Domain;
using FrameworkDAC.Parametro;
using EMailAdmin.ExternalServices.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data.Interfaces;

namespace EMailAdmin.BackEnd.Data
{
    public class DAONiceTrip : DAOObjetoNegocio<BaseEnvio>, IDAONiceTrip
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(BaseEnvio objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.CountryCode = Convert.ToInt32(dr["PAIS"]);
            objetoPersistido.VoucherCode = dr["CODIGO"].ToString();
            objetoPersistido.AgencyCode = dr["AGENCIA"].ToString();
            objetoPersistido.BranchNumber = Convert.ToInt32(dr["SUC_AGENCIA"]);
            objetoPersistido.Amount = dr["TARIFA_IMPRESA"].ToString();
            objetoPersistido.ProductCode = dr["PRODUCTO"].ToString();
            objetoPersistido.RateCode = dr["COD_TARIFA"].ToString();
            objetoPersistido.PaxType = dr["TIPO_PAX_VOUCHER"].ToString();
            objetoPersistido.EffectiveStartDate = Convert.ToDateTime(dr["FEC_VIG_INIC"].ToString()).ToShortDateString();
            objetoPersistido.EffectiveEndDate = Convert.ToDateTime(dr["FEC_VIF_FIN"].ToString()).ToShortDateString();
            objetoPersistido.Days = dr["CANT_DIAS"].ToString();
            objetoPersistido.Area = dr["AREA"].ToString();
            objetoPersistido.PaxName = dr["NOMBRE"].ToString();
            objetoPersistido.PaxSurname = dr["APELLIDO"].ToString();
            objetoPersistido.PaxPassport = dr["PASAPORTE"].ToString();
            objetoPersistido.PaxEMail = dr["EMAIL"].ToString();
            objetoPersistido.PaxAge = dr["EDAD"].ToString();
            objetoPersistido.PaxPhone = dr["TEL_PARTICULAR"].ToString();
            objetoPersistido.PaxAddress = dr["DOMICILIO"].ToString();
            objetoPersistido.EmergencyContact = dr["EMERG_CONTACTO"].ToString();
            objetoPersistido.EmergencyAddress = dr["EMERG_DOMICILIO"].ToString();
            objetoPersistido.EmergencyPhone = dr["EMERG_TEL1"].ToString();

            objetoPersistido.bEliminar = Convert.ToBoolean(dr["Eliminar"]);
        }       
    
        
        public IList<BaseEnvio> Find()
        {
            Parametros parameters = new Parametros();
            return Buscar(new Filtro(parameters, "dbo.NiceTrip_Emails")); 
        }

        protected override string StoredProcedureEliminar(FrameworkDAC.Negocio.ObjetoNegocio ObjetoNegocio)
        {
            return "dbo.NiceTrip_E";
        }

        protected override Parametros ParametrosEliminar(BaseEnvio ObjetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("CountryCode", ObjetoNegocio.CountryCode);
            parameters.AgregarParametro("VoucherCode", ObjetoNegocio.VoucherCode);
            parameters.AgregarParametro("AgencyCode", ObjetoNegocio.AgencyCode);
            parameters.AgregarParametro("BranchNumber", ObjetoNegocio.BranchNumber);

            return parameters;
        }


        protected override Parametros ParametrosCrear(BaseEnvio objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosModificar(BaseEnvio objetoNegocio)
        {
            throw new NotImplementedException();
        }

    }
}
