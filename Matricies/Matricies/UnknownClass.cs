using System.Collections.Generic;

namespace Matricies {
    /// <summary>
    /// An unknown class made up of a List of Points
    /// </summary>
    class UnknownClass {
        private List<Point> points;
        /// <summary>
        /// Creates a new Class made of a List of Points
        /// </summary>
        /// <param name="points"></param>
        public UnknownClass(List<Point> points) {
            this.points = points;
        }
        /// <summary>
        /// Gets the List of Points
        /// </summary>
        /// <returns>The List of Points</returns>
        public List<Point> GetPoints() {
            return points;
        }
        /// <summary>
        /// Adds a Point to the Point List
        /// </summary>
        /// <param name="point">The Point to add</param>
        public void AddPoint(Point point) {
            points.Add(point);
        }
        /// <summary>
        /// Gets the Average of all the Points
        /// </summary>
        /// <returns>The Average Point</returns>
        public Point GetMean() {
            Point avg = new Point(0, 0);
            foreach(Point p in points) {
                avg += p;
            }
            return avg / points.Count;
        }
        /// <summary>
        /// Subtracts the Average Point from each Point in the List
        /// </summary>
        /// <returns>A new List of Points with the mean Point subtracted from each Point</returns>
        public List<Point> SubtractMeanVector() {
            Point mean = GetMean();
            List<Point> result = new List<Point>(points.Count);
            for(int i = 0; i < points.Count; i++) {
                result.Add(points[i] - mean);
            }
            return result;
        }
        /// <summary>
        /// Creates an array of square matricies out of the List of Points
        /// </summary>
        /// <returns>An array of square matricies</returns>
        public Matrix[] GetSquareMatricies() {
            List<Point> vectors = SubtractMeanVector();
            Matrix[] result = new Matrix[points.Count];
            for(int i = 0; i < points.Count; i++) {
                result[i] = new Matrix(vectors[i]);
                result[i] = result[i].GetTranspose() * result[i];
            }
            return result;
        }
        /// <summary>
        /// Calculates the Covariance Matrix for the class
        /// </summary>
        /// <returns>The Covariance Matrix</returns>
        public Matrix GetCovarianceMatrix() {
            Matrix[] arr = GetSquareMatricies();
            Matrix result = new Matrix(arr[0].GetWidth(), arr[0].GetHeight());
            foreach(Matrix m in arr) {
                result = result + m;
            }
            return result * (1.0 / (double)points.Count);
        }
    }
}
