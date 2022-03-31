using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.ExternalServices.Data;
using EMailAdmin.BackEnd.Domain.External;
using System.Web.Script.Serialization;

namespace EMailAdmin
{
    public partial class TestOracle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public void Buscar(){

           
            
            //IList<PrepurchaseInformation> list = ExternalDAOLocator.Instance().GetDaoPrepurchase().FindMinimunBalance(1, 1);
            //int cantidad = list.Count();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int pais = 0;
            int.TryParse(txtPais.Text, out pais);

            int voucher = 0;
            int.TryParse(txtDato.Text, out voucher);

            if (pais > 0 && voucher>0)
            {
                var x = ExternalDAOLocator.Instance().GetDaoIssuanceInformation().Get(pais, voucher.ToString());
                
                JavaScriptSerializer jsSer = new JavaScriptSerializer();
                string sObj = jsSer.Serialize(x);
                txtRst.Text = sObj;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var x = ExternalDAOLocator.Instance().GetDaoRateInformation().Get(540, "AC60","","10");
            JavaScriptSerializer jsSer = new JavaScriptSerializer();
            string sObj = jsSer.Serialize(x);
            txtRst.Text = sObj;
        }
    }
}