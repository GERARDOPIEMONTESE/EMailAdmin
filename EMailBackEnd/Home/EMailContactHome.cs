using System.Collections.Generic;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class EMailContactHome
    {
        public static IList<EMailContact> FindAll()
        {
            return DAOLocator.Instance().GetDaoEMailContact().FindAll();
        }

        public static IList<EMailContact> Find(string name)
        {
            return DAOLocator.Instance().GetDaoEMailContact().Find(name);
        }

        public static IList<EMailContact> Find(string name, int idLocation)
        {
            return DAOLocator.Instance().GetDaoEMailContact().Find(name, idLocation);
        }

        public static IList<EMailContact> Find(string name, int idLocation, int idEMailContactType)
        {
            return DAOLocator.Instance().GetDaoEMailContact().Find(name, idLocation, idEMailContactType);
        }

        public static EMailContact Get(int id)
        {
            return DAOLocator.Instance().GetDaoEMailContact().Get(id);
        }

        public static EMailContactDTO GetDTO(int id)
        {
            EMailContact contact = DAOLocator.Instance().GetDaoEMailContact().Get(id);
            var dto = new EMailContactDTO
                          {
                              Id = contact.Id,
                              Name = contact.Name ?? "",
                              EMail = contact.EMail ?? "",
                              IdEMailContactType = contact.EMailContactType != null ? contact.EMailContactType.Id : 0
                          };


            foreach (EMailContactLocation location in contact.Location)
            {
                dto.CountryIds.Add(location.IdLocation);
            }

            foreach (EMailContactContent content in contact.Content)
            {
                if (!dto.Description.Keys.Contains(content.Language.Id))
                {
                    dto.Description.Add(content.Language.Id, content.ContentText);
                }
            }

            return dto;
        }
    }
}