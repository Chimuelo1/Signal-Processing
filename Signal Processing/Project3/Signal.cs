using System;
using System.Collections;
using System.Collections.Generic;

namespace SignalProcessing {
    /// <summary>
    /// A Signal used in signal processing
    /// </summary>
    public class Signal : IEnumerable<ComplexNumber> {
        private ComplexNumber[] frequencies;
        /// <summary>
        /// Gets the array of Complex numbers making up the Signal
        /// </summary>
        public ComplexNumber[] Frequencies => frequencies;
        /// <summary>
        /// Gets the length of the Signal
        /// </summary>
        public int Length => frequencies.Length;
        /// <summary>
        /// Creates a Signal based on a ComplexNumber[]
        /// </summary>
        /// <param name="frequencies">The Complex number[] to make the Signal</param>
        public Signal(ComplexNumber[] frequencies) {
            this.frequencies = frequencies;
        }
        /// <summary>
        /// Creates a Signal based on a double[]
        /// </summary>
        /// <param name="frequencies">The double[] to make the Signal</param>
        public Signal(double[] frequencies) {
            this.frequencies = new ComplexNumber[frequencies.Length];
            for(int i = 0; i < frequencies.Length; i++) {
                this.frequencies[i] = frequencies[i];
            }
        }
        /// <summary>
        /// Creates a blank Signal of the given size
        /// </summary>
        /// <param name="num">The size to make the Signal</param>
        public Signal(int num) {
            frequencies = new ComplexNumber[num];
        }
        /// <summary>
        /// Multiplies 2 Signals together
        /// </summary>
        /// <param name="a">The first signal</param>
        /// <param name="b">The second signal</param>
        /// <returns></returns>
        public static Signal operator *(Signal a, Signal b) {
            ComplexNumber[] signal = new ComplexNumber[a.Length];
            for(int i = 0; i < signal.Length; i++) {
                signal[i] = a[i] * b[i];
            }
            return new Signal(signal);
        }
        /// <summary>
        /// Gets a frequency in the Signal
        /// </summary>
        /// <param name="index">The index to get</param>
        /// <returns>The frequency at the given index</returns>
        public ComplexNumber this[int index] {
            get {
                return frequencies[index];
            }
            set {
                frequencies.SetValue(value, index);
            }
        }
        /// <summary>
        /// Allows a ComplexNumber[] to be cast to a Signal
        /// </summary>
        /// <param name="arr">The ComplexNumber[] to cast</param>
        public static implicit operator Signal(ComplexNumber[] arr) {
            return new Signal(arr);
        }
        /// <summary>
        /// Allows a double[] to be cast to a Signal
        /// </summary>
        /// <param name="arr">The double[] to cast</param>
        public static implicit operator Signal(double[] arr) {
            return new Signal(arr);
        }
        /// <summary>
        /// Allows a Signal to be cast to a ComplexNumber[]
        /// </summary>
        /// <param name="s">The Signal to cast</param>
        public static implicit operator ComplexNumber[](Signal s) {
            return s.Frequencies;
        }
        /// <summary>
        /// Allows a Signal to be cast to a double[]
        /// </summary>
        /// <param name="s">The Signal to cast</param>
        public static explicit operator double[] (Signal s) {
            double[] result = new double[s.Length];
            for (int i = 0; i < s.Length; i++)
                result[i] = s[i].Real;
            return result;
        }
        /// <summary>
        /// Divides a Signal by a Complex Number
        /// </summary>
        /// <param name="signal">The Signal to divide</param>
        /// <param name="scalar">The Complex Number scalar to divide by</param>
        /// <returns></returns>
        public static Signal operator /(Signal signal, ComplexNumber scalar) {
            Signal s = new Signal(signal.Length);
            for(int i = 0; i < signal.Length; i++) {
                s[i] = signal[i] / scalar;
            }
            return s;
        }
        /// <summary>
        /// Adds two Signals together
        /// </summary>
        /// <param name="a">The first Signal</param>
        /// <param name="b">The second Signal</param>
        /// <returns>The sum of the two Signals</returns>
        public static Signal operator +(Signal a, Signal b) {
            Signal result = new Signal(a.Length);
            for(int i = 0; i < result.Length; i++) {
                result[i] = a[i] + b[i];
            }
            return result;
        }
        /// <summary>
        /// Gets the Enumerator for the Signal
        /// </summary>
        /// <returns>The Enumerator for the Signal</returns>
        public IEnumerator<ComplexNumber> GetEnumerator() {
            return ((IEnumerable<ComplexNumber>)frequencies).GetEnumerator();
        }
        /// <summary>
        /// Gets the Enumerator for the Signal
        /// </summary>
        /// <returns>The Enumerator for the Signal</returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable<ComplexNumber>)frequencies).GetEnumerator();
        }
        /// <summary>
        /// Calculates the Power Spectral Density of the Signal
        /// </summary>
        /// <returns>The Power Spectral Density of the Signal</returns>
        public double[] PSD() {
            ComplexNumber[] fft = Fourier.FFT(Frequencies);
            double[] result = new double[Length];
            for (int i = 0; i < Length; i++) {
                result[i] = Math.Pow(fft[i].GetMagnitude(), 2.0);
            }
            return result;
        }
        /// <summary>
        /// Pads the Signal with 0s on either end to make the signal the desired length
        /// </summary>
        /// <param name="desiredLength">The desired length of the Signal</param>
        /// <returns>The Signal padded with 0s</returns>
        public Signal PadWithZeros(int desiredLength) {
            ComplexNumber[] result = new ComplexNumber[desiredLength];
            int j = 0;
            int index = (desiredLength - Length) / 2;
            for (int i = 0; i < desiredLength; i++) {
                if (i >= index && i < index + Length) {
                    result[i] = this[j];
                    j++;
                }
                else
                    result[i] = 0;
            }
            return result;
        }
        /// <summary>
        /// Gets the Conjugate for each frequency in the Signal
        /// </summary>
        /// <returns>A Signal made of the Conjugate of each frequency</returns>
        public Signal GetConjugate() {
            Signal s = new Signal(Length);
            for (int i = 0; i < Length; i++) {
                s[i] = this[i].GetConjugate();
            }
            return s;
        }
    }
}
