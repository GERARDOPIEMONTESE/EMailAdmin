using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.DTO;
using CapaNegocioDatos.CapaHome;
using CrystalDecisions.CrystalReports.Engine;
using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.Reports.Objects;
using CrystalDecisions.Web;
using System.IO;
using CrystalDecisions.Shared;
using iTextSharp.text.pdf;
using iTextSharp.text;
using EMailAdmin.BackEnd.DTO.EkitBenefits;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Strategies.Attachment
{
    public class MainBenefitsUSAAttachStrategy :AttachStrategy, IAttachStrategy
    {
        #region Constants

        private const string ATTACH_NAME = "ASSISTCARD-Info.pdf";

        private const string ATTACH_TYPE = "application/pdf";

        #endregion Constants

        public MainBenefitsUSAAttachStrategy()
        {
            ATTACHNAME = ATTACH_NAME;
            ATTACHTYPE = ATTACH_TYPE;
        }


        #region Properties

        public ReportDocument emailReportDocument;
        public CrystalDecisions.Web.CrystalReportPartsViewer crvEmailReport;

        public ReportDocument voucherInfoReportDocument;
        public CrystalDecisions.Web.CrystalReportPartsViewer crvVoucherInfoReport;

        #endregion

        #region Methods

        private string GetPolicyCost(string billRate, string fee)
        {
            double billRateFloat = 0;
            double feeFloat = 0;

            double.TryParse(billRate.Replace(".", ","), out billRateFloat);
            double.TryParse(fee.Replace(".", ","), out feeFloat);

            return (billRateFloat - feeFloat).ToString();
        }

        private string GetBenefitsFormat(IList<string> mainBenefits)
        {
            string benefits = "";

            foreach (string benefit in mainBenefits)
            {
                benefits += "<p>" + benefit + "</p>";
            }

            return benefits;
        }

        private string GetMainBenefits(EMailEkitDTO ekit)
        {
            if (ekit.GrupoClausulaDTO != null && ekit.GrupoClausulaDTO.Clausulas != null)
            {
                IList<string> services = new List<string>();
                IList<string> insurance = new List<string>();
                List<string> mainBenefits = new List<string>();

                foreach (ContenidoClausulaDTO contenido in ekit.GrupoClausulaDTO.Clausulas)
                {
                    if (contenido.CodigoTipoImpresionClausula == "IC" || 
                            contenido.CodigoTipoImpresionClausula == "SEM")
                    {
                        string text = contenido.GetContentClause(ekit.IdLanguage);

                        if (text != null)
                        {
                            if (contenido.Clausula.CodigoTipoClausula == "SERV")
                            {
                                services.Add(text.ToUpper().Trim());
                            }
                            if (contenido.Clausula.CodigoTipoClausula == "SEGU")
                            {
                                insurance.Add(text.ToUpper().Trim());
                            }
                        }
                    }
                }

                mainBenefits.AddRange(services);
                mainBenefits.AddRange(insurance);

                return GetBenefitsFormat(mainBenefits);
            }

            return "";
        }

        private VoucherTagReportObject GetVoucherTagReportObject(AbstractEMailDTO dto)
        {
            EMailEkitDTO ekitDTO = (EMailEkitDTO)dto;
            
            VoucherTagReportObject report = new VoucherTagReportObject();

            report.CompleteVoucherCode = ekitDTO.CompleteVoucherCode;
            report.FullPaxName = ekitDTO.RecipientFullName;
            report.EffectiveEndDate = Utils.DateUtil.FormatToShortDate(Convert.ToDateTime(ekitDTO.EffectiveEndDateShortFormat), dto.IdLanguage);
            report.ProductName = ekitDTO.ProductName;
            report.Modality = ekitDTO.RateModality;

            return report;
        }

        private USAReportObject GetUSAReportObject(AbstractEMailDTO dto)
        {
            var ekit = (EMailEkitDTO) dto;

            var report = new USAReportObject
            {
                Header = File.ReadAllBytes(ConfigurationValueHome.GetReportPath() + "ACLogo.png"),
                Plan = ekit.ProductName,
                Policy = ekit.CompleteVoucherCode,
                Underwritter = ekit.Underwritter,
                Tripcancellation = Utils.DateUtil.FormatToShortDate(Convert.ToDateTime(ekit.IssuanceDateShortFormat), dto.IdLanguage),
                EfectiveDate = Utils.DateUtil.FormatToShortDate(Convert.ToDateTime(ekit.EffectiveStartDateShortFormat), dto.IdLanguage),
                FullName = ekit.RecipientFullName,
                Phone = ekit.PaxPhone,
                Email = ekit.To,
                TripCost = ekit.TripCost,
                TripBegins = Utils.DateUtil.FormatToShortDate(Convert.ToDateTime(ekit.EffectiveStartDateShortFormat), dto.IdLanguage),
                TripEnds = Utils.DateUtil.FormatToShortDate(Convert.ToDateTime(ekit.EffectiveEndDateShortFormat), dto.IdLanguage),
                Days = ekit.Days,
                PolicyCost = GetPolicyCost(ekit.BillRate, ekit.Fee),
                AdminFee = ekit.Fee,
                Total = ekit.Amount,
                EmergencyFullName = ekit.EmergencyContact,
                EmergencyPhone = ekit.EmergencyPhone,
                BenefitsTable = GetMainBenefits(ekit)
            };

            return report;
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
                voucherInfoReportDocument = null;
            }
            try
            {
                GC.Collect();
            }
            catch (Exception) { }

        }

        private byte[] GetAttachContent(AbstractEMailDTO dto)
        {
            try
            {
                string reportPath = ConfigurationValueHome.GetReportPath();

                voucherInfoReportDocument = new ReportDocument();

                string voucherInfoPath = reportPath + "USAReport.rpt";
                voucherInfoReportDocument.Load(voucherInfoPath);
                voucherInfoReportDocument.SetDataSource(new List<USAReportObject> { GetUSAReportObject(dto) });

                crvVoucherInfoReport = new CrystalReportPartsViewer { ReportSource = voucherInfoReportDocument, Visible = false };

                emailReportDocument = new ReportDocument();
                string emailPath = reportPath + "VoucherTag.rpt";
                emailReportDocument.Load(emailPath);
                emailReportDocument.SetDataSource(new List<VoucherTagReportObject> { GetVoucherTagReportObject(dto) });

                crvEmailReport = new CrystalReportPartsViewer { ReportSource = emailReportDocument, Visible = false };

                //var emailReportDocument = new ReportDocument();
                //string emailPath = Settings.Default.ReportPath + "Voucher.rpt";
                //emailReportDocument.Load(emailPath);
                //emailReportDocument.SetDataSource(new List<VoucherReportObject> { GetVoucherReportObject(dto) });
                //var crvEmailReport = new CrystalReportPartsViewer { ReportSource = emailReportDocument, Visible = false };

                var sourceFiles = new List<byte[]>
                                  {
                                    ((MemoryStream)
                                        voucherInfoReportDocument.ExportToStream(ExportFormatType.PortableDocFormat)).ToArray(),
                                    ((MemoryStream)
                                        emailReportDocument.ExportToStream(ExportFormatType.PortableDocFormat)).ToArray()
                                  };

                byte[] report = MergeFiles(sourceFiles);

                //VER GC - POR CONCURRENCIA
                //GC.Collect();

                /* PROPUESTA MATI --- VER QUE SEAN ATTR PUBLICOS, OJO SINGLETON            
                            if (crvEmailReport != null)
                            {
                                crvEmailReport.Dispose();
                                crvEmailReport = null;
                            }

                            if (emailReportDocument != null)
                            {
                                emailReportDocument.Close();
                                emailReportDocument.Dispose();
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
                            GC.Collect();
                */

                /*
                 * CON ESTE CODIGO FALLA - ORIGINAL
                            emailReportDocument.Dispose();
                            crvEmailReport.Dispose();

                            voucherInfoReportDocument.Dispose();
                            crvVoucherInfoReport.Dispose();
                */
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

        #region IAttachStrategy Members

        public IList<Domain.AttachmentItem> GetAttachmentItems(DTO.AbstractEMailDTO dto)
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

        public byte[] FileToByteArray(string fileName)
	    {
	        byte[] buffer = null;
	 
	        try
	        {
	            // Open file for reading
	            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
	         
	            // attach filestream to binary reader
	            var binaryReader = new BinaryReader(fileStream);
	         
	            // get total byte length of the file
	            long totalBytes = new FileInfo(fileName).Length;
	         
	            // read entire file into buffer
	            buffer = binaryReader.ReadBytes((Int32)totalBytes);
	         
	            // close file reader
	            fileStream.Close();
	            fileStream.Dispose();
	            binaryReader.Close();
	        }
	        catch (Exception exception)
	        {
	            // Error
	            Console.WriteLine("Exception caught in process: {0}", exception.ToString());
	        }
	 
	        return buffer;
	    }
    }
}
