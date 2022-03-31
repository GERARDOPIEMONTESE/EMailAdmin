using System;
using System.Globalization;
using System.Web.UI;

namespace EMailAdmin.Controls.Picture
{
    public partial class PictureUploader : UserControl
    {
        #region Constructor

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GeneratePicture();
                var picture = (System.Web.UI.WebControls.Image)FindControl("imgPicture");
                if (picture != null && picture.ImageUrl != "")
                {
                    picture.Attributes.Add("onchange", "return checkFileExtension(this);");
                }
            }
        }

        #endregion Constructor

        #region Propiedades

        public string IdUsuario
        {
            get { return HfIdUsuario.Value; }
            set { HfIdUsuario.Value = value; }
        }

        #endregion

        #region Methods

        private void GeneratePicture()
        {
            var jQuery = "";
            var identifier = Guid.NewGuid();

            var handler = "/Handlers/SaveImage.ashx?IdUsuario=" + IdUsuario + "&Ticks=" +
                             DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture);

            var objectVideo = "'<img id=" + identifier.ToString() + " src=/Image/ShowImage.ashx?IdUsuario=" +
                                 IdUsuario + "&ImagenMiniatura=1&identifier='+Math.random()+'&Id=" +
                                 identifier.ToString() + "&ext='+extension+'" + " style=\"padding-right:5px;\" />'\n";

            jQuery += GenerateScript("0", identifier.ToString(), objectVideo, "jpg|png|jpeg", handler);

            Page.ClientScript.RegisterClientScriptBlock(GetType(), "onload", jQuery);
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "onload", "DeleteClick(0);");
        }

        private string GenerateScript(string prefix, string id, string objectHtml, string validExtensions, string handler)
        {
            string jQuery = "";

            jQuery += "<script type=\"text/javascript\">var extension; \n$(document).ready(function() {\n";
            jQuery += "new Ajax_upload('#up_button" + prefix + "', {\n";
            jQuery += "action: '" + handler + "&Id=" + id + "',\n";
            jQuery += "onSubmit: function(file, ext) {extension = ext;\n";
            jQuery += "if (!(ext && /^(" + validExtensions + ")$/.test(ext))) {\n";
            jQuery += "alert('Error: Solo se permiten archivos: " + validExtensions.Replace("|", ", ") + "');\n";
            jQuery += "return false;\n";
            jQuery += "}\n";
            jQuery += "this.disable();\n";
            jQuery += "$('#loading" + prefix + "').css('display', 'block');\n";
            jQuery += "$('#up_button" + prefix + "').css('display', 'none');\n";
            jQuery += "$('#up_button" + prefix + "').attr('disabled', 'true');\n";
            jQuery += "},\n";
            jQuery += "onComplete: function(file, response) {\n";
            jQuery += "$('#loading" + prefix + "').css('display', 'none');\n";
            jQuery += "$('#result" + prefix + "').css('display', 'block');\n";
            jQuery += "$('#image" + prefix + "').css('display', 'block');\n";
            jQuery += "$('#del_button" + prefix + "').css('display', 'block');\n";
            jQuery += "$('#panelConfirmar" + prefix + "').css('display', 'block');\n";

            jQuery += "var Node1 = document.getElementById('result" + prefix + "'); \n";
            jQuery += "var len = Node1.childNodes.length;\n";

            jQuery += "for(var i = 0; i < len; i++)\n";
            jQuery += "{           \n";
            jQuery += "if(Node1.childNodes[i].id =='" + id + "')\n";
            jQuery += "{\n";
            jQuery += "Node1.removeChild(Node1.childNodes[i]);\n";
            jQuery += "}\n";
            jQuery += "}\n";

            jQuery += "$('#result" + prefix + "').append(" + objectHtml + ");\n";

            jQuery += "this.enable();\n";
            jQuery += "}\n";
            jQuery += "});\n";
            jQuery += "});</script>\n";

            return jQuery;
        }

        #endregion Methods
    }
}