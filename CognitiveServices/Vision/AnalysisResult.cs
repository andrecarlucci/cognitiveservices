using System;
using CognitiveServices.Common;

namespace CognitiveServices.Vision {
    public class AnalysisResult {
        public Guid RequestId { get; set; }
        public Metadata Metadata { get; set; }
        public ImageType ImageType { get; set; }
        public Color Color { get; set; }
        public Adult Adult { get; set; }
        public Category[] Categories { get; set; }
        public Face[] Faces { get; set; }
        public Tag[] Tags { get; set; }
        public Description Description { get; set; }
    }
}