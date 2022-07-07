using Azure.Storage.Blobs;

namespace ElzaFunctionApp.Services
{
    internal class BlobService : IBlobService
    {
        private readonly BlobContainerClient _blobContainerClient;
        public BlobService(BlobContainerClient blobContainerClient)
        {
            _blobContainerClient = blobContainerClient;
        }
        public async Task<string> GetDataAsync(string name)
        {
            var blobClient = _blobContainerClient.GetBlobClient(name);
            var str = string.Empty;
            if (await blobClient.ExistsAsync())
            {
                var response = await blobClient.DownloadAsync();
                using (var streamReader = new StreamReader(response.Value.Content))
                {
                    while (!streamReader.EndOfStream)
                    {
                        var line = await streamReader.ReadLineAsync();
                        str += line;
                    }
                }
            }
            return str;
        }
    }
}
