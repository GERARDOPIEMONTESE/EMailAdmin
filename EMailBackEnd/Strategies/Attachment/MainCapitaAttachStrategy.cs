using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using CapaNegocioDatos.CapaHome;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Utils;

namespace EMailAdmin.BackEnd.Strategies.Attachment
{
    class MainCapitaAttachStrategy: AttachStrategy, IAttachStrategy
    {
        #region Constants

        private const string ATTACH_NAME = "CAPITA.pdf";

        private const string ATTACH_TYPE = "application/pdf";

        #endregion Constants

        public MainCapitaAttachStrategy()
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
            //rptDto.ReportDate = DAOLocator.Instance().GetDAOCapita().FindAll();

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
