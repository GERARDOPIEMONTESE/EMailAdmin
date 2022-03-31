using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class ConfigurationValueHome
    {
        public static ConfigurationValue GetById(int IdConfigurationValue)
        {
            return DAOConfigurationValue.Instance().GetById(IdConfigurationValue);
        }

        public static IList<ConfigurationValue> FindAll()
        {
            return DAOConfigurationValue.Instance().FindAll();
        }

        public static ConfigurationValue GetByCode(string code)
        {
            return DAOConfigurationValue.Instance().GetByCode(code);
        }

        public static string Obtener(string codigo)
        {
            var cv = DAOConfigurationValue.Instance().GetByCode(codigo);
            if (cv != null && cv.Id > 0)
            {
                return cv.Value;
            }
            else
                return "";
        }

        internal static bool Habilita(string codigo)
        {
            string valor = Obtener(codigo);

            bool bHabilita = false;
            bool.TryParse(valor, out bHabilita);

            return bHabilita;
        }
        
        internal static string GetMainBenefitsDocumentLinks()
        {
            return Obtener("MainBenefitsDocumentLinks");
        }

        internal static string GetApplicationUrl()
        {
            return Obtener("ApplicationUrl");
        }

        internal static string GetReportPath()
        {
            return Obtener("ReportPath");
        }

        internal static int GetMaxTimeAlert()
        {
            string valor = Obtener("MaxTimeAlert");
            int maxTimeAlert = 0;
            int.TryParse(valor, out maxTimeAlert);

            return maxTimeAlert;
        }

        internal static string GetAttachHeaderPath()
        {
            return Obtener("AttachHeaderPath");
        }

        internal static string GetAttachFooterPath()
        {
            return Obtener("AttachFooterPath");
        }

        internal static string GetAttachImagePath()
        {
            return Obtener("AttachImagePath");
        }

        internal static string GetContentImageUrl()
        {
            return Obtener("ContentImageUrl");
        }

        internal static string GetHandlerQR()
        {
            return Obtener("HandlerQR");
        }

        internal static bool ValidarHabilitarContinuaTuCompra()
        {
            return Habilita("HabilitarContinuaTuCompra");
        }

        internal static bool ValidaHabilitarMailAttachments_Grouped()
        {
            return Habilita("MAILATTACHMENTS_GROUPED");
        }
    }
}