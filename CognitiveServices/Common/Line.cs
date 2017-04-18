using Newtonsoft.Json;

namespace CognitiveServices.Common {
    public class Line {
        public string BoundingBox { get; set; }
        public Word[] Words { get; set; }
     
        [JsonIgnore]
        public Rectangle Rectangle => Rectangle.FromString(this.BoundingBox);
    }
}