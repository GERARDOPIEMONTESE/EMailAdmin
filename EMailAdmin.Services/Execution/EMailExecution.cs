using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;


namespace EMailAdmin.Services.Execution
{
    public class EMailExecution
    {
        public int CountryCode { get; set; }

        public string VoucherCode { get; set; }

        public string ModuleCode { get; set; }

        public void ExecuteSendMail()
        {
            EMailEkitDTO dto = new EMailEkitDTO();
            dto.CountryCode = CountryCode;
            dto.VoucherCode = VoucherCode == null || VoucherCode.Length == 0 ? "0" : VoucherCode.Trim();
            dto.ModuleCode = ModuleCode == null || ModuleCode.Length == 0 ? "-" : ModuleCode.Trim();

            ServiceLocator.Instance().GetSendMailService().SendMailEkit(dto);
        }
    }

    public class EMailMultipleExecution
    {
        public int CountryCode { get; set; }

        public string[] VouchersCode { get; set; }

        public string ModuleCode { get; set; }

        public string Emails { get; set; }

        public void ExecuteSendMail()
        {
            if (VouchersCode != null)
            {
                foreach (string voucherCode in VouchersCode)
                {
                    EMailEkitDTO dto = new EMailEkitDTO();
                    dto.CountryCode = CountryCode;
                    dto.VoucherCode = voucherCode == null || voucherCode.Length == 0 ? "0" : voucherCode.Trim();
                    dto.ModuleCode = ModuleCode == null || ModuleCode.Length == 0 ? "-" : ModuleCode.Trim();
                    dto.To = Emails == null || Emails.Length == 0 ? "" : Emails.Trim();

                    ServiceLocator.Instance().GetSendMailService().SendMailEkit(dto);
                }
            }
        }

    }

    public class EMailPolizaExecution
    {
        public bool Cancelacion { get; set; }

        public int CountryCode { get; set; }

        public string VoucherCode { get; set; }

        public string PolizaId { get; set; }

        public string PolizaVoidId { get; set; }

        public string ModuleCode { get; set; }

        public void ExecuteSendMail()
        {
            EmailPolizaDTO dto = new EmailPolizaDTO();
            dto.Cancelacion = Cancelacion;
            dto.CountryCode = CountryCode;
            dto.VoucherCode = VoucherCode == null || VoucherCode.Length == 0 ? "0" : VoucherCode.Trim();
            dto.ModuleCode = ModuleCode == null || ModuleCode.Length == 0 ? "-" : ModuleCode.Trim();
            dto.PolizaId = PolizaId == null || PolizaId.Length == 0 ? "0" : PolizaId.Trim();
            dto.polizaVoidId = PolizaVoidId == null || PolizaVoidId.Length == 0 ? "0" : PolizaVoidId.Trim();

            ServiceLocator.Instance().GetSendMailService().SendMailPoliza(dto);
        }
    }

    public class MailBalanceRequest
    {
        public string agencia { get; set; }
        public string sucursal { get; set; }
        public string mailComercial { get; set; }
        public int pais { get; set; }
        public int idioma { get; set; }

        public void ExecuteSendMail()
        {
            BalanceRequestDTO dto = new BalanceRequestDTO();
            dto.agencia = agencia;
            dto.sucursal = sucursal;
            dto.pais = pais;

            dto.IdLanguage = idioma;
            dto.To = mailComercial;
            dto.CountryCode = pais;
            dto.ModuleCode = "ACNET";
            ServiceLocator.Instance().GetSendMailService().SendMailBalanceRequest(dto);
        }
    }

