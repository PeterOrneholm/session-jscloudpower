using System;
using System.Configuration;
using System.IO;
using System.Web.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace PeterO.JsCloudPower.Controllers
{
    public class SasKeyController : ApiController
    {
        public dynamic Get(string filename)
        {
            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageAccount"].ConnectionString);
            var cloudBlobClient = storageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference("files");

            var extension = Path.GetExtension(filename);
            var fileId = Guid.NewGuid();
            var blobFilename = fileId + extension;
            var blob = cloudBlobContainer.GetBlobReference(blobFilename);

            var blobUri = blob.Uri;
            var blobSas = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Write,
                SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(30)
            });

            return new
            {
                downloadUrl = blobUri,
                uploadUrl = blobUri + blobSas
            };
        }
    }
}
