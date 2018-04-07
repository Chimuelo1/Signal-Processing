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
                result[k + N / 2] = even - wk * odd;
            }
            return result;
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

        public static void Main(string[] args) {
            int[] s = new int[] { 3, 10, 50 };
            int num = 512;
            Matrix fMat = F(s, num);
            Matrix gMat = G(s, num);
            double[][] signals = new double[fMat.GetWidth() -1+gMat.GetWidth()-1][];
            for(int i = 0; i < fMat.GetWidth()-1; i++) {
                signals[i] = fMat.GetColumn(i + 1);
            }
            for(int i = 0; i < gMat.GetWidth()-1; i++) {
                signals[i + fMat.GetWidth() - 1] = gMat.GetColumn(i + 1);
            }
            ComplexNumber[][] comps = new ComplexNumber[signals.Length][];
            double[][] psd = new double[signals.Length][];
            for(int i = 0; i < signals.Length;i++) {
                comps[i] = FFT(signals[i]);
                psd[i] = new double[comps[i].Length];
                for (int j = 0; j < comps[i].Length; j++) {
                    psd[i][j] = Math.Pow(comps[i][j].GetPhase(),2.0);
                }
            }
            foreach(double d in psd[2]) {
                Console.WriteLine(d);
            }
            Console.WriteLine("-------------------------------");
            foreach (double d in psd[5]) {
                Console.WriteLine(d);
            }

            Console.WriteLine("\n\nPress any key to close");
            Console.ReadKey();
        }
    }
}
