using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;
using FrameworkDAC.Negocio;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOConditionVariableText : DAOObjetoNegocio<ConditionVariableText>, IDAOConditionVariableText
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }
        protected override Parametros ParametrosCrear(ConditionVariableText objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("Name", objetoNegocio.Name);            
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.ObtenerCreado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(ConditionVariableText objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdConditionVariableText", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);            
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(ConditionVariableText objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdConditionVariableText", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", objetoNegocio.ObtenerEliminado());
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);

            return parameters;
        }

        protected override void Completar(ConditionVariableText objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdConditionVariableText"]);
            objetoPersistido.Name = dr["Name"].ToString();
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override Parametros ParametrosGrabarLog(ConditionVariableText objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdConditionVariableText", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.ObtenerCreado());

            return parameters;
        }

        public IList<ConditionVariableText> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.ConditionVariableText_Tx_Filters"), true);
        }

        public IList<ConditionVariableText> Find(int IdVariableText, string Name, string condicion)
        {
            var parameters = new Parametros();
            if (IdVariableText!=-1) parameters.AgregarParametro("IdVariableText", IdVariableText);
            if (Name!="") parameters.AgregarParametro("Name", Name);
            if (condicion != "") parameters.AgregarParametro("Condicion", condicion);

            return Buscar(new Filtro(parameters, "dbo.ConditionVariableText_Tx_Filters"));
        }

        public ConditionVariableText Get(int id)
        {
            return Obtener(id);
        }


        public ConditionVariableText FindByName(string Name, bool EqualName=false)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", Name);
            parameters.AgregarParametro("EqualName", EqualName);

            return Obtener(new Filtro(parameters, "dbo.ConditionVariableText_Tx_Filters"));
        }

        protected override void CompletarComposicion(ConditionVariableText objetoPersistido)
        {
            //CONTENT
            objetoPersistido.Contents =
                DAOLocator.Instance().GetDaoConditionVariableTextContent().GetByIdConditionVariableText(objetoPersistido.Id);
            //VARIABLETEXT
            if (objetoPersistido.VariablesText == null)
            {
                objetoPersistido.VariablesText = new List<ConditionVariableText_R_VariableText>();
            }
            foreach (
                ConditionVariableText_R_VariableText conditionVariableTextRVariableText in
                    DAOLocator.Instance().GetDaoConditionVariableText_R_VariableText().FindByConditionVariableTextId(objetoPersistido.Id))
            {
                objetoPersistido.VariablesText.Add(conditionVariableTextRVariableText);
            }
        }

        protected override void CrearComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            var conditionVariableText = (ConditionVariableText)objetoNegocio;

            foreach (ConditionVariableText_R_VariableText variableText in conditionVariableText.VariablesText)
            {
                variableText.ConditionVariableTextId = conditionVariableText.Id;
                DAOLocator.Instance().GetDaoConditionVariableText_R_VariableText().Crear(variableText);
            }

            foreach (ConditionVariableTextContent content in conditionVariableText.Contents)
            {
                var cvtContent = new ConditionVariableTextContent
                {
                    IdConditionVariableText = conditionVariableText.Id,
                    Content = content.Content,
                    Language = content.Language,
                    IdUsuario = conditionVariableText.IdUsuario
                };

                DAOLocator.Instance().GetDaoConditionVariableTextContent().Crear(cvtContent);
            }
        }

        protected override void ModificarComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            var conditionVariableText = (ConditionVariableText)objetoNegocio;
                        
            DAOLocator.Instance().GetDaoConditionVariableText_R_VariableText().DeleteByIdConditionVariableText(objetoNegocio.Id);
            foreach (ConditionVariableText_R_VariableText variableText in conditionVariableText.VariablesText)
            {
                variableText.ConditionVariableTextId = conditionVariableText.Id;
                DAOLocator.Instance().GetDaoConditionVariableText_R_VariableText().Crear(variableText);
            }

            DAOLocator.Instance().GetDaoConditionVariableTextContent().DeleteByIdConditionVariableText(objetoNegocio.Id);
            foreach (ConditionVariableTextContent content in conditionVariableText.Contents)
            {
                var cvtContent = new ConditionVariableTextContent
                {
                    IdConditionVariableText = conditionVariableText.Id,
                    Content = content.Content,
                    Language = content.Language,
                    IdUsuario = conditionVariableText.IdUsuario
                };

                DAOLocator.Instance().GetDaoConditionVariableTextContent().Crear(cvtContent);
            }
        }

        protected override void EliminarComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            DAOLocator.Instance().GetDaoConditionVariableTextContent().DeleteByIdConditionVariableText(objetoNegocio.Id, ts);
            DAOLocator.Instance().GetDaoConditionVariableText_R_VariableText().DeleteByIdConditionVariableText(objetoNegocio.Id, ts);
        }
    }
}