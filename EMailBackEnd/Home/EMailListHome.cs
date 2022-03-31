using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.DTO;
using CapaNegocioDatos.CapaNegocio;

namespace EMailAdmin.BackEnd.Home
{
    public class EMailListHome
    {
        public static IList<EMailList> FindAll()
        {
            return DAOLocator.Instance().GetDaoEMailList().FindAll();
        }

        public static IList<EMailListDTO> FindByFilters(int idLocacion, int IdEmailListType, string Correoelectronico)
        {
            return DAOLocator.Instance().GetDaoEMailListDTO().FindByFilters(idLocacion, IdEmailListType, Correoelectronico);
        }

        public static IList<EMailListUsuarioDTO> Find_Tx_Usuarios(string Nombre, string Apellido, string CorreoElectronico, int idLocacion, int idEmailListType)
        {
            //para idcategoria se puede usar UsuarioCategoria.USUARIO_INTERNO --> habria que hacerlo publico
            //-1 para que traiga todas las categorias
            return DAOLocator.Instance().GetDaoEMailListUsuarioDTO().FindByFilters(Nombre, Apellido,CorreoElectronico, idLocacion, idEmailListType, -1); 
        }

        public static IList<EMailListUsuarioDTO> FindForPrepurchace(int idLocacion)
        {
            return DAOLocator.Instance().GetDaoEMailListUsuarioDTO().FindForPrepurchace(idLocacion);
        }

        public static IList<EMailListUsuarioDTO> FindUsersMailList(int idLocacion, string EmailListTypeCode)
        {
            return DAOLocator.Instance().GetDaoEMailListUsuarioDTO().FindUsersMailList(idLocacion, EmailListTypeCode);
        }

        public static IList<EMailList> Find(string code)
        {
            return DAOLocator.Instance().GetDaoEMailList().Find(code);
        }

        public static EMailList Get(int id)
        {
            return DAOLocator.Instance().GetDaoEMailList().Get(id);
        }
    }
}
