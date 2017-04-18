namespace CognitiveServices.Common {
    public class Rectangle {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public static Rectangle FromString(string @string) {
            if (string.IsNullOrWhiteSpace(@string)) {
                return null;
            }
            var box = @string.Split(',');
            if (box.Length != 4) {
                return null;
            }
            if (int.TryParse(box[0], out int left) &&
                int.TryParse(box[1], out int top) &&
                int.TryParse(box[2], out int width) &&
                int.TryParse(box[3], out int height)) {
                return new Rectangle() {
                    Left = left,
                    Height = height,
                    Top = top,
                    Width = width
                };
            }
            return null;
        }

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