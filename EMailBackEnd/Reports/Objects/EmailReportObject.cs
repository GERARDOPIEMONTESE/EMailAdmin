namespace EMailAdmin.BackEnd.Reports.Objects
{
    public class EmailReportObject
    {
        public string Body { get; set; }
        public string BodyForPDF { get { return Body.Replace("</p>", "</p><div style=\"height: 30px;\"></div>"); } }
        public byte[] Header { get; set; }
        public byte[] Footer { get; set; }
    }
}