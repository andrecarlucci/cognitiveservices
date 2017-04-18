namespace CognitiveServices.Emotions {
    public class VideoAggregateEvent {
        public EmotionScores WindowFaceDistribution { get; set; }
        public EmotionScores WindowMeanScores { get; set; }
    }
}