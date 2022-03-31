using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Strategies.TemplateParser
{
    public class ParserSignature : ITemplateParserStrategy
    {
        #region ITemplateParserStrategy Members

        public string Parse(TemplateParserContext context)
        {
            if (context.Dto.IsPreview)
            {
                return context.Name;
            }

            var type = DAOLocator.Instance().GetDaoSignatureType().GetByCode(context.Name);
            var signatures = DAOLocator.Instance().GetDaoSignature().
                FindByFilters(type.Id, context.Dto.AssociationGroupDto.IdLocation);
            return signatures.Count == 0 ? "" : signatures[0].GetContent(context.Dto.IdLanguage).Content;
        }

        #endregion
    }
}
