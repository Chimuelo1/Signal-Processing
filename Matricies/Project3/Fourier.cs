using Matricies;
using QueueTips;
using System;
using NAudio.Wave.SampleProviders;

namespace Project3 {
    class Fourier {
        public static Signal FFT(Signal signal) {
            int N = signal.Length;
            if (N == 1)
                return signal;
            if ((N & (N - 1)) != 0)
                throw new ArgumentOutOfRangeException("signal length must be a power of 2");
            Signal evenArr = new Signal(N/2);
            Signal oddArr = new Signal(N/2);
            for (int i = 0; i < N / 2; i++) {
                evenArr[i] = signal[2 * i];
            }
            evenArr = FFT(evenArr);
            for (int i = 0; i < N / 2; i++) {
                oddArr[i] = signal[2 * i + 1];
            }
            oddArr = FFT(oddArr);
            Signal result = new Signal(N);
            for (int k = 0; k < N / 2; k++) {
                double w = -2.0 * k * Math.PI / N;
                ComplexNumber wk = new ComplexNumber(Math.Cos(w), Math.Sin(w));
                ComplexNumber even = evenArr[k];
                ComplexNumber odd = oddArr[k];
                result[k] = even + (wk * odd);
                result[k + N / 2] = even - (wk * odd);
            }
            return result;
        }
        public static ComplexNumber[][] FFT2D(ComplexNumber[][] signal) {
            ComplexNumber[][] result = new ComplexNumber[signal.Length][];
            for (int i = 0; i < result.Length; i++)
                result[i] = new ComplexNumber[signal[i].Length];
            //columns
            for(int k = 0; k < signal[0].Length; k++) {
                ComplexNumber[] col = new ComplexNumber[signal.Length];
                for(int j = 0; j < col.Length; j++) {
                    col[j] = signal[j][k];
                }
                col = FFT(col);
                for (int j = 0; j < col.Length; j++) {
                    result[j][k] = col[j];
                }
            }
            //rows
            for(int n = 0; n < signal.Length; n++) {
                result[n] = FFT(result[n]);
            }
            return result;

        }
        public static ComplexNumber[][] InverseFFT2D(ComplexNumber[][] signal) {
            ComplexNumber[][] result = new ComplexNumber[signal.Length][];
            for(int i = 0; i < signal.Length; i++) {
                result[i] = new ComplexNumber[signal[i].Length];
                for(int j = 0; j < signal[i].Length; j++) {
                    result[i][j] = signal[i][j].GetConjugate();
                }
            }
            result = FFT2D(result);
            for (int i = 0; i < signal.Length; i++) {
                for (int j = 0; j < signal[i].Length; j++) {
                    result[i][j] /= signal[0].Length*signal.Length;
                }
            }
            return result;
        }
        public static ComplexNumber[][]InverseFFT2D(Image image) {
            return InverseFFT2D(image.GetMatrix());
        }
        public static ComplexNumber[][] FFT2D(Image image) {
            return FFT2D(image.GetMatrix());
        }
        public static Signal InverseFFT(Signal signal) {
            Signal result = signal.GetConjugate();
            result = FFT(result);
            result /= signal.Length;
            return result;
        }
        public static Signal CrossCorrelation(Signal x, Signal y) {
            Signal Y = y;
            if (y.Length != x.Length)
                Y = y.PadWithZeros(x.Length);
            int shiftAmt = 0;
            Signal shift = new Signal(Y.Length+shiftAmt);
            for(int i = 0; i < shift.Length; i++) {
                if (i < shiftAmt)
                    shift[i] = 0;
                else {
                    shift[i] = Y[i - shiftAmt];
                }
            }
            for (int i = 0; i < Y.Length; i++) {
                Y[i] = shift[i];
            }
            Signal X = FFT(x);
            Y = FFT(Y).GetConjugate();
            Signal XY = new Signal(X.Length);
            for(int i = 0; i < X.Length; i++) {
                XY[i] = X[i] * Y[i];
            }
            return InverseFFT(XY);
        }
        public static ComplexNumber[][] CrossCorrelation2D(ComplexNumber[][] x, ComplexNumber[][] y) {
            return null;
        }
        public static Signal CrossConvolution(Signal signal, Signal filter) {
            Signal filterFFT = filter.PadWithZeros(signal.Length);
            Signal signalFFT = FFT(signal);
            filterFFT = FFT(filterFFT);
            Signal result = new Signal(signal.Length);
            for(int i = 0; i < signal.Length; i++) {
                result[i] = signalFFT[i] * filterFFT[i];
            }
            return InverseFFT(result);
         }
        public static Signal Low(Signal s) {
            Signal fft = FFT(s);
            for(int i = 7; i < s.Length; i++) {
                fft[i] = 0;
            }
            return InverseFFT(fft);
        }
        public static Signal High(Signal s) {
            Signal fft = FFT(s);
            for (int i = 0; i < 7; i++) {
                fft[i] = 0;
            }
            return InverseFFT(fft);
        }
        public static Signal Band(Signal s) {
            Signal fft = FFT(s);
            for(int i = 0; i < s.Length; i++) {
                if (i < 5 || i > 8)
                    fft[i] = 0;
            }
            return InverseFFT(fft);
        }
        public static Signal Notch(Signal s) {
            Signal fft = FFT(s);
            for (int i = 0; i < s.Length; i++) {
                if (!(i < 5 || i > 8))
                    fft[i] = 0;
            }
            return InverseFFT(fft);
        }
        public static void Main(string[] args) {
            double[][] data = Functions.GetDataFromFile("..\\..\\rangeTestDataSpring2018.txt");
            Signal f50 = Functions.F(50, 512).GetColumn(0);
            Signal filtered = CrossConvolution(f50, Filter.LowPass(f50.Length, 7, 50));
            foreach(ComplexNumber n in Notch(f50))
                Console.WriteLine(n.GetReal());
            Console.WriteLine("\n\nPress any key to close");
            Console.ReadKey();
        }
    }
}
