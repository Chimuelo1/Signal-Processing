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
            if(imag >= 0)
                return string.Format("{0} + {1}i", real, imag);
            else
                return string.Format("{0} - {1}i", real, -imag);
        }
        public bool Equals(ComplexNumber other) {
            return real == other.GetReal() && imag == other.GetImag();
        }
        public ComplexNumber GetConjugate() {
            return new ComplexNumber(real, -imag);
        }
        public double GetMagnitude() {
            return (real * real) + (imag * imag);
        }
        public ComplexNumber Sin() {
            return new ComplexNumber(Math.Sin(real)*Math.Cosh(imag),Math.Cos(real)*Math.Sinh(imag));
        }
        public ComplexNumber Cos() {
            return new ComplexNumber(Math.Cos(real) * Math.Cosh(imag), -(Math.Sin(real) * Math.Sinh(imag)));
        }
        public ComplexNumber Tan() {
            return Sin() / Cos();
        }
        public ComplexNumber Sinh() {
            return new ComplexNumber(Math.Sinh(real) * Math.Cos(imag), Math.Cosh(real) * Math.Sin(imag));
        }
        public ComplexNumber Cosh() {
            return new ComplexNumber(Math.Cosh(real) * Math.Cos(imag), Math.Sinh(real) * Math.Sin(imag));
        }
        public ComplexNumber Tanh() {
            return Sinh() / Cosh();
        }
        public static ComplexNumber operator+ (ComplexNumber a, ComplexNumber b) {
            double real = a.GetReal() + b.GetReal();
            double imag = a.GetImag() + b.GetImag();
            return new ComplexNumber(real, imag);
        }
        public static ComplexNumber operator* (ComplexNumber a, ComplexNumber b) {
            double real = (a.GetReal() * b.GetReal()) - (a.GetImag() * b.GetImag());
            double imag = (a.GetReal() * b.GetImag()) + (a.GetImag() * b.GetReal());
            return new ComplexNumber(real, imag);
        }
        public static ComplexNumber operator/ (ComplexNumber a, ComplexNumber b) {
            ComplexNumber conj = b.GetConjugate();
            ComplexNumber top = a * conj;
            ComplexNumber bottom = b * conj;
            if (bottom.GetReal() != 0.0 && bottom.GetImag() == 0.0)
                return new ComplexNumber(top.GetReal() / bottom.GetReal(), top.GetImag() / bottom.GetReal());
            else
                return null;
        }
        public static ComplexNumber operator- (ComplexNumber a) {
            return new ComplexNumber(-a.GetReal(), -a.GetImag());
        }

        static void Main(string[] args) {
            ComplexNumber a = new ComplexNumber(2, 3);
            ComplexNumber b = new ComplexNumber(4, -5);
            Console.WriteLine("({0}) / ({1}) \n= {2}", a, b, a / b);

            Console.WriteLine("\n\nPress any key to close");
            Console.ReadKey();
        }
    }
}
