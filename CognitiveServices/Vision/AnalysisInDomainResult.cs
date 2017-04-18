using System;

namespace CognitiveServices.Vision {
    public class AnalysisInDomainResult {
        public Guid RequestId { get; set; }
        public Metadata Metadata { get; set; }
        public object Result { get; set; }
    }
}