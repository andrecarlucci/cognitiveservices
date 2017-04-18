namespace CognitiveServices.Common {
    public class VideoOperationInfoResult<T> : VideoOperationResult {
        public T ProcessingResult { get; set; }

        public VideoOperationInfoResult() {
        }

        public VideoOperationInfoResult(VideoOperationResult other, T processingResult) : base(other) {
            ProcessingResult = processingResult;
        }
    }
}