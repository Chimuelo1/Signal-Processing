using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3 {
    /// <summary>
    /// A 2 dimensional Signal
    /// </summary>
    public class Signal2D : IEnumerable<Signal> {
        private Signal[] frequencies;
        /// <summary>
        /// Gets the frequencies of the Signal
        /// </summary>
        public Signal[] Frequencies => frequencies;
        /// <summary>
        /// Gets the width of the Signal
        /// </summary>
        public int Width { get { if (frequencies.Length > 0) return frequencies[0].Length; else return 0; } }
        /// <summary>
        /// Gets the height of the Signal
        /// </summary>
        public int Height => frequencies.Length;
        /// <summary>
        /// Creates a new 2D Signal based on an array of Signals
        /// </summary>
        /// <param name="signal">The Signal[] to base the Signal2D off of</param>
        public Signal2D(Signal[] signal) {
            frequencies = signal;
        }
        /// <summary>
        /// Creates a new 2D Signal based on a ComplexNumber[][]
        /// </summary>
        /// <param name="signal">The ComplexNumber[][] to base the Signal2D off of</param>
        public Signal2D(ComplexNumber[][] signal) {
            frequencies = new Signal[signal.Length];
            for (int i = 0; i < signal.Length; i++)
                frequencies[i] = signal[i];
        }
        /// <summary>
        /// Creates a new 2D Signal based on a double[][]
        /// </summary>
        /// <param name="signal">The double[][] to base the Signal2D off of</param>
        public Signal2D(double[][] signal) {
            frequencies = new Signal[signal.Length];
            for (int i = 0; i < signal.Length; i++)
                frequencies[i] = signal[i];
        }
        /// <summary>
        /// Creates an empty Signal2D with the given dimensions
        /// </summary>
        /// <param name="height">The height to make the Signal</param>
        /// <param name="width">The width to make the Signal</param>
        public Signal2D(int height, int width) {
            frequencies = new Signal[height];
            for (int i = 0; i < height; i++) {
                frequencies[i] = new Signal(width);
            }
        }
        /// <summary>
        /// Gets the Signal at the given index
        /// </summary>
        /// <param name="index">The index of the Signal</param>
        /// <returns>The Signal at the given index</returns>
        public Signal this[int index] {
            get {
                return frequencies[index];
            }
            set {
                frequencies.SetValue(value, index);
            }
        }
        /// <summary>
        /// Gets the Conjugate for each frequency in the Signal2D
        /// </summary>
        /// <returns>A new Signal2D containing the Conjugate for each frequency</returns>
        public Signal2D GetConjugate() {
            Signal2D result = new Signal2D(Height, Width);
            for (int i = 0; i < Height; i++)
                result[i] = this[i].GetConjugate();
            return result;
        }
        /// <summary>
        /// Gets a single Column of the Signal2D
        /// </summary>
        /// <param name="index">The index of the column to get</param>
        /// <returns>A Signal representing the column</returns>
        public Signal GetColumn(int index) {
            Signal column = new Signal(Height);
            for (int i = 0; i < Height; i++) {
                column[i] = this[i][index];
            }
            return column;
        }
        /// <summary>
        /// Allows a ComplexNumber[][] to be cast to a Signal2D
        /// </summary>
        /// <param name="mat">The ComplexNumber[][] to cast</param>
        public static implicit operator Signal2D(ComplexNumber[][] mat) {
            return new Signal2D(mat);
        }
        /// <summary>
        /// Allows a double[][] to be cast to a Signal2D
        /// </summary>
        /// <param name="mat">The double[][] to cast</param>
        public static implicit operator Signal2D(double[][] mat) {
            return new Signal2D(mat);
        }
        /// <summary>
        /// Allows a Signal2D to be cast to a Signal[]
        /// </summary>
        /// <param name="signal">The Signal2D to cast</param>
        public static implicit operator Signal[] (Signal2D signal) {
            return signal.Frequencies;
        }
        /// <summary>
        /// Allows a Signal2D to be cast to a ComplexNumber[][]
        /// </summary>
        /// <param name="signal">The Signal2D to cast</param>
        public static implicit operator ComplexNumber[][] (Signal2D signal) {
            ComplexNumber[][] result = new ComplexNumber[signal.Height][];
            for (int i = 0; i < signal.Height; i++) {
                result[i] = new ComplexNumber[signal.Width];
                for (int j = 0; j < signal.Width; j++) {
                    result[i][j] = signal[i][j];
                }
            }
            return result;
        }
        /// <summary>
        /// Allows a Signal2D to be cast to a double[][]
        /// </summary>
        /// <param name="signal">The Signal2D to cast</param>
        public static explicit operator double[][](Signal2D signal) {
            double[][] result = new double[signal.Height][];
            for (int i = 0; i < signal.Height; i++) {
                result[i] = new double[signal.Width];
                for (int j = 0; j < signal.Width; j++) {
                    result[i][j] = (double)signal[i][j];
                }
            }
            return result;
        }
        /// <summary>
        /// Allows an Image to be cast to a Signal2D
        /// </summary>
        /// <param name="image">The image to cast</param>
        public static implicit operator Signal2D(Image image) {
            return image.GetMatrix();
        }
        /// <summary>
        /// Gets the Enumerator of the Signal2D
        /// </summary>
        /// <returns>The Signal2D Enumerator</returns>
        public IEnumerator<Signal> GetEnumerator() {
            return ((IEnumerable<Signal>)frequencies).GetEnumerator();
        }
        /// <summary>
        /// Gets the Enumerator of the Signal2D
        /// </summary>
        /// <returns>The Signal2D Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable<Signal>)frequencies).GetEnumerator();
        }
        /// <summary>
        /// Multiplies 2 Signal2Ds together 
        /// </summary>
        /// <param name="a">The first Signal2D</param>
        /// <param name="b">The second Signal2D</param>
        /// <returns>The product of the 2 Signal2Ds</returns>
        public static Signal2D operator* (Signal2D a, Signal2D b) {
            if (a.Height != b.Height || a.Width != b.Width)
                throw new ArgumentException("Sizes must be equal");
            Signal2D result = new Signal2D(a.Height, a.Width);
            for (int y = 0; y < a.Height; y++) {
                for (int x = 0; x < a.Width; x++) {
                    result[y][x] = a[y][x] * b[y][x];
                }
            }
            return result;
        }
        /// <summary>
        /// Multiplies a Signal2D by a scalar
        /// </summary>
        /// <param name="a">The Signal2D to multiply</param>
        /// <param name="scalar">The scalar to multiply</param>
        /// <returns>The product of the Signal2D and the scalar</returns>
        public static Signal2D operator* (Signal2D a, ComplexNumber scalar) {
            Signal2D result = new Signal2D(a.Height, a.Width);
            for(int y = 0; y < a.Height; y++) {
                for(int x = 0; x < a.Width; x++) {
                    result[y][x] = a[y][x] * scalar;
                }
            }
            return result;
        }
        /// <summary>
        /// Adds 2 Signal2Ds together
        /// </summary>
        /// <param name="a">The first Signal2D</param>
        /// <param name="b">The second Signal2D</param>
        /// <returns>The sum of the two Signal2Ds</returns>
        public static Signal2D operator+ (Signal2D a, Signal2D b) {
            if (a.Height != b.Height || a.Width != b.Width)
                throw new ArgumentException("Sizes must be equal");
            Signal2D result = new Signal2D(a.Height, a.Width);
            for(int y = 0; y < a.Height; y++) {
                for(int x = 0; x < a.Width; x++) {
                    result[y][x] = a[y][x] + b[y][x];
                }
            }
            return result;
        }
        /// <summary>
        /// Subtracts a Signal2D from another
        /// </summary>
        /// <param name="a">The first Signal2D</param>
        /// <param name="b">The second Signal2D</param>
        /// <returns>The difference of the two Signals</returns>
        public static Signal2D operator- (Signal2D a, Signal2D b) {
            if (a.Height != b.Height || a.Width != b.Width)
                throw new ArgumentException("Sizes must be equal");
            Signal2D result = new Signal2D(a.Height, a.Width);
            for (int y = 0; y < a.Height; y++) {
                for (int x = 0; x < a.Width; x++) {
                    result[y][x] = a[y][x] - b[y][x];
                }
            }
            return result;
        }
        public Signal2D GetMagnitude() {
            Console.WriteLine("getting magnitude");
            Signal2D mag = this;
            int[] maxIndex = Max();
            int[] minIndex = Min();
            double max = this[maxIndex[0]][maxIndex[1]].Real;
            double min = this[minIndex[0]][minIndex[1]].Real;
            double scalar = 255.0 / max;
            for (int i = 0; i < mag.Height; i++) {
                for (int j = 0; j < mag.Width; j++) {
                    mag[i][j] *= scalar;
                }
            }
            maxIndex = Max();
            minIndex = Min();
            max = this[maxIndex[0]][maxIndex[1]].Real;
            min = this[minIndex[0]][minIndex[1]].Real;
            return mag;
        }
        public int[] Max() {
            int[] maxXY = new int[2];
            int x = 0;
            int y = 0;
            double max = double.MinValue;
            for(int i = 0; i < Height; i++) {
                for(int j = 0; j < Width; j++) {
                    if(this[j][i].Real > max) {
                        x = j;
                        y = i;
                        max = this[j][i].Real;
                    }
                }
            }
            maxXY[0] = x;
            maxXY[1] = y;
            return maxXY;
        }
        public int[] Min() {
            int[] minXY = new int[2];
            int x = 0;
            int y = 0;
            double min = double.MaxValue;
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    if (this[j][i].Real < min) {
                        x = j;
                        y = i;
                        min = this[j][i].Real;
                    }
                }
            }
            minXY[0] = x;
            minXY[1] = y;
            return minXY;
        }
    }
}
