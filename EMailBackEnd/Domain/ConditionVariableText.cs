using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Domain
{
    public class ConditionVariableText : ObjetoNegocio
    {
        #region Constant

        private const string NAME = "ConditionVariableText";

        #endregion Constant

        #region Attributes

        public string Name { get; set; }

        private IList<ConditionVariableText_R_VariableText> _variablesText = new List<ConditionVariableText_R_VariableText>();

        private IList<ConditionVariableTextContent> _content = new List<ConditionVariableTextContent>();

        #endregion

        #region Properties

        public IList<ConditionVariableText_R_VariableText> VariablesText
        {
            get
            {
                return _variablesText;
            }
            set
            {
                _variablesText = value;
            }
        }

        public IList<ConditionVariableTextContent> Contents
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        #endregion

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoConditionVariableText();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public ConditionVariableTextContent GetContent(int idLanguage)
        {
            ConditionVariableTextContent tc = null;
            try
            {
                tc = this.Contents.Where(c => c.Language.Id == idLanguage).First();
            }
            catch
            {
                if (this.Contents.Count == 1)
                    tc = this.Contents.First();
            }
            return tc;
        }
    }
}