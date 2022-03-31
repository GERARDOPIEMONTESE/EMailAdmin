using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOClausula_R_Estrategy : DAOObjetoPersistido<Clausula_R_Estrategy>, IDAOClausula_R_Estrategy    
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(Clausula_R_Estrategy ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.Id = Convert.ToInt32(dr["IdClausula_R_Estrategy"]);
            ObjetoPersistido.CodigoPais = Convert.ToInt32(dr["CodigoPais"]);
            ObjetoPersistido.IdEstrategy = Convert.ToInt32(dr["IdEstrategy"]);
            ObjetoPersistido.IdTipoClausula = Convert.ToInt32(dr["IdTipoClausula"]);
            ObjetoPersistido.ClausulaCode = dr["ClausulaCode"].ToString();
            ObjetoPersistido.Excluye = Convert.ToBoolean(dr["Excluye"]);
        }

        public IList<Clausula_R_Estrategy> FindByEstrategy(int CodigoPais, int IdEstrategy)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("CodigoPais", CodigoPais);
            parameters.AgregarParametro("IdEstrategy", IdEstrategy);

            return Buscar(new Filtro(parameters, "dbo.Clausula_R_Estrategy_ByIdEstrategy"));
        }
    }
}
