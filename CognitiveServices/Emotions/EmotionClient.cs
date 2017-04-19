using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CognitiveServices.Common;
using Newtonsoft.Json;

namespace CognitiveServices.Emotions {
    public class EmotionClient : ServiceClient, IEmotionClient {

        public const string DEFAULT_API_ROOT = "https://westus.api.cognitive.microsoft.com/emotion/v1.0";

        public EmotionClient(string subscriptionKey) : this(subscriptionKey, DEFAULT_API_ROOT) {
            
        }

        public EmotionClient(string subscriptionKey, string apiRoot) {
            ApiRoot = apiRoot?.TrimEnd('/');
            AuthKey = "Ocp-Apim-Subscription-Key";
            AuthValue = subscriptionKey;
        }

        public async Task<Emotion[]> RecognizeAsync(String imageUrl) {
            return await RecognizeAsync(imageUrl, null);
        }

        public async Task<Emotion[]> RecognizeAsync(String imageUrl, Rectangle[] faceRectangles) {
            return await PostAsync<UrlRequest, Emotion[]>(GetRecognizeUrl(faceRectangles), new UrlRequest { Url = imageUrl });
        }

        public async Task<Emotion[]> RecognizeAsync(Stream imageStream) {
            return await RecognizeAsync(imageStream, null);
        }

        public async Task<Emotion[]> RecognizeAsync(Stream imageStream, Rectangle[] faceRectangles) {
            return await PostAsync<Stream, Emotion[]>(GetRecognizeUrl(faceRectangles), imageStream);
        }

        private string GetRecognizeUrl(Rectangle[] faceRectangles) {
            var builder = new StringBuilder("/recognize");
            if (faceRectangles != null && faceRectangles.Length > 0) {
                builder.Append("?faceRectangles=");
                builder.Append(string.Join(";", faceRectangles.Select(r => $"{r.Left},{r.Top},{r.Width},{r.Height}")));
            }
            return builder.ToString();
        }

        public async Task<VideoEmotionRecognitionOperation> RecognizeInVideoAsync(Stream videoStream) {
            var operation = await PostAsync<Stream, VideoEmotionRecognitionOperation>(@"/recognizeInVideo", videoStream);
            return operation;
        }

        public async Task<VideoEmotionRecognitionOperation> RecognizeInVideoAsync(byte[] videoBytes) {
            using (var videoStream = new MemoryStream(videoBytes)) {
                var operation = await PostAsync<Stream, VideoEmotionRecognitionOperation>(@"/recognizeInVideo", videoStream);
                return operation;
            }
        }

        public async Task<VideoEmotionRecognitionOperation> RecognizeInVideoAsync(string videoUrl) {
            var operation = await PostAsync<UrlRequest, VideoEmotionRecognitionOperation>(@"/recognizeInVideo", new UrlRequest { Url = videoUrl });
            return operation;
        }

        public async Task<VideoOperationResult> GetOperationResultAsync(VideoEmotionRecognitionOperation operation) {
            var wireResult = await GetAsync<string, VideoOperationInfoResult<string>>(operation.Url, null);
            if (wireResult.Status != VideoOperationStatus.Succeeded) {
                return wireResult;
            }
            var aggregateResult = JsonConvert.DeserializeObject<VideoAggregateRecognitionResult>(wireResult.ProcessingResult);
            return new VideoOperationInfoResult<VideoAggregateRecognitionResult>(wireResult, aggregateResult);
        }
    }
}