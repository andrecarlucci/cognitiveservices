//using System;
//using System.Collections.Generic;
//using System.Dynamic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;

//namespace CognitiveServices.Vision {
//    public class VisionServiceClient : IVisionServiceClient {
//        private const string DEFAULT_API_ROOT = "https://westus.api.cognitive.microsoft.com/vision/v1.0";
//        private const int DEFAULT_TIMEOUT = 2 * 60 * 1000; // 2 minutes timeout
//        private const string AnalyzeQuery = "analyze";
//        private const string DescribeQuery = "describe";
//        private const string ModelsPart = "models";
//        private const string ThumbnailsQuery = "generateThumbnail";
//        private const string _maxCandidatesName = "maxCandidates";
//        private const string _subscriptionKeyName = "subscription-key";
//        private CamelCasePropertyNamesContractResolver _defaultResolver = new CamelCasePropertyNamesContractResolver();

//        private string _subscriptionKey;
//        protected virtual string ServiceHost { get; }
//        protected virtual int DefaultTimeout => DEFAULT_TIMEOUT;

//        public VisionServiceClient(string subscriptionKey) : this(subscriptionKey, DEFAULT_API_ROOT) {
//        }

//        public VisionServiceClient(string subscriptionKey, string apiRoot) {
//            ServiceHost = apiRoot?.TrimEnd('/');
//            _subscriptionKey = subscriptionKey;
//        }

//        public async Task<AnalysisResult> AnalyzeImageAsync(string url,
//                                                            IEnumerable<VisualFeature> visualFeatures = null, 
//                                                            IEnumerable<string> details = null) {
//            dynamic request = new ExpandoObject();
//            request.url = url;
//            return await AnalyzeImageAsync<ExpandoObject>(request, visualFeatures, details).ConfigureAwait(false);
//        }

//        public async Task<AnalysisResult> AnalyzeImageAsync(Stream imageStream,
//                                                            IEnumerable<VisualFeature> visualFeatures = null, 
//                                                            IEnumerable<string> details = null) {
//            return await AnalyzeImageAsync<Stream>(imageStream, visualFeatures, details).ConfigureAwait(false);
//        }

//        public async Task<AnalysisInDomainResult> AnalyzeImageInDomainAsync(string url, Model model) {
//            return await AnalyzeImageInDomainAsync(url, model.Name).ConfigureAwait(false);
//        }

//        public async Task<AnalysisInDomainResult> AnalyzeImageInDomainAsync(Stream imageStream, Model model) {
//            return await AnalyzeImageInDomainAsync(imageStream, model.Name).ConfigureAwait(false);
//        }

//        public async Task<AnalysisInDomainResult> AnalyzeImageInDomainAsync(string url, string modelName) {
//            var requestUrl = $"{ServiceHost}/{ModelsPart}/{modelName}/{AnalyzeQuery}?{_subscriptionKeyName}={_subscriptionKey}";
//            var request = WebRequest.Create(requestUrl);

//            dynamic requestObject = new ExpandoObject();
//            requestObject.url = url;

//            return await this.SendAsync<ExpandoObject, AnalysisInDomainResult>("POST", requestObject, request)
//                    .ConfigureAwait(false);
//        }

//        public async Task<AnalysisInDomainResult> AnalyzeImageInDomainAsync(Stream imageStream, string modelName) {
//            var requestUrl = $"{ServiceHost}/{ModelsPart}/{modelName}/{AnalyzeQuery}?{_subscriptionKeyName}={_subscriptionKey}";
//            var request = WebRequest.Create(requestUrl);

//            return await SendAsync<Stream, AnalysisInDomainResult>("POST", imageStream, request).ConfigureAwait(false);
//        }


//        public async Task<ModelResult> ListModelsAsync() {
//            var requestUrl = $"{ServiceHost}/{ModelsPart}?{_subscriptionKeyName}={_subscriptionKey}";
//            var request = WebRequest.Create(requestUrl);
//            return await GetAsync<ModelResult>("GET", request).ConfigureAwait(false);
//        }

//        public async Task<AnalysisResult> DescribeAsync(string url, int maxCandidates = 1) {
//            var requestUrl = $"{ServiceHost}/{DescribeQuery}?{_maxCandidatesName}={maxCandidates}&{_subscriptionKeyName}={_subscriptionKey}";
//            var request = WebRequest.Create(requestUrl);

//            dynamic requestObject = new ExpandoObject();
//            requestObject.url = url;

//            return await this.SendAsync<ExpandoObject, AnalysisResult>("POST", requestObject, request)
//                .ConfigureAwait(false);
//        }


