using Matricies;
using QueueTips;
using System;

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
            Console.WriteLine("Starting fft");
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
            Console.WriteLine("done columns");
            //rows
            for(int n = 0; n < signal.Length; n++) {
                result[n] = FFT(signal[n]);
            }
            Console.WriteLine("done rows");
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
                    result[i][j] /= signal[0].Length;
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
        public static void Main(string[] args) {
            Image image = new Image("smile.jpg");

          //  mat = InverseFFT2D(mat);
           // Console.WriteLine("done IFFT");
           for(int i = 0; i < 2; i++)
                image = new Image(InverseFFT2D(image.GetMatrix()));
            Console.WriteLine("done applying crap");
            image.Save("sad.jpg");
            
            Console.WriteLine("\n\nPress any key to close");
            Console.ReadKey();
        }
    }
}
