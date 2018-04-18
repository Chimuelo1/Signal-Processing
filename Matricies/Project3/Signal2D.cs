using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3 {
    class Signal2D : IEnumerable<Signal> {
        private Signal[] frequencies;
        public Signal[] Frequencies => frequencies;
        public int Width { get { if (frequencies.Length > 0) return frequencies[0].Length; else return 0; } }
        public int Height => frequencies.Length;
        public Signal2D(Signal[] signal) {
            frequencies = signal;
        }
        public Signal2D(ComplexNumber[][] signal) {
            frequencies = new Signal[signal.Length];
            for (int i = 0; i < signal.Length; i++)
                frequencies[i] = signal[i];
        }
        public Signal2D(double[][] signal) {
            frequencies = new Signal[signal.Length];
            for (int i = 0; i < signal.Length; i++)
                frequencies[i] = signal[i];
        }
        public Signal2D(int height, int width) {
            frequencies = new Signal[height];
            for (int i = 0; i < height; i++) {
                frequencies[i] = new Signal(width);
            }
        }
        public Signal this[int index] {
            get {
                return frequencies[index];
            }
            set {
                frequencies.SetValue(value, index);
            }
        }
        public Signal2D GetConjugate() {
            Signal2D result = new Signal2D(Height, Width);
            for (int i = 0; i < Height; i++)
                result[i] = this[i].GetConjugate();
            return result;
        }
        public Signal GetColumn(int index) {
            Signal column = new Signal(Height);
            for (int i = 0; i < Height; i++) {
                column[i] = this[i][index];
            }
            return column;
        }
        public static implicit operator Signal2D(ComplexNumber[][] mat) {
            return new Signal2D(mat);
        }
        public static implicit operator Signal2D(double[][] mat) {
            return new Signal2D(mat);
        }
        public static implicit operator Signal[] (Signal2D signal) {
            return signal.Frequencies;
        }
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
        public static implicit operator Signal2D(Image image) {
            return image.GetMatrix();
        }
        public IEnumerator<Signal> GetEnumerator() {
            return ((IEnumerable<Signal>)frequencies).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable<Signal>)frequencies).GetEnumerator();
        }
        public static Signal2D operator* (Signal2D a, Signal2D b) {
            if (a.Width != b.Height)
                throw new ArgumentException("A's width must be equal to B's Length");
            Signal2D result = new Signal2D(a.Height, b.Width);
            for(int i = 0; i < result.Height; i++) {
                for(int j = 0; j < b.Width; j++) {
                    for(int y = 0; y < a.Width; y++) {
                        result[i][j] = result[i][j] + (a[i][y] * b[y][j]);
                    }
                }
            }
            return result;
        }
        public static Signal2D operator* (Signal2D a, ComplexNumber scalar) {
            Signal2D result = new Signal2D(a.Height, a.Width);
            for(int y = 0; y < a.Height; y++) {
                for(int x = 0; x < a.Width; x++) {
                    result[y][x] = a[y][x] * scalar;
                }
            }
            return result;
        }
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
    }
}
