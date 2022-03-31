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
    public class DAOTableVariableText : DAOObjetoNegocio<TableVariableText>, IDAOTableVariableText
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosCrear(TableVariableText ObjetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("Name", ObjetoNegocio.Name);
            parameters.AgregarParametro("IdUser", ObjetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.ObtenerCreado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(TableVariableText ObjetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(TableVariableText ObjetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override void Completar(TableVariableText ObjetoPersistido, SqlDataReader dr)
        {
            ObjetoPersistido.Id = Convert.ToInt32(dr["IdTableVariableText"]);
            ObjetoPersistido.Name = dr["Name"].ToString();
        }

        protected override void CompletarComposicion(TableVariableText ObjetoPersistido)
        {
            ObjetoPersistido.Contents =
                DAOLocator.Instance().GetDaoTableVariableTextContent().GetByIdTableVariableText(ObjetoPersistido.Id);
        }

        public TableVariableText Get(string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", name);

            return Obtener(new Filtro(parameters, "dbo.TableVariableText_Tx_Filters"));
        }

        public TableVariableText Get(int id)
        {            
            return Obtener(id);
        }

        public IList<TableVariableText> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.TableVariableText_Tx_Filters"));
        }
    }
}
