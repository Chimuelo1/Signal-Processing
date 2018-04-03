using System;

namespace Project3 {
    class ComplexNumber {
        double real, imag;
        public ComplexNumber(double real, double imag) {
            this.real = real;
            this.imag = imag;
        }
        public double GetReal() {
            return real;
        }
        public double GetImag() {
            return imag;
        }
        public void SetReal(double real) {
            this.real = real;
        }
        public void SetImag(double imag) {
            this.imag = imag;
        }
        public override string ToString() {
            return string.Format("({0} + {1}i)", real, imag);
        }

        static void Main(string[] args) {


            Console.WriteLine("\n\nPress any key to close");
            Console.ReadKey();
        }
    }
}
