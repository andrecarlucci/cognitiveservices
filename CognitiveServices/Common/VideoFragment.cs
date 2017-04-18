using System;

namespace CognitiveServices.Common {
    public class VideoFragment<T> {
        public Int64 Start { get; set; }
        public Int64 Duration { get; set; }
        public Int64? Interval { get; set; }
        public T[][] Events { get; set; }
    }
}