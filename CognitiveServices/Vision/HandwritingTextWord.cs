using CognitiveServices.Common;
using Newtonsoft.Json;

namespace CognitiveServices.Vision {
    public class HandwritingTextWord {
        public int[] BoundingBox { get; set; }
        public string Text { get; set; }
        [JsonIgnore]
        public Polygon Polygon => Polygon.FromArray(this.BoundingBox);
    }
}