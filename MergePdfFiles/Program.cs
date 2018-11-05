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
            var genericPath = @"C:\Users\Anselmo\Documents\GitHub\ProtocoloV2\Entrega\Generico";
            var namePath = @"C:\Users\Anselmo\Documents\GitHub\ProtocoloV2\Entrega\Nombre";

            // Generico
            Merge(genericPath);

            // Nombre
            Merge(namePath);
            ImagetoPdf(namePath);
        }

        public static void Merge(string path)
        {
            using (PdfDocument one = PdfReader.Open($@"{path}\Telematica.PortadaProtocolo.pdf", PdfDocumentOpenMode.Import))
            using (PdfDocument two = PdfReader.Open($@"{path}\DesarrolloV2.pdf", PdfDocumentOpenMode.Import))
            using (PdfDocument outPdf = new PdfDocument())
            {
                CopyPages(one, outPdf);
                CopyPages(two, outPdf);

                outPdf.Save($@"{path}\Prtocolo.pdf");
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
            using (var stream = new FileStream($@"{path}\bear1.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                ipdf.PdfWriter.GetInstance(document, stream);
                document.Open();
                using (var imageStream = new FileStream($@"{path}\bear1.jpg", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var image = itex.Image.GetInstance(imageStream);
                    document.Add(image);
                }
                document.Close();
            }
        }
    }
}
