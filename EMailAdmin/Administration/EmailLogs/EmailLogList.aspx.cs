using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;

namespace EMailAdmin.Administration.EmailLogs
{
    public partial class EmailLogList : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadFilters();
                Bind();
            }
        }

        #endregion Constructor

        #region Methods

        protected void BtnReprocess_OnClick(object sender, EventArgs e)
        {
            Reprocess(-1);
        }

        protected void GrvEMailLogPageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvEMailLog.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void GrvEMailLogRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void GrvEMailLogRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reprocess")
            {
                var id = Convert.ToInt32(grvEMailLog.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                Reprocess(id);
            }else 
            if(e.CommandName == "View")
            {
                var id = Convert.ToInt32(grvEMailLog.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                lblError.Text = EMailLogHome.Get(id)[0].ErrorMessage;
                mpeError.Show();
            }
        }

        protected void BtnSearchOnClick(object sender, EventArgs e)
        {
            Bind();
        }

        #endregion Methods

        #region Private Methods

        private void Bind()
        {
            grvEMailLog.DataSource = EMailLogHome.FindAllPendings(txtVoucher.Text, Convert.ToInt32(ddlCountry.SelectedValue),
                                                                  Convert.ToInt32(ddlStatus.SelectedValue), txtFromDate.Text, txtToDate.Text);
            grvEMailLog.DataBind();
        }

        private void LoadFilters()
        {
            ddlCountry.Items.Clear();
            var countries = new List<Pais> { new Pais { Id = -1, Nombre = "" } };
            countries.AddRange(PaisHome.BuscarLazy());
            ddlCountry.DataSource = countries;
            ddlCountry.DataBind();

            ddlStatus.Items.Clear();
            ddlStatus.Items.Add(new ListItem("", "-1"));
            ddlStatus.Items.Add(new ListItem("Initial", EMailLog.INITIAL.ToString(CultureInfo.InvariantCulture)));
            ddlStatus.Items.Add(new ListItem("In Progress", EMailLog.INPROGRESS.ToString(CultureInfo.InvariantCulture)));
            ddlStatus.Items.Add(new ListItem("Information Incomplete", EMailLog.EXTERNALINFOCOMPLETE.ToString(CultureInfo.InvariantCulture)));
            ddlStatus.Items.Add(new ListItem("Error", EMailLog.ERROR.ToString(CultureInfo.InvariantCulture)));
        }

        private void Reprocess(int id)
        {
            if(id == -1)
            {
                ServiceLocator.Instance().GetSendMailService().ProcessEMails();
            }
            else
            {
                ServiceLocator.Instance().GetSendMailService().ProcessEMails(id);
            }
            Bind();
        }

        #endregion Private Methods
    }
}