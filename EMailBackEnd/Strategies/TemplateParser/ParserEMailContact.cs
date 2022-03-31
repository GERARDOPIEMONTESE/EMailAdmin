using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Strategies.TemplateParser
{
    public class ParserEMailContact : ITemplateParserStrategy
    {
        #region ITemplateParserStrategy Members

        public string Parse(TemplateParserContext context)
        {
            if (context.Dto.IsPreview)
            {
                return context.Name;
            }

            EMailContactType type = DAOLocator.Instance().GetDaoEMailContactType().GetByCode(context.Name);
            IList<EMailContact> iContact = DAOLocator.Instance().GetDaoEMailContact().
                Find(type.Id, context.Dto.AssociationGroupDto.IdLocation);

            return iContact.Count == 0 ? "" : iContact[0].GetContactContent(context.Dto.IdLanguage).ContentText;
        }

        #endregion
    }
}