    public class MailCotizacion
    {
        public List<Producto> Productos { get; set; }
        public string module { get; set; }
        public int idioma { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Country { get; set; }
        public bool ApplyPolicy { get; set; }
        public string DollarQuote { get; set; }
        public string HeaderEn { get; set; }
        public string HeaderEs { get; set; }
        public string HeaderPr { get; set; }
        public string FooterEn { get; set; }
        public string FooterEs { get; set; }
        public string FooterPr { get; set; }
        public String CountryName { get; set; }
        public int Days { get; set; }
        public string PaxName { get; set; }
        public string SellerData { get; set; }
        public string AdditionalData { get; set; }
        public String Destination { get; set; }
        public String PassengersEn { get; set; }
        public String PassengersEs { get; set; }
        public String PassengersPr { get; set; }
        public String ServicioAsistenciaEn { get; set; }
        public String ServicioAsistenciaEs { get; set; }
        public String ServicioAsistenciaPr { get; set; }
        public String ServicioSeguroEn { get; set; }
        public String ServicioSeguroEs { get; set; }
        public String ServicioSeguroPr { get; set; }
        public String From { get; set; }

        public String ServicioSeguro
        {
            get
            {
                switch (idioma)
                {
                    case 1:
                        return ServicioSeguroEs;
                    case 2:
                        return ServicioSeguroEn;
                    default:
                        return ServicioSeguroPr;
                }
            }
        }
        public String ServicioAsistencia
        {
            get
            {
                switch (idioma)
                {
                    case 1:
                        return ServicioAsistenciaEs;
                    case 2:
                        return ServicioAsistenciaEn;
                    default:
                        return ServicioAsistenciaPr;
                }
            }
        }
        public void ExecuteSendMail()
        {
            if (Productos != null)
            {
                EmailQuoteDTO dto = new EmailQuoteDTO();
                List<ProductQuoteDTO> productos = new List<ProductQuoteDTO>();
                Dictionary<string, string> keyClauses = new Dictionary<string, string>();
                Dictionary<string, string> keyClausesSegu = new Dictionary<string, string>();

                if (Country == 550)
                {

                }
                foreach (Producto producto in Productos)
                {
                    ProductQuoteDTO prodDTO = new ProductQuoteDTO();
                    prodDTO.ProductName = producto.ProductName;
                    prodDTO.Total = producto.Total;
                    prodDTO.Insurance = producto.Insurance;
                    prodDTO.Assistance = producto.Assistance;
                    prodDTO.AveragePerPerson = producto.AveragePerPerson;
                    dto.To = producto.To;

                    if (producto.Clauses != null)
                    {
                        List<ClausesDTO> clausesDTO = new List<ClausesDTO>();
                        Dictionary<string, string> dictionary = new Dictionary<string, string>();

                        foreach (Clausula clausula in producto.Clauses)
                        {
                            ClausesDTO clauses = new ClausesDTO();
                            clauses.Country = clausula.pais;
                            clauses.Product = clausula.producto;
                            clauses.Rate = clausula.tarifa;
                            clauses.Days = clausula.cantDias;
                            clauses.ClauseId = clausula.idClausula;
                            clauses.Leyend = clausula.leyenda;
                            clauses.Position = clausula.posicion;
                            clauses.Title = clausula.titulo;
                            clauses.CodigoTipoClausula = clausula.codigoTipoClausula;
                            if (object.Equals(clausula.codigoTipoClausula, "SEGU")
                                && !keyClausesSegu.ContainsKey(clausula.idClausula))
                            {
                                keyClausesSegu.Add(clausula.idClausula, clausula.titulo);
                            }
                            else if (!keyClauses.ContainsKey(clausula.idClausula))
                            {
                                keyClauses.Add(clausula.idClausula, clausula.titulo);
                            }
                            if (dictionary.ContainsKey(clausula.idClausula))
                            {
                                while (dictionary.ContainsKey(clausula.idClausula))
                                {
                                    clausula.idClausula = clausula.idClausula + " ";
                                }
                                dictionary.Add(clausula.idClausula, clausula.leyenda);
                            }
                            else
                            {
                                dictionary.Add(clausula.idClausula, clausula.leyenda);
                                //clausesDTO.Add(clauses); 
                            }
                        }
                        //prodDTO.Clauses = clausesDTO;
                        prodDTO.ClausesMap = dictionary;
                    }
                    productos.Add(prodDTO);
                }
                dto.Products = productos;
                dto.ModuleCode = module;
                dto.IdLanguage = idioma;
                dto.EffectiveStartDate = StartDate;
                dto.EffectiveEndDate = EndDate;
                dto.Country = Country;
                dto.CountryCode = Country;
                dto.ApplyPolicy = ApplyPolicy;
                dto.DollarQuote = DollarQuote;
                dto.HeaderEnQuoteAcnet = HeaderEn;
                dto.HeaderEsQuoteAcnet = HeaderEs;
                dto.HeaderPrQuoteAcnet = HeaderPr;
                dto.FooterEnQuoteAcnet = FooterEn;
                dto.FooterEsQuoteAcnet = FooterEs;
                dto.FooterPrQuoteAcnet = FooterPr;
                dto.CountryName = CountryName;
                dto.Days = Days;


                dto.PaxName = string.IsNullOrWhiteSpace(PaxName) ? string.Empty : PaxName;
                dto.SellerData = string.IsNullOrWhiteSpace(SellerData) ? string.Empty : SellerData;
                dto.AdditionalData = string.IsNullOrWhiteSpace(AdditionalData) ? string.Empty : AdditionalData;



                dto.Destination = Destination;
                dto.PassengersEn = PassengersEn;
                dto.PassengersEs = PassengersEs;
                dto.PassengersPr = PassengersPr;

                string itemsData = @"<tr><td>
                <table cellpadding='0' cellspacing='0' border='0' width='100%' style='padding:10px;'>
                    <tr>
                        <td valign='top' align='left' width='100%'>

                            <!--TABLE BEGIN-->
                            <table cellpadding='0' cellspacing='0' border='0' width='100%'>
                                <!--row 1-->
                                <tr>
                                    <!--row 1-->
                                    <td valign='top' align='left' width='20%' style=''>
                                        <table cellpadding='0' cellspacing='0' border='0' width='100%'>
                                            <tr>
                                                <td valign='top' align='left' width='100%' >

                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign='top' align='left' width='100%' >

                                                </td>
                                            </tr>
                                        </table>
                                    </td>";
                foreach (ProductQuoteDTO producto in dto.Products)
                {
                    itemsData += @"
                        <td valign='top' align='left'  >
                            <table cellpadding='0' cellspacing='0' border='0' width='100%'>
                                <tr>
                                    <td valign='top' align='left' style='font-size:12px;font-weight: bold;text-transform: uppercase;text-align:left;color: #ea0029;padding-bottom: 0px;padding-right:5px;padding-left: 5px;font-family:Arial, Helvetica, sans-serif;'>
                                        " + producto.ProductName + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td valign='top' align='left' style='font-size:9px;font-weight: bold;text-align: left;padding:5px;padding-top: 0px;color: #484848;font-family:Arial, Helvetica, sans-serif;'>
                                        " + producto.Total
                                         + "<br>" + producto.AveragePerPerson;
                    if (dto.ApplyPolicy)
                    {
                        itemsData += "<br>" + producto.Assistance;
                        itemsData += "<br>" + producto.Insurance;
                    }
                    itemsData += @"
                                    </td>
                                </tr>
                            </table>
                        </td>
                    
                    ";
                }

                itemsData += "</tr>";
                string tableBody = "";
                if (keyClausesSegu.Count > 0)
                {
                    tableBody += @"
                     <tr style='background-color:rgb(201, 0, 44)'>
                        <td style='
                                   font-size: 13px;
                                   line-height: 18px;
                                   color:#ffffff;
                                   font-family:Arial, Helvetica, sans-serif; 
                                   font-weight: bold;  padding-bottom: 3px; 
                                   padding-top: 3px; 
                                   padding-left: 7px;
                            ' 
                            colspan='" + (dto.Products.Count + 1) + @"'
                        >"
                                + ServicioSeguro + @"
                        </td>
                    </tr>
                ";
                    foreach (KeyValuePair<string, string> clausula in keyClausesSegu)
                    {
                        tableBody += @"
                    <tr>
                        <td valign='top' align='left' style='font-size:11px;padding:5px;border: 1px solid #dedede;color: #484848;font-family:Arial, Helvetica, sans-serif;'>"
                                + clausula.Value + @"
                        </td>";
                        foreach (ProductQuoteDTO producto in dto.Products)
                        {
                            tableBody += "<td valign='top' align='left' style='font-size:11px;padding:5px;border: 1px solid #dedede;color: #484848;font-family:Arial, Helvetica, sans-serif;' >";
                            if (producto.ClausesMap.ContainsKey(clausula.Key))
                            {
                                tableBody += producto.ClausesMap[clausula.Key];
                            }
                            tableBody += "</td>";
                        }
                        tableBody += "</tr>";
                    }
                }
                if (keyClauses.Count > 0)
                {
                    tableBody += @"
                     <tr style='background-color:rgb(201, 0, 44)'>
                        <td style='
                                   font-size: 13px;
                                   line-height: 18px;
                                   color:#ffffff;
                                   font-family:Arial, Helvetica, sans-serif; 
                                   font-weight: bold;  padding-bottom: 3px; 
                                   padding-top: 3px; 
                                   padding-left: 7px;
                            ' 
                            colspan='" + (dto.Products.Count + 1) + @"'
                        >"
                                + ServicioAsistencia + @"
                        </td>
                    </tr>
                ";
                    foreach (KeyValuePair<string, string> clausula in keyClauses)
                    {
                        if (keyClausesSegu.ContainsKey(clausula.Key))
                        {
                            continue;
                        }
                        tableBody += @"
                    <tr>
                        <td valign='top' align='left' style='font-size:11px;padding:5px;border: 1px solid #dedede;color: #484848;font-family:Arial, Helvetica, sans-serif;'>"
                                + clausula.Value + @"
                        </td>";
                        foreach (ProductQuoteDTO producto in dto.Products)
                        {
                            tableBody += "<td valign='top' align='left' style='font-size:11px;padding:5px;border: 1px solid #dedede;color: #484848;font-family:Arial, Helvetica, sans-serif;' >";
                            if (producto.ClausesMap.ContainsKey(clausula.Key))
                            {
                                tableBody += producto.ClausesMap[clausula.Key];
                            }
                            tableBody += "</td>";
                        }
                        tableBody += "</tr>";
                    }
                }
                tableBody += @"
                                </table>
                            </td>
                        </tr>
                    </table>
                    </td></tr>
                ";
                dto.TableHeader = itemsData + tableBody;
                dto.TableBody = "";
                dto.From = this.From;
                ServiceLocator.Instance().GetSendMailService().SendMailQuote(dto);
            }
        }

        private String getCssRow(int index)
        {
            if (index % 2 == 0)
            {


                return "background-color: rgb(240, 240, 240); color: #666666;";
            }
            else
            {
                return "background: #FFFFFF; color: #666666;";


            }
        }
    }

