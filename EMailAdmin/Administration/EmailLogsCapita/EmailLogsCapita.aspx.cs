using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.Administration.EmailLogsCapita
{
    public partial class EmailLogsCapita : CustomPage
    {
        private const int CELLPROCESSSTATUSNAME = 0;

        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadDropDown();
        }

        #endregion Constructor

        protected void BtnSearchOnClick(object sender, EventArgs e)
        {
            Buscar();
        }

        #region Methods

        private void LoadDropDown()
        {
            ddlPais.DataSource = CapaNegocioDatos.CapaHome.PaisHome.Buscar();
            ddlPais.DataBind();
            ddlPais.Items.Insert(0, new ListItem("Todos", "-1"));

            ddlEnvioLinks.Items.Insert(0, new ListItem("Todos", "-1"));
            ddlEnvioLinks.Items.Insert(1, new ListItem("Envio links", "1"));
            ddlEnvioLinks.Items.Insert(2, new ListItem("Sin links", "0"));
        }

        private void Buscar()
        {
            int codigoPais = -1;
            int.TryParse(ddlPais.SelectedValue, out codigoPais);

            grvLogs.DataSource = EmailLog_R_CapitaHome.Find(txtNombre.Text, txtApellido.Text, txtDocumento.Text, txtCapita.Text, txtPlan.Text, codigoPais, int.Parse(ddlEnvioLinks.SelectedItem.Value), txtFromDate.Text, txtToDate.Text);
            grvLogs.DataBind();
        }

        #endregion Methods

        protected void grvLogs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[CELLPROCESSSTATUSNAME].Text = GetLocalResourceObject(((EmailLog_R_Capita)e.Row.DataItem).ProcessStatusName).ToString();
            }
        }

        protected void grvLogs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvLogs.PageIndex = e.NewPageIndex;
            Buscar();
        }
    }
}