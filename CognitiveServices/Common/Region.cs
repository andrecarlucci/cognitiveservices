using CognitiveServices.Vision;
using Newtonsoft.Json;

namespace CognitiveServices.Common {
    public class Region {
        public string BoundingBox { get; set; }
        public Line[] Lines { get; set; }
        [JsonIgnore]
        public Rectangle Rectangle => Rectangle.FromString(this.BoundingBox);
    }
}