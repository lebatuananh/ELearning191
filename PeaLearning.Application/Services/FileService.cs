using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PeaLearning.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;
        private readonly UrlEndpoints _urls;
        private readonly HttpClient _httpClient;

        public FileService(IConfiguration configuration, IOptions<UrlEndpoints> config, HttpClient httpClient)
        {
            _urls = config.Value;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<UploadResponseDto> Upload(IFormFile file)
        {
            var uri = _urls.UploadApiUrlPrefix + UrlEndpoints.UploadApiMethods.UploadRecord(GenerateAccessToken());
            byte[] data;
            using (var br = new BinaryReader(file.OpenReadStream()))
                data = br.ReadBytes((int)file.OpenReadStream().Length);
            MultipartFormDataContent requestContent = new MultipartFormDataContent();
            var streamContent = new StreamContent(new MemoryStream(data));
            streamContent.Headers.Add("Content-Disposition", "form-data; name=\"upload\"; filename=\"" + file.FileName + "\"");
            requestContent.Add(streamContent);
            var response = await _httpClient.PostAsync(uri, requestContent);
            response.EnsureSuccessStatusCode();
            var responseStr = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseStr))
            {
                throw new ArgumentNullException("Upload failed");
            }
            var result = JsonConvert.DeserializeObject<UploadResponseDto>(responseStr, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            });
            return result;
        }

        string GenerateAccessToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                _configuration["Tokens:Issuer"],
                expires: DateTime.Now.AddDays(2),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class UploadResponseDto
    {
        public string ResourceType { get; set; }
        public CurrentFolder CurrentFolder { get; set; }
        public string FileName { get; set; }
        public bool Uploaded { get; set; }
    }

    public class CurrentFolder
    {
        public string Path { get; set; }
        public string Url { get; set; }
        public string Acl { get; set; }
    }
}
