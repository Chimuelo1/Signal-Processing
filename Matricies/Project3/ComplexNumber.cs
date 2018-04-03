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
            return string.Format("{0} + {1}i", real, imag);
        }
        public ComplexNumber GetConjugate() {
            return new ComplexNumber(real, -imag);
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
            Console.WriteLine("Conj: " + conj);
            ComplexNumber top = a * conj;
            ComplexNumber bottom = b * conj;
            Console.WriteLine(top + " / " + bottom);
            if (bottom.GetReal() != 0.0 && bottom.GetImag() == 0.0)
                return new ComplexNumber(top.GetReal() / bottom.GetReal(), top.GetImag() / bottom.GetReal());
            else
                return null;

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
