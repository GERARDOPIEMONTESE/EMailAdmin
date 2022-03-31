using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOPixel : IDAOObjetoNegocio
    {
        Pixel Get(int id);

        Pixel Get(string name);

        IList<Pixel> BuscarPixels(string nombre);
    }
}
