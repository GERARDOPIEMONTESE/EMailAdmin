using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Strategies.Attachment;
using System;

namespace EMailAdmin.BackEnd.Domain
{
    public class Estrategy : ObjetoCodificado
    {
        #region Constants
        
        private const string NAME = "Estrategy";
        
        #endregion Constants

        #region Properties

        public string Code { get; set; }
        public string Description { get { return Descripcion; } }
        public string Class { get; set; }
        public string AttachName { get; set; }
        public string AttachName_EN { get; set; }
        public string AttachName_PT { get; set; }
        public string AttachType { get; set; }
        public string UrlDownload { get; set; }
        public string KeyError { get; set; }
        public string JsonParams { get; set; }
        public int IdTemplate { get; set; }

        #endregion Properties

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        #endregion Methods

        public IAttachStrategy GetStrategy()
        {
            return (IAttachStrategy)Activator.CreateInstance(Type.GetType(Class));
        }
    }
}
