using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOContentImage : IDAOObjetoNegocio
    {
        ContentImage Get(int idContentImage);

        ContentImage Get(int idContent, int idLanguage, string name);
    }
}
