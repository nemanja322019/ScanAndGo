using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ServiceLibrary.Services
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;
        public ImageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> UploadImage(MemoryStream stream)
        {
            stream.Position = 0;
            BlobContainerClient blobContainerClient = new BlobContainerClient(
                _configuration["Azure:ConnectionString"],
                _configuration["Azure:Container"]
            );
            var uniqueName = Guid.NewGuid().ToString() + ".png";
            BlobClient blobClient = blobContainerClient.GetBlobClient(uniqueName);
            await blobClient.UploadAsync(stream, true);
            return blobClient.Uri.ToString();

        }
    }
}

