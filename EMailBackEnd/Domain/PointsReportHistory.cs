using System;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Domain
{
    public class PointsReportHistory : ObjetoNegocio
    {
        #region Constants

        private const string NAME = "PointsReportHistory";

        #endregion

        #region Properties

        public DateTime ReportDate { get; set; }

        #endregion

        #region Methods

        protected virtual IDAOObjetoNegocio GetConcreteDao()
        {
            return DAOLocator.Instance().GetDAOPointsReportHistory();                
        }

        protected virtual string GetConcreteName()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return GetConcreteDao();
        }

        public override string ObtenerNombre()
        {
            return GetConcreteName();
        }

        #endregion Methods
    }
}