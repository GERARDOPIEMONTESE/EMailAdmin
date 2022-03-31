using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CapaNegocioDatos.CapaHome;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Reports.Objects;
using iTextSharp.text;
using iTextSharp.text.pdf;
using EMailAdmin.BackEnd.DTO.EkitBenefits;
using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.Utils;

namespace EMailAdmin.BackEnd.Strategies.Attachment
{
    public class PointsReportAttachStrategy : AttachStrategy, IAttachStrategy
    {
        #region Constants

        private const string ATTACH_NAME = "emisiones.xls";

        private const string ATTACH_TYPE = "application/vnd.xls";

        #endregion Constants

        public PointsReportAttachStrategy()
        {
            ATTACHNAME = ATTACH_NAME;
            ATTACHTYPE = ATTACH_TYPE;
        }

        #region Properties
        #endregion

        #region IAttachStrategy Members

        public IList<AttachmentItem> GetAttachmentItems(AbstractEMailDTO dto)
        {
            IdLanguage = dto.IdLanguage;
            IdStrategy = dto.IdStrategy;

            IList<AttachmentItem> items = new List<AttachmentItem>();
            byte[] content = GetAttachContent(dto);

            var item = new AttachmentItem
            {
                Name = GetAttachName(),
                Description = GetAttachName(),
                Type = GetAttachType(),
                Language = IdiomaHome.Obtener(dto.IdLanguage),
                Content = content,
                Dimenssion = content == null ? 0 : content.Length
            };

            items.Add(item);

            return items;
        }

        #endregion

        #region Private methods
        
        private byte[] GetAttachContent(AbstractEMailDTO dto)
        {
            PointsReportDTO rptDto = (PointsReportDTO)dto;

            // Get last report datetime
            rptDto.ReportDate = DAOLocator.Instance().GetDAOPointsReportHistory().ObtenerFechaUltimoReporte();

            IList<VoucherACNETDTO> vouchers = GetVouchersPointsList(rptDto.ReportDate, DateTime.Now);
            string html = ExportHelper.ToHtml(vouchers);
            return Encoding.UTF8.GetBytes(html);
        }

        private IList<VoucherACNETDTO> GetVouchersPointsList(DateTime dateFrom, DateTime dateTo)
        {
            IList<VoucherACNETDTO> vouchers = DAOLocator.Instance().GetDAOVoucherACNETDTO().Find(dateFrom, dateTo);
            return vouchers;
        }

        #endregion // Private methods
    }
}