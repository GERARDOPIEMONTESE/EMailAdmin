using System.Xml.Serialization;
using System;

namespace EMailAdmin.BackEnd.DTO.EkitBenefits
{
    internal class EkitBenefitDTO
    {
    }

    public class ConsultaCondicionesUpgradeDTO
    {
        #region Propiedades

        public string CodigoUpgrade { get; set; }

        public string CodigoTarifa { get; set; }

        public int Categoria { get; set; }

        #endregion
    }

    public class ConsultasCondicionesDTO
    {
        #region Propiedades

        public ConsultaCondicionesDTO[] Consultas { get; set; }

        #endregion
    }

    /***
     * Invocacion servicio
     * **/

    public class ConsultaCondicionesDTO
    {
        #region Constructor

        public ConsultaCondicionesDTO()
        {
            Edad = -1;
        }

        #endregion

        #region Propiedades

        public int CodigoPais { get; set; }

        public string CodigoProducto { get; set; }

        public string CodigoTarifa { get; set; }

        public bool Anual { get; set; }

        public int Edad { get; set; }

        public int IdTipoPlan { get; set; }

        public int IdTipoModalidad { get; set; }

        public ConsultaCondicionesUpgradeDTO[] Upgrades { get; set; }

        public ConsultaCondicionesDTO ConsultaPadre { get; set; }

        #endregion
    }

    /***
     * Respuesta servicio
     * **/

    public class ClausulaIdiomaDTO
    {
        #region Propiedades

        public int IdIdioma { get; set; }

        public string Texto { get; set; }

        #endregion
    }

    public class ClausulaDTO
    {
        #region Propiedades

        public string CodigoTipoClausula { get; set; }

        public string NombreTipoClausula { get; set; }

        public string Codigo { get; set; }

        public ClausulaIdiomaDTO[] ClausulaIdioma { get; set; }

        #endregion
    }

    public class ContenidoClausulaRangoDTO
    {
        #region Propiedades

        public int EdadMinima { get; set; }

        public int EdadMaxima { get; set; }

        public string TipoPlan { get; set; }

        public string TipoModalidad { get; set; }

        public int Categoria { get; set; }

        public string Contenido { get; set; }

        public int IdValidezTerritorial { get; set; }

        public int IdValidezTerritorialClausula { get; set; }

        private decimal _tasa;
        public string Tasa
        {
            get 
            {
                return this._tasa.ToString();
            }
            set
            {
                try
                {
                    this._tasa = System.Decimal.Parse(value, System.Globalization.NumberStyles.Float);
                }
                catch (Exception e)
                {
                    this._tasa = System.Decimal.Parse("0");
                }
            }
        }

        public decimal Valor { get; set; }

        public int Moneda { get; set; }

        public LeyendaDTO[] Leyendas { get; set; }

        #endregion
    }

    public class CondicionEvaluacion
    {
        #region Propiedades

        public string Codigo { get; set; }

        public string TipoDato { get; set; }

        #endregion

        public CondicionEvaluacion()
        {
        }

        public CondicionEvaluacion(string pCodigo, string pTipoDato)
        {
            Codigo = pCodigo;
            TipoDato = pTipoDato;
        }
    }

    public class LeyendaDTO
    {
        #region Atributos

        #endregion

        #region Propiedades

        public int IdIdioma { get; set; }

        public string Texto { get; set; }

        #endregion

        public LeyendaDTO()
        {
        }
        public LeyendaDTO(int pIdIdioma, string pTexto)
        {
            IdIdioma = pIdIdioma;
            Texto = pTexto;
        }
    }

    public class ContenidoClausulaDTO
    {
        #region Constants

        public const string NONE = "NI";

        public const string ONLY_SHEET = "SIH";

        public const string PRINT_FULL = "FULL";

        public const string CODE_CONTENT = "IDCONT";

        public const string DESCRIPTION_CONTENT = "DESCONT";

        public const string CONTENT = "CONT";

        #endregion

        #region Propiedades

        public ClausulaDTO Clausula { get; set; }

        public string CodigoTipoImpresionClausula { get; set; }

