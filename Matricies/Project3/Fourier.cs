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

        public static void Main(string[] args) {
            double[] freq = {
                26160.0,
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
                17181.0,
            };
            ComplexNumber[] x = new ComplexNumber[freq.Length];
            for (int i = 0; i < freq.Length; i++) {
                x[i] = new ComplexNumber(freq[i], 0);
            }
            x = FFT(x);
            for(int i = 0; i < x.Length; i++) {
                Console.WriteLine(String.Format("{0}\t{1}\n", i,x[i]));
            }
            Console.WriteLine("\n\nPress any key to close");
            Console.ReadKey();
        }
    }
}
