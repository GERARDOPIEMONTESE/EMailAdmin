using System;
using System.Collections.Generic;
using CapaNegocioDatos.CapaHome;
using AjaxControlToolkit;
using EMailAdmin.Utils;

namespace EMailAdmin.Controls.TabRich
{
    public partial class TabRichTextControl : System.Web.UI.UserControl
    {
        #region Constructor

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion Constructor

        #region Methods

        protected override void OnInit(EventArgs e)
        {
            var language = SessionManager.GetLoguedUser(Session).Ididioma;
            var idiomas = IdiomaHome.Buscar();
            foreach (var idioma in idiomas)
            {
                var tabPanel = new TabPanel();
                switch (InternacionalUtils.GetCulture(language))
                {
                    case "es":
                        tabPanel.HeaderText = idioma.Descripcion;
                        break;
                    case "en":
                        tabPanel.HeaderText = idioma.DescripcionIngles;
                        break;
                    case "pt":
                        tabPanel.HeaderText = idioma.DescripcionPortugues;
                        break;
                }
                tabPanel.ID = Convert.ToString(idioma.Id);

                var tab = (RichTextControl)TemplateControl.LoadControl("~/Controls/TabRich/RichTextControl.ascx");

                tab.ID = "RichTextControl";
                tab.SetInformation(idioma.Id);

                tabPanel.Controls.Add(tab);

                tbcRichText.Tabs.Add(tabPanel);
            }

            tbcRichText.ActiveTabIndex = 0;
            base.OnInit(e);
        }

        #endregion Methods

        #region Public Methods

        public IDictionary<int, string> GetValues()
        {
            IDictionary<int, string> values = new Dictionary<int, string>();

            foreach (TabPanel panel in tbcRichText.Tabs)
            {
                var control = (RichTextControl)panel.FindControl("RichTextControl");
                values.Add(control.IdLanguage, control.Text);
            }

            return values;
        }

        public IDictionary<int, string> GetValuesHtml()
        {
            IDictionary<int, string> values = new Dictionary<int, string>();

            foreach (TabPanel panel in tbcRichText.Tabs)
            {
                var control = (RichTextControl)panel.FindControl("RichTextControl");
                values.Add(control.IdLanguage, control.HtmlText);
            }

            return values;
        }

        public void SetValues(IDictionary<int, string> values)
        {
            foreach (TabPanel panel in tbcRichText.Tabs)
            {
                var control = (RichTextControl)panel.FindControl("RichTextControl");
                control.Text = values.ContainsKey(control.IdLanguage) ? values[control.IdLanguage] : "";
            }
        }

        public void TabAddValue(string value)
        {
            TabPanel panel = tbcRichText.Tabs[tbcRichText.ActiveTabIndex];
            var control = (RichTextControl)panel.FindControl("RichTextControl");
            control.Text += value;
        }

        public bool IsValid()
        {
            bool isValid = false;
            foreach (var value in GetValues())
            {
                if(value.Value != "")
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }

        #endregion Methods
    }
}