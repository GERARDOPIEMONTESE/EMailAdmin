using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMailAdmin.Utils;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.Controls.ContentAttachmentVariable
{
    public partial class ContentAttachment : System.Web.UI.UserControl
    {
        public int IdAttachment
        {
            get
            {
                return Convert.ToInt32( hdfIdAttachment.Value);
            }
            set
            {
                hdfIdAttachment.Value = value.ToString();
            }
        }

        public void AddCodeRPT(string codeRPT)
        {
            ddlType.Items.Clear();
            ddlType.Items.Add(new ListItem(codeRPT,codeRPT));
        }
                
        public delegate void ContentAttachmentClose(object sender, EventArgs e);

        public event ContentAttachmentClose ContentAttachmentClosed;

        public void OnContentAttachmentClose(EventArgs e)
        {
            var handler = ContentAttachmentClosed;
            if (handler != null) handler(this, e);
        }

        public delegate void ContentAttachmentOpen(object sender, EventArgs e);

        public event ContentAttachmentOpen ContentAttachmentOpened;

        public void OnContentAttachmentOpen(EventArgs e)
        {
            var handler = ContentAttachmentOpened;
            if (handler != null) handler(this, e);
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        public void InitControl()
        {
            var template = SessionManager.GetTemplate(Session);
            hdfIdTemplate.Value = template.Id.ToString();

            lblAttachmentName.Text = AttachmentHome.Get(IdAttachment).Name;

            var contentsAttachment = ContentAttachmentHome.Find(template.Id, IdAttachment, ddlType.SelectedValue);

            var contents = new Dictionary<int, string>();
            foreach (var item in contentsAttachment)
            {
                contents.Add(item.IdLanguage, item.Body);
            }
            trtDescription.SetValues(contents);
        }

        protected void BtnCloseOnClick(object sender, EventArgs e)
        {
            SetContentsAttachment();
            OnContentAttachmentClose(EventArgs.Empty);
        }

        public void SetContentsAttachment()
        {
            var list = new List<BackEnd.Domain.ContentAttachment>();

            var template = SessionManager.GetTemplate(Session);
            var contentsAttachment = ContentAttachmentHome.Find(template.Id, IdAttachment, ddlType.SelectedValue);
            bool bCA = (contentsAttachment != null && contentsAttachment.Count > 0);

            var dic = trtDescription.GetValuesHtml();

            foreach (var item in dic)
            {
                var _ca = (bCA ? contentsAttachment.FirstOrDefault(x => x.IdLanguage == item.Key) : null);

                int IdCA = (_ca != null ? _ca.Id : 0);

                BackEnd.Domain.ContentAttachment ca = new BackEnd.Domain.ContentAttachment();
                ca.Id = IdCA;
                ca.IdAttachment = Convert.ToInt32(hdfIdAttachment.Value);
                ca.Body = item.Value;
                ca.CodeRPT = ddlType.SelectedValue;
                ca.IdLanguage = item.Key;

                list.Add(ca);
            }

            foreach (var item in  template.IAttachments)
            {
                if (item.Id == Convert.ToInt32( hdfIdAttachment.Value))
                {
                    item.AttachmentContentPDF = list;
                    break;
                }
            }
        }

        protected void CtmTabsValidator(object source, ServerValidateEventArgs validation)
        {
            validation.IsValid = trtDescription.IsValid();
            return;
        }

        protected void VariableTextUploadButtonPressed(object sender, EventArgs e)
        {
            ctrlVariableTextSelector.Visible = true;
            btnVariableTextSel.Visible = false;
            OnContentAttachmentOpen(EventArgs.Empty);
        }

        protected void VariableTextCompleted(object sender, EventArgs e)
        {
            int idLanguage = SessionManager.GetSelectedGridLanguage(Session);
            EMailAdmin.BackEnd.Domain.VariableText variableText = ctrlVariableTextSelector.VariableText;
            IDictionary<int, IList<EMailAdmin.BackEnd.Domain.VariableText>> variableTextDic = SessionManager.GetContentVariableTexts(Session) ??
                                                                    new Dictionary<int, IList<EMailAdmin.BackEnd.Domain.VariableText>>();
            if (variableTextDic.ContainsKey(idLanguage))
            {
                IList<EMailAdmin.BackEnd.Domain.VariableText> variableTexts = variableTextDic[idLanguage];
                variableTexts.Add(variableText);
            }
            else
            {
                var variableTexts = new List<EMailAdmin.BackEnd.Domain.VariableText> { variableText } ;
                variableTextDic.Add(idLanguage, variableTexts);
            }
            SessionManager.SetContentVariableTexts(variableTextDic, Session);
            trtDescription.TabAddValue(TagUtils.GenerateVariableTextTag(variableText.Name));

            ctrlVariableTextSelector.Visible = false;
            btnVariableTextSel.Visible = true;
            OnContentAttachmentOpen(EventArgs.Empty);
        }

        protected void VariableTextCanceled(object sender, EventArgs e)
        {
            ctrlVariableTextSelector.Visible = false;
            btnVariableTextSel.Visible = true;
            OnContentAttachmentClose(EventArgs.Empty);
        }

    }
}