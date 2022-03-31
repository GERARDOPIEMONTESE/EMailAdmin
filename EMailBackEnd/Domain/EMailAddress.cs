using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Domain
{
    public class EMailAddress : ObjetoNegocio
    {
        private const string NAME = "EMailAddress";

        #region Properties

        public string Name { get; set; }

        public string Address { get; set; }

        public string FullName
        {
            get
            {
                return Name + "[" + Address + "]";
            }
        }
        #endregion

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoEMailAddress();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
