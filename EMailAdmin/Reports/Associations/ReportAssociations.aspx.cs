using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using CapaNegocioDatos.CapaHome;
using ControlMenu;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using CapaNegocioDatos.CapaNegocio;

namespace EMailAdmin.Reports.Associations
{
    public partial class ReportAssociations : CustomPage
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

        protected void GrvTemplatePageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvTemplate.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void BtnSearch_OnClick(object sender, EventArgs e)
        {
            Bind();
        }

        protected void CountryChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }

        protected void GrvTemplatePageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvTemplate.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void GrvTemplateRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Export")
            {
                ExportToExcel(Search(), CargarColumnas(grvTemplate), CargarHeaderColumnas(grvTemplate), "AssociationsReport.xls");
            }
        }

        #endregion

        #region Private Methods

        private void Bind()
        {
            grvTemplate.DataSource = Search();
            grvTemplate.DataBind();
        }

        private IList<ReportAssociation> Search()
        {
            return GroupConditionsHome.Find(Convert.ToInt32(ddlType.SelectedValue),
                txtName.Text,
                Convert.ToInt32(ddlCountry.SelectedValue),
                txtAccount.Text,
                Convert.ToInt32(ddlProduct.SelectedValue),
                txtRate.Text,
                DateTime.Now,
                GroupTypeHome.TemplateGroup().Id,
                Convert.ToInt32(ddlAsociacion.SelectedValue));
        }

        private IList<ReportAssociation> CreateReportList(IEnumerable<Group_R_Template> groups)
        {
            var result = new List<ReportAssociation>();

            foreach(var group in groups)
            {
                group.Group.Conditions = GroupConditionsHome.FindByIdGroupWithValues(group.IdGroup);
                foreach (var groupCondition in group.Group.Conditions)
                {
                    var template = TemplateHome.Get(group.IdTemplate);
                    var report = new ReportAssociation
                                     {
                                         GroupDescription = @group.GroupName,
                                         TemplateName = template.Name,
                                         HierarchyDescription = template.HierarchyDescription,
                                         TemplateType = template.TemplateType.Nombre
                                     };                    
                    result.Add(report);
                }
                
            }
            return result;
        }

        private void LoadFilters()
        {
            ddlAsociacion.Items.Insert(0, new ListItem("Todos", "-1"));
            ddlAsociacion.Items.Insert(1, new ListItem("Solo asociados", "1"));
            ddlAsociacion.Items.Insert(2, new ListItem("No asociados", "0"));

            ddlType.Items.Clear();
            ddlType.DataSource = TemplateTypeHome.FindAll();
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("Todos","-1"));

            ddlCountry.Items.Clear();
            ddlCountry.DataSource = PaisHome.BuscarLazy();
            ddlCountry.DataBind();
            //adaptar para que no sea necesario elegir un pais
            ddlCountry.Items.Insert(0, new ListItem("Todos", "-1"));

            ddlProduct.Items.Insert(0, new ListItem("Todos", "-1"));
        }

        private void LoadProducts()
        {
            ddlProduct.Items.Clear();
            if (ddlCountry.SelectedValue != "-1")
            {
                Pais pais = PaisHome.ObtenerPorLocacion(int.Parse(ddlCountry.SelectedValue));
                ddlProduct.DataSource = ProductHome.FindAllByCountry(pais.Codigo);
                ddlProduct.DataBind();                
            }
            ddlProduct.Items.Insert(0, new ListItem("Todos", "-1"));
        }

        #endregion Private Methods
    }
}