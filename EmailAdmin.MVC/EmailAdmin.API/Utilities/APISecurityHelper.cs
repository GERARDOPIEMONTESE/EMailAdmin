using System;
using System.Threading;
using System.Web.Http;
using CapaNegocioDatos.Servicios;
using DTOMapper.Helpers;
using DTOMapper;

using EmailAdmin.Dto;
using EMailAdmin.BackEnd.Utils;
using EMailAdmin.BackEnd.Home;

namespace EmailAdmin.Api.Utilities
{
    public class APISecurityHelper
    {
        public static void CheckApiAuthentication(DTOFilter filter)
        {
            string user = RequestParameterHelper.GetGenericParameter<string>(filter, "User");
            string password = RequestParameterHelper.GetGenericParameter<string>(filter, "Password");
            try
            {
                ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(user, password);
            }
            catch (Exception)
            {
                DTOApplicationErrorData dtoApplicationErrorData = new DTOMapper.DTOApplicationErrorData();
                dtoApplicationErrorData.StatusCode = DTOResponseBase.FORBIDDEN_ACCESS_STATUS;
                dtoApplicationErrorData.ApplicationStatusCodeSource = DTOResponseBase.GLOBALCODE;
                throw new HttpErrorCustomizedException(dtoApplicationErrorData);
            }
        }
    }
}