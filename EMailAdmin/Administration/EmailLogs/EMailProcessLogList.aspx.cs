using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Home;
using CapaNegocioDatos.CapaNegocio;
using CapaNegocioDatos.CapaHome;
using System.Globalization;

namespace EMailAdmin.Administration.EmailLogs
{
    public partial class EMailProcessLogList : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
        }

        #endregion Constructor

        #region Methods

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

        }

        protected void BtnSearchOnClick(object sender, EventArgs e)
        {
            Bind();
        }

        #endregion Methods

        #region Private Methods

        private void Bind()
        {
            grvEMailLog.DataSource = EMailProcessLogHome.Find(
                Convert.ToDateTime(txtFromDate.Text, CultureInfo.CurrentCulture),
                Convert.ToDateTime(txtToDate.Text, CultureInfo.CurrentCulture));
            grvEMailLog.DataBind();
        }

        #endregion Private Methods
    }
}