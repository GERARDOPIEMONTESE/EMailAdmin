using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Controls.SignatureType
{
    public partial class SignatureTypeSelector : UserControl
    {
        #region Delegate

        public delegate void SignatureGridLoadComplete(object sender, EventArgs e);
        public delegate void CloseSignaturePress(object sender, EventArgs e);

        #endregion

        #region Constants

        private const int CHECKBOXCELL = 0;
        private const int DESCRIPCIONCELL = 1;

        #endregion Constants

        #region Constructor

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSelector();
            }
        }

        #endregion Constructor

        #region Properties

        public IList<BackEnd.Domain.SignatureType> GetSelectedItems()
        {
            var result = new List<BackEnd.Domain.SignatureType>();
            foreach (GridViewRow row in grvSignatureType.Rows)
            {
                var checkRow = (DataControlFieldCell)row.Cells[CHECKBOXCELL];
                if (((CheckBox)checkRow.Controls[1]).Checked)
                {
                    var signType = new BackEnd.Domain.SignatureType
                                       {
                                           Id = Convert.ToInt32(grvSignatureType.DataKeys[row.RowIndex].Value),
                                           Description = row.Cells[DESCRIPCIONCELL].Text
                                       };
                    result.Add(signType);
                }
            }

            return result;
        }

        public void SelectItems(IList<BackEnd.Domain.SignatureType> items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    foreach (GridViewRow row in grvSignatureType.Rows)
                    {
                        if (item.Id == Convert.ToInt32(grvSignatureType.DataKeys[row.RowIndex].Value))
                        {
                            var checkRow = (DataControlFieldCell) row.Cells[CHECKBOXCELL];
                            ((CheckBox) checkRow.Controls[1]).Checked = true;
                        }
                    }
                }
            }
        }

        public string GetSelectedItemsText()
        {
            var sb = new StringBuilder();
            foreach (var signatureType in GetSelectedItems())
            {
                if (sb.ToString() != "")
                {
                    sb.Append(", ");
                }
                sb.Append(signatureType.Description);
            }
            return sb.ToString();
        }

        #endregion

        #region Events

        public event SignatureGridLoadComplete SignatureGridLoadCompleted;

        public void OnSignatureGridLoadCompleted(EventArgs e)
        {
            SignatureGridLoadComplete handler = SignatureGridLoadCompleted;
            if (handler != null) handler(this, e);
        }

        public event CloseSignaturePress CloseSignaturePressed;

        public void OnCloseSignaturePressed(EventArgs e)
        {
            CloseSignaturePress handler = CloseSignaturePressed;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods

        protected void GrvCountriesRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.background ='#ecf0f9'");
                e.Row.Attributes.Add("onmouseout", "this.style.background ='#ffffff'");
            }
        }

        protected void BtnCloseOnClick(object sender, EventArgs e)
        {
            OnCloseSignaturePressed(EventArgs.Empty);
        }

        #endregion Methods

        #region Private Methods

        private void LoadSelector()
        {
            grvSignatureType.DataSource = SignatureTypeHome.FindAll();
            grvSignatureType.DataBind();
            OnSignatureGridLoadCompleted(EventArgs.Empty);
        }

        #endregion Private Methods
    }
}