using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CognitiveServices.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CognitiveServices {
    public abstract class ServiceClient {

        protected string ApiRoot { get; set; }
        protected string AuthKey { get; set; }
        protected string AuthValue { get; set; }

        protected static string OperationLocationHeaderName = "Operation-Location";

        protected static CamelCasePropertyNamesContractResolver s_defaultResolver = new CamelCasePropertyNamesContractResolver();

        protected static JsonSerializerSettings s_settings = new JsonSerializerSettings() {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = s_defaultResolver
        };

        protected HttpClient HttpClient { get; }

        protected ServiceClient() : this(new HttpClient()) {
        }

        protected ServiceClient(HttpClient httpClient) {
            HttpClient = httpClient;
            HttpClient.Timeout = TimeSpan.FromMinutes(2);
        }

        
        protected async Task<TResponse> PostAsync<TRequest, TResponse>(string apiUrl, TRequest requestBody) {
            return await SendAsync<TRequest, TResponse>(HttpMethod.Post, apiUrl, requestBody).ConfigureAwait(false);
        }

        protected async Task<TResponse> GetAsync<TRequest, TResponse>(string apiUrl, TRequest requestBody) {
            return await SendAsync<TRequest, TResponse>(HttpMethod.Get, apiUrl, requestBody).ConfigureAwait(false);
        }

        protected async Task<TResponse> SendAsync<TRequest, TResponse>(HttpMethod method, string apiUrl, TRequest requestBody) {
            var urlIsRelative = System.Uri.IsWellFormedUriString(apiUrl, UriKind.Relative);
            var requestUri = urlIsRelative ? ApiRoot + apiUrl : apiUrl;
            var request = new HttpRequestMessage(method, requestUri);
            request.Headers.Add(AuthKey, AuthValue);

            if (requestBody != null) {
                if (requestBody is Stream) {
                    request.Content = new StreamContent(requestBody as Stream);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                }
                else {
                    request.Content = new StringContent(JsonConvert.SerializeObject(requestBody, s_settings), Encoding.UTF8, "application/json");
                }
            }

            var response = await HttpClient.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode) {
                string responseContent = null;
                if (response.Content != null) {
                    responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }

                if (!string.IsNullOrWhiteSpace(responseContent)) {
                    return JsonConvert.DeserializeObject<TResponse>(responseContent, s_settings);
                }

                // For video submission, the response content is empty. The information is in the
                // response headers. 
                var output = System.Activator.CreateInstance<TResponse>();
                if (output is VideoOperation) {
                    var operation = output as VideoOperation;
                    operation.Url = response.Headers.GetValues(OperationLocationHeaderName).First();
                    return output;
                }

                return default(TResponse);
            }
            if (response.Content != null && response.Content.Headers.ContentType.MediaType.Contains("application/json")) {
                var errorObjectString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var wrappedClientError = JsonConvert.DeserializeObject<WrappedClientError>(errorObjectString);
                if (wrappedClientError?.Error != null) {
                    throw new ClientException(wrappedClientError.Error, response.StatusCode);
                }
            }

            response.EnsureSuccessStatusCode();
            return default(TResponse);
        }

        protected class UrlRequest {
            public string Url { get; set; }
        }

        public class WrappedClientError {
            public ClientError Error { get; set; }
        }
    }
}