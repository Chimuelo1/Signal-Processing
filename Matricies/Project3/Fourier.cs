using System;
using NAudio.Wave.SampleProviders;

namespace Project3 {
    /// <summary>
    /// Static methods used to perform Fourier Transforms and related functions
    /// </summary>
    class Fourier {
        /// <summary>
        /// Applies the Fast Fourier Transform algorithm to a Signal
        /// </summary>
        /// <param name="signal">The Signal to apply FFT to</param>
        /// <returns>The Fourier Transform of a Signal</returns>
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
        /// <summary>
        /// Applies the Fast Fourier Transform in 2 dimensions to a Signal in 2 dimensions
        /// </summary>
        /// <param name="signal">The Signal2D to apply FFT2D to</param>
        /// <returns>The Fourier Transform of the Signal2D</returns>
        public static Signal2D FFT2D(Signal2D signal) {
            Signal2D result = new Signal2D(signal.Height, signal.Width);
            for (int i = 0; i < result.Height; i++)
                result[i] = new ComplexNumber[signal[i].Length];
            //columns
            for(int k = 0; k < signal[0].Length; k++) {
                ComplexNumber[] col = new ComplexNumber[signal.Height];
                for(int j = 0; j < col.Length; j++) {
                    col[j] = signal[j][k];
                }
                col = FFT(col);
                for (int j = 0; j < col.Length; j++) {
                    result[j][k] = col[j];
                }
            }
            //rows
            for(int n = 0; n < signal.Height; n++) {
                result[n] = FFT(result[n]);
            }
            return result;

        }
        /// <summary>
        /// Applies the Inverted Fast Fourier Transform algorithm in 2 dimensions to a Signal
        /// </summary>
        /// <param name="signal">The Signal2D to apply IFFT2D to</param>
        /// <returns>The Inverted Fourier Transform of the Signal2D</returns>
        public static Signal2D InverseFFT2D(Signal2D signal) {
            Signal2D result = signal.GetConjugate();
            result = FFT2D(result);
            for (int i = 0; i < signal.Height; i++) {
                for (int j = 0; j < signal.Width; j++) {
                    result[i][j] /= signal.Width*signal.Height;
                }
            }
            return result;
        }
       /// <summary>
       /// Applies the Inverse Fast Fourier Transform algorithm to a Signal
       /// </summary>
       /// <param name="signal">The Signal to apply IFFT to</param>
       /// <returns>The Inverted Fourier Transform of the Signal</returns>
        public static Signal InverseFFT(Signal signal) {
            Signal result = signal.GetConjugate();
            result = FFT(result);
            result /= signal.Length;
            return result;
        }
        /// <summary>
        /// Applies Cross Correlation between 2 Signals
        /// </summary>
        /// <param name="x">The first Signal</param>
        /// <param name="y">The second Signal</param>
        /// <returns>The Cross Correlation of the 2 Signals</returns>
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
        /// <summary>
        /// Applies Cross Convolution between a Signal and a Filter. Used to apply Filters to a Signal
        /// </summary>
        /// <param name="signal">The Signal to Filter</param>
        /// <param name="filter">The Filter to be used</param>
        /// <returns>The Filtered Signal</returns>
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
        public static Signal2D CrossConvolution2D(Signal2D signal, Signal2D filter) {
            Signal2D filterFFT = FFT2D(filter);
            Signal2D signalFFT = FFT2D(signal);
            Signal2D result = filterFFT * signalFFT;
            return InverseFFT2D(result);
        }
        public static Signal2D CrossCorrelation2D(Signal2D signal, Signal2D filter) {
            Signal2D filterFFT = FFT2D(filter).GetConjugate();
            Signal2D signalFFT = FFT2D(signal);
            Signal2D result = filterFFT * signalFFT;
            return InverseFFT2D(result);
        }
        public static void Main(string[] args) {
            Image a = Image.GetImageA();
            Image b = Image.GetImageB();

            Signal2D[] colorA = a.Deconstruct();
            Signal2D[] colorB = b.Deconstruct();
            Signal2D[] resultColor = new Signal2D[3];
            resultColor[0] = CrossConvolution2D(colorA[0], colorB[0]).GetMagnitude();
            resultColor[1] = resultColor[0];
            resultColor[2] = resultColor[0];
            
            Image.Reconstruct(resultColor[0], resultColor[1], resultColor[2]).Save("corr2.jpg");
            for(int i = 0; i < a.Height; i++)
                for(int j = 0; j < a.Width; j++)
                   // Console.WriteLine(resultColor[0][i][j]+","+ resultColor[1][i][j]+","+ resultColor[2][i][j]);
            Functions.PlayWav("..\\..\\DTMF\\A.wav");
            Console.WriteLine("\n\nPress any key to close");
            Console.ReadKey();
        }
    }
}
