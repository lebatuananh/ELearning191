namespace PeaLearning.Application.Services
{
    public class UrlEndpoints
    {
        public string UploadApiUrlPrefix { get; set; }
        public class UploadApiMethods
        {
            public static string UploadRecord(string access_token) => $"/ckfinder/connector/?command=FileUpload&type=Files&currentFolder=/Record/&access_token={access_token}&responseType=json";
        }
    }
}
