namespace CognitiveServices.Common {
    public class Rectangle {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public override bool Equals(object o) {
            var other = o as Rectangle;
            if (other == null) return false;
            return Left == other.Left &&
                   Top == other.Top &&
                   Width == other.Width &&
                   Height == other.Height;
        }

        public override int GetHashCode() {
            return Left.GetHashCode() ^ Top.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();
        }
    }
}