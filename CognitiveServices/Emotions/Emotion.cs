using CognitiveServices.Common;

namespace CognitiveServices.Emotions {
    public class Emotion {
        public Rectangle FaceRectangle { get; set; }
        public EmotionScores Scores { get; set; }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        protected bool Equals(Emotion other) {
            return Equals(FaceRectangle, other.FaceRectangle) && Equals(Scores, other.Scores);
        }

        public override int GetHashCode() {
            unchecked {
                return ((FaceRectangle != null ? FaceRectangle.GetHashCode() : 0) * 397) ^ 
                        (Scores != null ? Scores.GetHashCode() : 0);
            }
        }
    }
}