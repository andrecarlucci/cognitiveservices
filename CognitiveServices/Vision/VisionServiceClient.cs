using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CognitiveServices.Vision {
    public class VisionClient : IVisionClient {
        private const string DEFAULT_API_ROOT = "https://westus.api.cognitive.microsoft.com/vision/v1.0";
        private static string _operationLocationHeaderName = "Operation-Location";
        private string _apiRoot;
        private HttpClient _httpClient;

        private static CamelCasePropertyNamesContractResolver _defaultResolver = new CamelCasePropertyNamesContractResolver();
        private static JsonSerializerSettings _jsonSettings = new JsonSerializerSettings() {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = _defaultResolver
        };

        public VisionClient(string subscriptionKey) : this(subscriptionKey, DEFAULT_API_ROOT) {
        }

        public VisionClient(string subscriptionKey, string apiRoot) {
            _apiRoot = apiRoot?.TrimEnd('/');
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
        }

        public async Task<AnalysisResult> AnalyzeImageAsync(string url,
                                                            IEnumerable<VisualFeature> visualFeatures = null,
                                                            IEnumerable<string> details = null) {
            var requestUrl = CreateAnalyzeImageUrl(visualFeatures, details);
            return await PostJsonAsync<AnalysisResult>(requestUrl, CreateUrlObject(url));
        }

        public async Task<AnalysisResult> AnalyzeImageAsync(Stream imageStream,
                                                            IEnumerable<VisualFeature> visualFeatures = null,
                                                            IEnumerable<string> details = null) {
            var requestUrl = CreateAnalyzeImageUrl(visualFeatures, details);
            return await PostStreamAsync<AnalysisResult>(requestUrl,imageStream);
        }

        private string CreateAnalyzeImageUrl(IEnumerable<VisualFeature> visualFeatures = null, IEnumerable<string> details = null) {
            var requestUrl = new StringBuilder(_apiRoot)
                .Append("/analyze?")
                .Append(string.Join("&", new List<string> {
                    VisualFeaturesToString(visualFeatures),
                    DetailsToString(details)
            }.Where(s => !string.IsNullOrEmpty(s))));
            return requestUrl.ToString();
        }

        public async Task<AnalysisInDomainResult> AnalyzeImageInDomainAsync(string url, Model model) {
            return await AnalyzeImageInDomainAsync(url, model.Name);
        }

        public async Task<AnalysisInDomainResult> AnalyzeImageInDomainAsync(Stream imageStream, Model model) {
            return await AnalyzeImageInDomainAsync(imageStream, model.Name);
        }

        public async Task<AnalysisInDomainResult> AnalyzeImageInDomainAsync(string url, string modelName) {
            var requestUrl = $"{_apiRoot}/models/{modelName}/analyze";
            return await PostJsonAsync<AnalysisInDomainResult>(requestUrl, CreateUrlObject(url));
        }

        public async Task<AnalysisInDomainResult> AnalyzeImageInDomainAsync(Stream imageStream, string modelName) {
            var requestUrl = $"{_apiRoot}/models/{modelName}/analyze";
            return await PostStreamAsync<AnalysisInDomainResult>(requestUrl, imageStream);
        }

        public async Task<ModelResult> ListModelsAsync() {
            var requestUrl = $"{_apiRoot}/models";
            return await GetAsync<ModelResult>(requestUrl);
        }

        public async Task<AnalysisResult> DescribeAsync(string url, int maxCandidates = 1) {
            var requestUrl = $"{_apiRoot}/describe?maxCandidates={maxCandidates}";
            return await PostJsonAsync<AnalysisInDomainResult>(requestUrl, CreateUrlObject(url));

        }

        public async Task<AnalysisResult> DescribeAsync(Stream imageStream, int maxCandidates = 1) {
            var requestUrl = $"{_apiRoot}/describe?maxCandidates={maxCandidates}";
            return await PostStreamAsync<AnalysisResult>(requestUrl, imageStream);
        }

        public async Task<byte[]> GetThumbnailAsync(string url, int width, int height, bool smartCropping = true) {
            var requestUrl =
                $"{_apiRoot}/generateThumbnail?width={width}&height={height}&smartCropping={smartCropping}";
            return await PostJsonAsync<byte[]>(requestUrl, CreateUrlObject(url));
        }

        public async Task<byte[]> GetThumbnailAsync(Stream stream, int width, int height, bool smartCropping = true) {
            var requestUrl =
                $"{_apiRoot}/generateThumbnail?width={width}&height={height}&smartCropping={smartCropping}";
            return await PostStreamAsync<byte[]>(requestUrl, stream);
        }

        public async Task<OcrResults> RecognizeTextAsync(string imageUrl,
                                                         string languageCode = LanguageCodes.AutoDetect, 
                                                         bool detectOrientation = true) {
            var requestUrl =
                $"{_apiRoot}/ocr?language={languageCode}&detectOrientation={detectOrientation}";
            return await PostJsonAsync<OcrResults>(requestUrl, CreateUrlObject(imageUrl));
        }

        public async Task<OcrResults> RecognizeTextAsync(Stream imageStream,
                                                         string languageCode = LanguageCodes.AutoDetect, 
                                                         bool detectOrientation = true) {
            var requestUrl =
                $"{_apiRoot}/ocr?language={languageCode}&detectOrientation={detectOrientation}";
            return await PostStreamAsync<OcrResults>(requestUrl, imageStream);
        }


        public async Task<HandwritingRecognitionOperation> CreateHandwritingRecognitionOperationAsync(string imageUrl) {
            var requestUrl = $"{_apiRoot}/recognizeText?handwriting=true";
            return await PostJsonAsync<HandwritingRecognitionOperation>(requestUrl, CreateUrlObject(imageUrl));
        }

        public async Task<HandwritingRecognitionOperation> CreateHandwritingRecognitionOperationAsync(Stream imageStream) {
            var requestUrl = $"{_apiRoot}/recognizeText?handwriting=true";
            return await PostStreamAsync<HandwritingRecognitionOperation>(requestUrl, imageStream);
        }

        public async Task<HandwritingRecognitionOperationResult> GetHandwritingRecognitionOperationResultAsync(HandwritingRecognitionOperation operation) {
            return await GetAsync<HandwritingRecognitionOperationResult>(operation.Url);
        }

        public async Task<AnalysisResult> GetTagsAsync(Stream imageStream) {
            var requestUrl = $"{_apiRoot}/tag";
            return await PostStreamAsync<AnalysisResult>(requestUrl, imageStream);
        }

        public async Task<AnalysisResult> GetTagsAsync(string imageUrl) {
            var requestUrl = $"{_apiRoot}/tag";
            return await PostJsonAsync<HandwritingRecognitionOperation>(requestUrl, CreateUrlObject(imageUrl));
        }

        private string VisualFeaturesToString(string[] features) {
            return (features == null || features.Length == 0)
                ? ""
                : "visualFeatures=" + string.Join(",", features);
        }

        private string VisualFeaturesToString(IEnumerable<VisualFeature> features) {
            return VisualFeaturesToString(features?.Select(feature => feature.ToString()).ToArray());
        }


        private string DetailsToString(IEnumerable<string> details) {
            return (details == null || !details.Any())
                ? ""
                : "details=" + string.Join(",", details);
        }

        private async Task<TResponse> GetAsync<TResponse>(string url) {
            var get = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await _httpClient.SendAsync(get);
            return await ProcessAsyncResponse<TResponse>(response);
        }

        private async Task<TResponse> PostStreamAsync<TResponse>(string url, Stream requestBody) {
            return await PostAsync<TResponse>(url, new StreamContent(requestBody), "application/octet-stream");
        }

        private async Task<TResponse> PostJsonAsync<TResponse>(string url, object requestBody) {
            var content = new StringContent(JsonConvert.SerializeObject(requestBody, _jsonSettings),
                                            Encoding.UTF8,
                                            "application/json");
            return await PostAsync<TResponse>(url, content, "application/json");
        }

        private async Task<TResponse> PostAsync<TResponse>(string url, HttpContent requestBody, string mediaType) {
            var post = new HttpRequestMessage(HttpMethod.Post, url) {
                Content = requestBody
            };
            post.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            var response = await _httpClient.SendAsync(post);
            return await ProcessAsyncResponse<TResponse>(response);
            
        }
        private async Task<T> ProcessAsyncResponse<T>(HttpResponseMessage response) {
            var mediaType = response.Content.Headers.ContentType.MediaType;
            using (response) {
                if (response.IsSuccessStatusCode) {
                    if (response.Content.Headers.ContentLength > 0) {
                        using (var stream = await response.Content.ReadAsStreamAsync()) {
                            if (stream == null) {
                                return default(T);
                            }
                            if (mediaType == "image/jpeg" || mediaType == "image/png") {
                                using (var ms = new MemoryStream()) {
                                    stream.CopyTo(ms);
                                    return (T) (object) ms.ToArray();
                                }
                            }
                            string message;
                            using (var reader = new StreamReader(stream)) {
                                message = reader.ReadToEnd();
                            }
                            return JsonConvert.DeserializeObject<T>(message, _jsonSettings);
                        }
                    }
                    if (response.Headers.Contains(_operationLocationHeaderName)) {
                        var message = $"{{Url: \"{response.Headers.GetValues(_operationLocationHeaderName).First()}\"}}";
                        return JsonConvert.DeserializeObject<T>(message, _jsonSettings);
                    }
                }
                else {
                    var body = await response.Content.ReadAsStringAsync();
                    var clientError = JsonConvert.DeserializeObject<ClientError>(body);
                    throw new ClientException(clientError, response.StatusCode);
                }
            }
            return default(T);
        }

        private dynamic CreateUrlObject(string url) {
            dynamic requestObject = new ExpandoObject();
            requestObject.url = url;
            return requestObject;
        }
    }
}