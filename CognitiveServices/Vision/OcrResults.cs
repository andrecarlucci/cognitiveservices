using CognitiveServices.Common;

namespace CognitiveServices.Vision {
    public class OcrResults {
        public string Language { get; set; }
        public double? TextAngle { get; set; }
        public string Orientation { get; set; }
        public Region[] Regions { get; set; }
    }
}