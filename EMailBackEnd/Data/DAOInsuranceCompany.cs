using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOInsuranceCompany : DAOObjetoPersistido<InsuranceCompany>, IDAOInsuranceCompany
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(InsuranceCompany ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.CountryCode = Convert.ToInt32(dr["CountryCode"]);
            ObjetoPersistido.ProductCode = dr["ProductCode"].ToString();
            ObjetoPersistido.Name = dr["Name"].ToString();
        }

        public InsuranceCompany Get(int countryCode, string productCode)
        {
            Parametros parameters = new Parametros();
            parameters.AgregarParametro("CountryCode", countryCode);
            parameters.AgregarParametro("ProductCode", productCode);

            return Obtener(new Filtro(parameters, "dbo.InsuranceCompany_Tx_ProductCode"));
        }
    }
}
