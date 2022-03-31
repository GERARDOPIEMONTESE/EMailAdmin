using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Domain
{
    /*
        TID: Id de seguimiento de Universal Analytics (para testing es uno y para prod es otro)
        CID: Id de cliente (generar un GUID)
        UID: Id de usuario (IdQuoteLog)
        EC: Categoria de evento ("email")
        EA: Acción de evento ("open")
        EL: Etiqueta del evento (generar un GUID)
        CS: Fuente de la campaña ("assistcard")
        CM: Medio de la campaña ("emailadmin")
        CN: Nombre de la campaña ("finalizatucompra")
        
        <img src="<http://www.google-analytics.com/collect?v=1&tid=UA-112410385-1&cid=112410385&t=event&ec=email&ea=open&el=${CompleteVoucherCode}$ &cs=newsletter&cm=email&cn=TESTDEPIXEL>" style="visibility: hidden;">  
         
        v=1&
        tid=UA-112410385-1&
        cid=112410385&
        t=event&
        ec=email&
        ea=open&
        el=${CompleteVoucherCode}$ &
        cs=newsletter&
        cm=email&
        cn=TESTDEPIXEL        
        */

    public class PixelData
    {
        public string PixelURL { get; set; }
        public string V { get; set; }
        public string T { get; set; }
        //public string TID { get; set; }
        //public string CID { get; set; }
        public string UID { get; set; }
        public string EC { get; set; }
        public string EA { get; set; }
        public string EL { get; set; }
        public string CS { get; set; }
        public string CM { get; set; }
        public string CN { get; set; }

        public  TrackEmail ConvertToTrackEmail()
        {
            int IdSeguimiento = 0;
            int.TryParse(UID, out IdSeguimiento);

            return new TrackEmail()
            {
                Campania = CN,
                FuenteCampania = CS,
                MedioCampania = CM,
                Evento = EA,
                IdEmailLog = IdSeguimiento
            };
        }
    }

    public class Pixel : VariableText
    {
        public PixelData pixel { get; set; }

        public static string GetJsonContenido(PixelData obj)
        {
            try
            {
                JavaScriptSerializer jsSer = new JavaScriptSerializer();
                return jsSer.Serialize(obj);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static PixelData GetPixel(string json)
        {
            try
            {
                JavaScriptSerializer jsSer = new JavaScriptSerializer();
                PixelData pixel = new PixelData();
                object objData = jsSer.Deserialize(json, pixel.GetType());
                return (PixelData)objData;
            }
            catch
            {
                return null;
            }
        }

        protected override FrameworkDAC.Dato.IDAOObjetoNegocio GetConcreteDao()
        {
            return DAOLocator.Instance().GetDaoPixel();
        }

        protected override string GetConcreteName()
        {
            return "Pixel";
        }
    }
}
