using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using HtmlAgilityPack;
using iTextSharp.tool.xml;
using System.Text.RegularExpressions;

namespace EMailAdmin.BackEnd.Utils
{
    public static class PdfUtils
    {
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

        public static byte[] GetPdfEkit(string htmlData)
        {
             var document = new Document();
            var output = new MemoryStream();

            try
            {
                htmlData = NormalizeHTMLTags(htmlData);

                // Initialize pdf writer
                var writer = PdfWriter.GetInstance(document, output);
                
                // Open document to write
                document.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, new StringReader(htmlData)   );
                
            }
            catch (Exception exception)
            {
                throw new Exception("GetPdfEkit", exception);
            }
            finally
            {
                document.Close();
            }
            return output.GetBuffer();
        }

        public static ReturnValue GetPdf(string HtmlData)
        {
            ReturnValue Result = new ReturnValue();
            HtmlData = NormalizeHTMLTags(HtmlData);
            Result.Html = HtmlData;
            try
            {
                var stream = new MemoryStream();
                var document = new Document();
                
                using (MemoryStream ms = new MemoryStream())
                {
                    using (Document doc = new Document())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                        doc.Open();

                        using (var srHtml = new StringReader(HtmlData))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, srHtml);
                        }
                    }
                    document.Close();
                    Result.Data = ms.ToArray();
                    Result.Success = true;
                }
            }
            catch (Exception ex)
            {                
                Result.Message = ex;
            }

            return Result;
        }

        public static ReturnValue GetPdfEtiquetas(string HtmlData, bool IsTag)
        {
            ReturnValue Result = new ReturnValue();
            HtmlData = NormalizeHTMLTags(HtmlData);
            Result.Html = HtmlData;
            try
            {
                var stream = new MemoryStream();
                var document = new Document();

                using (MemoryStream ms = new MemoryStream())
                {
                    using (Document doc = new Document())
                    {
                        if (IsTag)
                        {
                            doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                            doc.SetMargins(12, 0, 5, 0);
                        } 
                        PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                        doc.Open();

                        using (var srHtml = new StringReader(HtmlData))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, srHtml);
                        }
                    }
                    document.Close();
                    Result.Data = ms.ToArray();
                    Result.Success = true;
                }
            }
            catch (Exception ex)
            {
                Result.Message = ex;
            }

            return Result;
        }
                
        public static string NormalizeHTMLTags(string strHtml)
        {                      
            HtmlDocument doc = new HtmlDocument();
            doc.OptionAutoCloseOnEnd = false;
            doc.OptionCheckSyntax = false;
            doc.OptionFixNestedTags = false;
            doc.LoadHtml(strHtml);
            
            var nodes = doc.DocumentNode.SelectNodes("//comment()");
            if (nodes != null)
            {
                foreach (HtmlNode comment in nodes)
                {
                    if (!comment.InnerText.StartsWith("DOCTYPE"))
                        comment.ParentNode.RemoveChild(comment);
                }
            }
            
            string rst = doc.DocumentNode.InnerHtml;

            foreach (var item in doc.DocumentNode.ChildNodes)
            {
                NormalizeNode(item, ref rst);
            }
                                  
            rst = rst.Replace("&nbsp;", "");
            rst = rst.Replace("\r\n", " ");
            rst = rst.Replace("\n\t", "");
            rst = rst.Replace("\t", "");
            rst = rst.Replace("<tr></tr>", "");
            rst = rst.Replace("<tbody></tbody>", "");
            rst = rst.Replace("<theader></theader>", "");
            rst = rst.Replace("<tfooter></tfooter>", "");

            return rst;
        }

        private static void NormalizeNode(HtmlNode nodo, ref string html)
        {
            foreach (var item in nodo.ChildNodes)
            {
                NormalizeHTML(item, ref html);
            }
        }

        private static void NormalizeHTML(HtmlNode nodo, ref string html)
        {
            var imgs = nodo.SelectNodes("//img");
            if (imgs != null)
            {
                foreach (var item in imgs)
                {
                    string htmlNormalize = item.OuterHtml.Replace(">", "/>");
                    html = html.Replace(item.OuterHtml, htmlNormalize);
                }
            }

            var brs = nodo.SelectNodes("//br");
            if (brs != null)
            {
                foreach (var item in brs)
                {
                    string htmlNormalize = item.OuterHtml.Replace("<br>", "<br/>");
                    html = html.Replace(item.OuterHtml, htmlNormalize);
                }
            }
        }
    }
}

public class ReturnValue
{
    public ReturnValue()
    {
        this.Success = false;
        this.Message = null;
    }

    public bool Success = false;
    public Exception Message = null;
    public Byte[] Data = null;
    public string Html = "";
}