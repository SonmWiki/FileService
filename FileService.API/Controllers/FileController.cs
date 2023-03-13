using System.Text.Json;
using FileService.API.Configuration;
using FileService.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FileService.API.Controllers;

[ApiController]
[Route("attachments")]
public class FileController : ControllerBase
{

    private readonly IContentTypeProviderService _providerService;

    private readonly AppConfiguration _appConfiguration;

    public FileController(IContentTypeProviderService providerService, IOptions<AppConfiguration> appConfigurationAccessor)
    {
        _providerService = providerService;
        _appConfiguration = appConfigurationAccessor.Value;
    }

    [HttpGet]
    [Route("{fileName}")]
    public IActionResult Get(string fileName)
    { 
        var filePath = Path.Combine(_appConfiguration.FileStoragePath, fileName);

        if (!System.IO.File.Exists(filePath)) return NotFound();

        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096);
        
        var contentType = _providerService.GetContentType(fileName);
        
        return new FileStreamResult(fileStream, contentType);
    }
    
    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> Upload(IFormFile file, string token)
    {
        if (token != _appConfiguration.Token) return Unauthorized();

        if (file.Length == 0) return Conflict();
        
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(_appConfiguration.FileStoragePath, fileName);
        
        await using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
             await file.CopyToAsync(fileStream);
        }

        return new OkObjectResult(new { fileName = fileName });
    }
    
}