using System.IO;
using System.Threading.Tasks;
using CognitiveServices.Common;

namespace CognitiveServices.Emotions {
    public interface IEmotionClient {
        Task<Emotion[]> RecognizeAsync(string imageUrl);
        Task<Emotion[]> RecognizeAsync(string imageUrl, Rectangle[] faceRectangles);
        Task<Emotion[]> RecognizeAsync(Stream imageStream);
        Task<Emotion[]> RecognizeAsync(Stream imageStream, Rectangle[] faceRectangles);
        Task<VideoEmotionRecognitionOperation> RecognizeInVideoAsync(Stream videoStream);
        Task<VideoEmotionRecognitionOperation> RecognizeInVideoAsync(byte[] videoBytes);
        Task<VideoEmotionRecognitionOperation> RecognizeInVideoAsync(string videoUrl);
        Task<VideoOperationResult> GetOperationResultAsync(VideoEmotionRecognitionOperation operation);
    }
}