//        public async Task<AnalysisResult> DescribeAsync(Stream imageStream, int maxCandidates = 1) {
//            var requestUrl = string.Format("{0}/{1}?{2}={3}&{4}={5}", ServiceHost, DescribeQuery, _maxCandidatesName,
//                maxCandidates, _subscriptionKeyName, _subscriptionKey);
//            var request = WebRequest.Create(requestUrl);

//            return await SendAsync<Stream, AnalysisResult>("POST", imageStream, request).ConfigureAwait(false);
//        }


//        public async Task<byte[]> GetThumbnailAsync(string url, int width, int height, bool smartCropping = true) {
//            var requestUrl = string.Format("{0}/{1}?width={2}&height={3}&smartCropping={4}&{5}={6}", ServiceHost,
//                ThumbnailsQuery, width, height, smartCropping, _subscriptionKeyName, _subscriptionKey);
//            var request = WebRequest.Create(requestUrl);

//            dynamic requestObject = new ExpandoObject();
//            requestObject.url = url;

//            return await this.SendAsync<ExpandoObject, byte[]>("POST", requestObject, request).ConfigureAwait(false);
//        }


//        public async Task<byte[]> GetThumbnailAsync(Stream stream, int width, int height, bool smartCropping = true) {
//            var requestUrl = string.Format("{0}/{1}?width={2}&height={3}&smartCropping={4}&{5}={6}", ServiceHost,
//                ThumbnailsQuery, width, height, smartCropping, _subscriptionKeyName, _subscriptionKey);
//            var request = WebRequest.Create(requestUrl);

//            return await SendAsync<Stream, byte[]>("POST", stream, request).ConfigureAwait(false);
//        }


//        public async Task<OcrResults> RecognizeTextAsync(string imageUrl,
//            string languageCode = LanguageCodes.AutoDetect, bool detectOrientation = true) {
//            var requestUrl = string.Format("{0}/ocr?language={1}&detectOrientation={2}&{3}={4}", ServiceHost,
//                languageCode, detectOrientation, _subscriptionKeyName, _subscriptionKey);
//            var request = WebRequest.Create(requestUrl);

//            dynamic requestObject = new ExpandoObject();
//            requestObject.url = imageUrl;

//            return await this.SendAsync<ExpandoObject, OcrResults>("POST", requestObject, request)
//                .ConfigureAwait(false);
//        }


//        public async Task<OcrResults> RecognizeTextAsync(Stream imageStream,
//            string languageCode = LanguageCodes.AutoDetect, bool detectOrientation = true) {
//            var requestUrl = string.Format("{0}/ocr?language={1}&detectOrientation={2}&{3}={4}", ServiceHost,
//                languageCode, detectOrientation, _subscriptionKeyName, _subscriptionKey);
//            var request = WebRequest.Create(requestUrl);

//            return await SendAsync<Stream, OcrResults>("POST", imageStream, request).ConfigureAwait(false);
//        }


//        public async Task<HandwritingRecognitionOperation> CreateHandwritingRecognitionOperationAsync(string imageUrl) {
//            var requestUrl = string.Format("{0}/recognizeText?handwriting=true&{1}={2}", ServiceHost,
//                _subscriptionKeyName, _subscriptionKey);
//            var request = WebRequest.Create(requestUrl);

//            dynamic requestObject = new ExpandoObject();
//            requestObject.url = imageUrl;

//            return await this.SendAsync<ExpandoObject, HandwritingRecognitionOperation>("POST", requestObject, request)
//                .ConfigureAwait(false);
//        }


//        public async Task<HandwritingRecognitionOperation> CreateHandwritingRecognitionOperationAsync(
//            Stream imageStream) {
//            var requestUrl = string.Format("{0}/recognizeText?handwriting=true&{1}={2}", ServiceHost,
//                _subscriptionKeyName, _subscriptionKey);
//            var request = WebRequest.Create(requestUrl);

//            return await SendAsync<Stream, HandwritingRecognitionOperation>("POST", imageStream, request)
//                .ConfigureAwait(false);
//        }


//        public async Task<HandwritingRecognitionOperationResult> GetHandwritingRecognitionOperationResultAsync(
//            HandwritingRecognitionOperation opeartion) {
//            var requestUrl = string.Format("{0}?{1}={2}", opeartion.Url, _subscriptionKeyName, _subscriptionKey);
//            var request = WebRequest.Create(requestUrl);

//            return await GetAsync<HandwritingRecognitionOperationResult>("Get", request).ConfigureAwait(false);
//        }


//        public async Task<AnalysisResult> GetTagsAsync(Stream imageStream) {
//            var requestUrl = string.Format("{0}/tag?{1}={2}", ServiceHost, _subscriptionKeyName, _subscriptionKey);
//            var request = WebRequest.Create(requestUrl);