    public class Clausula
    {
        public int pais { get; set; }
        public String producto { get; set; }
        public String tarifa { get; set; }
        public String cantDias { get; set; }
        public String idClausula { get; set; }
        public String leyenda { get; set; }
        public int posicion { get; set; }
        public String titulo { get; set; }
        public String codigoTipoClausula { get; set; }
    }

    public class Producto
    {
        public String ProductName { get; set; }
        public Clausula[] Clauses { get; set; }
        public String Total { get; set; }
        public String Insurance { get; set; }
        public String Assistance { get; set; }
        public String StartDate { get; set; }
        public String EndDate { get; set; }
        public int Country { get; set; }
        public String To { get; set; }
        public String AveragePerPerson { get; set; }
    }

    public class EMailBotonPagoExecution
    {
        public string Url { get; set; }

        public string ModuleCode { get; set; }

        public string EmailTo { get; set; }

        public int countryCode { get; set; }

        public string voucherCode { get; set; }

        public int Idiom { get; set; }

        public void ExecuteSendMail()
        {
            EmailBotonPagoDTO dto = new EmailBotonPagoDTO();
            dto.Url = Url;
            dto.ModuleCode = ModuleCode == null || ModuleCode.Length == 0 ? "-" : ModuleCode.Trim();
            dto.To = EmailTo;
            dto.CountryCode = countryCode;
            dto.VoucherCode = voucherCode;
            dto.IdLanguage = Idiom;

            ServiceLocator.Instance().GetSendMailService().SendMailBotonPago(dto);
        }
    }

