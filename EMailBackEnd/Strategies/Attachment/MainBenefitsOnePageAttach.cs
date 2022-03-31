using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CapaNegocioDatos.CapaHome;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Reports.Objects;
using iTextSharp.text;
using iTextSharp.text.pdf;
using EMailAdmin.BackEnd.DTO.EkitBenefits;
using EMailAdmin.BackEnd.Properties;
using CapaNegocioDatos.CapaNegocio;
using CapaNegocioDatos.Servicios;
using EMailAdmin.BackEnd.Home;
using CapaNegocioDatos.Utilitarios;

namespace EMailAdmin.BackEnd.Strategies.Attachment
{
    public class MainBenefitsOnePageAttachStrategy : AttachStrategy, IAttachStrategy
    {
        #region Constants

        private const string ATTACH_NAME = "ASSISTCARD.pdf";

        private const string ATTACH_TYPE = "application/pdf";

        #endregion Constants

        private string textoLeyendaC1;

        #region Properties

        public ReportDocument emailReportDocument;
        public CrystalReportPartsViewer crvEmailReport;

        public ReportDocument voucherInfoReportDocument;
        public CrystalReportPartsViewer crvVoucherInfoReport;

        public ReportDocument conditionsReportDocument;
        public CrystalReportPartsViewer crvconditionsReport;

        #endregion

        public MainBenefitsOnePageAttachStrategy()
        {
            ATTACHNAME = ATTACH_NAME;
            ATTACHTYPE = ATTACH_TYPE;
        }

        #region IAttachStrategy Members

        public IList<AttachmentItem> GetAttachmentItems(AbstractEMailDTO dto)
        {
            IdLanguage = dto.IdLanguage;
            IdStrategy = dto.IdStrategy;

            IList<AttachmentItem> items = new List<AttachmentItem>();
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

            return items;
        }

        #endregion

        #region Private methods
       
        private byte[] GetAttachContent(AbstractEMailDTO dto)
        {
            var translations =
                DAOLocator.Instance().GetDaoReportLanguage().FindByLanguage(dto.IdLanguage, dto.IdStrategy);
            var ekitDto = (EMailEkitDTO)dto;
            var conditions = GetConditionsReportObject(ekitDto, translations);
           
            return GenerateReport(GetEmailReportObject(ekitDto),
                                  GetVoucherInformationReportObject(ekitDto, translations), conditions);
        }
             
        private void FreeReports()
        {
            if (crvEmailReport != null)
            {
                crvEmailReport.Dispose();
                crvEmailReport = null;
            }

            if (emailReportDocument != null)
            {
                emailReportDocument.Close();
                emailReportDocument.Dispose();
                emailReportDocument = null;
            }

            if (crvVoucherInfoReport != null)
            {
                crvVoucherInfoReport.Dispose();
                crvVoucherInfoReport = null;
            }

            if (voucherInfoReportDocument != null)
            {
                voucherInfoReportDocument.Close();
                voucherInfoReportDocument.Dispose();
            }

            if (crvconditionsReport != null)
            {
                crvconditionsReport.Dispose();
                crvconditionsReport = null;
            }

            if (conditionsReportDocument != null)
            {
                conditionsReportDocument.Close();
                conditionsReportDocument.Dispose();
            }
            try
            {
                GC.Collect();
            }
            catch (Exception) { }
        }

        private byte[] GenerateReport(EmailReportObject email, VoucherInformationReportObject voucher,
                                      IEnumerable<ConditionsReportObject> clauses)
        {
            try
            {
                string reportPath = ConfigurationValueHome.GetReportPath();

                bool b = ServicioBroker.Instancia().ObtenerServicioCodigoActivador().HabilitaPDFCondicionPrecio();

                string rptOnePage = (b ? "VoucherInformationOnePagePriceReport.rpt" : "VoucherInformationOnePageReport.rpt");
                string subrptConditions = (b ? "ParticularConditionsOnePageReportPrice.rpt" : "ParticularConditionsOnePageReport.rpt");
                //clauses = FilterAndReorderClauses(clauses);

                voucherInfoReportDocument = new ReportDocument();

                string voucherInfoPath = reportPath + rptOnePage;
                voucherInfoReportDocument.Load(voucherInfoPath);
                voucherInfoReportDocument.SetDataSource(new List<VoucherInformationReportObject> { voucher });
                voucherInfoReportDocument.Subreports[0].SetDataSource(clauses);

                crvVoucherInfoReport = new CrystalReportPartsViewer { ReportSource = voucherInfoReportDocument, Visible = false };

                conditionsReportDocument = new ReportDocument();

                string conditionsPath = reportPath + subrptConditions;
                conditionsReportDocument.Load(conditionsPath);
                conditionsReportDocument.SetDataSource(clauses);

                crvconditionsReport = new CrystalReportPartsViewer { ReportSource = conditionsReportDocument, Visible = false };

                byte[] report = ((MemoryStream)voucherInfoReportDocument.ExportToStream(ExportFormatType.PortableDocFormat)).ToArray();
                FreeReports();
                return report;
            }
            catch (Exception ex)
            {
                FreeReports();
                return new byte[0];
            }
        }

