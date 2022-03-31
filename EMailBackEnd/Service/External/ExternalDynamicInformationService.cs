using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.ExternalServices.Service.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace EMailAdmin.BackEnd.Service.External
{
    public class ExternalDynamicInformationService : IExternalInformationStrategyService
    {

        public string GetInformation(string url, string jsonData)
        {
            return Post(url, jsonData);
        }

        public static string Post(string url, string jsonData, int timeout = 0)
        {
            try
            {                  
                HttpWebRequest wr = WebRequest.Create(url) as HttpWebRequest;
                wr.Method = "POST";
                wr.ContentType = "application/json; charset=utf-8";
                using (StreamWriter streamWriter = new StreamWriter(wr.GetRequestStream()))
                {
                    streamWriter.Write(jsonData);
                }
                if (timeout > 0)
                    wr.Timeout = timeout;
                HttpWebResponse httpResponse = (HttpWebResponse)wr.GetResponse();
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception("Error " + httpResponse.StatusCode);
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    return result;
                }
            }
            catch (WebException wex)
            {
                using (WebResponse response = wex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string error = reader.ReadToEnd();
                        //DTOApiExceptionMessage exceptionMessage = JsonConvert.DeserializeObject<DTOApiExceptionMessage>(error);
                        throw new Exception(error);
                    }
                }
            }
        }
    }
}
