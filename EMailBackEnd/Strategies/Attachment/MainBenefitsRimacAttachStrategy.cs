using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
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
using System.Data;
using EMailAdmin.BackEnd.Utils;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Strategies.Attachment
{
    public class MainBenefitsRimacAttachStrategy : AttachStrategy, IAttachStrategy
    {
        #region Constants

        private const string ATTACH_NAME = "RIMACATTACH.pdf";

        private const string ATTACH_TYPE = "application/pdf";

        #endregion Constants

        public MainBenefitsRimacAttachStrategy()
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

        public ReportDocument voucherAdditionalInfoReportDocument;
        public CrystalReportPartsViewer crvVoucherAdditionalInfoReport;

        public CrystalReportSource crsReport;

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
            //return GenerateReport(GetEmailReportObject(ekitDto),
            //                   GetVoucherInformationReportRimacObject(ekitDto, translations), GetConditionsReportObject(ekitDto, translations));

            var voucher = GetVoucherInformationReportRimacObject(ekitDto, translations);
            var pasajeros = GetPasajerosObject(ekitDto);
            var condiciones = GetConditionsReport(ekitDto, translations);

            return GenerateReportRimac(voucher, pasajeros, condiciones);
            
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
            if (crvVoucherAdditionalInfoReport != null)
            {
                crvVoucherAdditionalInfoReport.Dispose();
                crvVoucherAdditionalInfoReport = null;
            }

            if (voucherAdditionalInfoReportDocument != null)
            {
                voucherAdditionalInfoReportDocument.Close();
                voucherAdditionalInfoReportDocument.Dispose();
            }
            try
            {
                GC.Collect();
            }
            catch (Exception) { }
        }

        private byte[] GenerateReport(EmailReportObject email, VoucherInformationReportRimacObject voucher,
                                      IEnumerable<ConditionsReportObject> clauses)
        {
            try
            {
                string reportPath = ConfigurationValueHome.GetReportPath();

                emailReportDocument = new ReportDocument();

                string emailPath = ConfigurationValueHome.GetReportPath() + "EmailReport.rpt";
                emailReportDocument.Load(emailPath);
                emailReportDocument.SetDataSource(new List<EmailReportObject> { email });

                crvEmailReport = new CrystalReportPartsViewer { ReportSource = emailReportDocument, Visible = false };

                voucherInfoReportDocument = new ReportDocument();


                string voucherInfoPath = reportPath + "RimacVoucherInformation.rpt";
                voucherInfoReportDocument.Load(voucherInfoPath);
                voucherInfoReportDocument.SetDataSource(new List<VoucherInformationReportRimacObject> { voucher });

                crvVoucherInfoReport = new CrystalReportPartsViewer { ReportSource = voucherInfoReportDocument, Visible = false };

                conditionsReportDocument = new ReportDocument();

                string conditionsPath = reportPath + "RimacParticularConditionsReport.rpt";
                conditionsReportDocument.Load(conditionsPath);
                conditionsReportDocument.SetDataSource(clauses);

                crvconditionsReport = new CrystalReportPartsViewer { ReportSource = conditionsReportDocument, Visible = false };
                
                
                voucherAdditionalInfoReportDocument = new ReportDocument();

                string voucherAdditionalInfoPath = reportPath + "RimacVoucherAdditionalInformation.rpt";
                voucherAdditionalInfoReportDocument.Load(voucherAdditionalInfoPath);
                voucherAdditionalInfoReportDocument.SetDataSource(new List<VoucherInformationReportRimacObject> { voucher });

                crvVoucherAdditionalInfoReport = new CrystalReportPartsViewer { ReportSource = voucherAdditionalInfoPath, Visible = false };

                var sourceFiles = new List<byte[]>
                                  {
                                      //((MemoryStream)
                                      // emailReportDocument.ExportToStream(ExportFormatType.PortableDocFormat)).ToArray(),
                                      ((MemoryStream)
                                       voucherInfoReportDocument.ExportToStream(ExportFormatType.PortableDocFormat)).ToArray(),
                                      ((MemoryStream)
                                       conditionsReportDocument.ExportToStream(ExportFormatType.PortableDocFormat)).ToArray(),
                                      ((MemoryStream)
                                       voucherAdditionalInfoReportDocument.ExportToStream(ExportFormatType.PortableDocFormat)).ToArray()
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

        private byte[] GenerateReportRimac(VoucherInformationReportRimacObject voucher, List<PasajerosObject> pasajeros, List<ConditionsReportObject> conditions)
        {
            try{
            var voucherObj = new List<VoucherInformationReportRimacObject> { voucher };

            var ds = new DataSet();
            ds.Tables.Add(ObjectToTable.ConvertToDataTable(conditions.ToArray()));
            ds.Tables.Add(ObjectToTable.ConvertToDataTable(voucherObj.ToArray()));
            ds.Tables.Add(ObjectToTable.ConvertToDataTable(pasajeros.ToArray()));



            ds.Tables[0].TableName = "CondicionesTabla";
            ds.Tables[1].TableName = "InformacionTabla";
            ds.Tables[2].TableName = "PasajerosTabla";


            voucherInfoReportDocument = new ReportDocument();

            string voucherInfoPath = ConfigurationValueHome.GetReportPath() + "RimacVoucherInformation.rpt";

            voucherInfoReportDocument.Load(voucherInfoPath);
     
            voucherInfoReportDocument.SetDataSource(ds);        
            
           crvVoucherInfoReport = new CrystalReportPartsViewer { ReportSource = voucherInfoReportDocument, Visible = false };
            

            byte[] report = ((MemoryStream)voucherInfoReportDocument.ExportToStream(ExportFormatType.PortableDocFormat)).ToArray();

            FreeReports();

            return report;
            }
            catch (Exception)
            {
                FreeReports();
                return new byte[0];
            }
        }
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

        private VoucherInformationReportRimacObject GetVoucherInformationReportRimacObject(EMailEkitDTO dto, IDictionary<string, string> translations)
        {
            
            var voucher = new VoucherInformationReportRimacObject(translations[ReportLanguage.REPORT],
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
                                                            translations[ReportLanguage.EMERGENCYADDRESS],
                                                            translations[ReportLanguage.SECURETYPETITLE],
                                                            translations[ReportLanguage.PARTICULARCONDITIONTITLE],
                                                            translations[ReportLanguage.COMPANYPOLIZADATATITLE],
                                                            translations[ReportLanguage.POLIZATITLE],
                                                            translations[ReportLanguage.COMPANYDATATITLE],
                                                            translations[ReportLanguage.RUCTITLE],
                                                            translations[ReportLanguage.AGENCYADRESSTITLE],
                                                            translations[ReportLanguage.AGENCYPROVINCETITLE],
                                                            translations[ReportLanguage.AGENCYPHONETITLE],
                                                            translations[ReportLanguage.AGENCYDISTRICTTITLE],
                                                            translations[ReportLanguage.AGENCYDEPARTAMENTTITLE],
                                                            translations[ReportLanguage.AGENCYFAXTITLE],
                                                            translations[ReportLanguage.PAXTYPETITLE],
                                                            translations[ReportLanguage.PAXDOCUMENTNUMBERTITLE],
                                                            translations[ReportLanguage.PAXCITYTITLE],
                                                            translations[ReportLanguage.PAXBIRTHDATETITLE],
                                                            translations[ReportLanguage.PAXEMAILTITLE],
                                                            translations[ReportLanguage.DEPENDENTTITLE],
                                                            translations[ReportLanguage.DEPENDENTNAMETITLE],
                                                            translations[ReportLanguage.DEPENDENTBIRTHDAYTITLE],
                                                            translations[ReportLanguage.BENEFICIARIESTITLE],
                                                            translations[ReportLanguage.INSUREDTITLE],
                                                            translations[ReportLanguage.DEPENDENT],
                                                            translations[ReportLanguage.BENEFICIARIES],
                                                            translations[ReportLanguage.CONSIDERATIONSTITLE],
                                                            translations[ReportLanguage.CONSIDERATIONS],
                                                            translations[ReportLanguage.PAYMENTPLANTITLE],
                                                            translations[ReportLanguage.PRIMANETATITLE],
                                                            translations[ReportLanguage.ALLOWANCETITLE],
                                                            translations[ReportLanguage.IGVTITLE],
                                                            translations[ReportLanguage.PRIMATOTALTITLE],
                                                            translations[ReportLanguage.PAYMENTPLANNOTE],
                                                            translations[ReportLanguage.FEATURESOFPRODUCTTITLE],
                                                            translations[ReportLanguage.MINAGE],
                                                            translations[ReportLanguage.MAXAGE],
                                                            translations[ReportLanguage.OTHERS],
                                                            translations[ReportLanguage.PROCEDURETITLE],
                                                            translations[ReportLanguage.ESPECIALCONDITIONSTITLE],
                                                            translations[ReportLanguage.ESPECIALCONDITIONS],
                                                            translations[ReportLanguage.IMPORTANTTITLE],
                                                            translations[ReportLanguage.IMPORTANT],
                                                            translations[ReportLanguage.PRINTDATETITLE],
                                                            translations[ReportLanguage.PROCEDUREONE],
                                                            translations[ReportLanguage.PROCEDURETWO],
                                                            translations[ReportLanguage.PROCEDURETHREE],
                                                            translations[ReportLanguage.PROCEDUREFOUR],
                                                            translations[ReportLanguage.PROCEDUREFIVE],
                                                            translations[ReportLanguage.PROCEDURESIX],
                                                            translations[ReportLanguage.PROCEDURESEVEN],
                                                            translations[ReportLanguage.PROCEDUREEIGTH]
                                                            )
            {
                //Datos dinamicos del voucher
                CardAgency = dto.AgencyCode,
                CardAmount = dto.Amount,
                CardArea = AreaHome.BuscarPorIdArea(Convert.ToInt32(dto.Area)).Nombre,
                CardDays = dto.Days,
                CardEmissionDate = Convert.ToDateTime(dto.IssuanceDate),
                CardNumber =dto.VoucherCode,
                CardType = dto.CardType,
                CardValidFrom = Convert.ToDateTime(dto.EffectiveStartDate),
                CardValidTo = Convert.ToDateTime(dto.EffectiveEndDate), 
                EmergencyAddress = dto.EmergencyAddress,
                EmergencyContact = dto.EmergencyContact,
                EmergencyPhone = dto.EmergencyPhone,
                Footer = dto.FooterPDF != null && dto.FooterPDF.Length > 0 ? dto.FooterPDF : dto.Footer,
                Header = dto.HeaderPDF != null && dto.HeaderPDF.Length > 0 ? dto.HeaderPDF : dto.Header,
                LastName = dto.RecipientSurname + " " + dto.RecipientName,
                Name = "",
                PersonalInfoAddress = dto.PaxAddress,
                PersonalInfoAge = dto.PaxAge,
                PersonalInfoCountry = PaisHome.ObtenerPorCodigo(dto.PaxCountry.ToString(CultureInfo.InvariantCulture)).Nombre,
                PersonalInfoPassport = dto.PaxPassport,
                PersonalInfoPhone = dto.PaxPhone,
                RUC = dto.Ruc,
                AgencyAddress = dto.Sucursal.Domicilio,
                AgencyProvince = dto.Sucursal.Provincia,
                AgencyDistrict = dto.Sucursal.Distrito,
                AgencyDepartament = dto.Sucursal.Departamento,
                AgencyPhone = dto.Sucursal.Telefono,
                AgencyFax = dto.Sucursal.Fax,
                PrimaNeta = dto.PrimaNeta,
                DerechoDeEmision = dto.DerechoDeEmision,
                Igv = dto.Igv,
                PrimaTotal = dto.PrimaTotal,
                PaxType = "Persona Natural",
                PaxDocumentNumber = dto.PaxDocumentNumber,
                PaxCity = dto.PaxCity,
                PaxBirthDate = dto.BirthDate,
                PaxEmail = dto.To,
                Poliza = dto.Poliza,
                ProductName = dto.ProductName
            };
           
            
            return voucher;
        }

        private IEnumerable<ConditionsReportObject> GetConditionsReportObject(EMailEkitDTO dto, IDictionary<string, string> translations)
        {
            var result = new List<ConditionsReportObject>();

            foreach (ContenidoClausulaDTO contenidoClausulaDto in dto.GrupoClausulaDTO.Clausulas)
            {
                if (contenidoClausulaDto.ShowClause())
                {
                    result.Add(new ConditionsReportObject(contenidoClausulaDto.CodigoTipoContenidoImpresion,
                                    contenidoClausulaDto.GetIdClause(dto.IdLanguage),
                                    contenidoClausulaDto.GetTitleClause(dto.IdLanguage),
                                    contenidoClausulaDto.GetContentClause(dto.IdLanguage),
                                    translations[ReportLanguage.CONDITIONSREPORT], dto.HeaderPDF, dto.FooterPDF,
                                    contenidoClausulaDto.EsClausulaDeSeguro()));
                }
            }

            return result;
        }

        private List<ConditionsReportObject> GetConditionsReport(EMailEkitDTO dto, IDictionary<string, string> translations)
        {
            var result = new List<ConditionsReportObject>();

            foreach (ContenidoClausulaDTO contenidoClausulaDto in dto.GrupoClausulaDTO.Clausulas)
            {
                if (contenidoClausulaDto.ShowClause())
                {
                    result.Add(new ConditionsReportObject(contenidoClausulaDto.CodigoTipoContenidoImpresion,
                                    contenidoClausulaDto.GetIdClause(dto.IdLanguage),
                                    contenidoClausulaDto.GetTitleClause(dto.IdLanguage),
                                    contenidoClausulaDto.GetContentClause(dto.IdLanguage),
                                    translations[ReportLanguage.CONDITIONSREPORT], dto.HeaderPDF, dto.FooterPDF,
                                    contenidoClausulaDto.EsClausulaDeSeguro()));
                }
            }

            return result;
        }

        private List<PasajerosObject> GetPasajerosObject(EMailEkitDTO dto) 
        {
            var result = new List<PasajerosObject>();

            foreach (ClienteInfoDTO clienteInfo in dto.clientes)
            {
                PasajerosObject pasajero = new PasajerosObject();
                pasajero.Apellido = clienteInfo.apellido;
                pasajero.Nombre = clienteInfo.nombre;
                pasajero.FecNacimiento = DateTime.Parse(clienteInfo.fecNacimiento).ToShortDateString();

                result.Add(pasajero);            
            }

           return result;
       
        }
        #endregion
    }
}