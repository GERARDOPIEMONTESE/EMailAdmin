using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.Controls.VariableText
{
    public partial class VariableTextSelector : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void VariableTextGridLoadComplete(object sender, EventArgs e);
        public delegate void AgregarVariableTextPress(object sender, EventArgs e);
        public delegate void CloseVariableTextPress(object sender, EventArgs e);

        #endregion

        #region Constants

        private const int CHECKBOXCELL = 0;
        private const int NAMECELL = 1;

        #endregion Constants

        #region Properties

        public ConditionVariableText_R_VariableText GetSelectedItem()
        {
            var signVariableText = new EMailAdmin.BackEnd.Domain.VariableText
                    {
                        Id = Convert.ToInt32(ddlVariableText.SelectedValue),
                        Name = ddlVariableText.Text
                    };
            ConditionVariableText_R_VariableText result = new BackEnd.Domain.ConditionVariableText_R_VariableText { VariableText = signVariableText, Condition = ddlOperador.Text +" "+ txtCondition.Text, DynamicName = txtDynamicName.Text };

            return result;
        }

        public void SelectItem(ConditionVariableText_R_VariableText item)
        {
            if (item != null)
            {
                ddlVariableText.SelectedValue = item.VariableText.Id.ToString();
                ddlOperador.SelectedValue = item.ConditionOperador;
                txtCondition.Text = item.ConditionValor.Trim();
                txtDynamicName.Text = item.DynamicName;
            }
        }

        protected void ctvCondition_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ConditionVariableText_R_VariableText crvt = GetSelectedItem();
            args.IsValid = crvt.Validar();
            if (!args.IsValid)
                ctvCondition.ErrorMessage = GetLocalResourceObject(crvt.MsgError).ToString();
            else
                txtCondition.Text = crvt.ValorFormateado;

        }

        public string GetSelectedItemText()
        {
            string text = "";
            if (ddlVariableText.SelectedItem.Text == "DynamicValue")
                text = string.Format("{0}$[{1}]$", ddlVariableText.SelectedItem.Text, txtDynamicName.Text);
            else
                text = ddlVariableText.SelectedItem.Text;

            return string.Format("{0} {1} {2}", text, ddlOperador.Text, txtCondition.Text);
        }

        #endregion

        #region Events

        public event VariableTextGridLoadComplete VariableTextGridLoadCompleted;

        public void OnVariableTextGridLoadCompleted(EventArgs e)
        {
            VariableTextGridLoadComplete handler = VariableTextGridLoadCompleted;
            if (handler != null) handler(this, e);
        }

        public event CloseVariableTextPress CloseVariableTextPressed;

        public void OnCloseVariableTextPressed(EventArgs e)
        {
            CloseVariableTextPress handler = CloseVariableTextPressed;
            if (handler != null) handler(this, e);
        }

        public event AgregarVariableTextPress AgregarVariableTextPressed;

        public void OnAgregarVariableTextPressed(EventArgs e)
        {   
            AgregarVariableTextPress handler = AgregarVariableTextPressed;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods

        protected void grvVariableTextRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.background ='#ecf0f9'");
                e.Row.Attributes.Add("onmouseout", "this.style.background ='#ffffff'");
            }
        }

        protected void BtnAgregarOnClick(object sender, EventArgs e)
        {            
            OnAgregarVariableTextPressed(EventArgs.Empty);
        }

        public bool IsValid()
        {            
            return (ctvCondition.IsValid); // && rfvCondicion.IsValid);
        }

        protected void BtnCloseOnClick(object sender, EventArgs e)
        {
            OnCloseVariableTextPressed(EventArgs.Empty);
        }

        #endregion Methods

        #region Private Methods

        public void Bind()
        {            
            ddlVariableText.DataSource = VariableTextHome.FindAll();
            ddlVariableText.DataBind();

            String[] operadores = ConditionVariableText_R_VariableText.GetOperadores();
            ddlOperador.DataSource = operadores;
            ddlOperador.DataBind();

            pnlGrid.Update();
        }

        #endregion Private Methods

    }
}