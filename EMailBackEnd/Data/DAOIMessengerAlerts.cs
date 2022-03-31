using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOIMessengerAlerts : DAOObjetoPersistido<EmailAlertChat>, IDAOIMessengerAlerts
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(EmailAlertChat ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.Negocio = dr["Negocio"].ToString();
            ObjetoPersistido.OperatorType = dr["OperatorType"].ToString();
            ObjetoPersistido.LatitudeLocation = EmailAlertChat.ConvertToPoint(dr["LatitudeLocation"].ToString());
            ObjetoPersistido.LongitudeLocation = EmailAlertChat.ConvertToPoint(dr["LongitudeLocation"].ToString());

            if (dr["IsFallBack"] != DBNull.Value)
                ObjetoPersistido.IsFallBack = Convert.ToBoolean(dr["IsFallBack"]);
            else
                ObjetoPersistido.IsFallBack = false;
            ObjetoPersistido.Cantidad = int.Parse(dr["Cantidad"].ToString());
        }

        public IList<EmailAlertChat> GetAlertaChatsEspera()
        {
            Parametros parameters = new Parametros();
            parameters.AgregarParametro("FechaUTC", DateTime.UtcNow);
            
            return Buscar(new Filtro(parameters, "dbo.Chat_Count_NotAssigned_Alert"));            
        }
    }
}
