using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using CapaNegocioDatos.CapaNegocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class EMailContactContent : ObjetoNegocio
    {
        private const string NAME = "EMailContactContent";

        #region Attributes

        private int _IdEMailContact;

        private Idioma _Language;

        private string _ContentText;

        #endregion

        #region Properties

        public int IdEMailContact
        {
            get
            {
                return _IdEMailContact;
            }
            set
            {
                _IdEMailContact = value;
            }
        }

        public Idioma Language
        {
            get
            {
                return _Language;
            }
            set
            {
                _Language = value;
            }
        }

        public string ContentText
        {
            get
            {
                return _ContentText;
            }
            set
            {
                _ContentText = value;
            }
        }

        #endregion

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            throw new NotImplementedException();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
