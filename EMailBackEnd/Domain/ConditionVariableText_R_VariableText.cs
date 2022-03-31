using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.DTO;

namespace EMailAdmin.BackEnd.Domain
{
    public class ConditionVariableText_R_VariableText : ObjetoNegocio
    {
        #region Constant

        private const string NAME = "ConditionVariableText_R_VariableText";

        #endregion Constant

        #region Properties

        public VariableText VariableText { get; set; }

        public string Condition { get; set; }

        public string DynamicName { get; set; }

        public string ConditionOperador
        {
            get
            {
                return Condition.Split(' ')[0].ToString();
            }
        }

        public string ConditionValor
        {
            get
            {
                return Condition.Replace(ConditionOperador, "");
            }
        }


        private string sMsgError;
        public string MsgError
        {
            get
            {
                return sMsgError;
            }
        }

        private string sValorFormateado;
        public string ValorFormateado
        {
            get
            {
                return sValorFormateado;
            }
        }
        
        public int ConditionVariableTextId { get; set; }

        #endregion Properties

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoConditionVariableText_R_VariableText();
        }

        #endregion Methods

        public static string[] GetOperadores()
        {
            return new String[] { "=", ">", "<", "<>", "RANGE_NUMBERS", "RANGE_DATES", "INCLUDED" };
        }

        public bool Validar()
        {
            bool bIsValid = false;
            sMsgError = "";

            switch (this.ConditionOperador)
            {
                case "=":
                case "<>": bIsValid = ValidText(this.ConditionValor); break;
                case ">":
                case "<": bIsValid = ValidNum(this.ConditionValor); break;
                case "RANGE_NUMBERS": bIsValid = validRangoNum(this.ConditionValor); break;
                case "RANGE_DATES": bIsValid = validRangoFecha(this.ConditionValor); break;
                case "INCLUDED": bIsValid = validIncluido(this.ConditionValor); break;
                default:
                    bIsValid = false;
                    break;
            }

            return bIsValid;
        }

        private bool ValidText(string valCondition)
        {
            if (valCondition != "")
            {
                sValorFormateado = valCondition;
                return true;
            }
            else
                return false;
        }

        private bool ValidNum(string valCondition)
        {
            int valCond = 0;
            if (int.TryParse(valCondition, out valCond))
            {
                sValorFormateado = valCond.ToString();
                return true;
            }
            else
            {
                sMsgError = "ERRORFORMATO_NUM";
                return false;
            }
        }

        private bool validIncluido(string valCondition)
        {
            string[] valores = valCondition.Split(',');
            if (valores.Length < 1)
            {
                sMsgError = "ERRORFORMATO_INCLUDED";
                return false;
            }
            else
            {
                sValorFormateado = valCondition;
                return true;
            }
        }

        private bool validRangoFecha(string valCondition)
        {
            bool rst = true;
            DateTime d1, d2;
            string[] valores = valCondition.Split('-');
            if (valores.Length == 2)
            {
                if (!DateTime.TryParse(valores[0].Trim(), out d1) || !DateTime.TryParse(valores[1].Trim(), out d2))
                {
                    sMsgError = "ERRORVALORES_RANGOFECHA";
                    rst = false;
                }
                else
                {
                    if (d1 < d2)
                        sValorFormateado = string.Format("{0}-{1}", d1.ToShortDateString(), d2.ToShortDateString());
                    else
                    {
                        sMsgError = "ERRORVAL1_RANGOFECHA";
                        rst = false;
                    }
                }
            }
            else
            {
                sMsgError = "ERRORFORMATO_RANGOFECHA";
                rst = false;
            }
            
            return rst;
        }

