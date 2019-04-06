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
            var genericPath = @"C:\Users\Anselmo-LT\Documents\GitHub\ProtocoloTerminal\Entrega\SinNombre";
            var namePath = @"C:\Users\Anselmo-LT\Documents\GitHub\ProtocoloTerminal\Entrega\ConNombre";

            // Generico
            Merge(genericPath);

            // Nombre
            //ImagetoPdf(namePath);
            Merge(namePath);
            
        }

        public static void Merge(string path)
        {
            using (PdfDocument one = PdfReader.Open($@"{path}\portada.pdf", PdfDocumentOpenMode.Import))
            using (PdfDocument two = PdfReader.Open($@"{path}\protocolo.pdf", PdfDocumentOpenMode.Import))
            using (PdfDocument outPdf = new PdfDocument())
            {
                CopyPages(one, outPdf);
                CopyPages(two, outPdf);

                outPdf.Save($@"{path}\PrtocoloEntrega.pdf");
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
