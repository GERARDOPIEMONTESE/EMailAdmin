using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Domain
{
    public class ConditionVariableTextContent : ObjetoNegocio
    {
        #region Constants

        private const string NAME = "ConditionVariableTextContent";

        #endregion

        #region Properties

        public int IdConditionVariableText { get; set; }

        public Idioma Language { get; set; }

        public string Content { get; set; }

        #endregion

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoConditionVariableTextContent();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
