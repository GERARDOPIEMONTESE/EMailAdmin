using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;
using System.Globalization;
using EMailAdmin.BackEnd.Service.Interfaces;

namespace EMailAdmin.BackEnd.Domain
{
    public class EmailAlertChat : ObjetoPersistido
    {
        private const string NAME = "EmailAlertChats";

        public string Negocio { get; set; }
        public string OperatorType { get; set; }
        public double LatitudeLocation { get; set; }
        public double LongitudeLocation{ get; set; }
        public bool IsFallBack { get; set; }
        public int Cantidad { get; set; }

        public static double ConvertToPoint(string val)
        {
            double cood;
            val = val.Replace(',', '.'); // si viene de la db trae separador coma, si viene del js trae separador punto
            Double.TryParse(val, NumberStyles.Number, new CultureInfo("en-US"), out cood);
            return cood;
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
