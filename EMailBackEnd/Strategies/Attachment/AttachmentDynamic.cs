using CapaNegocioDatos.CapaHome;
using DTOMapper;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.Strategies.EMailSender;
using EMailAdmin.BackEnd.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace EMailAdmin.BackEnd.Strategies.Attachment
{
    public class AttachmentTemplate : AttachStrategy, IAttachStrategy
    {
        #region Constants

        private const string ATTACH_NAME = "ASSISTCARD-Info.pdf";

        private const string ATTACH_TYPE = "application/pdf";

        #endregion Constants

        public AttachmentTemplate()
        {
            ATTACHNAME = ATTACH_NAME;
            ATTACHTYPE = ATTACH_TYPE;
        }

        #region Methods

        private byte[] GetAttachContent(AbstractEMailDTO dto)
        {
            try
            {
                if (IdStrategy > 0)
                {
                    IdLanguage = dto.IdLanguage;
                    IdStrategy = dto.IdStrategy;

                    Estrategy estrategy = EstrategyHome.Get(IdStrategy);

                    DynamicDTO dynamicdto = ConvertTo(dto, estrategy);
                    dynamicdto.StrategyData = estrategy.Code;
                    dynamicdto.IdLanguage = IdLanguage;

                    return EMailSenderStrategyLocator.Instance().GetEMailDynamicSender().GenerateAttachmentByTemplate(dynamicdto);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DynamicDTO ConvertTo(AbstractEMailDTO dto, Estrategy estrategy)
        {            
            Dictionary<string, object> dic = new Dictionary<string, object>();
            DynamicDTO dtoDym = new DynamicDTO();

            if (!string.IsNullOrEmpty(estrategy.JsonParams))
            {
                var dicData = JObject.Parse(estrategy.JsonParams);

                foreach (var item in dicData.Properties())
                {
                    if (item.Value == null || string.IsNullOrEmpty(item.Value.ToString()))
                    {
                        if (item.Name.ToUpper() == "LANGUAGE")
                        {
                            if (dto.IdLanguage > 0)
                            {
                                var idioma = IdiomaHome.Obtener(dto.IdLanguage);
                                var culture = new CultureInfo(idioma.Cultura);
                                item.Value = culture.TwoLetterISOLanguageName;
                            }

                            if (dto is DynamicDTO)
                            {
                                if (!string.IsNullOrEmpty(((DynamicDTO)dto).CultureUI))
                                {
                                    item.Value = ((DynamicDTO)dto).CultureUI;
                                }
                            }
                        }
                        else
                        {
                            var ekitdato = dto.GetType().GetProperties().Where(x => x.Name.ToUpper() == item.Name.ToUpper()).FirstOrDefault();

                            if (ekitdato != null)
                            {
                                var valor = ekitdato.GetValue(dto, null);
                                item.Value = valor.ToString();
                            }
                        }
                    }

                    dic.Add(item.Name, item.Value);
                }

                dtoDym.SetdicValues(dic);
            }
            else
            {
                //por si usa el dto dynamico del template main
                if (dto.GetType() == typeof(DynamicDTO))
                    dtoDym= ((DynamicDTO)dto);
                else            
                    dtoDym = ConvertToDynamicDTO(dto);
            }

            return dtoDym;
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

        public IList<AttachmentItem> GetAttachmentItems(AbstractEMailDTO dto)
        {
            IList<AttachmentItem> items = new List<AttachmentItem>();

            try
            {

                IdLanguage = dto.IdLanguage;
                IdStrategy = dto.IdStrategy;

                byte[] content = GetAttachContent(dto);

                var item = new AttachmentItem
                {
                    Name = GetAttachName(),
                    Description = GetAttachName(),
                    Type = GetAttachType(),
                    Language = IdiomaHome.Obtener(dto.IdLanguage),
                    Content = content,
                    Dimenssion = content == null ? 0 : content.Length
                };
                items.Add(item);
            }
            catch { }

            return items;
        }

        #endregion

        internal byte[] GetAttachmentEkit(string emailBody)
        {
            var body = PdfUtils.GetPdf(emailBody);
            if (body.Success)
                return body.Data;
            else
                throw body.Message;
        }
    }

}
