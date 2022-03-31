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
    public class DAOWelcomeBackInformation : DAOObjetoPersistido<WelcomeBackInformation>, IDAOWelcomeBackInformation
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(WelcomeBackInformation objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.CountryCode = Convert.ToInt32(dr["PAIS"]);
            objetoPersistido.VoucherCode = dr["CODIGO"].ToString();
            objetoPersistido.AgencyCode = dr["AGENCIA"].ToString();
            objetoPersistido.BranchNumber = Convert.ToInt32(dr["AGENCIA_SUC"]);
            objetoPersistido.PaxEMail = dr["EMAIL"].ToString();
            objetoPersistido.PaxName = dr["NOMBRE"].ToString();
            objetoPersistido.PaxSurname = dr["APELLIDO"].ToString();
        }
        
        #region IDAOWelcomeBackInformation Members

        public IList<WelcomeBackInformation> FindEffectiveEndDate()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.WelcomeBackInformation_Tx_Parameters"));
        }

        #endregion
    }
}
