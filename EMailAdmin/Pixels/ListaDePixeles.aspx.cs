using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Pixels
{
    public partial class ListaDePixeles : CustomPage
    {
        protected override void CustomPageLoad(object sender, EventArgs e)
        {

        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            var nombre = txtName.Text;

            Search(nombre);

        }

        protected void Search(string nombre)
        {
            IList<Pixel> pixeles = PixelHome.BuscarPixels(nombre);

            grvPixels.DataSource = pixeles;
            grvPixels.DataBind();

        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("EdicionDePixel.aspx");
        }

        protected void OnRowCommand_EditItem(object sender, GridViewCommandEventArgs e)
        {
            int idPixel = (int)this.grvPixels.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value;

            if (e.CommandName.Equals("Edit"))
            {
                Response.Redirect("EdicionDePixel.aspx?IdPixel=" + idPixel);
            }
        }

    }
}