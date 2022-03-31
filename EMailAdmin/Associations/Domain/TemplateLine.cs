using System;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Associations.Domain
{
    public class TemplateLine : Line
    {
        #region Constants

        private const int PARAMETERSQUANTITY = 5;

        private enum Parameters
        {
            Type = 0,
            Name = 1,
            Module = 2,
            Receive = 3,
            GroupName = 4
        }

        #endregion Constants

        #region Properties

        public TemplateType Type { get; set; }
        public BackEnd.Domain.Template Template { get; set; }
        public Modulo Module { get; set; }
        public bool Receive { get; set; }
        public string GroupName { get; set; }

        #endregion Properties

        #region Constructor

        public TemplateLine(string[] line, int lineNumber)
            : base(true, string.Empty, lineNumber)
        {
            IsValid = ValidArgumentsQuantity(line, PARAMETERSQUANTITY);
            IsValid = ValidType(line) && IsValid;
            IsValid = ValidName(line) && IsValid;
            IsValid = ValidModule(line) && IsValid;
            IsValid = ValidReceive(line) && IsValid;
            IsValid = ValidGroupName(line) && IsValid;
        }

        #endregion Constructor

        #region Private Methods

        private bool ValidType(string[] line)
        {
            if (!ValidPosition(line, (int)Parameters.Type))
                return false;

            var typeText = line[(int)Parameters.Type].Trim();
            var type = TemplateTypeHome.GetByDescription(typeText);
            if(type != null)
            {
                Type = type;
                return true;
            }
            Errors.Add("Error:InvalidType");
            return false;
        }

        private bool ValidName(string[] line)
        {
            if (!ValidPosition(line, (int)Parameters.Name))
                return false;

            var nameText = line[(int)Parameters.Name].Trim();
            var name = TemplateHome.FindByName(nameText);
            if (name.Count > 0)
            {
                Template = name[0];
                return true;
            }
            Errors.Add("Error:InvalidTemplate");
            return false;
        }

        private bool ValidModule(string[] line)
        {
            if (!ValidPosition(line, (int)Parameters.Module))
                return false;

            var moduleText = line[(int)Parameters.Module].Trim();
            var module = ModuloHome.Obtener(moduleText);
            if (module != null)
            {
                Module = module;
                return true;
            }
            Errors.Add("Error:InvalidModule");
            return false;
        }

        private bool ValidReceive(string[] line)
        {
            if (!ValidPosition(line, (int)Parameters.Receive))
                return false;

            var receive = line[(int)Parameters.Receive].Trim();
            if (receive.ToLower() == "true" || receive.ToLower() == "false" || receive.ToLower() == "verdadero" || receive.ToLower() == "falso")
            {
                Receive = Convert.ToBoolean(receive);
                return true;
            }
            Errors.Add("Error:InvalidReceive");
            return false;
        }

        private bool ValidGroupName(string[] line)
        {
            if (!ValidPosition(line, (int)Parameters.GroupName))
                return false;

            var nameText = line[(int)Parameters.GroupName].Trim();
            var name = GroupHome.FindByName(nameText);
            if (name.Count == 0)
            {
                GroupName = nameText;
                return true;
            }
            Errors.Add("Error:InvalidGroupName");
            return false;
        }

        #endregion Private Methods
    }
}