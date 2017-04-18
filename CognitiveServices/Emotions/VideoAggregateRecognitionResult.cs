using System;
using CognitiveServices.Common;

namespace CognitiveServices.Emotions {
    public class VideoAggregateRecognitionResult : VideoResultBase {
        public VideoFace[] FacesDetected { get; set; }
        public VideoFragment<VideoAggregateEvent>[] Fragments { get; set; }
    }
}