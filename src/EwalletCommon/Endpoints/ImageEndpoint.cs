using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EwalletCommon.Endpoints
{
    public class ImageEndpoint : ServiceEndpoint
    {
        public ImageEndpoint(string url) : base(url)
        {
        }

        public ImageEndpoint(HttpClient client) : base(client)
        {
        }
        
        public async Task<string> Upload(IFormFile formFile)
        {
            return await PostFileAsync("upload", formFile);
        }
    }
}