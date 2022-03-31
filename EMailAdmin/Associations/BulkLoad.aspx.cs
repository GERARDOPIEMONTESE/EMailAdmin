using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using ControlMenu;
using EMailAdmin.Associations.Domain;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.Utils;
using MenuPortalLibrary.CapaDTO;

namespace EMailAdmin.Associations
{
    public partial class BulkLoad : CustomPage
    {
        #region Constants

        private const char SEPARATOR = ',';

        #endregion Constants

        #region Constructor

        #endregion Constructor

        #region Propiedades

        protected override void ChequearSession()
        {
            UsuarioDTO usuario = base.UsuarioLogueadoDTO();
            if (usuario == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings["ExpireSessionRedirect"]);
            }
        }

        #endregion Propiedades

        #region Methods

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MakeInstructions(UsuarioLogueadoDTO().Ididioma);
            }
        }

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            var lines = ParseFile();
            var errorLines = (from var in lines where !var.IsValid select var).ToList();
            if (errorLines.Count > 0)
            {
                ShowErrors(errorLines);
            }
            else
            {
                Process(lines);
                mpeOk.Show();
            }
        }

        #endregion Methods

        #region Private Methods

        private IEnumerable<Line> ParseFile()
        {
            var lines = new List<Line>();
            int nRegistro = 1;
            try
            {
                if (fupBulk.HasFile)
                {
                    string ext = Path.GetExtension(fupBulk.FileName).ToLower();
                    if (ext == ".txt" || ext == ".csv")
                    {
                        var file = new StreamReader(FileUtils.Text(fupBulk));
                        while (!file.EndOfStream)
                        {
                            Line line = Line.BuildLine(file.ReadLine().Split(SEPARATOR), nRegistro++);

                            if (line != null)
                                lines.Add(line);
                        }
                    }
                    else
                    {
                        lines.Add(new Line(false, "El archivo debe tener extension .txt o .csv", 0));
                    }
                    lines.Add(new Line(true));
                }
            }
            catch (Exception ex)
            {
                lines = new List<Line> {new Line(false, ex.InnerException.Message, 0)};
            }

            return lines;
        }

        private void Process(IEnumerable<Line> lines)
        {
            TemplateLine temp = null;
            ConditionLine condition = null;
            Line empty = null;
            BackEnd.Domain.Group group = null;
            Group_R_Template rGroup = null;
            foreach (Line line in lines)
            {
                try
                {
                    temp = (TemplateLine) line;
                }
                catch
                {
                    try
                    {
                        condition = (ConditionLine) line;
                    }
                    catch
                    {
                        empty = line;
                    }
                }

                if (temp != null)
                {
                    group = new BackEnd.Domain.Group
                                {
                                    GroupType = GroupTypeHome.Find(GroupType.TEMPLATEGROUP),
                                    Name = temp.GroupName
                                };
                    rGroup = new Group_R_Template
                                 {Template = temp.Template, Receive = temp.Receive, Module = temp.Module};
                    temp = null;
                }
                if (condition != null)
                {
                    if (group != null)
                        group.Conditions.Add(condition.GroupCondition);
                    condition = null;
                }
                if (empty != null)
                {
                    if (rGroup != null)
                    {
                        rGroup.Group = group;
                        SaveAssociation(rGroup);
                    }
                    empty = null;
                    rGroup = null;
                }
            }
        }

        private void SaveAssociation(Group_R_Template association)
        {
            //association.IdUsuario = SessionManager.GetLoguedUser(Session).IdUsuario;
            association.IdUsuario = UsuarioLogueadoDTO().Id;
            ServiceLocator.Instance().GetGroupRTemplateService().Save(association);
        }

        private void MakeInstructions(int idLanguage)
        {
            var sb = new StringBuilder();
            switch (idLanguage)
            {
                    #region English

                default:
                    sb.Append(
                        @"The following are the parameters sorted for bulk loading of associations (they must be separated by commas):");
                    sb.Append(@"<br/>");
                    sb.Append(@"1st line - TEMPLATE DETAILS");
                    sb.Append(@"<br/>");
                    sb.Append(@"Template type = must be written the name of the type of template");
                    sb.Append(@"<br/>");
                    sb.Append(@"Template Name");
                    sb.Append(@"<br/>");
                    sb.Append(@"Module = name should be written the name of the module");
                    sb.Append(@"<br/>");
                    sb.Append(@"Receive = must be set true or false to indicate whether or not you receive.");
                    sb.Append(@"<br/>");
                    sb.Append(@"Group Name = name to be the group of the association. (not to be repeated)");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(@"Example: EMAIL eKit, Traditional, emailadmin, true, Traditional Group");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"Following lines (with 1 being a minimum and no maxima) - DETAILS OF THE TERMS OF THE GROUP");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"each type of condition has different requirements for parameters. Then each type will be detailed.");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(@"COUNTRY");
                    sb.Append(@"<br/>");
                    sb.Append(@"Type = you must select the type of condition being the possibilities: country - pais.");
                    sb.Append(@"<br/>");
                    sb.Append(@"Country Code");
                    sb.Append(@"<br/>");
                    sb.Append(@"Example: COUNTRY, 540");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(@"PRODUCT");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"Type = you must select the type of condition being the possibilities: Product - produto - producto.");
                    sb.Append(@"<br/>");
                    sb.Append(@"Country Code");
                    sb.Append(@"<br/>");
                    sb.Append(@"Product Code");
                    sb.Append(@"<br/>");
                    sb.Append(@"Example: PRODUCT, 540, R3");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(@"RATE");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"Type = you must select the type of condition being the possibilities: rate - taxa - tarifa.");
                    sb.Append(@"<br/>");
                    sb.Append(@"Country Code");
                    sb.Append(@"<br/>");
                    sb.Append(@"Product Code");
                    sb.Append(@"<br/>");
                    sb.Append(@"Rate Code");
                    sb.Append(@"<br/>");
                    sb.Append(@"Example: RATE, 540, R3, 10036");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(@"ACCOUNT");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"Type = you must select the type of condition being the possibilities: account - conta - cuenta.");
                    sb.Append(@"<br/>");
                    sb.Append(@"Country Code");
                    sb.Append(@"<br/>");
                    sb.Append(@"Account Code");
                    sb.Append(@"<br/>");
                    sb.Append(@"Number of branch");
                    sb.Append(@"<br/>");
                    sb.Append(@"Example: ACCOUNT, 540, 2511,0");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"If you want to enter more than one association in the same file, add a line with a ""-"" to identify the division.");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(@"Example: ");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(@"EMAIL eKit, Traditional, emailadmin, true, Traditional Group");
                    sb.Append(@"<br/>");
                    sb.Append(@"ACCOUNT, 540, 2511,0");
                    sb.Append(@"<br/>");
                    sb.Append(@"COUNTRY, 540");
                    sb.Append(@"<br/>");
                    sb.Append(@"-");
                    sb.Append(@"<br/>");
                    sb.Append(@"EMAIL eKit, Classic, emailadmin, true, classic GROUP");
                    sb.Append(@"<br/>");
                    sb.Append(@"PRODUCT, 540, R3");
                    sb.Append(@"<br/>");
                    sb.Append(@"RATE, 540, R3, 10036");
                    sb.Append(@"<br/>");
                    sb.Append(@"COUNTRY, 540");
                    sb.Append(@"<br/>");
                    sb.Append(@"-");
                    break;

                    #endregion english

                    #region Spanish

                case 1:
                    sb.Append(
                        @"Los siguientes, son los parametros ordenados para la carga masiva de asociaciones (los mismos deben estar separados por comas):");
                    sb.Append(@"<br/>");
                    sb.Append(@"1era linea - DETALLES DEL TEMPLATE");
                    sb.Append(@"<br/>");
                    sb.Append(@"Tipo de Template = se debe escribir el nombre del tipo de template");
                    sb.Append(@"<br/>");
                    sb.Append(@"Nombre de Template ");
                    sb.Append(@"<br/>");
                    sb.Append(@"Nombre Modulo = se debe escribir el nombre del modulo");
                    sb.Append(@"<br/>");
                    sb.Append(@"Recibe = se debe poner verdadero o falso para indicar si recibe o no.");
                    sb.Append(@"<br/>");
                    sb.Append(@"Nombre del grupo = nombre que tendrá el grupo de la asociación. (no debe repetirse)");
                    sb.Append(@"<br/>");
                    sb.Append(@"Ejemplo: EMAIL EKIT, Traditional, EMailAdmin, true, Grupo Traditional");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"Siguientes lineas (siendo 1 como minimo y sin maximos) -  DETALLES DE LAS CONDICIONES DEL GRUPO");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"cada tipo de condicion tiene diferentes requisitos de parametros. A continuación se detallara cada tipo.");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(@"PAIS");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"Tipo = se debe seleccionar el tipo de condicion siendo las posibilidades: pais - country.");
                    sb.Append(@"<br/>");
                    sb.Append(@"Codigo de Pais");
                    sb.Append(@"<br/>");
                    sb.Append(@"Ejemplo: PAIS, 540");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(@"PRODUCTO");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"Tipo = se debe seleccionar el tipo de condicion siendo las posibilidades: producto - produto - product.");
                    sb.Append(@"<br/>");
                    sb.Append(@"Codigo de Pais");
                    sb.Append(@"<br/>");
                    sb.Append(@"Codigo de Producto");
                    sb.Append(@"<br/>");
                    sb.Append(@"Ejemplo: PRODUCTO, 540, R3");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(@"TARIFA");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"Tipo = se debe seleccionar el tipo de condicion siendo las posibilidades: tarifa - taxa - rate.");
                    sb.Append(@"<br/>");
                    sb.Append(@"Codigo de Pais");
                    sb.Append(@"<br/>");
                    sb.Append(@"Codigo de Producto");
                    sb.Append(@"<br/>");
                    sb.Append(@"Codigo de Tarifa");
                    sb.Append(@"<br/>");
                    sb.Append(@"Ejemplo: TARIFA,540,R3,10036");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(@"CUENTA");
                    sb.Append(
                        @"Tipo = se debe seleccionar el tipo de condicion siendo las posibilidades: cuenta - conta - account.");
                    sb.Append(@"<br/>");
                    sb.Append(@"Codigo de Pais");
                    sb.Append(@"<br/>");
                    sb.Append(@"Codigo de cuenta");
                    sb.Append(@"<br/>");
                    sb.Append(@"Numero de sucursal");
                    sb.Append(@"<br/>");
                    sb.Append(@"Ejemplo: CUENTA, 540, 2511,0");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"Si se desea ingresar más de una asociación en un mismo archivo, se debe agregar una linea con un ""-"" para identificar la división.");
                    sb.Append(@"<br/>");
                    sb.Append(@"EJEMPLO: ");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(@"EMAIL EKIT, Traditional, EMailAdmin, true, Grupo Tradicional");
                    sb.Append(@"<br/>");
                    sb.Append(@"CUENTA, 540, 2511,0");
                    sb.Append(@"<br/>");
                    sb.Append(@"PAIS, 540");
                    sb.Append(@"<br/>");
                    sb.Append(@"-");
                    sb.Append(@"<br/>");
                    sb.Append(@"EMAIL EKIT, Clasico, EMailAdmin, true, GRUPO clasico");
                    sb.Append(@"<br/>");
                    sb.Append(@"PRODUCTO, 540, R3");
                    sb.Append(@"<br/>");
                    sb.Append(@"TARIFA,540,R3,10036");
                    sb.Append(@"<br/>");
                    sb.Append(@"PAIS, 550");
                    sb.Append(@"<br/>");
                    sb.Append(@"-");
                    break;

                    #endregion spanish

                    #region Portuguesse

                case 3:
                    sb.Append(
                        @"A seguir estão os parâmetros classificadas para carregamento em massa de associações (eles devem ser separados por vírgula):");
                    sb.Append(@"<br/>");
                    sb.Append(@"1 ª linha - detalhes do modelo");
                    sb.Append(@"<br/>");
                    sb.Append(@"Tipo de modelo = De tipo de modelo deve ser escrito o nome do tipo de modelo");
                    sb.Append(@"<br/>");
                    sb.Append(@"Nome do Modelo");
                    sb.Append(@"<br/>");
                    sb.Append(@"Nome do módulo = deve ser escrito o nome do módulo");
                    sb.Append(@"<br/>");
                    sb.Append(@"Receber = deve ser definida verdadeiro ou falso para indicar ou não que você recebe.");
                    sb.Append(@"<br/>");
                    sb.Append(@"Nome do grupo = nome a ser o grupo da associação. (Não deve ser repetido)");
                    sb.Append(@"<br/>");
                    sb.Append(@"Exemplo: EKit EMAIL, Tradicional, emailadmin, Grupo, true, Tradicional");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(@"Seguintes linhas (sendo 1 o mínimo e não máximo) - Detalhes dos termos do grupo");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"cada tipo de condição tem exigências diferentes para os parâmetros. Em seguida, cada tipo serão detalhados.");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(@"PAÍS");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"Tipo = você deve selecionar o tipo de condição de estar as possibilidades: Country - país.");
                    sb.Append(@"<br/>");
                    sb.Append(@"Código do país");
                    sb.Append(@"<br/>");
                    sb.Append(@"Exemplo: PAÍS, 540");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(@"PRODUTO");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"Tipo = você deve selecionar o tipo de condição de estar as possibilidades: Produto - Producto - product.");
                    sb.Append(@"<br/>");
                    sb.Append(@"Código do país");
                    sb.Append(@"<br/>");
                    sb.Append(@"Código do Produto");
                    sb.Append(@"<br/>");
                    sb.Append(@"Exemplo: PRODUTO, 540, R3");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(@"TAXA");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"Tipo = você deve selecionar o tipo de condição de estar as possibilidades: taxa - tarifa - rate.");
                    sb.Append(@"<br/>");
                    sb.Append(@"Código do país");
                    sb.Append(@"<br/>");
                    sb.Append(@"Código do Produto");
                    sb.Append(@"<br/>");
                    sb.Append(@"Código taxa");
                    sb.Append(@"<br/>");
                    sb.Append(@"Exemplo: TAXA, 540, R3, 10036");
                    sb.Append(@"<br/>");
                    sb.Append(@"CONTA");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"Tipo = você deve selecionar o tipo de condição de estar as possibilidades: Conta - Cuenta - Account.");
                    sb.Append(@"<br/>");
                    sb.Append(@"Código do país");
                    sb.Append(@"<br/>");
                    sb.Append(@"Código da Conta");
                    sb.Append(@"<br/>");
                    sb.Append(@"Número de sucursai");
                    sb.Append(@"<br/>");
                    sb.Append(@"Exemplo: CONTA, 540, 2511,0");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(
                        @"Se você quiser inserir mais de uma associação no mesmo arquivo, adicione uma linha com um ""-"" para identificar a divisão.");
                    sb.Append(@"<br/>");
                    sb.Append(@"EXEMPLO: ");
                    sb.Append(@"<br/>");
                    sb.Append(@"<br/>");
                    sb.Append(@"EMAIL eKit, Traditional, emailadmin, true, Traditional Group");
                    sb.Append(@"<br/>");
                    sb.Append(@"CONTA, 540, 2511,0");
                    sb.Append(@"<br/>");
                    sb.Append(@"PAIS, 540");
                    sb.Append(@"<br/>");
                    sb.Append(@"-");
                    sb.Append(@"<br/>");
                    sb.Append(@"EMAIL eKit, Classic, emailadmin, true, classic GROUP");
                    sb.Append(@"<br/>");
                    sb.Append(@"PRODUTO, 540, R3");
                    sb.Append(@"<br/>");
                    sb.Append(@"TAXA, 540, R3, 10036");
                    sb.Append(@"<br/>");
                    sb.Append(@"PAIS, 540");
                    sb.Append(@"<br/>");
                    sb.Append(@"-");
                    break;

                    #endregion portuguesse
            }
            lblInstructions.Text = sb.ToString();
        }

        private void ShowErrors(IEnumerable<Line> lines)
        {
            IEnumerable<Line> linesToShow = ChangeLanguage(lines);
            grvErrors.DataSource = linesToShow;
            grvErrors.DataBind();
            uplErrors.Update();
            mpeErrors.Show();
        }

        private IEnumerable<Line> ChangeLanguage(IEnumerable<Line> lines)
        {
            var linesToShow = new List<Line>();
            foreach (Line line in lines)
            {
                var newLine = new Line {IsValid = line.IsValid, LineNumber = line.LineNumber};
                foreach (string error in line.Errors)
                {
                    string newError = GetLocal(error);
                    newLine.Errors.Add(newError);
                }
                linesToShow.Add(newLine);
            }
            return linesToShow;
        }

        private string GetLocal(string key)
        {
            string localresourcestring;
            try
            {
                localresourcestring = (String) GetLocalResourceObject(key);
            }
            catch
            {
                localresourcestring = "Could not find local resource.";
            }
            return localresourcestring;
        }

        #endregion Private Methods
    }
}