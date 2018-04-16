using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3 {
    class Filter {
        public static ComplexNumber[] Averaging(int p) {
            ComplexNumber[] result = new ComplexNumber[p];
            for(int i = 0; i < p; i++) {
                result[i] = 1.0 / p;
            }
            return result;
        }
        public static ComplexNumber[] LowPass(int len, int h, int s) {
            ComplexNumber[] result = new ComplexNumber[len];
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
        public static ComplexNumber[] HighPass(int len, int m, int s) {
            ComplexNumber[] result = new ComplexNumber[len];
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
        public static ComplexNumber[] BandPass(int len, int m, int h, int s) {
            ComplexNumber[] result = new ComplexNumber[len];
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
        public static ComplexNumber[] Notch(int len, int h, int m, int s) {
            ComplexNumber[] result = new ComplexNumber[len];
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
