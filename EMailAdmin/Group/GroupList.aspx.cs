using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.Utils;
using MenuPortalLibrary.CapaDTO;
using CapaNegocioDatos.CapaHome;
using EMailAdmin.Controls.Group;
using System.Linq;

namespace EMailAdmin.Group
{
    public partial class GroupList : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFilters();
                Bind();
                SessionManager.SetLoguedUser(UsuarioLogueadoDTO(), Session);
                SessionManager.SetGroupType(new GroupType{Codigo = GroupType.NONE, Id = -1, Descripcion = "NONE"},Session);
            }
        }

        #endregion Constructor

        #region Propiedades

        protected override void ChequearSession()
        {
            UsuarioDTO usuario = base.UsuarioLogueadoDTO();
            if (usuario == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings["ExpireSessionRedirect"]);
            }
        }

        #endregion Propiedades

        #region Methods

        protected void GrvConditionsPageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvConditions.PageIndex = e.NewPageIndex;
            string sFiltro = SessionManager.GetFiltroDescCondAgregada(Session);
            if (sFiltro != "")
                Filtrar();
            else
                LoadGroupConditionsGrid();
            mpeConditions.Show();
        }

        protected void GrvGroupPageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvGroup.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void GrvGroupRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void GrvConditionsRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                    e.Row.Cells[0].Visible = false;

                if (e.Row.RowType == DataControlRowType.DataRow)
                    e.Row.Cells[0].Visible = false;

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }     

        protected void GrvGroupRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                var id = Convert.ToInt32(grvGroup.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);

                ltrGroupDelete.Text = grvGroup.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                lblMessage.Text = string.Format(GetLocalResourceObject("lblCantDeleteResource1.text").ToString(), grvGroup.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);
                lblDelete.Text = string.Format(lblDelete.Text, grvGroup.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);
                if (GroupHome.CanDelete(id))
                {
                    SessionManager.SetIdToDelete(id, Session);
                    mpeDelete.Show();
                }
                else
                {
                    mpeCantDelete.Show();
                }
            }
            else
            {
                var id = Convert.ToInt32(grvGroup.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                ltrGroup.Text = grvGroup.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                SessionManager.SetGroupIdToEdit(id, Session);
                LoadGroupConditionsGrid();
                mpeConditions.Show();
            }
        }

        protected void GrvGroupRowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void bDeleteCondition_Command(object sender, CommandEventArgs e)
        {
            try
            {
                var idCondition = Convert.ToInt32(e.CommandArgument);
                GroupCondition gc = GroupConditionsHome.Get(idCondition);
                gc.Eliminar();                
            }
            catch
            {
            }
            LoadGroupConditionsGrid();
            mpeConditions.Show();
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Bind();
        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("GroupInformation.aspx");
        }

        protected void BtnAcceptDeleteOnClick(object sender, EventArgs e)
        {
            try
            {
                ServiceLocator.Instance().GetGroupService().Delete(GroupHome.Get(SessionManager.GetIdToDelete(Session)));
            }
            catch
            {
            }
            Bind();
        }

        protected void BtnClose_OnClick(object sender, EventArgs e)
        {
            LimpiarFiltro();
            mpeConditions.Hide();
        }
            
        #endregion

        #region Private Methods

        private void Bind()
        {
            grvGroup.DataSource = GroupHome.FindByFilters(txtName.Text, Convert.ToInt32(ddlType.SelectedValue), true);
            grvGroup.DataBind();
            mpeConditions.Hide();
        }

        private void LoadGroupConditionsGrid()
        {
            IList<GroupCondition> lstGC = GroupConditionsHome.FindByIdGroupWithValues(SessionManager.GetGroupIdToEdit(Session)); 
            SessionManager.SetGroupConditions(lstGC, Session);
            BindGroupConditionsGrid();
        }

        private void BindGroupConditionsGrid()
        {
            grvConditions.DataSource = SessionManager.GetGroupConditions(Session);
            grvConditions.DataBind();
        }

        private void LoadFilters()
        {
            ddlType.Items.Clear();
            var types = new List<GroupType> { new GroupType { Id = -1, Descripcion = "", Nombre = "" } };
            types.AddRange(GroupTypeHome.FindAll());
            ddlType.DataSource = types;
            ddlType.DataBind();
        }

        #endregion

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            LimpiarFiltro();
            var id = SessionManager.GetGroupIdToEdit(Session);
            Response.Redirect("GroupInformation.aspx?IdGroup=" + id);
        }

        private void LimpiarFiltro()
        {
            SessionManager.DeleteFiltroDescCondAgregada(Session);
            if (grvConditions.Rows.Count > 0)
                ((TextBox)grvConditions.HeaderRow.FindControl("txtFiltroDescCondAgregada")).Text = "";
        }

        protected void ibnFiltrar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string sFiltro = ((TextBox)grvConditions.HeaderRow.FindControl("txtFiltroDescCondAgregada")).Text;
            SessionManager.SetFiltroDescCondAgregada(sFiltro, Session);           
            Filtrar();
        }

        private void Filtrar()
        {
            string sFiltro = SessionManager.GetFiltroDescCondAgregada(Session);
            ((TextBox)grvConditions.HeaderRow.FindControl("txtFiltroDescCondAgregada")).Text = sFiltro;
            IList<GroupCondition> condAgregadas = SessionManager.GetGroupConditions(Session);
            var filtro = condAgregadas.Where(c => c.VisibleValue.ToUpper().Contains(sFiltro.ToUpper())).ToList();
            grvConditions.DataSource = filtro;
            grvConditions.DataBind();
            mpeConditions.Show();
        }

        protected void ibnTodos_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            LimpiarFiltro();            
            BindGroupConditionsGrid();
            mpeConditions.Show();
        }

    }
}