        /*private IEnumerable<ConditionsReportObject> FilterAndReorderClauses(IEnumerable<ConditionsReportObject> clauses)
        {
            // Filtrar las clausulas generales y reordenar las clausulas para q seguro este al final

            var reordered = new List<ConditionsReportObject>();
            reordered.AddRange(clauses.Where(c => !c.EsClausulaDeSeguro && !string.IsNullOrEmpty(c.Code) && c.Code != "*"));
            reordered.AddRange(clauses.Where(c => c.EsClausulaDeSeguro && !string.IsNullOrEmpty(c.Code) && c.Code != "*"));
            return reordered;
        }*/

        #endregion

        #region Load information methods

        private EmailReportObject GetEmailReportObject(EMailEkitDTO dto)
        {
            return new EmailReportObject
            {
                Body = dto.EMailBody,
                Footer = dto.FooterPDF != null && dto.FooterPDF.Length > 0 ? dto.FooterPDF : dto.Footer,
                Header = dto.HeaderPDF != null && dto.HeaderPDF.Length > 0 ? dto.HeaderPDF : dto.Header
            };
        }

        private VoucherInformationReportObject GetVoucherInformationReportObject(EMailEkitDTO dto, IDictionary<string, string> translations)
        {
            Pais pais = PaisHome.ObtenerPorCodigo(dto.CountryCode.ToString());
            EMailContactType type = DAOLocator.Instance().GetDaoEMailContactType().GetByCode("EKitGral");
            IList<EMailContact> iContact = DAOLocator.Instance().GetDaoEMailContact().
                Find(type.Id, pais.IdLocacion);

            CultureInfo cultureInfo = null;
            if (dto.IdLanguage == 1) cultureInfo = new CultureInfo("es");
            if (dto.IdLanguage == 2) cultureInfo = new CultureInfo("en");
            if (dto.IdLanguage == 3) cultureInfo = new CultureInfo("pt");

            var voucher = new VoucherInformationReportObject(translations[ReportLanguage.REPORT],
                                                             translations[ReportLanguage.LASTNAMENAME],
                                                             translations[ReportLanguage.CARD],
                                                             translations[ReportLanguage.CARDVALIDFROM],
                                                             translations[ReportLanguage.CARDVALIDTO],
                                                             translations[ReportLanguage.CARDDAYS],
                                                             translations[ReportLanguage.CARDAREA],
                                                             translations[ReportLanguage.CARDEMISSIONDATE],
                                                             translations[ReportLanguage.CARDAMOUNT],
                                                             translations[ReportLanguage.PERSONALINFO],
                                                             translations[ReportLanguage.PERSONALINFOPASSPORT],
                                                             translations[ReportLanguage.PERSONALINFOAGE],
                                                             translations[ReportLanguage.PERSONALINFOADDRESS],
                                                             translations[ReportLanguage.PERSONALINFOCOUNTRY],
                                                             translations[ReportLanguage.PERSONALINFOPHONE],
                                                             translations[ReportLanguage.EMERGENCY],
                                                             translations[ReportLanguage.EMERGENCYCONTACT],
                                                             translations[ReportLanguage.EMERGENCYPHONE],
                                                             translations[ReportLanguage.EMERGENCYADDRESS])
            {
                CardAgency = dto.AgencyCode,
                CardAmount = dto.Amount,
                CardArea = dto.Area,
                CardDays = dto.Days,
                CardEmissionDate = dto.IssuanceDateShortFormat,
                CardNumber = dto.CompleteVoucherCode,
                CardType = dto.CardType,
                CardValidFrom = Convert.ToDateTime(dto.EffectiveStartDate).ToString("dd MMM yyyy", cultureInfo).ToUpper(),
                CardValidTo = Convert.ToDateTime(dto.EffectiveEndDate).ToString("dd MMM yyyy", cultureInfo).ToUpper(),
                EmergencyAddress = dto.EmergencyAddress,
                EmergencyContact = dto.EmergencyContact,
                EmergencyPhone = dto.EmergencyPhone,
                Footer = dto.FooterPDF != null && dto.FooterPDF.Length > 0 ? dto.FooterPDF : dto.Footer,
                Header = dto.HeaderPDF != null && dto.HeaderPDF.Length > 0 ? dto.HeaderPDF : dto.Header,
                ColorRed = dto.ColorRGB.R,
                ColorGreen = dto.ColorRGB.G,
                ColorBlue = dto.ColorRGB.B,
                LastName = dto.RecipientSurname + " " + dto.RecipientName,
                Name = "",
                PersonalInfoAddress = dto.PaxAddress,
                PersonalInfoAge = dto.PaxAge,
                PersonalInfoCountry = PaisHome.ObtenerPorCodigo(dto.PaxCountry.ToString(CultureInfo.InvariantCulture)).Nombre,
                PersonalInfoPassport = dto.PaxPassport,
                PersonalInfoPhone = dto.PaxPhone,
                Ruc = dto.PaxPassport, // documento, pasaporte
                ProductName = dto.ProductName,
                CompleteVoucherCode = dto.CompleteVoucherCode,

                ContactInfo1Title = translations[ReportLanguage.CONTACTINFO1TITLE].Replace("{ContactEmail}", iContact[0].EMail),
                ContactInfo2Title = translations[ReportLanguage.CONTACTINFO2TITLE],
                PersonalInfoHeader = translations[ReportLanguage.PERSONALINFOHEADER],
                ProductNameTitle = translations[ReportLanguage.PRODUCTNAMETITLE],
                CardValidTitle = translations[ReportLanguage.CARDVALIDTITLE],
                PersonalInfoRucTitle = translations[ReportLanguage.PERSONALINFORUCTITLE],
                VoucherCodeTitle = translations[ReportLanguage.VOUCHERCODETITLE],
                DateValidityTo = translations[ReportLanguage.DATEVALIDITYTO],
                DateValidityInclusive = translations[ReportLanguage.DATEVALIDITYINCLUSIVE],
                LocationsTitle = translations[ReportLanguage.LOCATIONSTITLE],
                ConditionsReportTitle = translations[ReportLanguage.CONDITIONSREPORTTITLE],
                ConditionsText = this.textoLeyendaC1, // Despues se sobreescribe con la C.1
                ReportFooter = translations[ReportLanguage.REPORTFOOTER],
                ContactEmail = iContact[0].EMail,
                Central1Label = translations[ReportLanguage.CENTRAL1LABEL], // América del Norte
                Central2Label = translations[ReportLanguage.CENTRAL2LABEL], // América Latina
                Central3Label = translations[ReportLanguage.CENTRAL3LABEL], // Europa
                Central4Label = translations[ReportLanguage.CENTRAL4LABEL] // Asia,
            };

            Domain.Attachment.SetContentAttachmentInfo(voucher, dto.XMLContentAttachment);

            return voucher;
        }
        
