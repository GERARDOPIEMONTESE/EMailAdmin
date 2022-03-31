using System;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Home;
using CapaNegocioDatos.Utilitarios;
using EMailAdmin.BackEnd.Domain;
using System.Collections.Generic;

namespace EMailAdmin.Legal
{
    public partial class LegalList : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadFilters();
            }
        }


        #endregion Constructor

        #region Methods

        protected void GrvLegalPageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvLegal.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void GrvLegalRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void GrvLegalRowCommand(object sender, GridViewCommandEventArgs e)
        { 
            if(e.CommandName == "View")
            {
                var id = Convert.ToInt32(grvLegal.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                lblError.Text = EMailLogHome.Get(id)[0].ErrorMessage;
                mpeError.Show();
            }
            if (e.CommandName == "ViewXml")
            {
                var id = Convert.ToInt32(grvLegal.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                var xmlDoc = ServicioCompresionXml.UnzipXml(EMailLogHome.Get(id)[0].ContextInformation);
                lblError.Text = "<pre lang='xml'>" + xmlDoc + "</pre>";
                mpeError.Show();
            }
            if (e.CommandName == "ViewBody")
            {
                var id = Convert.ToInt32(grvLegal.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                lblError.Text = EMailLogHome.Get(id)[0].Body;
                mpeError.Show();
            }
        }

        protected void BtnSearchOnClick(object sender, EventArgs e)
        {
            //if (ValidateSearching())
            Bind();
        }

        #endregion Methods

        #region Private Methods

        private void LoadFilters()
        {
            List<BackEnd.Domain.Template> types = new List<BackEnd.Domain.Template> { new BackEnd.Domain.Template { Id = -1, Name = "" } };
            types.AddRange(TemplateHome.FindByName(string.Empty));
            ddlTemplate.DataSource = types;
            ddlTemplate.DataBind();
        }

        private void Bind()
        {
            int countryCode = -1;
            if (!txtCountryCode.Text.Equals(string.Empty))
                countryCode = Convert.ToInt32(txtCountryCode.Text);

            grvLegal.DataSource = LegalHome.Find(countryCode, txtVoucher.Text, txtEmail.Text, ddlTemplate.SelectedItem.Text);
            grvLegal.DataBind();
        }

        private bool ValidateSearching()
        {
            bool valid = true;
            lblErrorMessage.Visible = false;

            if (txtCountryCode.Text.Equals(string.Empty) && ddlTemplate.SelectedIndex == 0)
            {
                lblErrorMessage.Visible = true;
                return false;
            }

            return valid;
        }

        #endregion Private Methods
    }
}