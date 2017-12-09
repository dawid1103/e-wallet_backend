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
    public class ServiceEndpoint
    {
        protected HttpClient _httpClient;
        
        public ServiceEndpoint(string baseUrl)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public ServiceEndpoint(HttpClient client)
        {
            _httpClient = client;
        }

        protected async Task CheckResponseSuccess(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = string.Empty;
                ServiceError serviceError = new ServiceError();
                if (response.Content.Headers.ContentLength > 0)
                {
                    errorContent = await response.Content.ReadAsStringAsync();
                    serviceError = JsonConvert.DeserializeObject<ServiceError>(errorContent);
                }

                switch (serviceError.Code)
                {
                    case "ObjectAlreadyExists":
                        throw new ObjectAlreadyExists();
                    case "NotUnique":
                        throw new NotUniqueException();
                    default:
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.NotFound:
                                throw new NotFoundException();
                            case HttpStatusCode.BadRequest:
                                throw new BadRequestException(errorContent);
                            default:
                                throw new Exception($"Method {response.RequestMessage.RequestUri.ToString()} failed with status code: {response.StatusCode}, content: {errorContent}");
                        }
                }
            }
        }

        protected async Task<TResponseType> GetAsync<TResponseType>(string requestUri)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

            await CheckResponseSuccess(response);

            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponseType>(responseContent);
        }

        private async Task<TResponseType> PostAsync<TResponseType>(string requestUri, HttpContent content)
        {
            string responseContent = await PostAsync(requestUri, content);
            return JsonConvert.DeserializeObject<TResponseType>(responseContent);
        }

        protected async Task<TResponseType> PostAsync<TResponseType>(string requestUri, object request)
        {
            string jsonRequestContent = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(jsonRequestContent, Encoding.UTF8, "application/json");
            return await PostAsync<TResponseType>(requestUri, content);
        }

        private async Task<string> PostAsync(string requestUri, HttpContent content)
        {
            HttpResponseMessage response = await _httpClient.PostAsync(requestUri, content);
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
            HttpResponseMessage response = await _httpClient.DeleteAsync(requestUri);

            await CheckResponseSuccess(response);
        }

        protected async Task PutAsync(string requestUri, object request)
        {
            string jsonRequestContent = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(jsonRequestContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(requestUri, content);

            await CheckResponseSuccess(response);
        }
    }
}
