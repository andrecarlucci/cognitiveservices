using CognitiveServices.Common;
using Newtonsoft.Json;

namespace CognitiveServices.Vision {
    public class HandwritingTextLine {
        public int[] BoundingBox { get; set; }
        public HandwritingTextWord[] Words { get; set; }

        [JsonIgnore]
        public Polygon Polygon => Polygon.FromArray(this.BoundingBox);
    }
}