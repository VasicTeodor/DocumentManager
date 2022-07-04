using Microsoft.AspNetCore.Http;

namespace ITCompanyCVManager.Domain.Services;

public interface IFileService
{
    string ReadTextFromPdfFile(IFormFile pdfFile);
    bool SaveFileToDirectory(string filePath, IFormFile file);
}