    public class EmailEndosoExecution
    {
        public string FullName { get; set; }
        public int CountryCode { get; set; }
        public string VoucherCode { get; set; }
        public string ModuleCode { get; set; }
        public string Email { get; set; }

        public void ExecuteSendMail()
        {
            EmailEndosoDTO dto = new EmailEndosoDTO();
            dto.FullName = this.FullName;
            dto.CountryCode = this.CountryCode;
            dto.VoucherCode = this.VoucherCode;
            dto.ModuleCode = this.ModuleCode;
            dto.EmailTo = this.Email;

            ServiceLocator.Instance().GetSendMailService().SendMailEndoso(dto);
        }
    }

    public class EmailExternalPaymentFinishExecution
    {
        public string EmailTo { get; set; }
        public string FullName { get; set; }
        public int IdLanguage { get; set; }
        public float Amount { get; set; }
        public int CountryCode { get; set; }

        public void ExecuteSendMail()
        {
            EmailExternalPaymentFinishDTO dto = new EmailExternalPaymentFinishDTO();
            dto.To = EmailTo;
            dto.IdLanguage = IdLanguage;
            dto.RecipientFullName = FullName;
            dto.CountryCode = CountryCode;
            dto.Amount = Amount.ToString();

            ServiceLocator.Instance().GetSendMailService().SendMailExternalPaymentFinish(dto);
        }
    }
}
