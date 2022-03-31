using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailAdmin.BackEnd.Utils
{
    public class JsonSerializerSettingsHelper
    {
        public static JsonSerializerSettings GetLocalAssembliesSerializerNoneTypesSettings()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple,
                NullValueHandling = NullValueHandling.Ignore //me pinta no incluirlos
            };
            return settings;
        }
    }
}
