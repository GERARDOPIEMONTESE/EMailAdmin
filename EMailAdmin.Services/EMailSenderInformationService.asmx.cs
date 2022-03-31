using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CapaNegocioDatos.Servicios;
using EMailAdmin.BackEnd.Service;

namespace EMailAdmin.Services
{
    /// <summary>
    /// Summary description for EMailSenderInformationService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EMailSenderInformationService : System.Web.Services.WebService
    {

        [WebMethod]
        public void SetPaxPassedAway(int countryCode, string voucherCode, string nationalId,
            bool passedAway, string user, string password)
        {
            ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(user, password);

            ServiceLocator.Instance().GetPaxPassedAwayService().
                Save(countryCode, voucherCode, nationalId, passedAway);

        }
    }
}
