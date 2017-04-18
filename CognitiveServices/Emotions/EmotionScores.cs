using System.Collections.Generic;
using System.Linq;

namespace CognitiveServices.Emotions {
    public class EmotionScores {
        public float Anger { get; set; }
        public float Contempt { get; set; }
        public float Disgust { get; set; }
        public float Fear { get; set; }
        public float Happiness { get; set; }
        public float Neutral { get; set; }
        public float Sadness { get; set; }
        public float Surprise { get; set; }

        public IEnumerable<KeyValuePair<string, float>> ToRankedList() {
            return new Dictionary<string, float> {
                    {"Anger", Anger},
                    {"Contempt", Contempt},
                    {"Disgust", Disgust},
                    {"Fear", Fear},
                    {"Happiness", Happiness},
                    {"Neutral", Neutral},
                    {"Sadness", Sadness},
                    {"Surprise", Surprise}
                }
                .OrderByDescending(kv => kv.Value)
                .ThenBy(kv => kv.Key)
                .ToList();
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        protected bool Equals(EmotionScores other) {
            return Anger.Equals(other.Anger) && 
                   Contempt.Equals(other.Contempt) && 
                   Disgust.Equals(other.Disgust) &&
                   Fear.Equals(other.Fear) && 
                   Happiness.Equals(other.Happiness) && 
                   Neutral.Equals(other.Neutral) &&
                   Sadness.Equals(other.Sadness) && 
                   Surprise.Equals(other.Surprise);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = Anger.GetHashCode();
                hashCode = (hashCode * 397) ^ Contempt.GetHashCode();
                hashCode = (hashCode * 397) ^ Disgust.GetHashCode();
                hashCode = (hashCode * 397) ^ Fear.GetHashCode();
                hashCode = (hashCode * 397) ^ Happiness.GetHashCode();
                hashCode = (hashCode * 397) ^ Neutral.GetHashCode();
                hashCode = (hashCode * 397) ^ Sadness.GetHashCode();
                hashCode = (hashCode * 397) ^ Surprise.GetHashCode();
                return hashCode;
            }
        }
    }
}