        private IEnumerable<ConditionsReportObject> GetConditionsReportObject(EMailEkitDTO dto, IDictionary<string, string> translations)
        {
            bool bHabilitaPrice = ServicioBroker.Instancia().ObtenerServicioCodigoActivador().HabilitaPDFCondicionPrecio();

            int cantPax = dto.clientes.Count;

            string paisesExcluyenSeguros = ServicioBroker.Instancia().
                            ObtenerServicioCodigoActivador().ObtenerValidarPaisExcluyeSeguro();

            var result = new List<ConditionsReportObject>();

            // Obtener dto clausula C1
            try
            {
                var C1 = (from c in dto.GrupoClausulaDTO.Clausulas
                          where c.Clausula.Codigo == "1."
                          select c).FirstOrDefault<ContenidoClausulaDTO>();

                var LeyendaC1 = (from l in C1.Rangos[0].Leyendas
                                 where l.IdIdioma == dto.IdLanguage
                                 select l).FirstOrDefault<LeyendaDTO>();

                this.textoLeyendaC1 = LeyendaC1.Texto;
            }
            catch { }

            foreach (ContenidoClausulaDTO contenidoClausulaDto in dto.GrupoClausulaDTO.Clausulas)
            {
                // filtrar la clausula C1
                if (contenidoClausulaDto.Clausula.Codigo != "1." &&
                    contenidoClausulaDto.ShowClause())
                {
                    if (paisesExcluyenSeguros.Length == 0 ||
                            (!paisesExcluyenSeguros.Contains(dto.CountryCode.ToString())) ||
                            (paisesExcluyenSeguros.Contains(dto.CountryCode.ToString()) && !contenidoClausulaDto.EsClausulaDeSeguro()))
                    {

                        result.Add(new ConditionsReportObject(contenidoClausulaDto.CodigoTipoContenidoImpresion,
                                        contenidoClausulaDto.GetIdClause(dto.IdLanguage),
                                        contenidoClausulaDto.GetTitleClause(dto.IdLanguage),
                                        contenidoClausulaDto.GetContentClause(dto.IdLanguage),
                                        (translations.ContainsKey(ReportLanguage.CONDITIONSREPORT)? translations[ReportLanguage.CONDITIONSREPORT]:""),
                                        dto.HeaderPDF, dto.FooterPDF,
                                        contenidoClausulaDto.EsClausulaDeSeguro(),
                                        (bHabilitaPrice? contenidoClausulaDto.GetPrice(cantPax) : "")));
                    }
                }
            }

            return result;
        }

        #endregion
    }
}