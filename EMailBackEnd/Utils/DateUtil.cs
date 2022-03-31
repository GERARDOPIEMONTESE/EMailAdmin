using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;

namespace EMailAdmin.BackEnd.Utils
{
    public class DateUtil
    {      
        public static DateTime ConvertToDate(string date)
        {
            return Convert.ToDateTime(date);
        }

        public static string FormatToCompleteDate(DateTime date, int idLanguage)
        {
            if (idLanguage == IdiomaHome.ObtenerPorCodigo(Idioma.INGLES).Id)
            {
                return MonthTraduction(idLanguage, String.Format("{0:/MM dd yyyy}", date));
            }

            return MonthTraduction(idLanguage, String.Format("{0:dd MM yyyy}", date));
        }

        public static string FormatToShortDate(DateTime date, int idLanguage)
        {
            if (idLanguage == IdiomaHome.ObtenerPorCodigo(Idioma.INGLES).Id)
            {
                return String.Format("{0:MM/dd/yyyy}", date);
            }

            return String.Format("{0:dd/MM/yyyy}", date);
        }

        private static string MonthTraduction(int idLanguage, string date)
        {
            if (idLanguage == IdiomaHome.ObtenerPorCodigo(Idioma.ESPANOL).Id)
            {
                return date.Replace(" 01 ", " Enero ").Replace(" 02 ", " Febrero ").
                    Replace(" 03 ", " Marzo ").Replace(" 04 ", " Abril ").Replace(" 05 ", " Mayo ").
                    Replace(" 06 ", " Junio ").Replace(" 07 ", " Julio ").Replace(" 08 ", " Agosto ").
                    Replace(" 09 ", " Septiembre ").Replace(" 10 ", " Octubre ").
                    Replace(" 11 ", " Noviembre ").Replace(" 12 ", " Diciembre ");
            }

            if (idLanguage == IdiomaHome.ObtenerPorCodigo(Idioma.INGLES).Id)
            {
                return date.Replace("/01 ", "January ").Replace("/02 ", "February ").
                    Replace("/03 ", "March ").Replace("/04 ", "April ").Replace("/05 ", "May ").
                    Replace("/06 ", "June ").Replace("/07 ", "July ").Replace("/08 ", "August ").
                    Replace("/09 ", "September ").Replace("/10 ", "October ").
                    Replace("/11 ", "November ").Replace("/12 ", "December ");
            }

            if (idLanguage == IdiomaHome.ObtenerPorCodigo(Idioma.PORTUGUES).Id)
            {
                return date.Replace(" 01 ", " Janeiro ").Replace(" 02 ", " Fevereiro ").
                    Replace(" 03 ", " Marco ").Replace(" 04 ", " Abril ").Replace(" 05 ", " Maio ").
                    Replace(" 06 ", " Junho ").Replace(" 07 ", " Julho ").Replace(" 08 ", " Agosto ").
                    Replace(" 09 ", " Setembro ").Replace(" 10 ", " Outubro ").
                    Replace(" 11 ", " Novembro ").Replace(" 12 ", " Dezembro ");
            }
            return date;
        }

        public static Nullable<DateTime> getFiltroFecha(string date)
        {
            Nullable<DateTime> fecha = null;
            DateTime filtroFecha;
            if (DateTime.TryParse(date, out filtroFecha))
                fecha = filtroFecha;

            return fecha;
        }
    }
}
