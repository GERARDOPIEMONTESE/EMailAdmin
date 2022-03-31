using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;
using System.Data.SqlClient;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOTableVariableTextContent : DAOObjetoNegocio<TableVariableTextContent>, IDAOTableVariableTextContent
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosCrear(TableVariableTextContent ObjetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdTableVariableText", ObjetoNegocio.IdTableVariableText);
            parameters.AgregarParametro("Content", ObjetoNegocio.Content);
            parameters.AgregarParametro("Language", ObjetoNegocio.Language);
            parameters.AgregarParametro("IdUser", ObjetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.ObtenerCreado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(TableVariableTextContent ObjetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(TableVariableTextContent ObjetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override void Completar(TableVariableTextContent ObjetoPersistido, SqlDataReader dr)
        {
            ObjetoPersistido.IdTableVariableText = Convert.ToInt32(dr["IdTableVariableText"]);
            ObjetoPersistido.Content = dr["ContentText"].ToString();
            ObjetoPersistido.Language.Id = Convert.ToInt32(dr["IdLanguage"]);
        }

        public IList<TableVariableTextContent> GetByIdTableVariableText(int idTableVariableText)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTableVariableText", idTableVariableText);

            return Buscar(new Filtro(parameters, "dbo.TableVariableTextContent_Tx_IdTableVariableTextContent"));
        }
    }
}
