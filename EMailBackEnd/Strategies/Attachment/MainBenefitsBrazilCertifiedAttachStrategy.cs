using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.DTO.EkitBenefits;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.Reports.Objects;
using EMailAdmin.BackEnd.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Strategies.Attachment
{
    public class MainBenefitsBrazilCertifiedAttachStrategy : AttachStrategy, IAttachStrategy
    {
        #region Constants

        private const string ATTACH_NAME = "ASSISTCARD-Info.pdf";

        private const string ATTACH_TYPE = "application/pdf";

        #endregion Constants

        public MainBenefitsBrazilCertifiedAttachStrategy()
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

        private byte[] GetAttachContent(AbstractEMailDTO dto)
        {
            try
            {
                var translations = DAOLocator.Instance().GetDaoReportLanguage().FindByLanguage(dto.IdLanguage, dto.IdStrategy);
                var ekitDto = (EMailEkitDTO)dto;
                var conditions = GetConditionsReportObject(ekitDto, translations);
                var reportObj = new List<BrazilVoucherInformationObject> { GetBrazilReportObject(dto) };

                var ds = new DataSet();
                ds.Tables.Add(ObjectToTable.ConvertToDataTable(conditions.ToArray()));
                ds.Tables.Add(ObjectToTable.ConvertToDataTable(reportObj.ToArray()));
                
                ds.Tables[1].TableName = "BrazilVoucherInformationObject";
                ds.Tables[0].TableName = "ConditionsObject";

                voucherInfoReportDocument = new ReportDocument();

                string voucherInfoPath = ConfigurationValueHome.GetReportPath() + "BrazilCertifiedAttachReport.rpt";
                //voucherInfoPath = "D:\\appNet\\Emisiones\\EMailAdmin\\EMailAdmin\\Reports\\BrazilCertifiedAttachReport.rpt";
                                    
                voucherInfoReportDocument.Load(voucherInfoPath);
                //voucherInfoReportDocument.SetDataSource(new List<Object> { reportObj, conditions });
                voucherInfoReportDocument.SetDataSource(ds);

                crvVoucherInfoReport = new CrystalReportPartsViewer { ReportSource = voucherInfoReportDocument, Visible = false };
                
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

        private BrazilVoucherInformationObject GetBrazilReportObject(AbstractEMailDTO dto)
        {
            var ekit = (EMailEkitDTO)dto;
            Cuenta agency = CuentaHome.ObtenerPorCodigoYPais(ekit.AgencyCode, ekit.CountryCode.ToString());
            Sucursal branch = SucursalHome.Obtener(ekit.CountryCode.ToString(), ekit.AgencyCode, ekit.BranchNumber);
            
            string agPhone = string.Empty;
            string agContact = string.Empty;
            try
            {
                agPhone = agency.Contactos != null && agency.Contactos.Any() ?
                    agency.Contactos.FirstOrDefault().Telefono : branch.PersonaJuridica != null && branch.PersonaJuridica.Contactos != null && branch.PersonaJuridica.Contactos.Any() ?
                            branch.PersonaJuridica.Contactos.FirstOrDefault().Telefono : string.Empty;

                agContact = agency.Contactos != null && agency.Contactos.Any() ?
                    agency.Contactos.FirstOrDefault().NombreCompleto : branch.PersonaJuridica != null && branch.PersonaJuridica.Contactos != null && branch.PersonaJuridica.Contactos.Any() ?
                            branch.PersonaJuridica.Contactos.FirstOrDefault().NombreCompleto : branch.PersonaJuridica.Denominacion;
            }
            catch { }

            DateTime issuanceDate;
            if (!DateTime.TryParse(ekit.IssuanceDate, CultureInfo.CreateSpecificCulture("es-AR"), DateTimeStyles.None, out issuanceDate))
                issuanceDate = DateTime.Now;


            var report = new BrazilVoucherInformationObject
            {
                //Plan = ekit.ProductName,
                PolicyCode = ekit.CodigoPoliza, //ekit.PolicyCode,
                Certificado = (agency.EsAssistCard ? "" : "Certificado " + ekit.CompleteVoucherCode),
                CardNumber = ekit.CompleteVoucherCode,
                CardValidFrom = DateUtil.FormatToShortDate(Convert.ToDateTime(ekit.EffectiveStartDate), dto.IdLanguage),
                CardValidTo = DateUtil.FormatToShortDate(Convert.ToDateTime(ekit.EffectiveEndDate), dto.IdLanguage),
                PersonalInfoAddress = ekit.PaxAddress,
                PersonalInfoCity = ekit.PaxCity,
                PersonalInfoState = ekit.PaxState,
                PersonalInfoPostalCode = ekit.PaxPostalCode,
                PersonalInfoContact = ekit.EmergencyContact,
                // PersonalInfoNeighbor,
                PersonalInfoDocument = ekit.PaxPassport,
                PersonalInfoFullName = ekit.RecipientFullName,
                PersonalInfoPhone = ekit.PaxPhone,
                PersonalInfoEmail = ekit.To,
                BenefitsTable = string.Empty, //GetConditionsHTML(dto), //GetConditionsReportObject(ekit, translations), //GetMainBenefits(ekit),
                IssuanceDate = issuanceDate.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("pt-BR")),
                IssuanceDateTime = issuanceDate.ToString("dd/MM/yyyy - HH:mm", CultureInfo.CreateSpecificCulture("pt-BR")),
                NetPrice = ekit.NetPrice,
                Iof = ekit.Iof,
                AditionalFractionation = ekit.AditionalFractionation,
                PolicyPrice = ekit.PolicyPrice,
                TotalPrice = ekit.TotalPrice,
                AgencyType = agency.TipoCuenta.Codigo
            };

            var sucCorretor = branch.SucursalesBroker;
            if (sucCorretor != null && sucCorretor.Count > 0)
            {
                var pjCorretor = sucCorretor[0].Sucursal.PersonaJuridica;
                report.CorretorName = sucCorretor[0].Sucursal.RazonSocial;
                report.CorretorDocument = sucCorretor[0].Sucursal.Cuit;
                report.CorretorCode = sucCorretor[0].Sucursal.CodigoSUSEP;
                string dir = string.Format("{0}{1}", pjCorretor.DireccionFiscal.Domicilio, ( pjCorretor.DireccionFiscal.Localidad!=""? ","+pjCorretor.DireccionFiscal.Localidad: ""));
                report.CorretorAddress = dir;
            }
            else
            {
                report.CorretorName = CodigoActivadorHome.Obtener("CORRETOR_DEFAULT").Valor;// "Apolix Corretora de Seguros Ltda.";
                report.CorretorDocument = CodigoActivadorHome.Obtener("CORRETOR_DOC").Valor; //"10.272.812/0001-40";
                report.CorretorCode = CodigoActivadorHome.Obtener("CORRETOR_SUSEP").Valor; //"10.061183-2";
                report.CorretorAddress = CodigoActivadorHome.Obtener("CORRETOR_DIR").Valor; //"Av. Professor Alfonso Bovero 1130, SP, SP";
            }

            Direccion branchDir = branch.PersonaJuridica.DireccionFiscal;
            report.CorretorType = "Agência";
            report.AgencyCity = branchDir != null ? branchDir.Localidad : string.Empty;
            report.AgencyState = branchDir != null ? branchDir.Provincia : string.Empty;
            report.AgencyPostalCode = branchDir != null ? branchDir.CodigoPostal : string.Empty;
            report.AgencyAddress = branchDir != null ? branchDir.Domicilio : string.Empty;
            report.AgencyDocument = agency.Cuit;
            report.AgencyEmail = agency.Email;
            report.AgencyName = agency.RazonSocial;
            report.AgencyCode = branch.CodigoSUSEP ?? string.Empty;
            report.AgencyContact = agContact;
            report.AgencyPhone = agPhone;
            report.AgencyUnidadFederativa = branchDir != null ? branchDir.UnidadFederativa : string.Empty;
            
            report.upgrade = (ekit.Upgrades != null && ekit.Upgrades.Count() > 0);

            return report;
        }

        private string GetConditionsHTML(AbstractEMailDTO dto)
        {
            var ekit = (EMailEkitDTO)dto;
            var translations = DAOLocator.Instance().GetDaoReportLanguage().FindByLanguage(dto.IdLanguage, dto.IdStrategy);
            
            return GetBenefitsFormat(GetConditionsReportObject(ekit, translations));
        }

        private string GetBenefitsFormat(IEnumerable<ConditionsObject> mainBenefits)
        {
            string benefits = "";

            foreach (var benefit in mainBenefits)
            {
                benefits += "<p><span align='left'>" + benefit.Code + " " + benefit.Text + " </span>" + "<span align='right'>" + benefit.Leyend + " </span></p>";
            }

            return benefits;
        }

        private IEnumerable<ConditionsObject> GetConditionsReportObject(EMailEkitDTO dto, IDictionary<string, string> translations)
        {
            var result = new List<ConditionsObject>();

            var excepciones = Clausula_R_EstrategyHome.FindByEstrategy(dto.CountryCode, dto.IdStrategy);

            foreach (ContenidoClausulaDTO contenidoClausulaDto in dto.GrupoClausulaDTO.Clausulas)
            {
                Clausula_R_Estrategy clausulaExcepcion = SetExceptions_ConditionsReportObject(contenidoClausulaDto, excepciones);

                if ((clausulaExcepcion!=null && !clausulaExcepcion.Excluye )
                    ||(clausulaExcepcion==null && contenidoClausulaDto.ShowClause() && contenidoClausulaDto.EsClausulaDeSeguro()))
                {
                    var condition = new ConditionsObject
                    {
                        Code = contenidoClausulaDto.GetIdClause(dto.IdLanguage),
                        Text = contenidoClausulaDto.GetTitleClause(dto.IdLanguage),
                        Leyend = contenidoClausulaDto.GetContentClause(dto.IdLanguage)
                    };

                    if (string.IsNullOrEmpty(condition.Text) || condition.Text == "*")
                    {
                        condition.Text = condition.Leyend;
                        condition.Leyend = string.Empty;
                    }

                    result.Add(condition);
                }
            }

            return result;
        }

        private Clausula_R_Estrategy SetExceptions_ConditionsReportObject(ContenidoClausulaDTO contenidoClausulaDto, IList<Clausula_R_Estrategy> excepciones)
        {
           return excepciones.FirstOrDefault(x => x.ClausulaCode == contenidoClausulaDto.Clausula.Codigo);
        }

        private List<ConditionsObject> SetExceptions_ConditionsReportObject(EMailEkitDTO dto, List<ConditionsObject> result)
        {
            var excepciones = Clausula_R_EstrategyHome.FindByEstrategy(dto.CountryCode, dto.IdStrategy);

            foreach (var clausula in excepciones)
            {
                if (clausula.Excluye)
                {
                    var itemExcluido = result.Find(x => x.Code == clausula.ClausulaCode);
                    result.Remove(itemExcluido);
                }
                else
                {
                    var itemAgregado = dto.GrupoClausulaDTO.Clausulas.First(x => x.Clausula.Codigo == clausula.ClausulaCode);
                    if (itemAgregado != null)
                    {
                        var condition = new ConditionsObject
                        {
                            Code = itemAgregado.GetIdClause(dto.IdLanguage),
                            Text = itemAgregado.GetTitleClause(dto.IdLanguage),
                            Leyend = itemAgregado.GetContentClause(dto.IdLanguage)
                        };

                        if (string.IsNullOrEmpty(condition.Text) || condition.Text == "*")
                        {
                            condition.Text = condition.Leyend;
                            condition.Leyend = string.Empty;
                        }
                        result.Add(condition);
                    }
                }
            }

            return result;
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

            try
            {
                GC.Collect();
            }
            catch (Exception) { }
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
    }
}
