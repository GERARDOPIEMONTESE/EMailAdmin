using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using CapaNegocioDatos.CapaNegocio;
using CapaNegocioDatos.CapaHome;

namespace EMailAdmin.Controls.Selectors.Capita
{
    public partial class CapitaSelector : System.Web.UI.UserControl
    {
        private const int colIDCAPITA = 1;
        private const int colIDPLAN = 4;

        private const int  keyIDCAPITA = 0;
        private const int keyCAPITACODIGO = 1;
        private const int keyCAPITA = 2;
        private const int keyIDPLAN = 3;
        private const int keyPLANCODIGO = 4;
        private const int keyPLAN = 5;

        public string CodigoPaisDefault { get; set; }
        
        #region Delegate

        public delegate void CapitaClose(EMailAdmin.BackEnd.Domain.Capita capitaSel);
        public delegate void CapitaOpen(object sender, EventArgs e);

        #endregion

        #region Events
                
        public event CapitaClose CapitaClosed;
        public event CapitaOpen CapitaOpened;

        public void OnCapitaClose(EMailAdmin.BackEnd.Domain.Capita capitaSel)
        {
            var handler = CapitaClosed;
            if (handler != null) handler(capitaSel);
        }

        public void OnCapitaOpen(EventArgs e)
        {
            var handler = CapitaOpened;
            if (handler != null) handler(this, e);
        }
        
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();
            }
        }

        private void CargarCombos()
        {         
            ddlCountry.DataSource = PaisHome.BuscarLazy();
            ddlCountry.DataBind();            
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            mpeCapita.Hide();
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            OnCapitaOpen(EventArgs.Empty);
            Limpiar();
            mpeCapita.Show();
        }

        private void Limpiar()
        {
            ddlCountry.SelectedIndex = GetIndexById(CodigoPaisDefault, ddlCountry);
            grvCapitas.DataSource = new List<BackEnd.Domain.Capita>();
            grvCapitas.DataBind();
            txtDescripcion.Text = "";
            txtPlan.Text = "";            
        }

        private static int GetIndexById(string valorKey, DropDownList ddl)
        {
            int result = -1;
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if (ddl.Items[i].Value == valorKey)
                {
                    result = i;
                }
            }
            return result;
        }

        protected void btnFiltrar_Click(object sender, ImageClickEventArgs e)
        {
            int codigoPais = -1;
            if (int.TryParse(ddlCountry.SelectedItem.Value, out codigoPais))
            {
                grvCapitas.DataSource = CapitaHome.FindAll(codigoPais, txtDescripcion.Text, txtPlan.Text);
                grvCapitas.DataBind();
            }
            mpeCapita.Show();

        }

        protected void btnSelect_Command(object sender, CommandEventArgs e)
        {
            int rowIdx = int.Parse(e.CommandArgument.ToString());
            Pais pais = PaisHome.ObtenerPorCodigo(ddlCountry.SelectedItem.Value);

            OnCapitaClose(new EMailAdmin.BackEnd.Domain.Capita()
            {
                Id = int.Parse(grvCapitas.DataKeys[rowIdx].Values[keyIDCAPITA].ToString()),
                Codigo = grvCapitas.DataKeys[rowIdx].Values[keyCAPITACODIGO].ToString(),
                Descripcion = grvCapitas.DataKeys[rowIdx].Values[keyCAPITA].ToString(),
                 Plan = new Plan() { Id = int.Parse(grvCapitas.DataKeys[rowIdx].Values[keyIDPLAN].ToString()),
                 Codigo= grvCapitas.DataKeys[rowIdx].Values[keyPLANCODIGO].ToString(),
                 Descripcion = grvCapitas.DataKeys[rowIdx].Values[keyPLAN].ToString()},
                Pais = pais
            });
        }

        protected void grvCapitas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[colIDCAPITA].Visible = false;
                e.Row.Cells[colIDPLAN].Visible = false;
            }
        }

        
    }
}