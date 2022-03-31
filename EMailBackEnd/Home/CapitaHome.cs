using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class CapitaHome
    {
        public static IList<Capita> FindAll(int countryCode, string capita, string plan)
        {
            return DAOLocator.Instance().GetDAOCapita().FindAll(countryCode, capita, plan);
        }

        public static int[] CondicionesTiposDocumento(string[] activados)
        {
            List<int> idsActivados = new List<int>();
            Dictionary<int, string> dic = new Dictionary<int, string>();

            dic = DAOLocator.Instance().GetDAOCapita().GetCondicionesTipoDocumento();
            foreach (var item in dic)
            {
                if (activados.Contains(item.Value))
                    idsActivados.Add(item.Key);
            }

            return idsActivados.ToArray();
        }
    }
}
