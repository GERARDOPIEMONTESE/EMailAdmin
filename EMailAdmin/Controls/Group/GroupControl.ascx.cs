using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;
using CapaNegocioDatos.CapaHome;
using System.Linq;

namespace EMailAdmin.Controls.Group
{
    public partial class GroupControl : System.Web.UI.UserControl
    {
        #region Delegates

        public delegate void FiltersClear(object sender, EventArgs e);
        public delegate void FiltersClose(object sender, EventArgs e);
        public delegate void AddFilters(object sender, EventArgs e);
        public delegate void AccountSearchButtonPress(object sender, EventArgs e);
        public delegate void ProductSearchButtonPress(object sender, EventArgs e);
        public delegate void RateSearchButtonPress(object sender, EventArgs e);
        public delegate void DynamicValueSearchButtonPress(object sender, EventArgs e);
        public delegate void DeleteButtonPress(object sender, EventArgs e);

        #endregion Delegates

        #region Properties

        public BackEnd.Domain.Group Group
        {
            get { return GetGroup(); }
        }

        public bool CanSave()
        {
            return (SessionManager.GetGroupConditions(Session) ?? new List<GroupCondition>()).Count > 0 && txtName.Text != "";
        }

        public void ClearControls()
        {
            ClearFilters();
            var items = new List<GroupCondition>();
            SessionManager.SetGroupConditions(items, Session);
            Bind();
        }

        public void SetAccountText(string text)
        {
            ddlAccount.Text = text;
        }

        public void SetCountryText(string text)
        {
            ddlCountry.Text = text;
        }

        public void SetProductText(string text)
        {
            ddlProduct.Text = text;
        }

        public void SetRateText(string text)
        {
            ddlRate.Text = text;
        }

        public void SetDynamicValue(string text)
        {
            ddlDynamicValue.Text = text;
        }

        #endregion Properties

