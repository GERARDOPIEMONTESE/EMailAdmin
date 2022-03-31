using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using CapaNegocioDatos.Servicios;
using CapaNegocioDatos.Utilitarios;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Strategies.Attachment
{
    public class MainBenefitsAttachStrategy : AttachStrategy, IAttachStrategy
    {
        #region Constants

        private const string ATTACH_NAME = "ASSISTCARD.pdf";

        private const string ATTACH_TYPE = "application/pdf";

        #endregion Constants

        public MainBenefitsAttachStrategy()
        {
            ATTACHNAME = ATTACH_NAME;
            ATTACHTYPE = ATTACH_TYPE;
        }

        #region Properties

        public ReportDocument emailReportDocument;
        public CrystalReportPartsViewer crvEmailReport;

        public ReportDocument voucherInfoReportDocument;
        public CrystalReportPartsViewer crvVoucherInfoReport;

        public ReportDocument conditionsReportDocument;
        public CrystalReportPartsViewer crvconditionsReport;

        #endregion

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
            return GenerateReport(GetEmailReportObject(ekitDto),
                                  GetVoucherInformationReportObject(ekitDto, translations), GetConditionsReportObject(ekitDto, translations));
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
                emailReportDocument = new ReportDocument();

                string emailPath = ConfigurationValueHome.GetReportPath() +"EmailReport.rpt";
                emailReportDocument.Load(emailPath);
                emailReportDocument.SetDataSource(new List<EmailReportObject> { email });

                crvEmailReport = new CrystalReportPartsViewer { ReportSource = emailReportDocument, Visible = false };

                voucherInfoReportDocument = new ReportDocument();

                string voucherInfoPath = ConfigurationValueHome.GetReportPath() + "VoucherInformationReport.rpt";
                voucherInfoReportDocument.Load(voucherInfoPath);
                voucherInfoReportDocument.SetDataSource(new List<VoucherInformationReportObject> { voucher });

                crvVoucherInfoReport = new CrystalReportPartsViewer { ReportSource = voucherInfoReportDocument, Visible = false };

                conditionsReportDocument = new ReportDocument();

                string conditionsPath = ConfigurationValueHome.GetReportPath() + "ParticularConditionsReport.rpt";
                conditionsReportDocument.Load(conditionsPath);
                conditionsReportDocument.SetDataSource(clauses);

                crvconditionsReport = new CrystalReportPartsViewer { ReportSource = conditionsReportDocument, Visible = false };

                var sourceFiles = new List<byte[]>
                                  {
                                      ((MemoryStream)
                                       emailReportDocument.ExportToStream(ExportFormatType.PortableDocFormat)).ToArray(),
                                      //((MemoryStream)
                                      // voucherInfoReportDocument.ExportToStream(ExportFormatType.PortableDocFormat)).
                                      //    ToArray(),
                                      ((MemoryStream)
                                       conditionsReportDocument.ExportToStream(ExportFormatType.PortableDocFormat)).
                                          ToArray()
                                  };
                //MemoryStream ms = MergeFiles(sourceFiles);
                byte[] report = MergeFiles(sourceFiles);
                FreeReports();
                
                return report;
            }
            catch (Exception ex)
            {
                FreeReports();
                return new byte[0];
            }
            //byte[] report = ms.ToArray();
                        
            
            /*
            //LO ANTERIOR
            emailReportDocument.Dispose();
            
            voucherInfoReportDocument.Dispose();
            conditionsReportDocument.Dispose();

            crvEmailReport.Dispose();
            crvVoucherInfoReport.Dispose();
            crvconditionsReport.Dispose();
            //ms.Dispose();
            */
            
        }
                
        public static byte[] MergeFiles(List<byte[]> sourceFiles)
        {
            var document = new Document();
            var output = new MemoryStream();

            try
            {
                // Initialize pdf writer
                var writer = PdfWriter.GetInstance(document, output);

                // Open document to write
                document.Open();
                var content = writer.DirectContent;

                // Iterate through all pdf documents
                for (int fileCounter = 0; fileCounter < sourceFiles.Count; fileCounter++)
                {
                    // Create pdf reader
                    var reader = new PdfReader(sourceFiles[fileCounter]);
                    int numberOfPages = reader.NumberOfPages;

                    // Iterate through all pages
                    for (int currentPageIndex = 1; currentPageIndex <=
                                       numberOfPages; currentPageIndex++)
                    {
                        // Determine page size for the current page
                        document.SetPageSize(
                           reader.GetPageSizeWithRotation(currentPageIndex));

                        // Create page
                        document.NewPage();
                        var importedPage =
                          writer.GetImportedPage(reader, currentPageIndex);


                        // Determine page orientation
                        var pageOrientation = reader.GetPageRotation(currentPageIndex);
                        if ((pageOrientation == 90) || (pageOrientation == 270))
                        {
                            content.AddTemplate(importedPage, 0, -1f, 1f, 0, 0,
                               reader.GetPageSizeWithRotation(currentPageIndex).Height);
                        }
                        else
                        {
                            content.AddTemplate(importedPage, 1f, 0, 0, 1f, 0, 0);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("There has an unexpected exception" +
                      " occured during the pdf merging process.", exception);
            }
            finally
            {
                document.Close();
            }
            return output.GetBuffer();
        }

        #endregion

        #region Load information methods

        private EmailReportObject GetEmailReportObject(EMailEkitDTO dto)
        {
            return new EmailReportObject { Body = dto.EMailBody, 
                Footer = dto.FooterPDF != null && dto.FooterPDF.Length > 0 ? dto.FooterPDF : dto.Footer, 
                Header = dto.HeaderPDF != null && dto.HeaderPDF.Length > 0 ? dto.HeaderPDF : dto.Header };
        }

        private VoucherInformationReportObject GetVoucherInformationReportObject(EMailEkitDTO dto, IDictionary<string, string> translations)
        {                        
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
                CardNumber = dto.VoucherCode,
                CardType = dto.CardType,
                CardValidFrom = dto.EffectiveStartDateShortFormat,//Utils.DateUtil.FormatToShortDate( Convert.ToDateTime(dto.EffectiveStartDate), dto.IdLanguage ),
                CardValidTo = dto.EffectiveEndDateShortFormat, //Utils.DateUtil.FormatToShortDate(Convert.ToDateTime(dto.EffectiveEndDate), dto.IdLanguage),
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
                PersonalInfoPhone = dto.PaxPhone
            };

            Domain.Attachment.SetContentAttachmentInfo(voucher, dto.XMLContentAttachment);

            return voucher;
        }
        
        private IEnumerable<ConditionsReportObject> GetConditionsReportObject(EMailEkitDTO dto, IDictionary<string, string> translations)
        {
            string paisesExcluyenSeguros = ServicioBroker.Instancia().
                            ObtenerServicioCodigoActivador().ObtenerValidarPaisExcluyeSeguro();

            var result = new List<ConditionsReportObject>();
            
            foreach (ContenidoClausulaDTO contenidoClausulaDto in dto.GrupoClausulaDTO.Clausulas)
            {
                if (contenidoClausulaDto.ShowClause())
                {
                    if (paisesExcluyenSeguros.Length == 0 || 
                            (!paisesExcluyenSeguros.Contains(dto.CountryCode.ToString())) || 
                            (paisesExcluyenSeguros.Contains(dto.CountryCode.ToString()) && !contenidoClausulaDto.EsClausulaDeSeguro())) {

                    result.Add(new ConditionsReportObject(contenidoClausulaDto.CodigoTipoContenidoImpresion,
                                    contenidoClausulaDto.GetIdClause(dto.IdLanguage),
                                    contenidoClausulaDto.GetTitleClause(dto.IdLanguage),
                                    contenidoClausulaDto.GetContentClause(dto.IdLanguage),
                                    translations[ReportLanguage.CONDITIONSREPORT], dto.HeaderPDF, dto.FooterPDF,
                                    contenidoClausulaDto.EsClausulaDeSeguro()));
                    }
                }
            }

            return result;
        }

        #endregion
    }
}