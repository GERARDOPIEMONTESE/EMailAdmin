using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class PixelHome
    {
        public static Pixel Get(int id)
        {
            return DAOLocator.Instance().GetDaoPixel().Get(id);
        }

        public static Pixel Get(string name)
        {
            return DAOLocator.Instance().GetDaoPixel().Get(name);
        }

        public static IList<Pixel> BuscarPixels(string nombre)
        {
            return DAOLocator.Instance().GetDaoPixel().BuscarPixels(nombre);
        }
    }
}
