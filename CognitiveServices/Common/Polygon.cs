using System.Collections.Generic;

namespace CognitiveServices.Common {
    public class Polygon {
        public Polygon() {
            Points = new List<Point>();
        }
        public List<Point> Points { get; set; }

        public static Polygon FromArray(int[] boundingBox) {
            var polygon = new Polygon();
            for (var i = 0; i + 1 < boundingBox.Length; i += 2) {
                polygon.Points.Add(new Point() { X = boundingBox[i], Y = boundingBox[i + 1] });
            }
            return polygon;
        }
    }
}