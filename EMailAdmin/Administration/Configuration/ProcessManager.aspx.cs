using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;

namespace EMailAdmin.Administration.Configuration
{
    public partial class ProcessManager : CustomPage
    {
        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadConfigurationValues();
            }
        }

        private void LoadConfigurationValues()
        {
            IList<EMailProcessType> values = EMailProcessTypeHome.FindAll();

            ddlConfigurationValues.DataSource = values;
            ddlConfigurationValues.DataBind();
            ddlConfigurationValues.Items.Insert(0, new ListItem("(Todos)", "-1"));

            grvConfiguration.DataSource = values;
            grvConfiguration.DataBind();
        }

        private void SaveConfigurationValue()
        {
            try
            {
                EMailProcessType configVal = SessionManager.GetSelectedProcessTypeValue(Session);
                configVal.Codigo = txtCode.Text;
                configVal.Descripcion = txtDescription.Text;
                configVal.Period = int.Parse( txtPeriod.Text);
                configVal.PeriodHours = txtPeriodHours.Text;
                configVal.CheckLote = ChkCheckLote.Checked;
                configVal.IdUsuario = UsuarioLogueadoDTO().IdUsuario;

                configVal.Persistir();
                CleanPopUp();
                LoadConfigurationValues();
            }
            catch (Exception ex)
            {
            }
        }

        private void CleanPopUp()
        {
            txtCode.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtPeriod.Text= "0";
            txtPeriodHours.Text = string.Empty;
            txtCode.Enabled = true;
            ChkCheckLote.Checked = false;
            SessionManager.RemoveSelectedConfigurationValue(Session);
            mpeConfiguration.Hide();
        }

        private void DeleteConfigurationValue(int IdConfigurationValue)
        {
            EMailProcessType configValue = EMailProcessTypeHome.Get(IdConfigurationValue);
            configValue.IdUsuario = UsuarioLogueadoDTO().IdUsuario;

            configValue.Eliminar();
            LoadConfigurationValues();
        }

        private void EditConfigurationValue(int IdConfigurationValue)
        {
            EMailProcessType configValue = EMailProcessTypeHome.Get(IdConfigurationValue);

            txtCode.Text = configValue.Codigo;
            txtDescription.Text = configValue.Descripcion;
            txtPeriod.Text = configValue.Period.ToString();
            txtPeriodHours.Text = configValue.PeriodHours.ToString();
            txtCode.Enabled = false;
            ChkCheckLote.Checked = configValue.CheckLote;

            SessionManager.SetSelectedProcessTypeValue(configValue, Session);
            mpeConfiguration.Show();
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            if (ddlConfigurationValues.SelectedIndex == 0)
                LoadConfigurationValues();
            else
            {
                string code = ddlConfigurationValues.SelectedItem.ToString();
                EMailProcessType value = EMailProcessTypeHome.Get(code);
                IList<EMailProcessType> valueList = new List<EMailProcessType>();

                valueList.Add(value);
                grvConfiguration.DataSource = valueList;
                grvConfiguration.DataBind();
            }
        }

        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            mpeConfiguration.Show();
        }

        protected void btnSaveConfiguration_OnClick(object sender, EventArgs e)
        {
            SaveConfigurationValue();
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            CleanPopUp();
        }

        #region GridEvents

        protected void grvConfiguration_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvConfiguration.PageIndex = e.NewPageIndex;
            grvConfiguration.EditIndex = -1;

            LoadConfigurationValues();
        }

        protected void grvConfiguration_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                lblErrorMessage.Text = string.Empty;
                int index = Convert.ToInt32(e.CommandArgument);
                int idConfigurationValue;
                if (e.CommandName.ToUpper().Equals("EDITAR"))
                {
                    idConfigurationValue = Convert.ToInt32(grvConfiguration.DataKeys[index].Value);
                    EditConfigurationValue(idConfigurationValue);
                }
                else if (e.CommandName.ToUpper().Equals("ELIMINAR"))
                {
                    idConfigurationValue = Convert.ToInt32(grvConfiguration.DataKeys[index].Value);
                    DeleteConfigurationValue(idConfigurationValue);
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
            }
        }

        #endregion
    }
}