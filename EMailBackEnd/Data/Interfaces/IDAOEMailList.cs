using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.DTO;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOEMailList : IDAOObjetoNegocio
    {
        IList<EMailList> FindAll();

        IList<EMailList> Find(string code);

        EMailList Get(int id);

        EMailList GetByCode(string code);

    }

    public interface IDAOEMailListDTO
    {
        IList<EMailListDTO> FindByFilters(int idLocacion, int idEmailListType, string CorreoElectronico);
    }

    public interface IDAOEMailListUsuarioDTO
    {
        IList<EMailListUsuarioDTO> FindByFilters(string Nombre, string Apellido, string CorreoElectronico, int idLocacion, int idEmailListType, int idCategoria);
        IList<EMailListUsuarioDTO> FindForPrepurchace(int Pais);
        IList<EMailListUsuarioDTO> FindUsersMailList(int Pais, string EmailListTypeCode);
    }
}
