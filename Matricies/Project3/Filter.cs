using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3 {
    class Filter : Signal {
        public Filter(double[] frequencies) : base(frequencies) {

        }
        public Filter(ComplexNumber[] frequencies) : base(frequencies) {

        }
        public Filter(int length) :base(length) {
            
        }
        public static implicit operator Filter(ComplexNumber[] arr) {
            return new Filter(arr);
        }
        public static implicit operator Filter(double[] arr) {
            return new Filter(arr);
        }
        public static implicit operator ComplexNumber[] (Filter s) {
            return s.Frequencies;
        }
        public static Filter Averaging(int p) {
            Filter result = new Filter(p);
            for(int i = 0; i < p; i++) {
                result[i] = 1.0 / p;
            }
            return result;
        }
        public static Filter LowPass(int len, int h, int s) {
            Filter result = new Filter(len);
            for (int i = 0; i < len; i++) {
                if (i == 0) {
                    result[i] = (2.0 * h) / s;
                }
                else {
                    result[i] = (1.0 / (Math.PI * i)) * Math.Sin((2.0 * i * Math.PI * h) / s);
                }
            }
            return result;
        }
        public static Filter HighPass(int len, int m, int s) {
            Filter result = new Filter(len);
            for (int i = 0; i < len; i++) {
                if (i == 0) {
                    result[i] = (s - 2.0 * m) / s;
                }
                else {
                    result[i] = (-1.0 / (Math.PI * i)) * Math.Sin((2.0 * i * Math.PI * m) / s);
                }
            }
            return result;
        }
        public static Filter BandPass(int len, int m, int h, int s) {
            Filter result = new Filter(len);
            for (int i = 0; i < len; i++) {
                if (i == 0) {
                    result[i] = (2.0 * (h - m)) / s;
                }
                else {
                    result[i] = (1.0 / (Math.PI * i)) * (Math.Sin((2.0 * i * Math.PI * h) / s) - Math.Sin((2.0 * i * Math.PI * m)));
                }
            }
            return result;
        }
        public static Filter Notch(int len, int h, int m, int s) {
            Filter result = new Filter(len);
            for (int i = 0; i < len; i++) {
                if (i == 0) {
                    result[i] = (s - (2.0 * (h - m))) / s;
                }
                else {
                    result[i] = (1.0 / (Math.PI * i)) * (Math.Sin((2.0 * i * Math.PI * m) / s) - Math.Sin((2.0 * i * Math.PI * h)));
                }
            }
            return result;
        }
        public static ComplexNumber BarlettWindow(int p,int i) {
            return 1.0 - Math.Abs(i)/(double)p;
        }
        public static ComplexNumber WelchWindow(int p, int i) {
            return 1.0 - Math.Pow(Math.Abs(i) / (double)p,2.0);
        }
        public static ComplexNumber HanningWindow(int p, int i) {
            return .5 * (1 + Math.Cos((i * Math.PI) / p));
        }
        public static ComplexNumber HammingWindow(int p, int i) {
            return .54 + .46 * Math.Cos((i * Math.PI) / p);
        }
 
    }
}
