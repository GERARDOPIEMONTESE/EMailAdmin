using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMailAdmin.Utils;

namespace EMailAdmin.Controls.VariableText
{
    public partial class VariableTextDropDown : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void GridVariableTextLoadComplete(object sender, EventArgs e);
        public delegate void GridVariableTextCancelComplete(object sender, EventArgs e);

        #endregion

        #region Properties

        public IList<BackEnd.Domain.ConditionVariableText_R_VariableText> GetSelectedItems()
        {
            List<BackEnd.Domain.ConditionVariableText_R_VariableText> lst = new List<BackEnd.Domain.ConditionVariableText_R_VariableText>();
            lst.Add(VariableTextSelector1.GetSelectedItem());
            return lst;
        }

        public void SelectItems(IList<BackEnd.Domain.ConditionVariableText_R_VariableText> items)
        {
            VariableTextSelector1.SelectItem(items[0]);
            CompleteDropDownList();
        }

        #endregion Properties

        #region Events

        public event GridVariableTextLoadComplete GridVariableTextLoadCompleted;

        public void OnGridVariableTextLoadCompleted(EventArgs e)
        {
            GridVariableTextLoadComplete handler = GridVariableTextLoadCompleted;
            if (handler != null) handler(this, e);
        }

        public event GridVariableTextCancelComplete GridVariableTextCancelCompleted;
        public void OnGridVariableTextCancelCompleted(EventArgs e)
        {
            GridVariableTextCancelComplete handler = GridVariableTextCancelCompleted;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                ddlVariableText.Attributes.Add("onclick", "javascript:ShowSelectorPopUp();");
            }
        }

        protected void GridVariableTextLoadedCompleted(object sender, EventArgs e)
        {
            OnGridVariableTextLoadCompleted(EventArgs.Empty);
        }

        protected void AgregarButtonPressed(object sender, EventArgs e)
        {            
            if (VariableTextSelector1.IsValid())
            {
                CompleteDropDownList();
                mpeSelector.Hide();
            }
            else
            {                
                mpeSelector.Show();
            }
        }

        protected void CloseButtonPressed(object sender, EventArgs e)
        {
            mpeSelector.Hide();
            OnGridVariableTextCancelCompleted(EventArgs.Empty);
        }

        #endregion Methods

        #region Private Methods

        private void CompleteDropDownList()
        {
            ddlVariableText.Text = VariableTextSelector1.GetSelectedItemText();            
        }

        #endregion Private Methods

        #region Public Methods

        public void CleanAndBind()
        {
            VariableTextSelector1.Bind();
            CompleteDropDownList();
        }

        #endregion Public Methods

    }
}