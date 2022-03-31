using EMailAdmin.BackEnd.Domain;
using EMailAdmin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMailAdmin.Controls.DynamicValue
{
    public partial class DynamicValueConfig : UserControl
    {
        #region Delegate

        public delegate void CloseDynamicValuePress(object sender, EventArgs e);
        public delegate void SearchPress(object sender, EventArgs e);
        public delegate void ChkPress(object sender, EventArgs e);
        #endregion

        #region Events
        public event CloseDynamicValuePress CloseDynamicValuePressed;

        public void OnCloseDynamicValuePressed(EventArgs e)
        {
            CloseDynamicValuePress handler = CloseDynamicValuePressed;
            if (handler != null) handler(this, e);
        }

        public event SearchPress SearchPressed;

        public void OnSearchPressed(EventArgs e)
        {
            SearchPress handler = SearchPressed;
            if (handler != null) handler(this, e);
        }

        public event ChkPress ChkPressed;

        public void OnChkPressed(EventArgs e)
        {
            ChkPress handler = ChkPressed;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods      

        protected void BtnCloseOnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDynamicValue.Text) && !string.IsNullOrEmpty(txtDynamicName.Text))
            {
                SessionManager.SetDynamicValuesSelected(GetSelectedItems(), Session);
            }
            OnCloseDynamicValuePressed(EventArgs.Empty);            
        }

        #endregion

        #region Properties

        public IList<DynamicCondition> GetSelectedItems()
        {
            var dic = SessionManager.GetDynamicValuesSelected(Session);
            dic.Add(new DynamicCondition()
            {
                DynamicKey = txtDynamicName.Text,
                Value = txtDynamicValue.Text
            });

            return dic;
        }

        public string GetSelectedItemsText()
        {
            if (!string.IsNullOrEmpty(txtDynamicValue.Text) && !string.IsNullOrEmpty(txtDynamicName.Text))
            {
                return txtDynamicValue.Text + "(" + txtDynamicName.Text + ")";
            }
            else
                return "";
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDynamicName.Text = "";
                txtDynamicValue.Text = "";
            }
        }
    }
}