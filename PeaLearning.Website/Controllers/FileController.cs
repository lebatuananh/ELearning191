using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeaLearning.Application.Services;
using System.Threading.Tasks;

namespace PeaLearning.Website.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [Route("file/upload")]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var result = await _fileService.Upload(file);
            return Ok(result);
        }
    }
}
