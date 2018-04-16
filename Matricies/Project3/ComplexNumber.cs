using System;

namespace Project3 {
    public class ComplexNumber {
        private double real, imag;
        /// <summary>
        /// Creates a new ComplexNumber based on a real value and an imaginary value
        /// </summary>
        /// <param name="real">The real value</param>
        /// <param name="imag">The imaginary value</param>
        public ComplexNumber(double real, double imag) {
            this.real = real;
            this.imag = imag;
        }
        /// <summary>
        /// Gets the real value
        /// </summary>
        /// <returns>The real value</returns>
        public double GetReal() {
            return real;
        }
        /// <summary>
        /// Gets the imaginary value
        /// </summary>
        /// <returns>The imaginary value</returns>
        public double GetImag() {
            return imag;
        }
        /// <summary>
        /// Changes the real value
        /// </summary>
        /// <param name="real">The new real value</param>
        public void SetReal(double real) {
            this.real = real;
        }
        /// <summary>
        /// Changes the imaginary value
        /// </summary>
        /// <param name="imag">The new imaginary value</param>
        public void SetImag(double imag) {
            this.imag = imag;
        }
        /// <summary>
        /// Creates a string representation in the form: a + bi
        /// </summary>
        /// <returns>a + bi</returns>
        public override string ToString() {
            if (imag > 0)
                return string.Format("{0} + {1}i", real, imag);
            else if (imag == 0)
                return real.ToString();
            else
                return string.Format("{0} - {1}i", real, -imag);
        }

        public static explicit operator double(ComplexNumber v) {
            return v.GetReal();
        }

