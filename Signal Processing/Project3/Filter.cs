using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing {
    /// <summary>
    /// A Filter which is a subclass of Signal
    /// </summary>
    public class Filter : Signal {
        /// <summary>
        /// Creates a new Filter based on a double[]
        /// </summary>
        /// <param name="frequencies">The double[] to base the Filter on</param>
        public Filter(double[] frequencies) : base(frequencies) {

        }
        /// <summary>
        /// Creates a new Filter based on a ComplexNumber[]
        /// </summary>
        /// <param name="frequencies">The Complex[] to base the Filter on</param>
        public Filter(ComplexNumber[] frequencies) : base(frequencies) {

        }
        /// <summary>
        /// Creates a blank Filter based on the given size
        /// </summary>
        /// <param name="length">The desired size of the Filter</param>
        public Filter(int length) :base(length) {
            
        }
        /// <summary>
        /// Allows a ComplexNumber[] to be cast to a Filter
        /// </summary>
        /// <param name="arr">The ComplexNumber[] to cast</param>
        public static implicit operator Filter(ComplexNumber[] arr) {
            return new Filter(arr);
        }
        /// <summary>
        /// Allows a double[] to be cast to a Filter
        /// </summary>
        /// <param name="arr">The double[] to cast</param>
        public static implicit operator Filter(double[] arr) {
            return new Filter(arr);
        }
        /// <summary>
        /// Allows a Filter to be cast to a ComplexNumber[]
        /// </summary>
        /// <param name="s">The Filter to cast</param>
        public static implicit operator ComplexNumber[] (Filter s) {
            return s.Frequencies;
        }
        /// <summary>
        /// Creates a new averaging Filter
        /// </summary>
        /// <param name="p">The number of points for the filter</param>
        /// <returns>The Averaging Filter</returns>
        public static Filter Averaging(int p) {
            Filter result = new Filter(p);
            for(int i = 0; i < p; i++) {
                result[i] = 1.0 / p;
            }
            return result;
        }
        /// <summary>
        /// Applies a Low Pass Filter to a Signal
        /// </summary>
        /// <param name="s">The Signal to filter</param>
        /// <returns>The Filtered Signal</returns>
        public static Signal Low(Signal s) {
            Signal result = new Signal(s.Length);
            Signal fft = Fourier.FFT(s);
            for (int i = 0; i < result.Length; i++)
                result[i] = 0;
            int indexPass = (7 * 2);
            for (int i = 1; i < indexPass; i += 2) {
                result[i] = fft[i];
            }
            for (int i = result.Length - indexPass+1; i < result.Length; i += 2) {
                result[i] = fft[i];
            }

            return Fourier.InverseFFT(result);
        }
        /// <summary>
        /// Applies a High Pass Filter to a Signal
        /// </summary>
        /// <param name="s">The Signal to filter</param>
        /// <returns>The Filtered Signal</returns>
        public static Signal High(Signal s) {
            Signal fft = Fourier.FFT(s);
            int indexPass = (7 * 2) - 1;
            Signal result = new Signal(s.Length);
            for (int i = 0; i < result.Length; i++)
                result[i] = 0;
            for (int i = 15; i < result.Length - indexPass; i += 2) {
                result[i] = fft[i];
            }
            return Fourier.InverseFFT(result);
        }
        /// <summary>
        /// Applies a Band Filter to a Signal
        /// </summary>
        /// <param name="s">The Signal to filter</param>
        /// <returns>The Filtered Signal</returns>
        public static Signal Band(Signal s) {
            Signal fft = Fourier.FFT(s);
            Signal result = new Signal(s.Length);
            for (int i = 0; i < result.Length; i++)
                result[i] = 0;
            for (int i = 9; i < 16; i+=2) {
                result[i] = fft[i];
            }
            for (int i = 497; i < 504; i+=2) {
                result[i] = fft[i];
            }
            return Fourier.InverseFFT(result);
        }
        /// <summary>
        /// Applies a Notch Filter to a Signal
        /// </summary>
        /// <param name="s">The Signal to filter</param>
        /// <returns>The Filtered Signal</returns>
        public static Signal Notch(Signal s) {
            Signal fft = Fourier.FFT(s);
            Signal result = new Signal(s.Length);
            for (int i = 0; i < result.Length; i++)
                result[i] = fft[i];
            for (int i = 9; i < 16; i++) {
                result[i] = 0;
            }
            for (int i = 497; i < 504; i++) {
                result[i] = 0;
            }
            return Fourier.InverseFFT(result);
        }
    }
}
