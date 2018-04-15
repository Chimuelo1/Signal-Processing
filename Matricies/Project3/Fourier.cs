using Matricies;
using QueueTips;
using System;
using NAudio.Wave.SampleProviders;

namespace Project3 {
    class Fourier {
        public static ComplexNumber[] ConvertArray(double[] orig) {
            ComplexNumber[] result = new ComplexNumber[orig.Length];
            for (int i = 0; i < orig.Length; i++) {
                result[i] = orig[i];
            }
            return result;
        }
        public static ComplexNumber[] InverseFFT(double[] signal) {
            return InverseFFT(ConvertArray(signal));
        }
        public static ComplexNumber[] FFT(double[] signal) {
            return FFT(ConvertArray(signal));
        }
        public static ComplexNumber[] LowPass(double[] signal) {
            return LowPass(ConvertArray(signal));
        }
        public static ComplexNumber[] HighPass(double[] signal) {
            return HighPass(ConvertArray(signal));
        }
        public static ComplexNumber[] FFT(ComplexNumber[] signal) {
            int N = signal.Length;
            if (N == 1)
                return signal;
            if ((N & (N - 1)) != 0)
                throw new ArgumentOutOfRangeException("signal length must be a power of 2");
            ComplexNumber[] evenArr = new ComplexNumber[N / 2];
            ComplexNumber[] oddArr = new ComplexNumber[N / 2];
            for (int i = 0; i < N / 2; i++) {
                evenArr[i] = signal[2 * i];
            }
            evenArr = FFT(evenArr);
            for (int i = 0; i < N / 2; i++) {
                oddArr[i] = signal[2 * i + 1];
            }
            oddArr = FFT(oddArr);
            ComplexNumber[] result = new ComplexNumber[N];
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
        public static ComplexNumber[] InverseFFT(ComplexNumber[] signal) {
            ComplexNumber[] result = new ComplexNumber[signal.Length];
            for (int i = 0; i < signal.Length; i++)
                result[i] = signal[i].GetConjugate();
            result = FFT(result);
            for(int i = 0; i < signal.Length; i++) {
                result[i] /= signal.Length;
            }
            return result;
        }
        public static double[] PSD(double[] signal) {
            ComplexNumber[] fft = FFT(signal);
            double[] result = new double[signal.Length];
            for(int i = 0; i < signal.Length; i++) {
                result[i] = Math.Pow(fft[i].GetMagnitude(), 2.0);
            }
            return result;
        }
        public static ComplexNumber[] LowPass(ComplexNumber[] signal) {
            ComplexNumber[] result = FFT(signal);
            for(int i = 0; i < result.Length; i++) {
                if (i > 6)
                    result[i] = 0;
            }
            return InverseFFT(result); 
        }
        public static ComplexNumber[][] LowPass2D(ComplexNumber[][] signal) {
            ComplexNumber[][] result = FFT2D(signal);
            for(int i = 0; i < result.Length; i++) {
                for(int j = 0; j < result[0].Length; j++) {
                    if (i > 6)
                        result[i][j] = 0;
                }
            }
            return InverseFFT2D(result);
        }
        public static ComplexNumber[] HighPass(ComplexNumber[] signal) {
            ComplexNumber[] result = FFT(signal);
            for (int i = 0; i < result.Length; i++) {
                if (i < 6)
                    result[i] = 0;
            }
            return InverseFFT(result);
        }
        public static ComplexNumber[] CrossCorrelation(ComplexNumber[] x, ComplexNumber[] y) {
            ComplexNumber[] Y = y;
            if (y.Length != x.Length)
                Y = Functions.PadWithZeroes(Y, x.Length);
            int shiftAmt = 100;
            ComplexNumber[] shift = new ComplexNumber[Y.Length+shiftAmt];
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
            ComplexNumber[] X = FFT(x);
            Y = FFT(Y);
            for (int i = 0; i < Y.Length; i++)
                Y[i] = Y[i].GetConjugate();
            ComplexNumber[] XY = new ComplexNumber[X.Length];
            for(int i = 0; i < X.Length; i++) {
                XY[i] = X[i] * Y[i];
            }
            return InverseFFT(XY);
         }
        public static ComplexNumber[] CrossCorrelation(double[] x, double[] y) {
            return CrossCorrelation(ConvertArray(x), ConvertArray(y));
        }
        public static void Main(string[] args) {
            double[][] data = Functions.GetDataFromFile("..\\..\\rangeTestDataSpring2018.txt");
            double[] transmitted = data[0];
            double[] recieved = data[1];
            double[] test1 = new double[] { 1, 2, 3, 4, 5, 6, 7,8 };
            double[] test2 = new double[] { 1, 2 };
            ComplexNumber[] result = CrossCorrelation(recieved, transmitted);
            foreach(ComplexNumber c in result) {
                Console.WriteLine(c.GetMagnitude());
            }
            Console.WriteLine(Functions.CalcDistance(1500,15.7)+"m");
            Console.WriteLine("\n\nPress any key to close");
            Console.ReadKey();
        }
    }
}
