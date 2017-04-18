using System;
using CognitiveServices.Emotions;

namespace CognitiveServices.Common {
    public abstract class VideoOperationResult {

        public VideoOperationStatus Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastActionDateTime { get; set; }
        public string Message { get; set; }
        public string ResourceLocation { get; set; }

        protected VideoOperationResult() {

        }

        protected VideoOperationResult(VideoOperationResult other) {
            Status = other.Status;
            CreatedDateTime = other.CreatedDateTime;
            LastActionDateTime = other.LastActionDateTime;
            Message = other.Message;
            ResourceLocation = other.ResourceLocation;
        }
    }
}