using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.Administration.EmailLogsPrecompra
{
    public partial class EmailLogPrecompraList : CustomPage
    {      
        private const int CELLPROCESSSTATUSNAME = 6;
        
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
            ddlPais.Items.Insert( 0,new ListItem("Todos","-1" ));
        }

        private void Buscar()
        {
            int codPaxBox = -1;
            if (txtCodPaxBox.Text!="") int.TryParse(txtCodPaxBox.Text, out codPaxBox);

            int codigoPais = -1;
            int.TryParse(ddlPais.SelectedValue, out codigoPais);

            grvLogs.DataSource = PrepurchasePaxHome.FindAllByCodigoVerif( codPaxBox, txtCodigoVerif.Text, txtVoucherGroup.Text, codigoPais);
            grvLogs.DataBind();
        }

        #endregion Methods

        protected void grvLogs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[CELLPROCESSSTATUSNAME].Text = GetLocalResourceObject(((EmailLog_R_PrepurchasePax)e.Row.DataItem).ProcessStatusName).ToString();                
            }
        }

        protected void grvLogs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvLogs.PageIndex = e.NewPageIndex;
            Buscar();
        }
    }
}