using System.Net.Http;
using EwalletCommon.Endpoints;

namespace EwalletCommon.Services
{
    public class Picasso
    {
        public ImageEndpoint Image { get; }

        public Picasso(string url)
        {
            Image = new ImageEndpoint(url);
        }

        public Picasso(HttpClient client)
        {
            Image = new ImageEndpoint(client);
        }
    }
}