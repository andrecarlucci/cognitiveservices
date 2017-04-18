using System;

namespace CognitiveServices {
    public class ClientError {
        public string Code { get; set; }
        public string Message { get; set; }
        public Guid RequestId { get; set;}
    }
}