using Minio;
using Minio.DataModel.Args; // Thêm namespace này để sử dụng Args
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

public class MinioService
{
    private readonly IMinioClient _minioClient;  // Sử dụng IMinioClient thay vì MinioClient

    public MinioService(IConfiguration configuration)
    {
        var minioConfig = configuration.GetSection("Minio");

        _minioClient = new MinioClient()
            .WithEndpoint(minioConfig["Endpoint"])
            .WithCredentials(minioConfig["AccessKey"], minioConfig["SecretKey"])
            .Build();
    }

    public async Task UploadFileAsync(string bucketName, string objectName, string filePath)
    {
        // Kiểm tra sự tồn tại của bucket với BucketExistsArgs
        var bucketExistsArgs = new BucketExistsArgs()
            .WithBucket(bucketName);

        bool bucketExists = await _minioClient.BucketExistsAsync(bucketExistsArgs);
        if (!bucketExists)
        {
            // Tạo bucket nếu không tồn tại với MakeBucketArgs
            var makeBucketArgs = new MakeBucketArgs()
                .WithBucket(bucketName);

            await _minioClient.MakeBucketAsync(makeBucketArgs);
        }

        // Đọc tệp từ hệ thống
        using (var fileStream = File.OpenRead(filePath))
        {
            // Upload file với PutObjectArgs
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length);

            await _minioClient.PutObjectAsync(putObjectArgs);
        }
    }
}
