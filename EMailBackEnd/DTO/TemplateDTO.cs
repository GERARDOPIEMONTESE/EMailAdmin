using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.DTO
{
    public class TemplateDTO : ObjetoPersistido
    {
        private const string NAME = "Template";

        public string Name { get; set; }

        public DateTime EffectiveStartDate { get; set; }

        public DateTime EffectiveEndDate { get; set; }

        public int Hierarchy { get; set; }

        public string TemplateTypeDescription { get; set; }
        
        public override string ObtenerNombre()
        {
            return NAME;
        }

        public string EffectiveStartDateDescription
        {
            get { return EffectiveStartDate.ToShortDateString(); }
        }

        public string EffectiveEndDateDescription
        {
            get { return EffectiveEndDate.ToShortDateString(); }
        }

        public string HierarchyDescription
        {
            get { return Hierarchy.ToString(); }
        }

    }
}
