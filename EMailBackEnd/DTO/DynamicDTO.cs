using CapaNegocioDatos.CapaHome;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace EMailAdmin.BackEnd.DTO
{
    public class DynamicDTO : AbstractEMailDTO, IDynamicDTO
    {
        public string CultureUI { get; set; }
        public string TemplateCode { get; set; }
        public string StrategyData { get; set; }
        public string EmailListCode { get; set; }
        public List<DynamicItemDTO> dicValues { get; set; }
        
        public DynamicDTO()
        {
            dicValues = new List<DynamicItemDTO>();
            TemplateType = TemplateTypeHome.GetDynamic();
        }

        public void SetdicValues(Dictionary<string, object> data)
        {
            dicValues = new List<DynamicItemDTO>();
            foreach (var item in data)
            {
                dicValues.Add(new DynamicItemDTO() { Key = item.Key, Value = item.Value });
            }

            SetValuesBase();
        }

        public void Convert(dynamic data)
        {
            foreach (var item in data)
            {                
                dicValues.Add(new DynamicItemDTO() { Key = item.Name, Value = item.Value });
            }

            SetValuesBase(data);
        }

        public string GetJson()
        {
            var td = dicValues.ToDictionary(x => x.Key, x => x.Value.ToString());
            var js = new JavaScriptSerializer();
            var json = js.Serialize(td);
            return json;
        }

        public void SetValuesBase()
        {
            if (string.IsNullOrEmpty(To)) To = GetValDicValue("email");
            if (string.IsNullOrEmpty(RecipientName)) RecipientName = GetValDicValue("RecipientName");
            if (string.IsNullOrEmpty(RecipientSurname)) RecipientSurname = GetValDicValue("RecipientSurname");
            if (string.IsNullOrEmpty(CompleteVoucherCode)) CompleteVoucherCode = GetValDicValue("CompleteVoucherCode");

            string ModuleCode = GetValDicValue("ModuleCode");
            if (!string.IsNullOrEmpty(ModuleCode))
                Module = ModuloHome.ObtenerPorNombre(ModuleCode);
            
            AssociationGroupDto = new AssociationGroupDTO();
            string CountryCode = GetValDicValue("CountryCode");
            if (!string.IsNullOrEmpty(CountryCode))
                AssociationGroupDto.IdLocation = PaisHome.ObtenerPorCodigo(CountryCode).IdLocacion;
            else
            {
                string CountryISO = GetValDicValue("CountryISO");
                if (!string.IsNullOrEmpty(CountryISO))
                    AssociationGroupDto.IdLocation = PaisHome.ObtenerPorCodigoISOA2(CountryISO).IdLocacion;
            }

            string AgencyCode = GetValDicValue("AgencyCode");
            string BranchNumber = GetValDicValue("BranchNumber");
            if (!string.IsNullOrEmpty(AgencyCode) && !string.IsNullOrEmpty(CountryCode) && !string.IsNullOrEmpty(BranchNumber))
            {
                AssociationGroupDto.IdAccount = SucursalHome.Obtener(
                 CountryCode, AgencyCode, int.Parse(BranchNumber)).Id;
            }

            string ProductCode = GetValDicValue("ProductCode");
            if (!string.IsNullOrEmpty(CountryCode) && !string.IsNullOrEmpty(ProductCode))
            {
                AssociationGroupDto.IdProduct = ProductHome.Get(CountryCode,
                ProductCode != null ? ProductCode : "-", Product.PRODUCT).Id;

                string RateCode = GetValDicValue("RateCode");
                if (!string.IsNullOrEmpty(RateCode))
                {
                    AssociationGroupDto.IdRate = RateHome.GetByProductCode(AssociationGroupDto.IdProduct,
                        RateCode != null ? RateCode : "-").Id;
                }
            }
        }

        public void SetValuesBase(dynamic data)
        {
            To = data["email"];            
        }

        public DynamicItemDTO GetDicValue(string key)
        {
            DynamicItemDTO item = new DynamicItemDTO();
            item = dicValues.FirstOrDefault(x => x.Key == key);
            return item;
        }

        public string GetValDicValue(string key)
        {
            DynamicItemDTO item = new DynamicItemDTO();
            item = dicValues.FirstOrDefault(x => x.Key == key);
            if (item != null && item.Value != null)
                return item.Value.ToString();
            else
                return "";
        }

        public void AddDicValue(string pKey, IEnumerable pPropertyValue)
        {
            var pPropertyValueJSON = JsonConvert.SerializeObject(pPropertyValue, JsonSerializerSettingsHelper.GetLocalAssembliesSerializerNoneTypesSettings());
            var pPropertyDynamicValue = JsonConvert.DeserializeObject(pPropertyValueJSON);

            var pValue = new Dictionary<string, object>();
            pValue.Add(pKey, pPropertyDynamicValue);

            this.SetdicValues(pValue);
        }

        public bool ContainsKeyValue(string key, string value)
        {
            DynamicItemDTO item = new DynamicItemDTO();
            item = dicValues.FirstOrDefault(x => x.Key == key);
            if (item != null && item.Value != null)
                return (item.Value.ToString() == value);
            else
                return false;
        }

        public bool ContainsKeyValue(string key, List<string> values)
        {
            DynamicItemDTO item = new DynamicItemDTO();
            item = dicValues.FirstOrDefault(x => x.Key == key);
            if (item != null && item.Value != null)
                return (values.Contains(item.Value.ToString().ToUpper()));
            else
                return false;
        }

        internal bool ValidGroupConditions(IList<GroupCondition> lstGroupConditions)
        {
            //para que el grupo sea valido tiene que dar verdadero todas las keys
            //se puede configurar varios valores para la misma key
            //en ese caso seria un OR si el valor esta dentro de la lista de posibles esta OK
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
            foreach (var item in lstGroupConditions)
            {
                if (!dic.ContainsKey(item.DynamicKey))
                    dic.Add(item.DynamicKey, new List<string>());

                dic[item.DynamicKey].Add(item.Value.ToUpper());
            }

            foreach (var item in dic)
            {
                if (!ContainsKeyValue(item.Key, item.Value))
                    return false;
            }

            return true;
        }
    }

    public interface IDynamicDTO
    {
        DynamicItemDTO GetDicValue(string key);
    }


    public class DynamicItemDTO
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
