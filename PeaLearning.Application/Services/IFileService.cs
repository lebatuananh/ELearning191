using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PeaLearning.Application.Services
{
    public interface IFileService
    {
        Task<UploadResponseDto> Upload(IFormFile file);
    }
}
