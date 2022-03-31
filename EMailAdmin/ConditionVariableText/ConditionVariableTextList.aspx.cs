using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.Utils;
using System.Configuration;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.ConditionVariableText
{
    public partial class ConditionVariableTextList : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFilters();
                Bind();
                SessionManager.SetLoguedUser(UsuarioLogueadoDTO(), Session);
            }
        }

        #endregion Constructor

        #region Propiedades

        protected override void ChequearSession()
        {
            var usuario = base.UsuarioLogueadoDTO();
            if (usuario == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings["ExpireSessionRedirect"]);
            }
        }

        #endregion Propiedades

        #region Methods

        protected void GrvConditionVariableTextPageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvConditionVariableText.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void GrvConditionVariableTextRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void GrvConditionVariableTextRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                var id = Convert.ToInt32(grvConditionVariableText.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                Response.Redirect("ConditionVariableTextInformation.aspx?IdConditionVariableText=" + id);
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Bind();
        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ConditionVariableTextInformation.aspx");
        }

        #endregion

        #region Private Methods

        private void LoadFilters()
        {            
            ddlVariableText.Items.Clear();
            var types = new List<VariableText> { new VariableText { Id = -1, Name = "" } };
            types.AddRange(VariableTextHome.FindAll());
            ddlVariableText.DataSource = types;
            ddlVariableText.DataBind();
        }

        private void Bind()
        {
            int idVariableText = int.Parse(ddlVariableText.SelectedValue);            
            grvConditionVariableText.DataSource = ConditionVariableTextHome.Find(idVariableText, txtName.Text, txtCondicion.Text);
            grvConditionVariableText.DataBind();
        }

        #endregion
    }
}