        #region Constructor

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtName.Enabled = true;
                LoadCombo();
                if(SessionManager.GetGroupType(Session).Codigo != GroupType.NONE)
                {
                    SelectInCombo(SessionManager.GetGroupType(Session).Description);
                    ddlType.Enabled = false;
                }
                else
                {
                    ddlType.Enabled = true;
                }
                ddlAccount.Attributes.Add("onclick", "javascript:showAccountPopUp();");
                ddlCountry.Attributes.Add("onclick", "javascript:showCountryPopUp();");
                ddlProduct.Attributes.Add("onclick", "javascript:showProductPopUp();");
                ddlRate.Attributes.Add("onclick", "javascript:showRatePopUp();");
                ddlDynamicValue.Attributes.Add("onclick", "javascript:showDynamicValuePopUp();");
                Cargar();
            }
        }

        private void Cargar()
        {
            int idGroup = SessionManager.GetGroupIdToEdit(Session);
            if (idGroup != -1)
            {
                BackEnd.Domain.Group gp = GroupHome.Get(idGroup);
                txtName.Text = gp.Name;
                txtName.Enabled = false;
                if (gp.GroupType != null)
                    ddlType.SelectedValue = gp.GroupType.Id.ToString();

                Bind(idGroup);
            }
        }

        #endregion Constructor

        #region Events

        public event FiltersClear FiltersCleared;

        public void OnFiltersCleared(EventArgs e)
        {
            FiltersClear handler = FiltersCleared;
            if (handler != null) handler(this, e);
        }

        public event FiltersClose FiltersClosed;

        public void OnFiltersClosed(EventArgs e)
        {
            FiltersClose handler = FiltersClosed;
            if (handler != null) handler(this, e);
        }

        public event AddFilters FiltersAdded;

        public void OnFiltersAdded(EventArgs e)
        {
            AddFilters handler = FiltersAdded;
            if (handler != null) handler(this, e);
        }

        public event AccountSearchButtonPress AccountSearchButtonPressed;

        public void OnAccountSearchButtonPressed(EventArgs e)
        {
            AccountSearchButtonPress handler = AccountSearchButtonPressed;
            if (handler != null) handler(this, e);
        }

        public event ProductSearchButtonPress ProductSearchButtonPressed;

        public void OnProductSearchButtonPressed(EventArgs e)
        {
            ProductSearchButtonPress handler = ProductSearchButtonPressed;
            if (handler != null) handler(this, e);
        }

        public event RateSearchButtonPress RateSearchButtonPressed;

        public void OnRateSearchButtonPressed(EventArgs e)
        {
            RateSearchButtonPress handler = RateSearchButtonPressed;
            if (handler != null) handler(this, e);
        }

        public event DynamicValueSearchButtonPress DynamicValueSearchButtonPressed;

        public void OnDynamicValueSearchButtonPressed(EventArgs e)
        {
            DynamicValueSearchButtonPress handler = DynamicValueSearchButtonPressed;
            if (handler != null) handler(this, e);
        }

        public event DeleteButtonPress DeletePressed;

        public void OnDeleteButtonPressed(EventArgs e)
        {
            DeleteButtonPress handler = DeletePressed;
            if (handler != null) handler(this, e);
        }

        #endregion Events

        #region Methods

        protected void IbnDelete_Onclick(object sender, EventArgs e)
        {
            var bt = (ImageButton)sender;
            var items = SessionManager.GetGroupConditions(Session) ?? new List<GroupCondition>();

            GroupCondition gc = ((GroupCondition)items[((GridViewRow)bt.Parent.Parent).RowIndex]);
            //gc.Eliminar();
            items.Remove(gc);
            SessionManager.SetGroupConditions(items, Session);
            Bind();
        }

        protected void BtnAddOnClick(object sender, EventArgs e)
        {
            var changed = false;
            var items = SessionManager.GetGroupConditions(Session) ?? new List<GroupCondition>();
            foreach (var value in SessionManager.GetCountriesSelected(Session))
            {
                var item = new GroupCondition
                                   {
                                       ConditionType = ConditionTypeHome.Get(ConditionType.Country),
                                       Value = value.Id.ToString(CultureInfo.InvariantCulture),
                                       VisibleValue = value.Nombre
                                   };
                    items.Add(item);
                changed = true;
            }
            foreach (var value in SessionManager.GetAccountsSelected(Session))
            {
                var item = new GroupCondition
                {
                    ConditionType = ConditionTypeHome.Get(ConditionType.Branch),
                    Value = value.Id.ToString(CultureInfo.InvariantCulture),
                    VisibleValue = value.NombreCombinado
                };
                items.Add(item);
                changed = true;
            }
            foreach (var value in SessionManager.GetProductsSelected(Session))
            {
                var item = new GroupCondition
                {
                    ConditionType = ConditionTypeHome.Get(ConditionType.Product),
                    Value = value.Id.ToString(CultureInfo.InvariantCulture),
                    VisibleValue = value.Description
                };
                items.Add(item);
                changed = true;
            }
            foreach (var value in SessionManager.GetRatesSelected(Session))
            {
                var item = new GroupCondition
                {
                    ConditionType = ConditionTypeHome.Get(ConditionType.Rate),
                    Value = value.Id.ToString(CultureInfo.InvariantCulture),
                    VisibleValue = value.Description
                };
                items.Add(item);
                changed = true;
            }
            foreach (var value in SessionManager.GetDynamicValuesSelected(Session))
            {
                var item = new GroupCondition
                {
                    ConditionType = ConditionTypeHome.Get(ConditionType.DynamicValue),
                    VisibleCode=value.DynamicKey,
                    Value = value.Value.ToString(CultureInfo.InvariantCulture),
                    VisibleValue = value.Value.ToString(CultureInfo.InvariantCulture),
                    DynamicKey = value.DynamicKey
                };
                items.Add(item);
                changed = true;
            }
            if (changed)
            {
                SessionManager.SetGroupConditions(items, Session);
            }
            ClearFilters();
            Bind();
            OnFiltersAdded(EventArgs.Empty);
        }

        protected void FilterClosed(object sender, EventArgs e)
        {
            OnFiltersClosed(EventArgs.Empty);
        }

        protected void AccAccountSearchPressed(object sender, EventArgs e)
        {
            OnAccountSearchButtonPressed(EventArgs.Empty);
        }

        protected void proProductSearchPressed(object sender, EventArgs e)
        {
            OnProductSearchButtonPressed(EventArgs.Empty);
        }

        protected void rteRateSearchPressed(object sender, EventArgs e)
        {
            OnRateSearchButtonPressed(EventArgs.Empty);
        }

        protected void dvcDynamicValueOnChkPressed(object sender, EventArgs e)
        {
            OnDynamicValueSearchButtonPressed(EventArgs.Empty);
        }

        protected void CtmNameValidator(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = ValidateName();
            return;
        }

        protected void grvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    string countryName = string.Empty;
            //    var item = (GroupCondition)e.Row.DataItem;
            //    switch (item.ConditionType.Id)
            //    {
            //        case 2: //branch
            //            var codigoPais = SucursalHome.Obtener(Convert.ToInt32(item.Value)).CodigoPais;
            //            if (codigoPais != null)
            //            {
            //                countryName = PaisHome.ObtenerPorCodigo(codigoPais).Nombre;
            //            }
            //            break;
            //        case 3: //prod
            //            var codigoPaisProd = ProductHome.Get(Convert.ToInt32(item.Value)).CountryCode;
            //            if (codigoPaisProd != null)
            //            {
            //                countryName = PaisHome.ObtenerPorCodigo(codigoPaisProd).Nombre;
            //            }
            //            break;
            //    }
            //    if (countryName != string.Empty)
            //    {
            //        e.Row.Cells[1].Text += " (" + countryName + ")";
            //    }
            //}
        }

        #endregion Methods

        #region Private Methods


        private void Bind(int idGroup)
        {
            IList<GroupCondition> lstGC = GroupConditionsHome.FindByIdGroupWithValues(idGroup);
            SessionManager.SetGroupConditions(lstGC, Session);
            Bind();
        }

        private void Bind()
        {
            grvItems.DataSource = SessionManager.GetGroupConditions(Session);
            grvItems.DataBind();
        }

        private BackEnd.Domain.Group GetGroup()
        {
            return new BackEnd.Domain.Group
                       {
                           Name = txtName.Text,
                           GroupType = GroupTypeHome.Get(Convert.ToInt32(ddlType.SelectedValue)),
                           Conditions = SessionManager.GetGroupConditions(Session) ?? new List<GroupCondition>()
                       };
        }

        private void LoadCombo()
        {
            ddlType.DataSource = GroupTypeHome.FindAll();
            ddlType.DataBind();
        }

        private void SelectInCombo(string value)
        {
            try
            {
                ddlType.Items.FindByText(value).Selected = true;
            }
            catch{}
        }

        private void ClearFilters()
        {
            SessionManager.SetCountriesSelected(new List<Locacion>(), Session);
            SessionManager.SetAccountsSelected(new List<Sucursal>(), Session);
            SessionManager.SetProductsSelected(new List<BackEnd.Domain.Product>(), Session);
            SessionManager.SetRatesSelected(new List<BackEnd.Domain.Rate>(), Session);
            SessionManager.SetDynamicValuesSelected(new List<DynamicCondition>(), Session);
            OnFiltersCleared(EventArgs.Empty);
        }

        private bool ValidateName()
        {
            if (txtName.Enabled == true)
            {
                if (txtName.Text != "")
                {
                    return !GroupHome.Exist(txtName.Text);
                }
                //ES VACIO
                return false;
            }
            else
                return true; //esta editado, el nombre no se edita.
        }

        protected void ibnFiltrar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {            
           string sFiltro = ((TextBox) grvItems.HeaderRow.FindControl("txtFiltroDescCondAgregada")).Text.ToUpper();
            IList<GroupCondition> condAgregadas = SessionManager.GetGroupConditions(Session);
            var filtro = condAgregadas.Where(c => c.VisibleValue.ToUpper().Contains(sFiltro)).ToList();
            grvItems.DataSource = filtro;
            grvItems.DataBind();
        }

        protected void ibnTodos_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Bind();
        }

        #endregion Private Methods
    }
}