using FrameworkDAC.Negocio;
namespace EMailAdmin.BackEnd.Domain
{
    public class ReportAssociation : ObjetoPersistido
    {
        #region Constants

        private const string NAME = "ReportAssociation";
        #endregion

        #region Properties

        public string TemplateName { get; set; }
        public string HierarchyDescription { get; set; }
        public string GroupDescription { get; set; }
        public string TemplateType { get; set; }
        public string EffectiveStartDate { get; set; }
        public string EffectiveEndDate { get; set; }
        
        #endregion

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
