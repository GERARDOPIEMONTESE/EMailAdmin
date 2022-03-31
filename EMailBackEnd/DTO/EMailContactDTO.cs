using System.Collections.Generic;
using CapaNegocioDatos.CapaNegocio;

namespace EMailAdmin.BackEnd.DTO
{
    public class EMailContactDTO
    {
        #region Attributes

        private IList<Locacion> _Countries = new List<Locacion>();
        private IList<int> _CountryIds = new List<int>();
        private IDictionary<int, string> _Description = new Dictionary<int, string>();

        #endregion

        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public IDictionary<int, string> Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public IList<Locacion> Countries
        {
            get { return _Countries; }
            set { _Countries = value; }
        }
        public IList<int> CountryIds
        {
            get { return _CountryIds; }
            set { _CountryIds = value; }
        }
        public int IdEMailContactType { get; set; }
        public string EMail { get; set; }

        public int IdUser { get; set; }

        #endregion

        #region Constructor

        public EMailContactDTO()
        {

        }

        #endregion Constructor
    }
}