//            return await SendAsync<Stream, AnalysisResult>("POST", imageStream, request).ConfigureAwait(false);
//        }


//        public async Task<AnalysisResult> GetTagsAsync(string imageUrl) {
//            var requestUrl = string.Format("{0}/tag?{1}={2}", ServiceHost, _subscriptionKeyName, _subscriptionKey);
//            var request = WebRequest.Create(requestUrl);

//            dynamic requestObject = new ExpandoObject();
//            requestObject.url = imageUrl;

//            return await this.SendAsync<ExpandoObject, AnalysisResult>("POST", requestObject, request)
//                .ConfigureAwait(false);
//        }


//        private async Task<AnalysisResult> AnalyzeImageAsync<T>(T body, IEnumerable<VisualFeature> visualFeatures,
//            IEnumerable<string> details) {
//            var requestUrl = new StringBuilder(ServiceHost).Append('/').Append(AnalyzeQuery).Append("?");
//            requestUrl.Append(string.Join("&", new List<string> {
//                    VisualFeaturesToString(visualFeatures),
//                    DetailsToString(details),
//                    _subscriptionKeyName + "=" + _subscriptionKey
//                }
//                .Where(s => !string.IsNullOrEmpty(s))));

//            var request = WebRequest.Create(requestUrl.ToString());

//            return await SendAsync<T, AnalysisResult>("POST", body, request).ConfigureAwait(false);
//        }


//        private string VisualFeaturesToString(string[] features) {
//            return (features == null || features.Length == 0)
//                ? ""
//                : "visualFeatures=" + string.Join(",", features);
//        }


//        private string VisualFeaturesToString(IEnumerable<VisualFeature> features) {
//            return VisualFeaturesToString(features?.Select(feature => feature.ToString()).ToArray());
//        }


//        private string DetailsToString(IEnumerable<string> details) {
//            return (details == null || details.Count() == 0)
//                ? ""
//                : "details=" + string.Join(",", details);
//        }

//        private async Task<TResponse> GetAsync<TResponse>(string method, WebRequest request,
//            Action<WebRequest> setHeadersCallback = null) {
//            if (request == null) new ArgumentNullException("request");

//            try {
//                request.Method = method;
//                if (null == setHeadersCallback) SetCommonHeaders(request);
//                else setHeadersCallback(request);

//                var getResponseAsync = Task.Factory.FromAsync<WebResponse>(
//                    request.BeginGetResponse,
//                    request.EndGetResponse,
//                    null);

//                await Task.WhenAny(getResponseAsync, Task.Delay(DefaultTimeout)).ConfigureAwait(false);

//                //Abort request if timeout has expired
//                if (!getResponseAsync.IsCompleted) request.Abort();

//                return ProcessAsyncResponse<TResponse>(getResponseAsync.Result as HttpWebResponse);
//            }
//            catch (AggregateException ae) {
//                ae.Handle(e => {
//                    HandleException(e);
//                    return true;
//                });
//                return default(TResponse);
//            }
//            catch (Exception e) {
//                HandleException(e);
//                return default(TResponse);
//            }
//        }


//        private async Task<TResponse> SendAsync<TRequest, TResponse>(string method, TRequest requestBody,
//            WebRequest request, Action<WebRequest> setHeadersCallback = null) {
//            try {
//                if (request == null) throw new ArgumentNullException("request");

//                request.Method = method;
//                if (null == setHeadersCallback) SetCommonHeaders(request);
//                else setHeadersCallback(request);

//                if (requestBody is Stream) request.ContentType = "application/octet-stream";

//                var asyncState = new WebRequestAsyncState {
//                    RequestBytes = SerializeRequestBody(requestBody),
//                    WebRequest = (HttpWebRequest) request
//                };

//                var continueRequestAsyncState = await Task.Factory.FromAsync<Stream>(
//                        asyncState.WebRequest.BeginGetRequestStream,
//                        asyncState.WebRequest.EndGetRequestStream,
//                        asyncState,
//                        TaskCreationOptions.None)
//                    .ContinueWith(
//                        task => {
//                            var requestAsyncState = (WebRequestAsyncState) task.AsyncState;
//                            if (requestBody != null)
//                                using (var requestStream = task.Result) {
//                                    if (requestBody is Stream) (requestBody as Stream).CopyTo(requestStream);
//                                    else
//                                        requestStream.Write(requestAsyncState.RequestBytes, 0,
//                                            requestAsyncState.RequestBytes.Length);
//                                }

//                            return requestAsyncState;
//                        })
//                    .ConfigureAwait(false);

