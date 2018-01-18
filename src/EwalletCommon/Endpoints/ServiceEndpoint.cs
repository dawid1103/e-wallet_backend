using EwalletCommon.Utils;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EwalletCommon.Endpoints
{
    public abstract class ServiceEndpoint
    {
        protected HttpClient httpClient;

        public ServiceEndpoint(string baseUrl)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);
        }

        public ServiceEndpoint(HttpClient client)
        {
            httpClient = client;
        }

        protected async Task CheckResponseSuccess(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        throw new NotFoundException();
                    case HttpStatusCode.BadRequest:
                        throw new BadRequestException();
                    default:
                        throw new Exception($"Method {response.RequestMessage.RequestUri.ToString()} failed with status code: {response.StatusCode}");
                }
            }
        }

        protected async Task<TResponseType> GetAsync<TResponseType>(string requestUri)
        {
            HttpResponseMessage response = await httpClient.GetAsync(requestUri);

            await CheckResponseSuccess(response);

            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponseType>(responseContent);
        }

        protected async Task<TResponseType> PostAsync<TResponseType>(string requestUri, object request)
        {
            string jsonRequestContent = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(jsonRequestContent, Encoding.UTF8, "application/json");
            string responseContent = await PostAsync(requestUri, content);
            return JsonConvert.DeserializeObject<TResponseType>(responseContent);
        }

        private async Task<string> PostAsync(string requestUri, HttpContent content)
        {
            HttpResponseMessage response = await httpClient.PostAsync(requestUri, content);
            await CheckResponseSuccess(response);
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<string> PostFileAsync(string requestUri, IFormFile formFile)
        {
            var content = new MultipartFormDataContent(formFile.ContentType);

            using (var streamContent = new StreamContent(formFile.OpenReadStream()))
            {
                content.Add(streamContent, "uploadFile", formFile.FileName);
                return await PostAsync(requestUri, content);
            }
        }

        protected async Task DeleteAsync(string requestUri)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(requestUri);
            await CheckResponseSuccess(response);
        }

        protected async Task PutAsync(string requestUri, object request)
        {
            string jsonRequestContent = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(jsonRequestContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PutAsync(requestUri, content);

            await CheckResponseSuccess(response);
        }
    }
}