        public string CodigoTipoContenidoImpresion { get; set; }

        public bool EvaluableEnAsistencia { get; set; }

        public bool VisibleEnAsistencia { get; set; }

        public string TipoCobertura { get; set; }

        public ContenidoClausulaDTO[] Padres { get; set; }

        public ContenidoClausulaRangoDTO[] Rangos { get; set; }

        public CondicionEvaluacion[] CondicionesEvaluacion { get; set; }

        public string CodigoTipoGrupo { get; set; }

        #endregion

        public bool ShowClause()
        {
            return CodigoTipoImpresionClausula != NONE && CodigoTipoImpresionClausula != ONLY_SHEET;
        }

        public bool EsClausulaDeSeguro()
        {
            return (Clausula.CodigoTipoClausula == "SEGU");
        }

        public string GetIdClause(int idLanguage)
        {
            return CodigoTipoContenidoImpresion == PRINT_FULL || 
                CodigoTipoContenidoImpresion == CODE_CONTENT ? Clausula.Codigo : "";
        }

        public string GetTitleClause(int idLanguage)
        {
            if (CodigoTipoContenidoImpresion == PRINT_FULL ||
                            CodigoTipoContenidoImpresion == DESCRIPTION_CONTENT)
            {
                if (Clausula != null && Clausula.ClausulaIdioma != null)
                {

                    foreach (var languageClause in Clausula.ClausulaIdioma)
                    {
                        if (languageClause.IdIdioma == idLanguage)
                        {
                            return languageClause.Texto;
                        }
                    }
                }
            }

            return "";
        }
        public string GetContentClause(int idLanguage)
        {
            if (CodigoTipoContenidoImpresion == PRINT_FULL ||
                            CodigoTipoContenidoImpresion == CODE_CONTENT ||
                            CodigoTipoContenidoImpresion == CONTENT ||
                            CodigoTipoContenidoImpresion == DESCRIPTION_CONTENT)
            {
                if (Rangos != null && Rangos.Length > 0 && Rangos[0].Leyendas != null)
                {
                    foreach (var leyend in Rangos[0].Leyendas)
                    {
                        if (leyend.IdIdioma == idLanguage)
                        {
                            return leyend.Texto;
                        }
                    }
                }
            }

            return "";
        }

        public string GetLanguageClause(int idLanguage)
        {
            if (Clausula != null && Clausula.ClausulaIdioma != null)
            {
                foreach (var languageClause in Clausula.ClausulaIdioma)
                {
                    if (languageClause.IdIdioma == idLanguage)
                    {
                        return languageClause.Texto;
                    }
                }
            }

            return "";
        }

        public string GetLeyend(int idLanguage)
        {
            if (Rangos != null && Rangos.Length > 0 && Rangos[0].Leyendas != null)
            {
                foreach (var leyend in Rangos[0].Leyendas)
                {
                    if (leyend.IdIdioma == idLanguage)
                    {
                        return leyend.Texto;
                    }
                }
            }

            return "";
        }

        internal string GetPrice(int cantPax)
        {
            if (Rangos != null && Rangos.Length > 0 && Rangos[0] != null)
            {
                var rango = this.Rangos[0];
                decimal coefDolar = (rango.Moneda == 1 ? 1 : System.Decimal.Parse(rango.Tasa, System.Globalization.NumberStyles.Float));
                decimal priceClausula = ((rango.Valor * coefDolar) * cantPax);
                return (rango.Moneda==1 ? "U$S":"$") + priceClausula.ToString();
            }
            else
                return "";
        }
    }

    public class TarifaDTO
    {
        #region Propiedades

        public int CodigoPais { get; set; }

        public string CodigoProducto { get; set; }

        public string CodigoTarifa { get; set; }

        public string Sufijo { get; set; }

        #endregion
    }

    public class DocumentoDTO
    {
        #region Propiedades

        public int IdDocumento { get; set; }

        public string CodigoValidacion { get; set; }

        public int IdTipoDocumento { get; set; }

        public string Nombre { get; set; }

        public string Observaciones { get; set; }

        public int DocumentoDimension { get; set; }

