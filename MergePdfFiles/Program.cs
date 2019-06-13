using System.IO;
using itex = iTextSharp.text;
using ipdf = iTextSharp.text.pdf;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace MergePdfFiles
{
    internal class Program
    {
        private static void Main()
        {
            var genericPath = @"C:\Users\Anselmo-LT\Documents\GitHub\ProtocoloTerminal\Entrega\3\";

            // Generico
            Merge(genericPath);
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
