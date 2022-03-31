using CapaNegocioDatos.CapaHome;
using DTOMapper;
using EmailAdmin.Dto;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.ExternalServices.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace EMailAdmin.BackEnd.Service
{
    public class InformationDynamicServices : IInformationDynamicService
    {        
        public Estrategy GetStrategy(string strategyCode)
        {
            return EstrategyHome.GetByCode(strategyCode);
        }

        public void CompleteInformationMore(AbstractEMailDTO dto)
        {
            DynamicDTO infoDto = ((DynamicDTO)dto);

            Estrategy estrategy = GetStrategy(infoDto.StrategyData);

            if (estrategy != null && !string.IsNullOrEmpty(estrategy.JsonParams))
            {
                string filtros = infoDto.GetJson();

                string information = ExternalServiceLocator.Instance().
                    GetExternalDynamicInformationService().GetInformation(estrategy.UrlDownload, filtros);

                var rst = JsonConvert.DeserializeObject<ResponseDynamicDto>(information);
                if (rst.IsOK)
                {
                    infoDto.Convert(rst.responseData);

                    Debug.Write(infoDto);
                }
            }
        }

        private DynamicDTO ConvertToDynamicDTO(AbstractEMailDTO dto)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            DynamicDTO dtoDym = new DynamicDTO();
            foreach (var ekitdato in dto.GetType().GetProperties())
            {
                var valor = ekitdato.GetValue(dto, null);
                dic.Add(ekitdato.Name, valor);
            }
            dtoDym.SetdicValues(dic);

            return dtoDym;
        }
        
        public void CompleteInformation(AbstractEMailDTO dto)
        {            
            CompleteInformationMore(dto);
        }
    }
}
