using System;

namespace CognitiveServices.Common {
    public class VideoResultBase {
        public int Version { get; set; }
        public double Timescale { get; set; }
        public Int64 Offset { get; set; }
        public double Framerate { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int? Rotation { get; set; }
    }
}