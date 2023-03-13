namespace FileService.API.Services;

public interface IContentTypeProviderService
{
    string GetContentType(string fileName);
}

public class ContentTypeProviderService : IContentTypeProviderService
{

    public string GetContentType(string fileName)
    {
        var fileExtension = Path.GetExtension(Path.Combine("D:/Projects/FileService/Data", fileName)).ToLowerInvariant();
        switch (fileExtension)
        {
            case ".txt":
                return "text/plain";
            case ".pdf":
                return "application/pdf";
            case ".doc":
            case ".docx":
                return "application/msword";
            case ".xls":
            case ".xlsx":
                return "application/vnd.ms-excel";
            case ".ppt":
            case ".pptx":
                return "application/vnd.ms-powerpoint";
            case ".jpg":
            case ".jpeg":
                return "image/jpeg";
            case ".png":
                return "image/png";
            case ".gif":
                return "image/gif";
            case ".bmp":
                return "image/bmp";
            case ".mp4":
                return "video/mp4";
            case ".avi":
                return "video/x-msvideo";
            default:
                return "application/octet-stream";
        }
    }
}