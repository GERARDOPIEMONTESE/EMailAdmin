using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using FrameworkDAC.Parametro;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.DTO;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOEMailListDTO : DAOObjetoPersistido<EMailListDTO>, IDAOEMailListDTO
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        public IList<EMailListDTO> FindByFilters(int idLocacion, int idEmailListType, string CorreoElectronico)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdEmailListType", idEmailListType);
            parameters.AgregarParametro("IdLocacion", idLocacion);
            parameters.AgregarParametro("CorreoElectronico", CorreoElectronico);

            return Buscar(new Filtro(parameters, "dbo.EMailList_Tx_Filters"), false);
        }

        protected override void Completar(EMailListDTO ObjetoPersistido, SqlDataReader dr)
        {
            ObjetoPersistido.IdEmailList = Int32.Parse(dr["IdEmailList"].ToString());
            ObjetoPersistido.Pais = (string)dr["LocacionNombre"];
            ObjetoPersistido.EmailListType = (string)dr["EmailListTypeDescription"];
            ObjetoPersistido.Code = (string)dr["EmailListTypeCode"];
            ObjetoPersistido.Nombre = (string)dr["UsuarioNombre"];
            ObjetoPersistido.Apellido = (string)dr["UsuarioApellido"];
            ObjetoPersistido.CorreoElectronico = (string)dr["UsuarioCorreoElectronico"];    
        }

    }

    public class DAOEMailListUsuarioDTO : DAOObjetoPersistido<EMailListUsuarioDTO>, IDAOEMailListUsuarioDTO
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        public IList<EMailListUsuarioDTO> FindByFilters(string Nombre, string Apellido, string CorreoElectronico ,int idLocacion, int idEmailListType, int idCategoria)
        {
            var parameters = new Parametros();            
            parameters.AgregarParametro("Nombre", Nombre);
            parameters.AgregarParametro("Apellido", Apellido);
            parameters.AgregarParametro("CorreoElectronico", CorreoElectronico);
            parameters.AgregarParametro("IdLocacion", idLocacion);
            if (idCategoria>0) parameters.AgregarParametro("IdCategoriaUsuario", idCategoria);
            parameters.AgregarParametro("IdEmailListType", idEmailListType);

            return Buscar(new Filtro(parameters, "dbo.EMailList_Tx_Usuarios"), false);
        }

        protected override void Completar(EMailListUsuarioDTO ObjetoPersistido, SqlDataReader dr)
        {
            ObjetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString());
            ObjetoPersistido.Nombre = (string)dr["Nombre"];
            ObjetoPersistido.Apellido = (string)dr["Apellido"];
            ObjetoPersistido.CorreoElectronico = (string)dr["CorreoElectronico"];
            ObjetoPersistido.Idioma = new Idioma() { Id = Convert.ToInt32(dr["IdIdioma"]), Cultura = dr["Cultura"].ToString(), Descripcion="", DescripcionIngles="", DescripcionPortugues="" }; 
        }


        public IList<EMailListUsuarioDTO> FindForPrepurchace(int Pais)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Pais", Pais);
            
            return Buscar(new Filtro(parameters, "dbo.EMailList_Prepurchace"), false);
        }

        public IList<EMailListUsuarioDTO> FindUsersMailList(int Pais, string EmailListTypeCode)
        {
            var parameters = new Parametros();
            if (Pais>0) parameters.AgregarParametro("Pais", Pais);
            if (EmailListTypeCode!="") parameters.AgregarParametro("EmailListTypeCode", EmailListTypeCode);

            return Buscar(new Filtro(parameters, "dbo.EMailList_Usuarios"), false);
        }
    }
    public class DAOEMailList : DAOObjetoNegocio<EMailList>, IDAOEMailList
    {

        #region IDAOEMailList Members       

        public EMailList Get(int id)
        {
            return Obtener(id);
        }
        
        #endregion

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosCrear(EMailList objetoNegocio)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdEmailListType", objetoNegocio.IdEmailListType);
            parameters.AgregarParametro("IdLocacion", objetoNegocio.IdPais);            
            parameters.AgregarParametro("IdUsuario", objetoNegocio.IdUsuarioEL);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(EMailList objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(EMailList objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEmailList", objetoNegocio.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(EMailList objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEmailList", objetoNegocio.Id);
            parameters.AgregarParametro("IdEmailListType", objetoNegocio.IdEmailListType);
            parameters.AgregarParametro("IdLocacion", objetoNegocio.IdPais);
            parameters.AgregarParametro("IdUsuario", objetoNegocio.IdUsuarioEL);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override void Completar(EMailList ObjetoPersistido, SqlDataReader dr)
        {
            throw new NotImplementedException();
        }

        public IList<EMailList> FindAll()
        {
            throw new NotImplementedException();
        }

        public IList<EMailList> Find(string code)
        {
            throw new NotImplementedException();
        }

        public EMailList GetByCode(string code)
        {
            throw new NotImplementedException();
        }        
    }
}