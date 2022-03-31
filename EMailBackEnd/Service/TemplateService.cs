using System.Collections.Generic;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Strategies.TemplateParser;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Service
{
    public class TemplateService : ITemplateService
    {
        #region Properties

        private readonly IDictionary<string, ITemplateParserStrategy> _strategies =
            new Dictionary<string, ITemplateParserStrategy>();

        public IDAOTemplate DaoTemplate { get; set; }

        #endregion Properties

        #region Methods

        public TemplateService()
        {
            Init();
        }

        public string ParseBody(AbstractEMailDTO dto, Template template)
        {
            string body = template.GetBody(dto.IdLanguage);

            IDictionary<string, string> iText = GetVariableValues(body, dto, template);

            foreach (string key in iText.Keys)
            {
                body = body.Replace(key, iText[key]);
            }
            //Reprocess for context information added in variable texts
            iText = GetVariableValues(body, dto, template);
            foreach (string key in iText.Keys)
            {
                body = body.Replace(key, iText[key]);
            }

            return AddHeader(template, dto.IdLanguage) + body + AddFooter(template, dto.IdLanguage);
        }

        public string ParseBody(AbstractEMailDTO dto, Template template, string body)
        {            
            IDictionary<string, string> iText = GetVariableValues(body, dto, template);

            foreach (string key in iText.Keys)
            {
                body = body.Replace(key, iText[key]);
            }
            //Reprocess for context information added in variable texts
            iText = GetVariableValues(body, dto, template);
            foreach (string key in iText.Keys)
            {
                body = body.Replace(key, iText[key]);
            }

            return AddHeader(template, dto.IdLanguage) + body + AddFooter(template, dto.IdLanguage);
        }

        public string ParseBody(int idLanguage, int idLocation, Template template)
        {
            return ParseBody(idLanguage, idLocation, template, false);
        }

        public string ParseBody(int idLanguage, int idLocation, Template template, bool isPreview)
        {
            AbstractEMailDTO dto = new EMailPreviewDTO();
            dto.IdLanguage = idLanguage;
            dto.AssociationGroupDto = new AssociationGroupDTO { IdLocation = idLocation };
            dto.IsPreview = isPreview;

            return ParseBody(dto, template);
        }

        public void Save(Template template)
        {
            if (template.Name != "" && template.EffectiveStartDate <= template.EffectiveEndDate)
            {
                DaoTemplate.Persistir(template);
            }
            else
            {
                throw new NonSavedObjectException("Template not saved");
            }
        }

        public void SaveAssociations(IList<Group_R_Template> items, int idUser)
        {
            if (items != null && items.Count > 0)
            {
                DAOLocator.Instance().GetDaoGroup_R_Template().DeleteByIdTemplate(items[0].IdTemplate, idUser);
                foreach (Group_R_Template groupRTemplate in items)
                {
                    groupRTemplate.IdUsuario = idUser;
                    groupRTemplate.IdEstado = groupRTemplate.ObtenerCreado();
                    DAOLocator.Instance().GetDaoGroup_R_Template().Crear(groupRTemplate);
                }
            }
        }

        public void SaveAssociations(Group_R_Template item, int idUser)
        {
            SaveAssociations(new List<Group_R_Template> {item}, idUser);
        }

        public void Delete(Template template)
        {
            if (template.Id != 0)
            {
                DaoTemplate.Eliminar(template);
            }
            else
            {
                //throw new NonEliminatedObjectException("Signature not deleted");
            }
        }

        private void Init()
        {
            //TODO: sacar el hardcode, ver de ponerlo en settings
            _strategies.Add("IMAGE", new ParserContentImage());
            _strategies.Add("LINK", new ParserLink());
            _strategies.Add("CONTACT", new ParserEMailContact());
            _strategies.Add("SIGNATURE", new ParserSignature());
            _strategies.Add("VARIABLETEXT", new ParserVariableText());
            _strategies.Add("CTRYVTEXT", new ParserCountryVisibleText());
            _strategies.Add("UPGTEXT", new ParserUpgradeVariableText());
            _strategies.Add("TABLE", new ParserTable());
            _strategies.Add("CONDITION", new ParserConditionVariableText());
            _strategies.Add("GUID", new ParserGUID());
            _strategies.Add("PIXEL", new ParserPixel());
            _strategies.Add("CLAUSETEXT", new ParserClause());
            _strategies.Add("DYNT", new ParserDynamicTable());
        }

        protected virtual string AddImage(ContentImage contentImage, string divStyle="")
        {
            return contentImage == null || contentImage.Id == 0 ? "" :
                "<div"+ (divStyle!=""? divStyle: "")+"><img src='" + ConfigurationValueHome.GetContentImageUrl() + "?IdContentImage=" +
                   contentImage.Id + "' alt='" + contentImage.Name + "' /></div>";
        }

        protected virtual string AddHeader(Template template, int idLanguage)
        {
            return AddImage(template.GetContent(idLanguage).Header, @" class='divHeader' style='text-align: -webkit-center;text-align: center;'");
        }

        protected virtual string AddFooter(Template template, int idLanguage)
        {
            return AddImage(template.GetContent(idLanguage).Footer, @" class='divFooter' style='text-align: -webkit-center;text-align: center;'");
        }

        #endregion Methods

        #region Private methods

        public IList<string> GetVariableTags(string body)
        {
            IList<string> iText = new List<string>();

            var variableInitTag = Settings.Default["VariableInitTag"].ToString();
            var variableEndTag = Settings.Default["VariableEndTag"].ToString();
            var variableSeparator = Settings.Default["VariableSeparator"].ToString().ToCharArray()[0];

            var indexInit = body.IndexOf(variableInitTag);
            var indexEnd = body.IndexOf(variableEndTag, indexInit > -1 ? indexInit : 0);

            while (indexInit != -1)
            {
                string replaceText = body.Substring(indexInit, indexEnd + variableEndTag.Length - indexInit);
                iText.Add(replaceText);

                indexInit = body.IndexOf(variableInitTag, indexEnd);
                indexEnd = body.IndexOf(variableEndTag, indexInit > -1 ? indexInit : 0);
            }
            return iText;
        }

        private ITemplateParserStrategy GetStrategy(string key)
        {
            if (_strategies.Keys.Contains(key))
            {
                return _strategies[key];
            }
            return new ParserDummy();
        }

        private TemplateParserContext GetContext(AbstractEMailDTO dto, Template template, string name,
                                                 string replaceText)
        {
            var context = new TemplateParserContext
                              {
                                  Name = name,
                                  ReplaceText = replaceText,
                                  Dto = dto,
                                  Template = template,
                                  Links = dto.GetLinks()
                              };

            return context;
        }



        private IDictionary<string, string> GetVariableValues(string body, AbstractEMailDTO dto, Template template)
        {
            IDictionary<string, string> iText = new Dictionary<string, string>();

            string variableInitTag = Settings.Default["VariableInitTag"].ToString();
            string variableEndTag = Settings.Default["VariableEndTag"].ToString();
            char variableSeparator = Settings.Default["VariableSeparator"].ToString().ToCharArray()[0];

            int indexInit = body.IndexOf(variableInitTag);
            int indexEnd = body.IndexOf(variableEndTag, indexInit > -1 ? indexInit : 0);

            while (indexInit != -1)
            {
                string replaceText = body.Substring(indexInit, indexEnd + variableEndTag.Length - indexInit);
                string[] iValue =
                    replaceText.Replace(variableInitTag, "").Replace(variableEndTag, "").Split(variableSeparator);

                string key = iValue[0].Trim();
                string name = iValue.Length > 1 ? iValue[1].Trim() : "";

                if (!iText.Keys.Contains(replaceText) && name.Length > 0)
                {
                    iText.Add(replaceText, GetStrategy(key).Parse(
                        GetContext(dto, template, name, replaceText)));
                }
                indexInit = body.IndexOf(variableInitTag, indexEnd);
                indexEnd = body.IndexOf(variableEndTag, indexInit > -1 ? indexInit : 0);
            }
            return iText;
        }

        public Template Copy(Template template)
        {
            Template copy = template.Clone();

            return copy;
        }

        #endregion

        #region ITemplateService Members

        public Template Get(TemplateType type, AssociationGroupDTO associationGroupDto)
        {
            return DAOLocator.Instance().GetDaoTemplate().Get(type.Id, "");
        }

        public Template Copy(int idTemplate)
        {
            Template template = DaoTemplate.Get(idTemplate);

            return Copy(template);
        }

        #endregion
    }
}