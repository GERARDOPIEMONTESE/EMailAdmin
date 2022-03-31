using System;
using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Negocio;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOEMailContactType : DAOObjetoNegocio<EMailContactType>, IDAOEMailContactType
    {
        protected override void Completar(EMailContactType objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdEMailContactType"]);
            objetoPersistido.Code = dr["Code"].ToString();
            objetoPersistido.Description = dr["Description"].ToString();
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        #region IDAOEMailContactType Members

        public IList<EMailContactType> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.EMailContactType_Tx_Code"));
        }

        public IList<EMailContactType> Find(string code)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Code", code);

            return Buscar(new Filtro(parameters, "dbo.EMailContactType_Tx_Code"));
        }

        public IList<EMailContactType> FindByFilters(string description)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Description", description);

            return Buscar(new Filtro(parameters, "dbo.EMailContactType_Tx_Filters"));
        }
        

        public EMailContactType Get(int id)
        {
            return Obtener(id);
        }

        public EMailContactType GetByCode(string code)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Code", code);

            return Obtener(new Filtro(parameters, "dbo.EMailContactType_Tx_Code"));
        }

        #endregion

        protected override Parametros ParametrosCrear(EMailContactType objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("Code", objetoNegocio.Code);
            parameters.AgregarParametro("Description", objetoNegocio.Description);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(EMailContactType objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(EMailContactType objetoNegocio)
        {
            throw new NotImplementedException();
        }
    }
}
