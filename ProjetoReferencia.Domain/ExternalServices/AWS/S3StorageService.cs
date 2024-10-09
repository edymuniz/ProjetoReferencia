using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ProjetoReferencia.Domain.ExternalServices.AWS
{
    public class S3StorageService : IStorageService
    {
        private readonly string _accessKey;
        private readonly string _secretKey;
        private readonly string _region;
        private readonly string _bucketName;

        public S3StorageService(IConfiguration configuration)
        {
            _accessKey = configuration["AWS:AccessKey"];
            _secretKey = configuration["AWS:SecretKey"];
            _region = configuration["AWS:Region"];
            _bucketName = configuration["AWS:BucketName"];
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            //var s3Client = new AmazonS3Client(_accessKey, _secretKey, RegionEndpoint.GetBySystemName(_region));

            //using (var stream = file.OpenReadStream())
            //{
            //    var request = new PutObjectRequest
            //    {
            //        BucketName = _bucketName,
            //        Key = file.FileName,
            //        InputStream = stream,
            //        ContentType = file.ContentType
            //    };

            //    await s3Client.PutObjectAsync(request);
            //}

            //return $"https://{_bucketName}.s3.{_region}.amazonaws.com/{file.FileName}";
            return file.FileName;
        }
    }
}
