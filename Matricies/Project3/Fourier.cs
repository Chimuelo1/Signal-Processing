using Matricies;
using System;

namespace Project3 {
    class Fourier {
        public static ComplexNumber[] FFT(ComplexNumber[] signal) {
            int N = signal.Length;
            if (N == 1)
                return signal;
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
        public static ComplexNumber[] InverseFFT(double[] signal) {
            ComplexNumber[] arr = new ComplexNumber[signal.Length];
            for(int i = 0; i < signal.Length; i++) {
                arr[i] = new ComplexNumber(signal[i], 0.0);
            }
            return InverseFFT(arr);
        }
        public static ComplexNumber[] FFT(double[] signal) {
            ComplexNumber[] complexSignal = new ComplexNumber[signal.Length];
            for(int i = 0; i < signal.Length; i++) {
                complexSignal[i] = new ComplexNumber(signal[i], 0.0);
            }
            return FFT(complexSignal);
        }
        public static Matrix F(int[] s, int numSamples) {
            Matrix m = new Matrix(numSamples, s.Length+1);
            for(int j = 0; j < s.Length; j++) {
                for (int i = 0; i < numSamples; i++) {
                    double sum = 0.0;
                    double t = (double)i / numSamples;
                    if(j == 0)
                        m[i][0] = t;
                    for (int k = 1; k <= s[j]; k++) {
                        double val = (Math.Sin(2.0 * Math.PI * (2.0 * k - 1.0) * t)) / (2.0 * k - 1.0);
                        sum += val;
                    }
                    m[i][j+1] = sum;
                }
            }
            m.WriteToFile("..\\..\\fMatrix.csv");
            return m;
        }
        public static Matrix F(int s, int numSamples) {
            Matrix m = new Matrix(numSamples, 1);
            for (int i = 0; i < numSamples; i++) {
                double sum = 0.0;
                double t = (double)i / numSamples;
                for (int k = 1; k <= s; k++) {
                    sum += (Math.Sin(2.0 * Math.PI * (2.0 * k - 1.0) * t)) / (2.0 * k - 1.0);
                }
                m[i][0] = sum;
            }
            m.WriteToFile("..\\..\\f2Matrix.csv");
            return m;
        }
        public static Matrix G(int[] s, int numSamples) {
            Matrix m = new Matrix(numSamples, s.Length + 1);
            for (int j = 0; j < s.Length; j++) {
                for (int i = 0; i < numSamples; i++) {
                    double sum = 0.0;
                    double t = (double)i / numSamples;
                    if (j == 0)
                        m[i][0] = t;
                    for (int k = 1; k <= s[j]; k++) {
                        double val = (Math.Sin(2.0 * Math.PI * (2.0 * k) * t)) / (2.0 * k);
                        sum += val;
                    }
                    m[i][j + 1] = sum;
                }
            }
            m.WriteToFile("..\\..\\gMatrix.csv");
            return m;
        }
        public static Matrix G(int s, int numSamples) {
            Matrix m = new Matrix(numSamples, 1);
            for (int i = 0; i < numSamples; i++) {
                double sum = 0.0;
                double t = (double)i / numSamples;
                for (int k = 1; k <= s; k++) {
                    sum += (Math.Sin(2.0 * Math.PI * (2.0 * k) * t)) / (2.0 * k);
                }
                m[i][0] = sum;
            }
            m.WriteToFile("..\\..\\f2Matrix.csv");
            return m;
        }
        public static double[] V(int freq, int numSamples) {
            double[] result = new double[numSamples];
            for(int i = 0; i < numSamples; i++) {
                double t = (double)i / numSamples;
                result[i] = Math.Sin(2.0 * freq * Math.PI*t);
            }
            return result;
        }
        public static double[] X(int numSamples) {
            double[] result = new double[numSamples];
            double[] v1 = V(13, numSamples);
            double[] v2 = V(31, numSamples);
            for(int i = 0; i < numSamples; i++) {
                result[i] = v1[i] + v2[i];
            }
            return result;
        }
        public static double[] Y(int numSamples) {
            double[] result = new double[numSamples];
            double[] v1 = V(13, numSamples);
            double[] v2 = V(31, numSamples);
            for (int i = 0; i < numSamples; i++) {
                result[i] = v1[i] * v2[i];
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
        public static void Main(string[] args) {
            //Values of F(t)
            double[] f3 = F(3, 512).GetColumn(0);
            double[] f10 = F(10, 512).GetColumn(0);
            double[] f50 = F(50, 512).GetColumn(0);
            double[] f100 = F(100, 512).GetColumn(0);
            
            //Values of G(t)
            double[] g3 = G(3, 512).GetColumn(0);
            double[] g10 = G(10, 512).GetColumn(0);
            double[] g50 = G(50, 512).GetColumn(0);
            double[] g100 = G(100, 512).GetColumn(0);

            double[] x = PSD(X(512));
            double[] y = PSD(Y(512));
            double[] test = new double[] {26160.0,
19011.0,
18757.0,
18405.0,
17888.0,
14720.0,
14285.0,
17018.0,
18014.0,
17119.0,
16400.0,
17497.0,
17846.0,
15700.0,
17636.0,
17181.0};
            foreach(ComplexNumber d in FFT(test))
                Console.WriteLine(d);
            Console.WriteLine("----------------");
            foreach (ComplexNumber d in InverseFFT(FFT(test))) 
                Console.WriteLine(d);
            Console.WriteLine("\n\nPress any key to close");
            Console.ReadKey();
        }
    }
}