        private bool validRangoNum(string valCondition)
        {
            bool rst = true;
            int v1, v2;
            string[] valores = valCondition.Split('-');
            if (valores.Length == 2)
            {
                if (!int.TryParse(valores[0].Trim(), out v1) || !int.TryParse(valores[1].Trim(), out v2))                    
                {
                    sMsgError = "ERRORVALORES_RANGONUM";
                    rst = false;
                }
                else
                {
                    if (v1 < v2)
                        sValorFormateado = string.Format("{0}-{1}", v1, v2);
                    else
                    {
                        sMsgError = "ERRORVAL1_RANGONUM";
                        rst = false;
                    }
                }
            }
            else
            {
                sMsgError = "ERRORFORMATO_RANGONUM";
                rst = false;
            }
            return rst;
        }

       public static bool Evaluar(string valProp, ConditionVariableText_R_VariableText cvt)
        {
           bool bIsValid = false;
           try
           {
               switch (cvt.ConditionOperador)
               {
                   case "=": bIsValid = EsIgual(valProp.Trim(), cvt.ConditionValor.Trim()); break;
                   case ">": bIsValid = EsMayor(valProp.Trim(), cvt.ConditionValor.Trim()); break;
                   case "<": bIsValid = EsMenor(valProp.Trim(), cvt.ConditionValor.Trim()); break;
                   case "<>": bIsValid = EsDistinto(valProp.Trim(), cvt.ConditionValor.Trim()); break;
                   case "RANGE_NUMBERS": bIsValid = RangoNum(valProp.Trim(), cvt.ConditionValor.Trim()); break;
                   case "RANGE_DATES": bIsValid = RangoFecha(valProp.Trim(), cvt.ConditionValor.Trim()); break;
                   case "INCLUDED": bIsValid = EstaIncluido(valProp.Trim(), cvt.ConditionValor.Trim()); break;
                   default:
                       bIsValid = false;
                       break;
               }
           }
           catch
           {
               bIsValid = false;
           }
            return bIsValid;
        }

       private static bool EstaIncluido(string valProp, string valCondition)
       {
           DateTime fecha = new DateTime();
           if (DateTime.TryParse(valProp, out fecha))
               valProp = fecha.ToShortDateString();

           string[] valores = valCondition.Split(',');
           for (int i = 0; i < valores.Length; i++)
           {
               valores[i] = valores[i].Trim();
               if (valores[i].Equals(valProp))
                   return true;               
           }
           return false;
       }

       private static bool RangoFecha(string valProp, string valCondition)
       {
           string[] valores = valCondition.Split('-');
           int dd = DateTimeToInt(Convert.ToDateTime(valores[0].ToString()));
           int dh = DateTimeToInt(Convert.ToDateTime(valores[1].ToString()));
           int vp = DateTimeToInt(Utils.DateUtil.ConvertToDate(valProp));
           return (EsMayor(vp.ToString(), dd.ToString()) && EsMenor(vp.ToString(), dh.ToString()));
       }

       private static bool RangoNum(string valProp, string valCondition)
       {
           string[] valores = valCondition.Split('-');           
           return (EsMayor(valProp, valores[0]) && EsMenor(valProp, valores[1]));
       }

       public static int DateTimeToInt(DateTime theDate)
       {
           return (int)(theDate.Date - new DateTime(1900, 1, 1)).TotalDays + 2;
       }

       private static bool EsDistinto(string valProp, string valCondition)
       {
           return !valProp.Equals(valCondition);
       }

       private static bool EsMenor(string valProp, string valCondition)
       {
           int iValProp = 0;
           int iValCond = 0;
           if (int.TryParse(valProp, out iValProp) && (int.TryParse(valCondition, out iValCond)))
           {
               return (iValProp < iValCond ? true : false);
           }
           else 
               return false;
       }

       private static bool EsMayor(string valProp, string valCondition)
       {
           int iValProp = 0;
           int iValCond = 0;
           if (int.TryParse(valProp, out iValProp) && (int.TryParse(valCondition, out iValCond)))
           {
               return (iValProp > iValCond ? true : false);
           }
           else
               return false;
       }

       private static bool EsIgual(string valProp, string valCondition)
       {
           return valProp.Equals(valCondition);
       }       
    }    
}