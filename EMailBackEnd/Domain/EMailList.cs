using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using System.Collections.Generic;
using CapaNegocioDatos.CapaNegocio;
using System;

namespace EMailAdmin.BackEnd.Domain
{
    public class EMailList : ObjetoNegocio
    {
        #region Constant

        private const string NAME = "EMailList";

        #endregion Constant

        #region Constructor
        public EMailList()
        {
        }
        public EMailList(int idEmailList, int idUser)
        {
            Id = idEmailList;
            IdUsuario = idUser;            
            IdPais = -1;
            IdUsuarioEL = -1;
        }
        #endregion

        #region Properties

        public int IdEmailListType { get; set; }
        public int IdPais { get; set; }
        public int IdUsuarioEL { get; set; }
        #endregion 

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoEMailList();
        }


        #endregion Methods
    }
    
}
