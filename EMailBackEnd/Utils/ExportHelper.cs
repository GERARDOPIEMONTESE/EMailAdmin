using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using EMailAdmin.BackEnd.DTO;


namespace EMailAdmin.BackEnd.Utils
{
    public static class ExportHelper
    {
        public static string ToHtml(IList<VoucherACNETDTO> vouchers)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.AppendLine("<head runat=\"server\"><title>Award</title></head>");
            sb.AppendLine("<body><table><tr align='center'>");
            sb.AppendLine("<td>PAIS_NOMBRE</td>");
            sb.AppendLine("<td>PAIS_CODIGO</td>");
            sb.AppendLine("<td>VOUCHER</td>");
            sb.AppendLine("<td>UNIQUE_VOUCHER_ID</td>");
            sb.AppendLine("<td>PRODUCTO</td>");
            sb.AppendLine("<td>COD_TARIFA</td>");
            sb.AppendLine("<td>CANT_DIAS</td>");
            sb.AppendLine("<td>AGENCIA</td>");
            sb.AppendLine("<td>FECHA_EMISION</td>");
            sb.AppendLine("<td>PLAN_FAMILIA</td>");
            sb.AppendLine("<td>GRUPO_VOUCHER</td>");
            sb.AppendLine("<td>ID_PROMOCION</td>");
            sb.AppendLine("<td>TARIFA_EMITIDA</td>");
            sb.AppendLine("<td>NOMBRE</td>");
            sb.AppendLine("<td>APELLIDO</td>");
            sb.AppendLine("<td>FEC_NACIMIENTO</td>");
            sb.AppendLine("<td>NRO_DOCUMENTO</td>");
            sb.AppendLine("<td>EMAIL</td>");
            sb.AppendLine("<td>EDAD</td>");
            sb.AppendLine("<td>KMs</td>");
            sb.AppendLine("<td>ConcPais+Prod+Tar</td>");
            sb.AppendLine("<td>concpais-agv-prd-tar</td>");
            sb.AppendLine("</tr>");

            foreach (VoucherACNETDTO voucher in vouchers)
            {
                sb.AppendLine("<tr align='center'>");
                sb.AppendFormat("<td>{0}</td>", voucher.CountryName); // PAIS_NOMBRE
                sb.AppendFormat("<td>{0}</td>", voucher.CountryCode); // PAIS_CODIGO
                sb.AppendFormat("<td>{0}</td>", voucher.VoucherCode); // VOUCHER
                sb.AppendFormat("<td>{0}</td>", voucher.UniqueVoucherId); // UNIQUE_VOUCHER_ID
                sb.AppendFormat("<td>{0}</td>", voucher.ProductCode); // PRODUCTO
                sb.AppendFormat("<td>{0}</td>", voucher.RateCode); // COD_TARIFA
                sb.AppendFormat("<td>{0}</td>", voucher.Days); // CANT_DIAS
                sb.AppendFormat("<td>{0}</td>", voucher.BranchNumber); // AGENCIA
                sb.AppendFormat("<td>{0}</td>", voucher.IssueDate); // FECHA_EMISION
                sb.AppendFormat("<td>{0}</td>", voucher.FamilyPlan); // PLAN_FAMILIA
                sb.AppendFormat("<td>{0}</td>", voucher.VoucherGroup); // GRUPO_VOUCHER
                sb.AppendFormat("<td>{0}</td>", voucher.PromotionId); // ID_PROMOCION
                sb.AppendFormat("<td>{0}</td>", voucher.RateAmount); // TARIFA_EMITIDA
                sb.AppendFormat("<td>{0}</td>", voucher.Name); // NOMBRE
                sb.AppendFormat("<td>{0}</td>", voucher.LastName); // APELLIDO
                sb.AppendFormat("<td>{0}</td>", voucher.BirthDate); // FEC_NACIMIENTO
                sb.AppendFormat("<td>{0}</td>", voucher.DocNumber); // NRO_DOCUMENTO
                sb.AppendFormat("<td>{0}</td>", voucher.Email); // EMAIL
                sb.AppendFormat("<td>{0}</td>", voucher.Age); // EDAD
                sb.AppendFormat("<td>{0}</td>", voucher.Points); // KMs
                sb.AppendFormat("<td>{0}</td>", voucher.ConcPaisProdTar); // ConcPais+Prod+Tar
                sb.AppendFormat("<td>{0}</td>", voucher.ConcPaisAgvPrdTar); // concpais-agv-prd-tar
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</table></body></html>");
            return sb.ToString();
        }
    }
}