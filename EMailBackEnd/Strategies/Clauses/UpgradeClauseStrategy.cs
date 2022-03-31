using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.DTO;

namespace EMailAdmin.BackEnd.Strategies.Clauses
{
    public class UpgradeClauseStrategy : IClauseStrategy
    {
        private const string CODE = "UPG";

        #region IClauseStrategy Members

        public string GetCode()
        {
            return CODE;
        }

        public bool Evaluate(TemplateParser.TemplateParserContext context, string value)
        {
            try
            {
                EMailEkitDTO ekitDto = (EMailEkitDTO)context.Dto;

                foreach (EMailEKitUpgradeDTO upgrade in ekitDto.Upgrades)
                {
                    if (value.ToLower().Equals(upgrade.Upgrade.ToLower()))
                    {
                        return true;
                    }
                }
            }
            catch (Exception) { }
            
            return false;
        }

        #endregion
    }
}
