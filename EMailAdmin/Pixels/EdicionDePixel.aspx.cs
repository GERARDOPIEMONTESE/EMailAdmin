using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using ControlMenu;

namespace EMailAdmin.Pixels
{
    public partial class EdicionDePixel : CustomPage
    {
        protected override void CustomPageLoad(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CargarControles();
                btnCancel.Text = "Cancel";

                if (string.IsNullOrEmpty(Request.QueryString["IdPixel"]))
                {
                    btnInsertSave.Text = "Create";
                    btnDelete.Visible = false;
                }
                else
                {
                    CargarDatos();
                    btnInsertSave.Text = "Save";
                }
            }
        }

        #region Controles / Datos
        protected void CargarDatos()
        {
            Pixel pixel = PixelHome.Get(Convert.ToInt32(Request.QueryString["IdPixel"].ToString()));
            PixelData pixelData = pixel.pixel;

            txName.Text = pixel.Name.ToString();
            txUrl.Text = pixelData.PixelURL.ToString();
            txtCM.Text = pixelData.CM;
            txtCN.Text = pixelData.CN;
            txtCS.Text = pixelData.CS;
            txtEA.Text = pixelData.EA;
            txtEC.Text = pixelData.EC;
            txtEL.Text = pixelData.EL;
            txtUID.Text = pixelData.UID;
            txtV.Text = pixelData.V;
            txtT.Text = pixelData.T;
        }

        protected void CargarControles()
        {
            txUrl.Text = "PixelURL";
            txtT.Text = "1";
            txtV.Text = "event";
            txtEC.Text = "email";
            txtEL.Text = "${GUID, NEW}$";
            txtCS.Text = "assistcard";
        }
        #endregion

        #region Metodos Botones
        protected void btnInsertSave_Click(object sender, EventArgs e)
        {
            var name = txName.Text;
            var url = txUrl.Text;
            Pixel pixel;

            if (string.IsNullOrEmpty(Request.QueryString["IdPixel"]))
            {
                pixel = new Pixel();
            }
            else
            {
                pixel = PixelHome.Get(Convert.ToInt32(Request.QueryString["IdPixel"].ToString()));
            }

            PixelData pixelData = new PixelData();
            pixel.Name = name;
            pixelData.PixelURL = url;
            //pixelData.CID = txtCID.Text;
            pixelData.CM = txtCM.Text;
            pixelData.CN = txtCN.Text;
            pixelData.CS = txtCS.Text;
            pixelData.EA = txtEA.Text;
            pixelData.EC = txtEC.Text;
            pixelData.EL = txtEL.Text;
            //pixelData.TID = txtTID.Text;
            pixelData.UID = txtUID.Text;
            pixelData.V = txtV.Text;
            pixelData.T = txtT.Text;
            pixel.IdUsuario = UsuarioLogueadoDTO().Id;
            pixel.IdEstado = pixel.ObtenerCreado();

            pixel.pixel = pixelData;

            pixel.Persistir();

            btnCancel.Text = "Back";

            Response.Redirect("ListaDePixeles.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var name = txName.Text;
            var url = txUrl.Text;

            Pixel nuevo = PixelHome.Get(Convert.ToInt32(Request.QueryString["IdPixel"].ToString()));
            nuevo.IdUsuario = UsuarioLogueadoDTO().Id;

            nuevo.Eliminar();

            Response.Redirect("ListaDePixeles.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaDePixeles.aspx");
        }
        #endregion
    }
}