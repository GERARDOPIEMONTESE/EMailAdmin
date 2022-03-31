using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOEMailProcessType : DAOObjetoNegocio<EMailProcessType>, IDAOEMailProcessType
    {
        protected override void Completar(EMailProcessType objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdEMailProcessType"]);
            objetoPersistido.Codigo = dr["Code"].ToString();
            objetoPersistido.Descripcion = dr["Description"].ToString();
            objetoPersistido.Period = Convert.ToInt32(dr["Period"]);
            objetoPersistido.PeriodHours = dr["PeriodHours"].ToString();
            objetoPersistido.CheckLote = Convert.ToBoolean(dr["CheckLote"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        #region IDAOEMailProcessType Members

        public EMailProcessType Get(int id)
        {
            return Obtener(id);
        }

        public EMailProcessType Get(string code)
        {
            Parametros parameters = new Parametros();
            parameters.AgregarParametro("Code", code);

            return Obtener(new Filtro(parameters, "dbo.EMailProcessType_Tx_Code"));
        }

        public IList<EMailProcessType> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.EMailProcessType_Tx_Code"));
        }

        #endregion

        protected override Parametros ParametrosCrear(EMailProcessType objetoNegocio)
        {
            Parametros parameters = new Parametros();
            parameters.AgregarParametro("Code", objetoNegocio.Codigo);
            parameters.AgregarParametro("Description", objetoNegocio.Descripcion);
            parameters.AgregarParametro("Period", objetoNegocio.Period);
            parameters.AgregarParametro("PeriodHours", objetoNegocio.PeriodHours);
            parameters.AgregarParametro("CheckLote", objetoNegocio.CheckLote);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(EMailProcessType objetoNegocio)
        {
            Parametros parameters = new Parametros();
            parameters.AgregarParametro("IdEMailProcessType", objetoNegocio.Id);
            parameters.AgregarParametro("Code", objetoNegocio.Codigo);
            parameters.AgregarParametro("Description", objetoNegocio.Descripcion);
            parameters.AgregarParametro("Period", objetoNegocio.Period);
            parameters.AgregarParametro("PeriodHours", objetoNegocio.PeriodHours);
            parameters.AgregarParametro("CheckLote", objetoNegocio.CheckLote);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Modificado());

            return parameters;
        }

        protected override Parametros ParametrosEliminar(EMailProcessType ObjetoNegocio)
        {
            throw new NotImplementedException();
        }
    }
}
