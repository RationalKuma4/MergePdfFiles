using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.IO;
using ipdf = iTextSharp.text.pdf;
using itex = iTextSharp.text;

namespace MergePdfFiles
{
    internal class Program
    {
        private static void Main()
        {
            //var genericPath = @"C:\Users\Anselmo-LT\Documents\GitHub\ProtocoloTerminal\Entrega\3\";

            // Generico
            //Merge(genericPath);
            MergeAllFromFolder();
        }

        private static void MergeAllFromFolder()
        {
            var folder = Directory.GetFiles(@"C:\Users\Anselmo\Downloads\doc-servicio");
            using (var documentOne = PdfReader.Open(folder[1], PdfDocumentOpenMode.Import))
            using (var documentTwo = PdfReader.Open(folder[0], PdfDocumentOpenMode.Import))
            using (var pdfResult = new PdfDocument())
            {
                CopyPages(documentOne, pdfResult);
                CopyPages(documentTwo, pdfResult);
                pdfResult.Save(@"C:\Users\Anselmo\Downloads\doc-servicio\servicio.pdf");
            }

            void CopyPages(PdfDocument from, PdfDocument to)
            {
                for (var i = 0; i < from.PageCount; i++)
                    to.AddPage(from.Pages[i]);
            }
        }

        public static void Merge(string path)
        {
            using (var port = PdfReader.Open($@"{path}\Telematica.PortadaProtocolo.NoNombre.pdf", PdfDocumentOpenMode.Import))
            using (var prot = PdfReader.Open($@"{path}\protocolo.pdf", PdfDocumentOpenMode.Import))
            using (var outPdf = new PdfDocument())
            {
                CopyPages(port, outPdf);
                CopyPages(prot, outPdf);

                outPdf.Save($@"{path}\NoNombre\3a.DTA.MI.2019-2.11.pdf");
            }

            void CopyPages(PdfDocument from, PdfDocument to)
            {
                for (var i = 0; i < from.PageCount; i++)
                    to.AddPage(from.Pages[i]);
            }
        }

        public static void ImagetoPdf(string path)
        {
            itex.Document document = new itex.Document();
            using (var stream = new FileStream($@"{path}\portada.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                ipdf.PdfWriter.GetInstance(document, stream);
                document.Open();
                using (var imageStream = new FileStream($@"{path}\portada.jpg", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var image = itex.Image.GetInstance(imageStream);
                    document.Add(image);
                }
                document.Close();
            }
        }
    }
}
