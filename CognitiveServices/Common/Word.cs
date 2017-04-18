using Newtonsoft.Json;

namespace CognitiveServices.Common {
    public class Word {
        public string BoundingBox { get; set; }
        public string Text { get; set; }

        [JsonIgnore]
        public Rectangle Rectangle => Rectangle.FromString(this.BoundingBox);
    }
}