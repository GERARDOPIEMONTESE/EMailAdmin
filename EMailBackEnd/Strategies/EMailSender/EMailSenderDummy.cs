using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Strategies.EMailSender
{
    public class EMailSenderDummy : IEMailSenderStrategy
    {
        #region IEMailSenderStrategy Members

        protected override DTO.AbstractEMailDTO GetDto(string xml)
        {
            throw new NotImplementedException();
        }

        protected override Domain.Template GetTemplate(AbstractEMailDTO dto)
        {
            throw new NotImplementedException();
        }

        protected override void CompleteDto(AbstractEMailDTO dto)
        {
            throw new NotImplementedException();
        }

        protected override IList<Domain.Attachment> FindAttachemnts(AbstractEMailDTO dto, Template template)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
