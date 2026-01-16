using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace SimulationRetraite0.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExtensionController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<ExtensionController> _logger;

        public ExtensionController(IWebHostEnvironment environment, ILogger<ExtensionController> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        [HttpGet("download-table-extractor")]
        public IActionResult DownloadTableExtractor()
        {
            try
            {
                var extensionPath = Path.Combine(_environment.WebRootPath, "extensions", "table-extractor");

                if (!Directory.Exists(extensionPath))
                {
                    _logger.LogError("Extension directory not found: {Path}", extensionPath);
                    return NotFound("Extension files not found");
                }

                // Create a temporary file for the ZIP
                var tempZipPath = Path.Combine(Path.GetTempPath(), $"table-extractor-{Guid.NewGuid()}.zip");

                try
                {
                    // Create ZIP file
                    ZipFile.CreateFromDirectory(extensionPath, tempZipPath);

                    // Read the ZIP file into memory
                    var fileBytes = System.IO.File.ReadAllBytes(tempZipPath);

                    // Clean up temp file
                    System.IO.File.Delete(tempZipPath);

                    // Return the ZIP file
                    return File(fileBytes, "application/zip", "table-extractor-extension.zip");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating ZIP file");

                    // Clean up temp file if it exists
                    if (System.IO.File.Exists(tempZipPath))
                    {
                        System.IO.File.Delete(tempZipPath);
                    }

                    return StatusCode(500, "Error creating download file");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DownloadTableExtractor");
                return StatusCode(500, "An error occurred while preparing the download");
            }
        }
    }
}
