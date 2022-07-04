using System.Text;
using ITCompanyCVManager.Domain.Services;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Microsoft.AspNetCore.Http;

namespace ITCompanyCVManager.Services.Implementation;

public class FileService : 
    IFileService
{
    public string ReadTextFromPdfFile(IFormFile pdfFile)
    {
        var textFromPdf = new StringBuilder();
        using var ms = new MemoryStream();

        pdfFile.OpenReadStream().CopyTo(ms);
        byte[] bytesArrayFromFile = ms.ToArray();
        using var memory = new MemoryStream(bytesArrayFromFile);

        var pdfTextReader = new PdfReader(memory);
        var pdfDocument = new PdfDocument(pdfTextReader);

        var pageNumber = pdfDocument.GetNumberOfPages();

        for (int page = 1; page <= pageNumber; page++)
        {
            ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
            var currentPageText = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(page), strategy);
            currentPageText = Encoding.UTF8.GetString(Encoding.Convert(
                Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentPageText)));

            textFromPdf.Append(currentPageText);
        }
        return textFromPdf.ToString();
    }

    public bool SaveFileToDirectory(string filePath, IFormFile file)
    {
        new FileInfo(filePath).Directory?.Create();
        using FileStream stream = new FileStream(filePath, FileMode.Create);
        file.CopyTo(stream);

        return true;
    }
}