using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;
using MenuPortalLibrary.CapaDTO;

namespace EMailAdmin.Administration.Links
{
    public partial class ListaDeLinks : CustomPage
    {
        protected override void CustomPageLoad(object sender, EventArgs e)
        {
          
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            var nombre = txtName.Text;
            var url = txtUrl.Text;

            Search(nombre, url);

        }

        protected void Search(string nombre, string url) 
        {
            IList<Link> links = LinkHome.BuscarLinks(nombre, url);

            grvLinks.DataSource = links;
            grvLinks.DataBind();
        
        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("EdicionDeLinks.aspx");
        }

        protected void OnRowCommand_EditItem(object sender, GridViewCommandEventArgs e)
        {
            int idLink = (int)this.grvLinks.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value;

            if (e.CommandName.Equals("Edit"))
            {
                Response.Redirect("EdicionDeLinks.aspx?IdLink=" + idLink);
            }
        }

    }
}