        public string DocumentoTipoContenido { get; set; }

        public int IdIdioma { get; set; }

        #endregion
    }

    public class GrupoClausulaDTO
    {
        #region Constants

        private const string DAYS_INTERNATIONAL = "C.5.4.1";
	    private const string DAYS_NATIONAL = "C.5.4.2";
        
        #endregion
        
        #region Propiedades

        public int IdLocacion { get; set; }

        public string TipoGrupoClausula { get; set; }

        public string Texto { get; set; }

        public string TipoTextoResumen { get; set; }

        public bool Anual { get; set; }

        public int DiasConsecutivos { get; set; }

        public ContenidoClausulaDTO[] Clausulas { get; set; }

        public TarifaDTO[] Tarifas { get; set; }

        public DocumentoDTO[] Documentos { get; set; }

        #endregion

        public ContenidoClausulaDTO GetBenefit(string benefitCode)
        {
            if (Clausulas != null)
            {
                foreach (ContenidoClausulaDTO dto in Clausulas)
                {
                    if (dto.Clausula != null && benefitCode.Equals(dto.Clausula.Codigo))
                    {
                        return dto;
                    }
                }

            }

            return null;
        }

        public string GetBenefitContent(string benefitCode, int idLanguage)
        {
            if (Clausulas != null && benefitCode != null)
            {
                foreach (ContenidoClausulaDTO dto in Clausulas)
                {
                    if (dto.Clausula != null && benefitCode.Equals(dto.Clausula.Codigo))
                    {
                        string content = dto.GetTitleClause(idLanguage) + " " + dto.GetContentClause(idLanguage);
                        return content != null ? content.Trim() : "";
                    }
                }

            }

            return "";
        }

        public string GetConsecutiveDays(int idLanguage)
        {
            //ContenidoClausulaDTO dto = GetBenefit(DAYS_NATIONAL);
            //if (dto != null)
            //{
            //    //return GetTitle(idLanguage) + GetTitleNationalDays(idLanguage) + dto.GetLeyend(idLanguage);
            //    return dto.GetLanguageClause(idLanguage) + " " + dto.GetLeyend(idLanguage);
            //}

            //dto = GetBenefit(DAYS_INTERNATIONAL);
            //if (dto != null)
            //{
            //    //return GetTitle(idLanguage) + GetTitleInternationalDays(idLanguage) + dto.GetLeyend(idLanguage);
            //    return dto.GetLeyend(idLanguage);
            //}
            ContenidoClausulaDTO dto = GetBenefit(DAYS_NATIONAL);
            if (dto == null)
            {
                dto = GetBenefit(DAYS_INTERNATIONAL);
            }
            
            return dto != null ? 
                dto.GetLanguageClause(idLanguage) + " " + dto.GetLeyend(idLanguage) : "";
        }

        private string GetTitle(int idLanguage)
        {
            if (idLanguage == 2)
            {
                return "With a maximum: ";
            }
            if (idLanguage == 3)
            {
                return "Com um m&aacute;ximo: ";
            }
            return "Con un m&aacute;ximo: ";
        }

        private string GetTitleInternationalDays(int idLanguage)
        {
            if (idLanguage == 2)
            {
                return "International Travel ";
            }
            if (idLanguage == 3)
            {
                return "International Travel ";
            }
            return "Para Viajes Internacionales de ";
        }

        private string GetTitleNationalDays(int idLanguage)
        {
            if (idLanguage == 2)
            {
                return "Travel within the country of residence of ";
            }
            if (idLanguage == 3)
            {
                return "Viagens dentro do país de residência de titular de ";
            }
            return "Para Viajes dentro del pa&iacute;s de residencia del Titular de ";
        }
    }

    public class ConjuntoGruposClausulaDTO
    {
        #region Propiedades

        public GrupoClausulaDTO[] Grupos { get; set; }

        #endregion
    }

    public class GruposClausulaDTO
    {
        public GrupoClausulaDTO[] Grupos { get; set; }
    }

    public class DocumentosDTO
    {
        public DocumentoDTO[] Documentos { get; set; }
    }


}