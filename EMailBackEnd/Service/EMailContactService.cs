using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Data;
using CapaNegocioDatos.CapaNegocio;
using CapaNegocioDatos.CapaHome;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Exceptions;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Service
{
    public class EMailContactService : IEMailContactService
    {
        #region Attributes

        private IDAOEMailContact _DaoEMailContact;

        #endregion

        #region Properties

        public IDAOEMailContact DaoEMailContact
        {
            get
            {
                return _DaoEMailContact;
            }
            set
            {
                _DaoEMailContact = value;
            }
        }
        #endregion

        #region Private methods

        private EMailContact GetContact(EMailContactDTO dto)
        {
            EMailContact contact = new EMailContact();

            contact.Id = dto.Id;
            contact.Name = dto.Name;
            contact.EMail = dto.EMail;
            contact.EMailContactType = DAOLocator.Instance().GetDaoEMailContactType().Get(dto.IdEMailContactType);
            contact.IdEstado = ObjetoNegocio.Creado();
            contact.IdUsuario = dto.IdUser;

            foreach (Locacion location in dto.Countries)
            {
                EMailContactLocation emailContactLocation = new EMailContactLocation();
                emailContactLocation.IdLocation = location.Id;

                contact.Location.Add(emailContactLocation);
            }

            foreach (int IdLanguage in dto.Description.Keys)
            {
                EMailContactContent content = new EMailContactContent();

                content.Language = IdiomaHome.Obtener(IdLanguage);
                content.ContentText = dto.Description[IdLanguage];

                contact.Content.Add(content);
            }

            return contact;
        }

        private void ValidateValues(EMailContactDTO dto)
        {
            if (dto.Name == null || dto.Name.Length == 0)
            {
                throw new NonValidValueException("Invalid name value");
            }

            if (dto.EMail == null || dto.EMail.Length == 0)
            {
                throw new NonValidValueException("Invalid e-mail value");
            }
        }

        #endregion

        #region IEMailContactService Members

        public void Save(DTO.EMailContactDTO dto)
        {
            try
            {
                ValidateValues(dto);
                DaoEMailContact.Persistir(GetContact(dto));
            }
            catch (NonValidValueException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new NonSavedObjectException(ex.Message + ex.StackTrace, ex);
            }
        }

        public void Delete(EMailContactDTO dto)
        {
            Delete(dto.Id);
        }

        public void Delete(int IdEMailContact)
        {
            try
            {
                EMailContact contact = DaoEMailContact.Get(IdEMailContact);
                contact.IdEstado = ObjetoNegocio.Eliminado();
                DaoEMailContact.Eliminar(contact);
            }
            catch (Exception ex)
            {
                throw new NonEliminatedObjectException(ex.Message + ex.StackTrace, ex);
            }
        }

        #endregion
    }
}
