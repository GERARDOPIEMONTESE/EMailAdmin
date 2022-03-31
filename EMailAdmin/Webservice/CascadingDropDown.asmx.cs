using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AjaxControlToolkit;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Webservice
{
    /// <summary>
    /// Summary description for CascadingDropDown
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CascadingDropDown : System.Web.Services.WebService
    {
        [WebMethod]
        public CascadingDropDownNameValue[] BuscarGroupsByType(string knownCategoryValues, string category, string contextKey)
        {
            string[] _categoryValues = knownCategoryValues.Split(':', ';');
            int idGroupType = Int32.Parse(contextKey);
            List<CascadingDropDownNameValue> _Items = new List<CascadingDropDownNameValue>();

            IList<BackEnd.Domain.Group> groups = GroupHome.FindByGroupType(idGroupType, true);

            if (groups.Count > 0)
            {
                for (int x = 0; x < groups.Count; x++)
                {
                    _Items.Add(new CascadingDropDownNameValue(groups[x].Name, groups[x].Id.ToString()));
                }
            }
            else
            {
                _Items.Add(new CascadingDropDownNameValue("", "-1"));
            }

            return _Items.ToArray();
        }


        [WebMethod]
        public CascadingDropDownNameValue[] BuscarItemAssociationByType(string knownCategoryValues, string category, string contextKey)
        {            
            string[] _categoryValues = knownCategoryValues.Split(':',';');
            string codigoGroupType = contextKey;
            int IdType = -1;
            List<CascadingDropDownNameValue> _Items = new List<CascadingDropDownNameValue>();
            
            if (int.TryParse(_categoryValues[1].ToString(), out IdType))
            {
                if (codigoGroupType == GroupType.TEMPLATEGROUP)
                    _Items = LoadTemplateFilter(IdType);            
                else
                    _Items = LoadAttachFilter(IdType);
            }
            else
                _Items.Add(new CascadingDropDownNameValue("", "-1"));

            return _Items.ToArray();
            
        }

        private List<CascadingDropDownNameValue> LoadTemplateFilter(int idType)
        {
            List<CascadingDropDownNameValue> _Templates = new List<CascadingDropDownNameValue>();
            IList<BackEnd.Domain.Template> templates = TemplateHome.FindByType(idType, true);

            if (templates.Count > 0)
            {
                for (int x = 0; x < templates.Count; x++)
                {
                    _Templates.Add(new CascadingDropDownNameValue(templates[x].Name, templates[x].Id.ToString()));
                }
            }
            else
            {
                _Templates.Add(new CascadingDropDownNameValue("", "-1"));
            }

            return _Templates;
        }

        private List<CascadingDropDownNameValue> LoadAttachFilter(int idType)
        {
            List<CascadingDropDownNameValue> _Attachments = new List<CascadingDropDownNameValue>();
            IList<BackEnd.Domain.Attachment> attachments = AttachmentHome.FindByType(idType, true);

            if (attachments.Count > 0)
            {
                for (int x = 0; x < attachments.Count; x++)
                {
                    _Attachments.Add(new CascadingDropDownNameValue(attachments[x].Name, attachments[x].Id.ToString()));
                }
            }
            else
            {
                _Attachments.Add(new CascadingDropDownNameValue("", "-1"));
            }

            return _Attachments;
        }
    }
}