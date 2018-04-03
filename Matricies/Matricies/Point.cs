
namespace Matricies {
    /// <summary>
    /// A Point on an (x,y) axis
    /// </summary>
    public class Point {
        private double x, y;
        /// <summary>
        /// Creates a new Point
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        public Point(double x, double y) {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// Gets the X coordinate
        /// </summary>
        /// <returns>The X coordinate</returns>
        public double GetX() {
            return x;
        }
        /// <summary>
        /// Gets the Y coordinate
        /// </summary>
        /// <returns>The Y coordinate</returns>
        public double GetY() {
            return y;
        }
        /// <summary>
        /// Converts the Point to a double[]
        /// </summary>
        /// <returns>A double[] representation of the Point</returns>
        public double[] AsArray() {
            return new double[] { x, y };
        }
        /// <summary>
        /// Changes the X coordinate
        /// </summary>
        /// <param name="x">The new X coordinate</param>
        public void SetX(double x) {
            this.x = x;
        }
        /// <summary>
        /// Changes the Y coordinate
        /// </summary>
        /// <param name="y">The new Y coordinate</param>
        public void SetY(double y) {
            this.y = y;
        }
        /// <summary>
        /// Puts the Point in the form: (x,y)
        /// </summary>
        /// <returns>(x,y)</returns>
        public override string ToString() {
            return string.Format("({0},{1})", x, y);
        }
        /// <summary>
        /// Subtracts Point b from Point a
        /// </summary>
        /// <param name="a">The first Point</param>
        /// <param name="b">The second Point</param>
        /// <returns></returns>
        public static Point operator- (Point a, Point b) {
            return new Point(a.GetX() - b.GetX(), a.GetY() - b.GetY());
        }
        /// <summary>
        /// Adds Point a with Point b
        /// </summary>
        /// <param name="a">The first Point</param>
        /// <param name="b">The second Point</param>
        /// <returns></returns>
        public static Point operator+ (Point a, Point b) {
            return new Point(a.GetX() + b.GetX(), a.GetY() + b.GetY());
        }
        /// <summary>
        /// Multiplies Point a by a scalar
        /// </summary>
        /// <param name="a">The Point</param>
        /// <param name="scalar">The Scalar</param>
        /// <returns></returns>
        public static Point operator* (Point a, double scalar) {
            return new Point(a.GetX() * scalar, a.GetY() * scalar);
        }
        /// <summary>
        /// Divides Point a by a scalar
        /// </summary>
        /// <param name="a">The Point</param>
        /// <param name="scalar">The Scalar</param>
        /// <returns></returns>
        public static Point operator/ (Point a, double scalar) {
            return new Point(a.GetX() / scalar, a.GetY() / scalar);
        }
    }
}
