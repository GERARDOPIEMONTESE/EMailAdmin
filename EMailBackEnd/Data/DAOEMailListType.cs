using System;
using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Negocio;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOEMailListType : DAOObjetoNegocio<EMailListType>, IDAOEMailListType
    {
        bool bField_UsusAsignados = false;
        protected override void Completar(EMailListType objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdEMailListType"]);
            objetoPersistido.Code = dr["Code"].ToString();
            objetoPersistido.Description = dr["Description"].ToString();
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
            if (bField_UsusAsignados)
            {
                //solo trae este campo en Obtener(Id)
                objetoPersistido.UsuariosAsignados = Convert.ToInt32(dr["UsuariosAsignados"].ToString());
                bField_UsusAsignados = false;
            }
        }

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        #region IDAOEMailListType Members

        public IList<EMailListType> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.EMailListType_Tx_Filters"));
        }

        public IList<EMailListType> Find(string code)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Code", code);

            return Buscar(new Filtro(parameters, "dbo.EMailListType_Tx_Code"));
        }

        public IList<EMailListType> FindByFilters(string description)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Description", description);

            return Buscar(new Filtro(parameters, "dbo.EMailListType_Tx_Filters"));
        }


        public EMailListType Get(int id)
        {
            bField_UsusAsignados = true;
            return Obtener(id);
        }

        public EMailListType GetByCode(string code)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Code", code);

            return Obtener(new Filtro(parameters, "dbo.EMailListType_Tx_Code"));
        }

        #endregion

        protected override Parametros ParametrosCrear(EMailListType objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("Code", objetoNegocio.Code);
            parameters.AgregarParametro("Description", objetoNegocio.Description);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(EMailListType objetoNegocio)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdEmailListType", objetoNegocio.Id);
            parameters.AgregarParametro("Code", objetoNegocio.Code);
            parameters.AgregarParametro("Description", objetoNegocio.Description);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Modificado());

            return parameters;
        }

        protected override Parametros ParametrosEliminar(EMailListType objetoNegocio)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdEmailListType", objetoNegocio.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(EMailListType objetoNegocio)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdEmailListType", objetoNegocio.Id);
            parameters.AgregarParametro("Code", objetoNegocio.Code);
            parameters.AgregarParametro("Description", objetoNegocio.Description);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }
    }
}