//                var continueWebRequest = continueRequestAsyncState.WebRequest;
//                var getResponseAsync = Task.Factory.FromAsync<WebResponse>(
//                    (Func<AsyncCallback, object, IAsyncResult>) continueWebRequest.BeginGetResponse,
//                    continueWebRequest.EndGetResponse,
//                    continueRequestAsyncState);

//                await Task.WhenAny(getResponseAsync, Task.Delay(DefaultTimeout)).ConfigureAwait(false);

//                //Abort request if timeout has expired
//                if (!getResponseAsync.IsCompleted) request.Abort();

//                return ProcessAsyncResponse<TResponse>(getResponseAsync.Result as HttpWebResponse);
//            }
//            catch (AggregateException ae) {
//                ae.Handle(e => {
//                    HandleException(e);
//                    return true;
//                });
//                return default(TResponse);
//            }
//            catch (Exception e) {
//                HandleException(e);
//                return default(TResponse);
//            }
//        }


//        private T ProcessAsyncResponse<T>(HttpWebResponse webResponse) {
//            using (webResponse) {
//                if (webResponse.StatusCode == HttpStatusCode.OK ||
//                    webResponse.StatusCode == HttpStatusCode.Accepted ||
//                    webResponse.StatusCode == HttpStatusCode.Created)
//                    if (webResponse.ContentLength != 0) {
//                        using (var stream = webResponse.GetResponseStream()) {
//                            if (stream != null)
//                                if (webResponse.ContentType == "image/jpeg" ||
//                                    webResponse.ContentType == "image/png") {
//                                    using (var ms = new MemoryStream()) {
//                                        stream.CopyTo(ms);
//                                        return (T) (object) ms.ToArray();
//                                    }
//                                }
//                                else {
//                                    var message = string.Empty;
//                                    using (var reader = new StreamReader(stream)) {
//                                        message = reader.ReadToEnd();
//                                    }

//                                    JsonSerializerSettings settings = new JsonSerializerSettings();
//                                    settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
//                                    settings.NullValueHandling = NullValueHandling.Ignore;
//                                    settings.ContractResolver = _defaultResolver;

//                                    return JsonConvert.DeserializeObject<T>(message, settings);
//                                }
//                        }
//                    }
//                    else {
//                        if (webResponse.Headers.AllKeys.Contains("Operation-Location")) {
//                            var message = string.Format("{{Url: \"{0}\"}}", webResponse.Headers["Operation-Location"]);

//                            JsonSerializerSettings settings = new JsonSerializerSettings();
//                            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
//                            settings.NullValueHandling = NullValueHandling.Ignore;
//                            settings.ContractResolver = _defaultResolver;

//                            return JsonConvert.DeserializeObject<T>(message, settings);
//                        }
//                    }
//            }

//            return default(T);
//        }


//        private void SetCommonHeaders(WebRequest request) {
//            request.ContentType = "application/json";
//            request.Headers[HttpRequestHeader.Authorization] = "Basic ZTkwNTE2ZmQ4NThlNDVjMmFhNDMzMjRlZjBlOThlN2E=";
//        }


//        private byte[] SerializeRequestBody<T>(T requestBody) {
//            if (requestBody == null || requestBody is Stream) {
//                return null;
//            }
//            JsonSerializerSettings settings = new JsonSerializerSettings();
//            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
//            settings.ContractResolver = _defaultResolver;

//            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestBody, settings));
//        }


//        private void HandleException(Exception exception) {
//            WebException webException = exception as WebException;
//            if (webException != null && webException.Response != null)
//                if (webException.Response.ContentType.ToLower().Contains("application/json")) {
//                    Stream stream = null;

//                    try {
//                        stream = webException.Response.GetResponseStream();
//                        if (stream != null) {
//                            string errorObjectString;
//                            using (var reader = new StreamReader(stream)) {
//                                stream = null;
//                                errorObjectString = reader.ReadToEnd();
//                            }

//                            ClientError errorCollection = JsonConvert.DeserializeObject<ClientError>(errorObjectString);

//                            // HandwritingOcr error message use the latest format, so add the logic to handle this issue.
//                            if (errorCollection.Code == null && errorCollection.Message == null) {
//                                var errorType = new {Error = new ClientError()};
//                                var errorObj = JsonConvert.DeserializeAnonymousType(errorObjectString, errorType);
//                                errorCollection = errorObj.Error;
//                            }

//                            if (errorCollection != null)
//                                throw new ClientException {
//                                    Error = errorCollection
//                                };
//                        }
//                    }
//                    finally {
//                        if (stream != null) stream.Dispose();
//                    }
//                }

//            throw exception;
//        }

//        internal class WebRequestAsyncState {
//            public byte[] RequestBytes { get; set; }
//            public HttpWebRequest WebRequest { get; set; }
//            public object State { get; set; }
//        }
//    }
//}