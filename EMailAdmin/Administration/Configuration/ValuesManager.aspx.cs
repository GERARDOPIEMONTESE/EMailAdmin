using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.Utils;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Administration.Configuration
{
    public partial class ValuesManager : CustomPage
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
            IList<ConfigurationValue> values = ConfigurationValueHome.FindAll();

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
                ConfigurationValue configVal = SessionManager.GetSelectedConfigurationValue(Session);
                configVal.Code = txtCode.Text;
                configVal.Description = txtDescription.Text;
                configVal.Value = txtValue.Text;
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
            txtValue.Text = string.Empty;
            txtCode.Enabled = true;
            SessionManager.RemoveSelectedConfigurationValue(Session);
            mpeConfiguration.Hide();
        }

        private void DeleteConfigurationValue(int IdConfigurationValue)
        {
            ConfigurationValue configValue = ConfigurationValueHome.GetById(IdConfigurationValue);
            configValue.IdUsuario = UsuarioLogueadoDTO().IdUsuario;

            configValue.Eliminar();
            LoadConfigurationValues();
        }

        private void EditConfigurationValue(int IdConfigurationValue)
        {
            ConfigurationValue configValue = ConfigurationValueHome.GetById(IdConfigurationValue);

            txtCode.Text = configValue.Code;
            txtDescription.Text = configValue.Description;
            txtValue.Text = configValue.Value;
            txtCode.Enabled = false;

            SessionManager.SetSelectedConfigurationValue(configValue, Session);
            mpeConfiguration.Show();
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            if (ddlConfigurationValues.SelectedIndex == 0)
                LoadConfigurationValues();
            else
            {
                string code = ddlConfigurationValues.SelectedItem.ToString();
                ConfigurationValue value = ConfigurationValueHome.GetByCode(code);
                IList<ConfigurationValue> valueList = new List<ConfigurationValue>();

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