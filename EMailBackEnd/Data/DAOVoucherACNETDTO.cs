using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.DTO;
using FrameworkDAC.Parametro;
using EMailAdmin.BackEnd.Data.Interfaces;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOVoucherACNETDTO : DAOObjetoPersistido<VoucherACNETDTO>, IDAOVoucherACNETDTO
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(VoucherACNETDTO ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.CountryCode = dr["PAIS"].ToString();
            ObjetoPersistido.VoucherCode = dr["CODIGO"].ToString();
            ObjetoPersistido.ProductCode = dr["PRODUCTO"].ToString();
            ObjetoPersistido.RateCode = dr["COD_TARIFA"].ToString();
            ObjetoPersistido.IssueDate = string.Format("{0:M/d/yyyy HH:mm:ss}", dr["FECHA_EMISION"]);
            ObjetoPersistido.AgencyCode = dr["AGENCIA"].ToString();
            ObjetoPersistido.BranchNumber = dr["SUC_AGENCIA"].ToString();
            ObjetoPersistido.Modality = dr["MODALIDAD"].ToString();
            ObjetoPersistido.LastName = dr["APELLIDO"].ToString();
            ObjetoPersistido.Name = dr["NOMBRE"].ToString();
            ObjetoPersistido.Email = dr["EMAIL"].ToString();
            ObjetoPersistido.UniqueVoucherId = dr["UNIQUE_VOUCHER_ID"].ToString();
            ObjetoPersistido.DocNumber = dr["NRO_DOCUMENTO"].ToString();
            ObjetoPersistido.CountryName = dr["NOMBRE_PAIS"].ToString();
            ObjetoPersistido.Days = dr["CANT_DIAS"].ToString();
            ObjetoPersistido.RateAmount= dr["TARIFA_EMITIDA"].ToString();
            ObjetoPersistido.PromotionId = dr["ID_PROMOCION"].ToString();
            ObjetoPersistido.FamilyPlan = dr["PLAN_FAMILIA"].ToString();
            ObjetoPersistido.VoucherGroup = string.Format("{0:##\\.###\\.###}", dr["GRUPO_VOUCHER"]);
            ObjetoPersistido.BirthDate = string.Format("{0:M/d/yyyy HH:mm:ss}", dr["FEC_NACIMIENTO"]);
            ObjetoPersistido.ConcPaisProdTar = dr["ConcPaisProdTar"].ToString();
            ObjetoPersistido.ConcPaisAgvPrdTar = dr["ConcPaisAgvPrdTar"].ToString();
            ObjetoPersistido.Points = dr["POINTS"].ToString();
        }

        #region IDAOVoucherACNETDTO Members

        public IList<VoucherACNETDTO> Find(DateTime dateFrom, DateTime dateTo)
        {
            Parametros parameters = new Parametros();
            parameters.AgregarParametro("DateFrom", dateFrom);
            parameters.AgregarParametro("DateTo", dateTo);

            return Buscar(new Filtro(parameters, "Vouchers_Tx_Filter"));
        }

        public int VoucherPointsToExpire()
        {
            Parametros parameters = new Parametros();

            return Cantidad(new Filtro(parameters, "ACCOM_Prod.Points.BranchRatePoint_Tx_PointsToExpire", "VOUCHERS_COUNT"));
        }

        #endregion
    }
}
