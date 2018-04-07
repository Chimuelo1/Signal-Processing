using System;
using System.Collections.Generic;

namespace Matricies {
    /// <summary>
    /// A Matrix composed of an array of arrays with doubles as values
    /// </summary>
    public class Matrix {
        private double[][] matrix;
        private int width, height;
        /// <summary>
        /// Creates a blank Matrix with 0 values of the designated size
        /// </summary>
        /// <param name="rows">The number of rows in the new Matrix</param>
        /// <param name="columns">The number of columns in the new Matrix</param>
        public Matrix(int rows, int columns) {
            width = columns;
            height = rows;
            matrix = new double[rows][];
            for (int i = 0; i < rows; i++) {
                matrix[i] = new double[columns];
                for (int j = 0; j < columns; j++) {
                    matrix[i][j] = 0.0;
                }
            }
        }
        /// <summary>
        /// Creates a Matrix based arount an (x,y) Point
        /// </summary>
        /// <param name="p">The Point</param>
        public Matrix(Point p) {
            width = 2;
            height = 1;
            matrix = new double[1][];
            matrix[0] = new double[2];
            matrix[0][0] = p.GetX();
            matrix[0][1] = p.GetY();
        }
        /// <summary>
        /// Gets an array of the desired row
        /// </summary>
        /// <param name="index">The index of the row</param>
        /// <returns>An array containing the values of the row</returns>
        public double[] GetRow(int index) {
            return matrix[index];
        }
        /// <summary>
        /// Gets an array of the desired column
        /// </summary>
        /// <param name="index">The index of the column</param>
        /// <returns>An array containing the values of the column</returns>
        public double[] GetColumn(int index) {
            double[] column = new double[height];
            for (int i = 0; i < height; i++) {
                column[i] = matrix[i][index];
            }
            return column;
        }
        /// <summary>
        /// Gets the height of the Matrix
        /// </summary>
        /// <returns>The height of the Matrix</returns>
        public int GetHeight() {
            return height;
        }
        /// <summary>
        /// Gets the width of the Matrix
        /// </summary>
        /// <returns>The width of the Matrix</returns>
        public int GetWidth() {
            return width;
        }
        /// <summary>
        /// Prints the Matrix to Console
        /// </summary>
        public void Print() {
            foreach (double[] arr in matrix) {
                for (int i = 0; i < arr.Length; i++) {
                    if (i < arr.Length - 1)
                        Console.Write(arr[i] + ", ");
                    else
                        Console.WriteLine(arr[i]);
                }
            }
        }
        /// <summary>
        /// Writes this Matrix to a CSV file
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        public void WriteToFile(string fileName) {
            using(System.IO.StreamWriter file = new System.IO.StreamWriter(fileName)) {
                foreach(double[] arr in matrix) {
                    foreach(double d in arr) {
                        file.Write(d + ",");
                    }
                    file.WriteLine();
                }
            }

        }
        /// <summary>
        /// Overrides the Matrix[i] operator to allow acces to the Matrix's values 
        /// </summary>
        /// <param name="i">The index to get</param>
        /// <returns>The array at the given index</returns>
        public double[] this[int i] {
            get { return matrix[i]; }
            set { matrix[i] = value; }
        }
        /// <summary>
        /// Creates a deep copy of this Matrix
        /// </summary>
        /// <returns>A copy of the Matrix</returns>
        public Matrix CopyOf() {
            Matrix result = new Matrix(height, width);
            for (int i = 0; i < height; i++) {
                for(int j = 0; j < width; j++) {
                    result[i][j] = this[i][j];
                }
            }
            return result;
        }
        /// <summary>
        /// Checks if the Matrix is a square Matrix
        /// </summary>
        /// <returns>True if the Matrix is square, false otherwise</returns>
        public bool IsSquare() {
            return width == height;
        }
        /// <summary>
        /// Gets the identity Matrix of the same size as the current Matrix
        /// </summary>
        /// <returns>The identity Matrix</returns>
        public Matrix GetIdentityMatrix() {
            if(IsSquare()) {
                Matrix result = new Matrix(height, width);
                for(int i = 0; i < width; i++) {
                    result[i][i] = 1;
                }
                return result;
            }
            else {
                throw new InvalidOperationException("Matrix must be square");
            }
        }
        /// <summary>
        /// Creates a new Matrix of the transpose for this Matrix
        /// </summary>
        /// <returns>The transpose of this Matrix</returns>
        public Matrix GetTranspose() {
            Matrix result = new Matrix(width, height);
            for(int y = 0; y < height; y++) {
                for(int x = 0; x < width; x++) {
                    result[x][y] = this[y][x];
                }
            }
            return result;
        }
        /// <summary>
        /// Swaps two rows inside of the Matrix
        /// </summary>
        /// <param name="rowA">The index of the first row to swap</param>
        /// <param name="rowB">The index of the second row to swap</param>
        public void SwapRows(int rowA, int rowB) {
            double[] temp = this[rowA];
            this[rowA] = this[rowB];
            this[rowB] = temp;
        }
        /// <summary>
        /// Combines this Matrix with another
        /// </summary>
        /// <param name="other">The other Matrix</param>
        /// <returns>The combination of the two Matricies</returns>
        public Matrix CombineWith(Matrix other) {
            if (height != other.GetHeight())
                throw new InvalidOperationException("Heights do not match");
            int newWidth = width + other.GetWidth();
            Matrix newMatrix = new Matrix(height, newWidth);
            for(int y = 0; y < height; y++) {
                int x = 0;
                for(x = 0; x < width; x++) {
                    newMatrix[y][x] = this[y][x];
                }
                for(; x < newWidth; x++) {
                    newMatrix[y][x] = other[y][x-width];
                }
            }
            return newMatrix;
        }
        private int FindPivot(int index) {
            int result = -1;
            double max = -1;
            for(int i = 0; i < height; i++) {
                if(Math.Abs(this[i][index]) > max) {
                    max = Math.Abs(this[i][index]);
                    result = i;
                }
            }
            return result;
        } 
        /// <summary>
        /// Calculates the sum of the diagonal from top left to bottom right
        /// </summary>
        /// <returns>The trace value of the Matrix</returns>
        public double GetTrace() {
            if(IsSquare()) {
                double result = 0.0;
                for(int i = 0; i < width; i++) {
                    result += this[i][i];
                }
                return result;
            }
            throw new InvalidOperationException("Matrix must be Square");
        }
        /// <summary>
        /// Finds the coefficients of the characteristic polynomial
        /// </summary>
        /// <returns>The coefficients in an Array for the characteristic polynomial</returns>
        private double[] FaddeevLeVerrier() {
            Matrix identity = GetIdentityMatrix();
            Matrix m = identity;
            double c = 1.0;
            double[] result = new double[width + 1];
            result[0] = c;
            for(double k = 1; k <= width; k++) {
                m = this * m;
                c = (-1.0 / k) * m.GetTrace();
                m = m + (identity * c);
                result[(int)k] = c;
            }
            return result;            
        }
        /// <summary>
        /// Finds the EigenValues for a Matrix.
        /// </summary>
        /// <returns>The EigenValues for the Matrix in an array</returns>
        public double[] FindEigenValues() {
            double[] coefficients = FaddeevLeVerrier();
            if (coefficients.Length == 3) {
                double[] result = new double[2];
                result[0] = (-coefficients[1] + Math.Sqrt(Math.Pow(coefficients[1], 2.0) - (4.0 * coefficients[0] * coefficients[2]))) / 2.0 * coefficients[0];
                result[1] = (-coefficients[1] - Math.Sqrt(Math.Pow(coefficients[1], 2.0) - (4.0 * coefficients[0] * coefficients[2]))) / 2.0 * coefficients[0];
                return result;
            }
            else
                throw new InvalidOperationException("Can only handle 3 coefficients");
        }
        /// <summary>
        /// Finds the Eigenvector of an Eigenvalue
        /// </summary>
        /// <param name="eigenValue">The current Eigenvalue</param>
        /// <returns>The eigenVector matching the eigenvalue</returns>
        public double[] FindEigenVector(double eigenValue) {
            if (IsSquare() && width == 2) {
                Matrix m = this - (GetIdentityMatrix() * eigenValue);
                double x = m[0][1];
                double y = -m[0][0];
                if (x < 0 && y < 0) {
                    x = -x;
                    y = -y;
                }
                return new double[] { x, y };
            }
            else if (IsSquare() && width == 5) {
                double[] arr = new double[width];
                for (int i = 0; i < width; i++) {
                    arr[i] = Math.Pow(eigenValue, i);
                }
                return arr;
            }
            else
                throw new InvalidOperationException("Cannot compute larger non square Matricies");
        }
        /// <summary>
        /// GaussianElimination implementation
        /// </summary>
        /// <returns>The result of Gaussian Elimination</returns>
        public double[] GaussianElimination() {
            double[] x = new double[10];
            Matrix b = new Matrix(height, 1);
            Matrix c = CombineWith(b);
            for(int j = 0; j < height; j++) {
                int p = c.FindPivot(j);
                if (c[p][j] == 0.0)
                    return null;
                if(p > j) 
                    c.SwapRows(p, j);
                double cJJ = c[j][j];
                for(int i = 0; i < height; i++) {
                    if(i > j) {
                        double cIJ = c[i][j];
                        for(int k = 0; k < c.GetWidth(); k++) {
                            c[i][k] = c[i][k] - (cIJ / cJJ) * c[j][k];
                        }
                    }
                }
            }
            return x;
        }
        /// <summary>
        /// Implementation of the Direct power method for estimating eigenvalues
        /// </summary>
        /// <returns>The strongest eigenvalue</returns>
        public double DirectMethod() {
            Random rand = new Random();
            double[] eigenVector = new double[width];
            for(int i = 0; i < width; i++) {
                eigenVector[i] = rand.NextDouble();
            }
            double rUnitLength = 999999;
            double epsilon = .000000001;
            int m = 1600;
            Matrix y = new Matrix(eigenVector.Length, 1);
            for (int i = 0; i < eigenVector.Length; i++) {
                y[i][0] = eigenVector[i];
            }
            Matrix x = this * y;
            double u = -1;
            for(int i = 0; i < m && rUnitLength > epsilon; i++) {
                y = x * (1.0 / FindUnitLength(x));
                x = this * y;
                u = ((y.GetTranspose() * x) * ((y.GetTranspose() * y).GetInverse()))[0][0];
                Matrix r = (y * u) - x;
                rUnitLength = FindUnitLength(r);
            }
            return u;
        }
        /// <summary>
        /// Calculates the Inverse Matrix
        /// </summary>
        /// <returns>The inverse Matrix</returns>
        public Matrix GetInverse() {
            Matrix c = CombineWith(GetIdentityMatrix());
            for(int j = 0; j < c.GetHeight(); j++) {
                int p = c.FindPivot(j);
                if (c[p][j] == 0.0)
                    return null;
                if (p > j)
                    c.SwapRows(p, j);
                double cJJ = c[j][j];
                for(int k = 0; k < c.GetWidth(); k++) {
                    c[j][k] = c[j][k] / cJJ;
                }
                for(int i = 0; i < c.GetHeight(); i++) {
                    if( i != j) {
                        double cIJ = c[i][j];
                        for(int k = 0; k < c.GetWidth(); k++) {
                            c[i][k] = c[i][k] - cIJ * (c[j][k]);
                        }
                    }
                }
            }
            Matrix result = new Matrix(height, width);
            for(int y = 0; y < height; y++) {
                for(int x = 0; x < width; x++) {
                    result[y][x] = c[y][x + width];
                }
            }
            return result;
        }
        /// <summary>
        /// Finds the Determinant of the Matrix
        /// </summary>
        /// <returns>The Determinant</returns>
        public double FindDeterminant() {
            Matrix m = CopyOf();
            int r = 0;
            for(int j = 0; j < width-1; j++) {
                int p = m.FindPivot(j);
                if (m[p][j] == 0.0)
                    return 0.0;
                if (p > j) {
                    m.SwapRows(p, j);
                    r++;
                }
                for(int i = j+1; i < height; i++) {
                    double d = m[i][j] / m[j][j];
                    for(int k = j; k < width; k++) {
                        m[i][k] = m[i][k] - (d * m[j][k]);
                    }
                }
            }
            double val = 1.0;
            for(int n = 0; n < width; n++) {
                val *= m[n][n];
            }
            return Math.Pow(-1.0, r) * val;
        }
        /// <summary>
        /// Gets a Matrix from a text file delimited by tabs and new lines
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Matrix GetFromFile(string fileName) {
            string[] file = System.IO.File.ReadAllLines(@fileName);
            List<List<double>> matrix = new List<List<double>>();
            foreach(string line in file) {
                List<double> array = new List<double>();
                if (line.ToLower().Contains("x") || line.Contains("Eigendata"))
                    continue;
                string[] arr = line.Split('\t');
                foreach(string s in arr) {
                    array.Add(Double.Parse(s.Trim()));
                }
                matrix.Add(array);
            }
            Matrix result = new Matrix(matrix.Count, matrix[0].Count);
            for(int y = 0; y < result.GetHeight(); y++) {
                for(int x = 0; x < result.GetWidth(); x++) {
                    result[y][x] = matrix[y][x];
                }
            }
            return result;
        }
        /// <summary>
        /// Finds the Unit Length Vector of an Eigenvector
        /// </summary>
        /// <param name="eigenVector">The Eigenvector</param>
        /// <returns>The Unit Length Vector</returns>
        public static double[] FindUnitLengthVector(double[] eigenVector) {
            double scalar = FindUnitLength(eigenVector);
            double[] result = new double[eigenVector.Length];
            for(int i = 0; i < eigenVector.Length; i++) {
                result[i] = (1.0 / scalar) * eigenVector[i];
            }
            return result;
        }
        /// <summary>
        /// Finds the Unit length of an eigenvector
        /// </summary>
        /// <param name="eigenVector">The eigenvector</param>
        /// <returns>The unit length of an eigenvector</returns>
        public static double FindUnitLength(double[] eigenVector) {
            double scalar = 0.0;
            foreach(double d in eigenVector) {
                scalar += Math.Pow(d, 2.0);
            }
            return Math.Sqrt(scalar);
        }
        /// <summary>
        /// Finds the Unit length of an eigenvector
        /// </summary>
        /// <param name="eigenVector">The eigenvector</param>
        /// <returns>The unit length of an eigenvector</returns>
        public static double FindUnitLength(Matrix eigenVector) {
            double scalar = 0.0;
            if(eigenVector.GetWidth() == 1) {
                for(int i = 0; i < eigenVector.GetHeight(); i++) {
                    scalar += Math.Pow(eigenVector[i][0], 2.0);
                }
            }
            return Math.Sqrt(scalar);
        }
        /// <summary>
        /// Overrides the * operator to allow multiplication between Matricies
        /// </summary>
        /// <param name="a">The first Matrix</param>
        /// <param name="b">The second Matrix</param>
        /// <returns>The product of the two Matricies</returns>
        public static Matrix operator* (Matrix a, Matrix b) {
            if (a.GetWidth() == b.GetHeight()) {
                Matrix result = new Matrix(a.GetHeight(), b.GetWidth());
                for (int i = 0; i < result.GetHeight(); i++) { //each Row
                    for (int j = 0; j < result.GetWidth(); j++) { //each Column
                        for (int y = 0; y < a.GetWidth(); y++) {
                            result[i][j] = result[i][j] + (a[i][y] * b[y][j]);
                        }
                    }
                }
                return result;
            }
            else {
                Console.WriteLine("Trying to multiply:");
                a.Print();
                Console.WriteLine("by:");
                b.Print();
                throw new InvalidOperationException("A's width must be equal to B's height");
            }
        }
        /// <summary>
        /// Overrides the * operator to allow Matrix multiplication by a scalar
        /// </summary>
        /// <param name="a">The Matrix being multiplied</param>
        /// <param name="scalar">The scalar to multiply by</param>
        /// <returns>The product of the multiplication</returns>
        public static Matrix operator* (Matrix a, double scalar) {
            Matrix result = new Matrix(a.GetHeight(), a.GetWidth());
            for(int y = 0; y < a.GetHeight(); y++) {
                for(int x = 0; x < a.GetWidth(); x++) {
                    result[y][x] = a[y][x] * scalar;
                }
            }
            return result;
        }
        /// <summary>
        /// Overrides the + operator to allow Matrix addition
        /// </summary>
        /// <param name="a">The first Matrix</param>
        /// <param name="b">The second Matrix</param>
        /// <returns>The sum of the two Matricies</returns>
        public static Matrix operator+ (Matrix a, Matrix b) {
            if (a.GetWidth() == b.GetWidth() && a.GetHeight() == b.GetHeight()) {
                Matrix result = new Matrix(a.GetHeight(), a.GetWidth());
                for(int y = 0; y < a.GetHeight(); y++) {
                    for(int x = 0; x < a.GetWidth(); x++) {
                        result[y][x] = a[y][x] + b[y][x];
                    }
                }
                return result;
            }
            else {
                throw new InvalidOperationException("Sizes of the two Matricies must be equal");
            }
        }
        /// <summary>
        /// Overrides the - operator to allow Matrix subtraction
        /// </summary>
        /// <param name="a">The first Matrix</param>
        /// <param name="b">The second Matrix</param>
        /// <returns>The difference of the two Matricies</returns>
        public static Matrix operator- (Matrix a, Matrix b) {
            if (a.GetWidth() == b.GetWidth() && a.GetHeight() == b.GetHeight()) {
                Matrix result = new Matrix(a.GetHeight(), a.GetWidth());
                for (int y = 0; y < a.GetHeight(); y++) {
                    for (int x = 0; x < a.GetWidth(); x++) {
                        result[y][x] = a[y][x] - b[y][x];
                    }
                }
                return result;
            }
            else {
                throw new InvalidOperationException("Sizes of the two Matricies must be equal");
            }
        }

        static void Main(string[] args) {
            Matrix m = GetFromFile("data.txt");
            m.Print();
            Console.WriteLine("Determinant: " + m.FindDeterminant());
            Matrix m2 = m.GetInverse();
            m2.Print();

            Console.WriteLine("\n\nPress any key to close");
            Console.ReadKey();
        }
    }
}
