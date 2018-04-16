using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3 {
    public class Signal : IEnumerable<ComplexNumber> {
        private ComplexNumber[] frequencies;
        public ComplexNumber[] Frequencies => frequencies;
        public int Length => frequencies.Length;
        public Signal(ComplexNumber[] frequencies) {
            this.frequencies = frequencies;
        }
        public Signal(double[] frequencies) {
            this.frequencies = new ComplexNumber[frequencies.Length];
            for(int i = 0; i < frequencies.Length; i++) {
                this.frequencies[i] = frequencies[i];
            }
        }
        public Signal(int num) {
            frequencies = new ComplexNumber[num];
        }
        public static Signal operator *(Signal a, Signal b) {
            ComplexNumber[] signal = new ComplexNumber[a.Length];
            for(int i = 0; i < signal.Length; i++) {
                signal[i] = a[i] * b[i];
            }
            return new Signal(signal);
        }
        public ComplexNumber this[int index] {
            get {
                return frequencies[index];
            }
            set {
                frequencies.SetValue(value, index);
            }
        }
        public static implicit operator Signal(ComplexNumber[] arr) {
            return new Signal(arr);
        }
        public static implicit operator Signal(double[] arr) {
            return new Signal(arr);
        }
        public static implicit operator ComplexNumber[](Signal s) {
            return s.Frequencies;
        }
        public static Signal operator /(Signal signal, double scalar) {
            Signal s = new Signal(signal.Length);
            for(int i = 0; i < signal.Length; i++) {
                s[i] = signal[i] / scalar;
            }
            return s;
        }
        public IEnumerator<ComplexNumber> GetEnumerator() {
            return ((IEnumerable<ComplexNumber>)frequencies).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable<ComplexNumber>)frequencies).GetEnumerator();
        }
        public double[] PSD() {
            ComplexNumber[] fft = Fourier.FFT(Frequencies);
            double[] result = new double[Length];
            for (int i = 0; i < Length; i++) {
                result[i] = Math.Pow(fft[i].GetMagnitude(), 2.0);
            }
            return result;
        }
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
        public Signal GetConjugate() {
            Signal s = new Signal(Length);
            for (int i = 0; i < Length; i++) {
                s[i] = this[i].GetConjugate();
            }
            return s;
        }
    }
}
