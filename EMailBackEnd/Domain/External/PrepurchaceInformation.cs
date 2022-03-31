using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.DTO;
using System.Globalization;
using CapaNegocioDatos.CapaNegocio;

namespace EMailAdmin.BackEnd.Domain.External
{
    public class PrepurchaseCountryInformation
    {
        public Pais Country { get; set; }
        public List<PrepurchaseInformation> prepurchaseAccount { get; set; }
        public IList<EMailListUsuarioDTO> EMailListUsuario { get; set; }

        public string CountryDesc()
        {
            return string.Format("({0}) {1}", Country.Codigo, Country.Nombre);
        }
    }

    public class PrepurchaseInformation : ObjetoPersistido
    {
        public enum UnitType{
            NO_CONFIG = 0,
            DIAS = 1,
            TARJETAS = 2            
        }        

        private const string NAME = "Prepurchase";

        #region Properties

        public int CountryCode { get; set; }

        public double Cant_precompra { get; set; }

        public double Cant_utilizado { get; set; }

        public UnitType Type { get; set; }
        
        public string AgencyCode { get; set; }

        public string AgencyName { get; set; }

        public string AgencySuc { get; set; }

        public string Product { get; set; }

        public string Tarif { get; set; }
     
        #endregion

        public double Saldo
        {
            get
            {
                return Cant_precompra - Cant_utilizado;
            }
        }

        public static string TypeDesc(UnitType type, Idioma idioma, int cantidad)
        {
            bool MayorA1 = (cantidad > 1 ? true : false);
            string valor = "";
            switch (type)
            {
                case UnitType.NO_CONFIG:
                    break;
                case UnitType.DIAS:
                    valor = TraducirDias(GetCulture(idioma.IdIdioma), MayorA1);
                    break;
                case UnitType.TARJETAS:
                    valor = TraducirTarjetas(GetCulture(idioma.IdIdioma), MayorA1);
                    break;
                default:
                    break;
            }
            return valor;
        }
        public static UnitType ConvertToUnitType(string type)
        {
            switch (type)
            {
                case "D": return UnitType.DIAS;
                case "T": return UnitType.TARJETAS;
                default: return UnitType.NO_CONFIG;
            }
        }
        #region traduccion
        private static string TraducirDias(string cultura, bool MayorA1)
        {
            string valor = "";
            switch (cultura)
            {
                case "es":
                    valor = (MayorA1? "días": "día");
                    break;
                case "en": 
                    valor = (MayorA1? "days": "day");
                    break;
                case "pt":
                    valor = "día";
                    break;
                default:
                    break;
            }
            return valor;
        }
        private static string TraducirTarjetas(string cultura, bool MayorA1)
        {
            string valor = "";
            switch (cultura)
            {
                case "es":
                    valor = (MayorA1 ? "tarjetas" : "tarjeta");
                    break;
                case "en":
                    valor = (MayorA1 ? "cards" : "card");
                    break;
                case "pt":
                    valor = (MayorA1 ? "cartões" : "cartão");
                    break;
                default:
                    break;
            }
            return valor;
        }

        private static string GetCulture(int idIdioma)
        {
            switch (idIdioma)
            {
                case 1:
                    return "es";
                case 2:
                    return "en";
                case 3:
                    return "pt";
                default://RESTO
                    return "es";
            }
        }
        #endregion

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
