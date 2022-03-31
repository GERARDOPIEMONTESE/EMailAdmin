using System;
using System.Globalization;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Associations.Domain
{
    public class ConditionLine : Line
    {
        #region Constants

        private enum Parameters
        {
            Type = 0,
        }

        #region Product

        private const int PRODUCTPARAMETERSQUANTITY = 3;

        private enum ProductParameters
        {
            Country = 1,
            Code = 2,
        }

        #endregion Product

        #region Rate

        private const int RATEPARAMETERSQUANTITY = 4;

        private enum RateParameters
        {
            Country = 1,
            Product = 2,
            Rate = 3
        }

        #endregion Rate

        #region Country

        private const int COUNTRYPARAMETERSQUANTITY = 2;

        private enum CountryParameters
        {
            Country = 1,
        }

        #endregion Country

        #region Account

        private const int ACCOUNTPARAMETERSQUANTITY = 4;

        private enum AccountParameters
        {
            Country = 1,
            Account = 2,
            Branch = 3
        }

        #endregion Account

        #endregion Constants

        #region Properties

        public GroupCondition GroupCondition { get; set; }
        private int CountryId { get; set; }
        private int ProductId { get; set; }

        #endregion Properties

        #region Constructor

        public static bool IsConditionLine(string[] line)
        {
            switch (line[0].ToLower())
            {
                case "pais":
                    return true;
                case "country":
                    return true;
                case "país":
                    return true;
                case "producto":
                    return true;
                case "product":
                    return true;
                case "produto":
                    return true;
                case "rate":
                    return true;
                case "taxa":
                    return true;
                case "tarifa":
                    return true;
                case "cuenta":
                    return true;
                case "conta":
                    return true;
                case "account":
                    return true;
            }
            return false;
        }

        public ConditionLine(string[] line, int lineNumber)
            : base(true, string.Empty, lineNumber)
        {
            GroupCondition = new GroupCondition();
            switch (line[0].ToLower())
            {
                case "pais":
                    BuildCountry(line);
                    break;
                case "country":
                    BuildCountry(line);
                    break;
                case "país":
                    BuildCountry(line);
                    break;
                case "producto":
                    BuildProduct(line);
                    break;
                case "product":
                    BuildProduct(line);
                    break;
                case "produto":
                    BuildProduct(line);
                    break;
                case "rate":
                    BuildRate(line);
                    break;
                case "taxa":
                    BuildRate(line);
                    break;
                case "tarifa":
                    BuildRate(line);
                    break;
                case "cuenta":
                    BuildAccount(line);
                    break;
                case "conta":
                    BuildAccount(line);
                    break;
                case "account":
                    BuildAccount(line);
                    break;
            }
        }

        #endregion Constructor

        #region Private Methods

        private bool ValidType(string[] line)
        {
            if (!ValidPosition(line, (int) Parameters.Type))
                return false;

            string typeText = line[(int) Parameters.Type].Trim();
            if (typeText.ToLower() == "pais" || typeText.ToLower() == "país" || typeText.ToLower() == "country")
            {
                GroupCondition.ConditionType = ConditionTypeHome.Get(ConditionType.Country);
                return true;
            }
            if (typeText.ToLower() == "producto" || typeText.ToLower() == "produto" || typeText.ToLower() == "product")
            {
                GroupCondition.ConditionType = ConditionTypeHome.Get(ConditionType.Product);
                return true;
            }
            if (typeText.ToLower() == "rate" || typeText.ToLower() == "taxa" || typeText.ToLower() == "tarifa")
            {
                GroupCondition.ConditionType = ConditionTypeHome.Get(ConditionType.Rate);
                return true;
            }
            if (typeText.ToLower() == "cuenta" || typeText.ToLower() == "conta" || typeText.ToLower() == "account")
            {
                GroupCondition.ConditionType = ConditionTypeHome.Get(ConditionType.Branch);
                return true;
            }
            Errors.Add("Error:InvalidType");
            return false;
        }

        #region Country

        private void BuildCountry(string[] line)
        {
            IsValid = ValidArgumentsQuantity(line, COUNTRYPARAMETERSQUANTITY);
            IsValid = ValidType(line) && IsValid;
            IsValid = ValidCountryCode(line) && IsValid;
        }

        private bool ValidCountryCode(string[] line)
        {
            if (!ValidPosition(line, (int) CountryParameters.Country))
                return false;

            string countryText = line[(int)CountryParameters.Country].Trim();
            Pais country = PaisHome.ObtenerPorCodigo(countryText);
            if (country != null && country.Id != 0)
            {
                GroupCondition.Value = country.IdLocacion.ToString(CultureInfo.InvariantCulture);
                return true;
            }
            Errors.Add("Error:CountryNotExist");
            return false;
        }

        #endregion Country

        #region Product

        private void BuildProduct(string[] line)
        {
            IsValid = ValidArgumentsQuantity(line, PRODUCTPARAMETERSQUANTITY);
            IsValid = ValidType(line) && IsValid;
            IsValid = ValidProductCountryCode(line) && IsValid;
            IsValid = ValidProductCode(line) && IsValid;
        }

        private bool ValidProductCountryCode(string[] line)
        {
            if (!ValidPosition(line, (int) ProductParameters.Country))
                return false;

            string countryText = line[(int)ProductParameters.Country].Trim();
            Pais country = PaisHome.ObtenerPorCodigo(countryText);
            if (country != null && country.Id != 0)
            {
                CountryId = country.IdLocacion;
                return true;
            }
            Errors.Add("Error:CountryNotExist");
            return false;
        }

        private bool ValidProductCode(string[] line)
        {
            if (!ValidPosition(line, (int) ProductParameters.Code))
                return false;

            string codeText = line[(int)ProductParameters.Code].Trim();
            Product product = ProductHome.Get(codeText, CountryId, Product.PRODUCT);
            if (product != null && product.Id != 0)
            {
                GroupCondition.Value = product.Id.ToString(CultureInfo.InvariantCulture);
                return true;
            }
            Errors.Add("Error:ProductNotExist");
            return false;
        }

        #endregion Product

        #region Rate

        private void BuildRate(string[] line)
        {
            IsValid = ValidArgumentsQuantity(line, RATEPARAMETERSQUANTITY);
            IsValid = ValidType(line) && IsValid;
            IsValid = ValidRateCountryCode(line) && IsValid;
            IsValid = ValidRateProductCode(line) && IsValid;
            IsValid = ValidRateCode(line) && IsValid;
        }

        private bool ValidRateCountryCode(string[] line)
        {
            if (!ValidPosition(line, (int) RateParameters.Country))
                return false;

            string countryText = line[(int)RateParameters.Country].Trim();
            Pais country = PaisHome.ObtenerPorCodigo(countryText);
            if (country != null && country.Id != 0)
            {
                CountryId = country.IdLocacion;
                return true;
            }
            Errors.Add("Error:CountryNotExist");
            return false;
        }

        private bool ValidRateProductCode(string[] line)
        {
            if (!ValidPosition(line, (int) RateParameters.Product))
                return false;

            string codeText = line[(int)RateParameters.Product].Trim();
            var product = ProductHome.Get(codeText, CountryId, Product.PRODUCT);
            if (product != null && product.Id != 0)
            {
                ProductId = product.Id;
                return true;
            }
            Errors.Add("Error:ProductNotExist");
            return false;
        }

        private bool ValidRateCode(string[] line)
        {
            if (!ValidPosition(line, (int) RateParameters.Rate))
                return false;

            string codeText = line[(int)RateParameters.Rate].Trim();
            Rate rate = RateHome.GetByProductCode(ProductId, codeText);
            if (rate != null && rate.Id != 0)
            {
                GroupCondition.Value = rate.Id.ToString(CultureInfo.InvariantCulture);
                return true;
            }
            Errors.Add("Error:RateNotExist");
            return false;
        }

        #endregion Rate

        #region Account

        private void BuildAccount(string[] line)
        {
            IsValid = ValidArgumentsQuantity(line, ACCOUNTPARAMETERSQUANTITY);
            IsValid = ValidType(line) && IsValid;
            IsValid = ValidAccount(line) && IsValid;
        }

        private bool ValidAccount(string[] line)
        {
            if (!ValidPosition(line, (int) AccountParameters.Account))
                return false;

            Sucursal sucursal = SucursalHome.Obtener(line[(int)AccountParameters.Country].Trim(),
                                                     line[(int)AccountParameters.Account].Trim(),
                                                     Convert.ToInt32(line[(int)AccountParameters.Branch].Trim()));
            if (sucursal != null && sucursal.Id != 0)
            {
                GroupCondition.Value = sucursal.Id.ToString(CultureInfo.InvariantCulture);
                return true;
            }
            Errors.Add("Error:AccountNotExist");
            return false;
        }

        #endregion Account

        #endregion Private Methods
    }
}