        /// <summary>
        /// Checks to see if 2 ComplexNumbers are equivalant
        /// </summary>
        /// <param name="other">The other Complex number</param>
        /// <returns>True if they are equivalent, false otherwise</returns>
        public override bool Equals(Object other) {
            if(other is ComplexNumber o)
                return real == o.GetReal() && imag == o.GetImag();
            return false;
        }
        /// <summary>
        /// Gets the Conjugate of the Complex number
        /// </summary>
        /// <returns>The Conjugate of the Complex number</returns>
        public ComplexNumber GetConjugate() {
            return new ComplexNumber(real, -imag);
        }
        /// <summary>
        /// Gets the Magnitude of the Complex Number
        /// </summary>
        /// <returns>The Magnitude of the Complex Number</returns>
        public double GetMagnitude() {
            return Math.Sqrt((real * real) + (imag * imag));
        }
        /// <summary>
        /// Gets the Phase of the Complex Number
        /// </summary>
        /// <returns>The Phase of the Complex Number</returns>
        public double GetPhase() {
            return Math.Atan2(imag, real);
        }
        /// <summary>
        /// Gets the Sin of the Complex Number
        /// </summary>
        /// <returns>The Sin of the Complex Number</returns>
        public ComplexNumber Sin() {
            return new ComplexNumber(Math.Sin(real)*Math.Cosh(imag),Math.Cos(real)*Math.Sinh(imag));
        }
        /// <summary>
        /// Gets the Cos of the Complex Number
        /// </summary>
        /// <returns>The Cos of the Complex Number</returns>
        public ComplexNumber Cos() {
            return new ComplexNumber(Math.Cos(real) * Math.Cosh(imag), -(Math.Sin(real) * Math.Sinh(imag)));
        }
        /// <summary>
        /// Gets the Tan of the Complex Number
        /// </summary>
        /// <returns>The Tan of the Complex Number</returns>
        public ComplexNumber Tan() {
            return Sin() / Cos();
        }
        /// <summary>
        /// Gets the hyperbolic Sin of the Complex Number
        /// </summary>
        /// <returns>The hyperbolic Sin of the Complex Number</returns>
        public ComplexNumber Sinh() {
            return new ComplexNumber(Math.Sinh(real) * Math.Cos(imag), Math.Cosh(real) * Math.Sin(imag));
        }
        /// <summary>
        /// Gets the hyperbolic Cos of the Complex Number
        /// </summary>
        /// <returns>The hyperbolic Cos of the Complex Number</returns>
        public ComplexNumber Cosh() {
            return new ComplexNumber(Math.Cosh(real) * Math.Cos(imag), Math.Sinh(real) * Math.Sin(imag));
        }
        /// <summary>
        /// Gets the hyperbolic Tan of the Complex Number
        /// </summary>
        /// <returns>The hyperbolic Tan of the Complex Number</returns>
        public ComplexNumber Tanh() {
            return Sinh() / Cosh();
        }
        public ComplexNumber Exp() {
            return new ComplexNumber(Math.Exp(real) * Math.Cos(imag), Math.Exp(real) * Math.Sin(imag));
        }
        /// <summary>
        /// Adds 2 Complex Numbers together
        /// </summary>
        /// <param name="a">The first Complex Number</param>
        /// <param name="b">The second Complex Number</param>
        /// <returns>The sum of the two Complex Numbers</returns>
        public static ComplexNumber operator+ (ComplexNumber a, ComplexNumber b) {
            double real = a.GetReal() + b.GetReal();
            double imag = a.GetImag() + b.GetImag();
            return new ComplexNumber(real, imag);
        }
        /// <summary>
        /// Subtracts two Complex Numbers
        /// </summary>
        /// <param name="a">The first Complex Number</param>
        /// <param name="b">The second Complex Number</param>
        /// <returns></returns>
        public static ComplexNumber operator- (ComplexNumber a, ComplexNumber b) {
            return a + -b;
        }
        /// <summary>
        /// Multiplies two Complex Numbers together
        /// </summary>
        /// <param name="a">The first Complex Number</param>
        /// <param name="b">The second Complex Number</param>
        /// <returns>The product of the two Complex Numbers</returns>
        public static ComplexNumber operator* (ComplexNumber a, ComplexNumber b) {
            double real = (a.GetReal() * b.GetReal()) - (a.GetImag() * b.GetImag());
            double imag = (a.GetReal() * b.GetImag()) + (a.GetImag() * b.GetReal());
            return new ComplexNumber(real, imag);
        }
        /// <summary>
        /// Divides two Complex Numbers
        /// </summary>
        /// <param name="a">The first Complex Number</param>
        /// <param name="b">The second Complex Number</param>
        /// <returns>The quotient of the two Complex Numbers</returns>
        public static ComplexNumber operator/ (ComplexNumber a, ComplexNumber b) {
            ComplexNumber conj = b.GetConjugate();
            ComplexNumber top = a * conj;
            ComplexNumber bottom = b * conj;
            if (bottom.GetReal() != 0.0 && bottom.GetImag() == 0.0)
                return new ComplexNumber(top.GetReal() / bottom.GetReal(), top.GetImag() / bottom.GetReal());
            else
                return null;
        }
        public static ComplexNumber operator/ (ComplexNumber a, double b) {
            return new ComplexNumber(a.real / b, a.imag / b);
        }
        /// <summary>
        /// Negates a Complex Number
        /// </summary>
        /// <param name="a">The Complex Number</param>
        /// <returns>The negated Complex Number</returns>
        public static ComplexNumber operator- (ComplexNumber a) {
            return new ComplexNumber(-a.GetReal(), -a.GetImag());
        }
        public static implicit operator ComplexNumber(double d) {
            return new ComplexNumber(d, 0.0);
        }
        public static ComplexNumber[][] MatrixMult(ComplexNumber[][] a, ComplexNumber[][] b) {
            if (a[0].Length != b.Length)
                throw new ArgumentException("A's width must be equal to B's height");
            ComplexNumber[][] result = new ComplexNumber[a.Length][];
            for(int i = 0; i < a.Length; i++) {
                for(int j = 0; j < b[0].Length; j++) {
                    for(int y = 0; y < a.Length; y++) {
                        result[i][j] = result[i][j] + (a[i][y] * b[y][j]);
                    }
                }
            }
            return result;
        }
    }
}
