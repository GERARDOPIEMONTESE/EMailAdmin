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
    public class DAODistributionType : DAOObjetoCodificado<DistributionType>, IDAODistributionType
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(DistributionType ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.Id = Convert.ToInt32(dr["IdDistributionType"]);
            ObjetoPersistido.Codigo = dr["Code"].ToString();
            ObjetoPersistido.Descripcion = dr["Description"].ToString();
        }

        #region IDAODistributionType Members

        public DistributionType Get(int idDistributionType)
        {
            return Obtener(idDistributionType);
        }

        public DistributionType Get(string code)
        {
            Parametros parameters = new Parametros();
            parameters.AgregarParametro("Code", code);

            return Obtener(new Filtro(parameters, "dbo.DistributionType_Tx_Code"));
        }

        public IList<DistributionType> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.DistributionType_Tx_Code"));
        }

        #endregion
    }
}
