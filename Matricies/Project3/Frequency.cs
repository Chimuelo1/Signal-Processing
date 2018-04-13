using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3 {
    class Frequency : IComparable {
        private ComplexNumber value;
        private int index;
        /// <summary>
        /// The Complex number value of the frequency
        /// </summary>
        public ComplexNumber Value => value;
        /// <summary>
        /// The index in the Signal where the frequency is
        /// </summary>
        public int Index => index;
        /// <summary>
        /// Creates a new Frequency
        /// </summary>
        /// <param name="freq">The value of the Frequency</param>
        /// <param name="index">The index in the Signal of the frequency</param>
        public Frequency(ComplexNumber freq, int index) {
            value = freq;
            this.index = index;
        }
        /// <summary>
        /// Compares the Magnitude of 2 Frequencies
        /// </summary>
        /// <param name="obj">The other Frequency</param>
        /// <returns>A comparison of the 2 Magnitudes</returns>
        public int CompareTo(object obj) {
            return value.GetMagnitude().CompareTo(((Frequency)obj).Value.GetMagnitude());
        }
        /// <summary>
        /// Checks if this Frequency is equal to another
        /// </summary>
        /// <param name="other">The other Frequency to check</param>
        /// <returns>True if both values are equal and they are at the same index</returns>
        public override bool Equals(Object other) {
            if(other is Frequency o) {
                if (value.Equals(o.Value) && index == o.Index)
                    return true;
            }
            return false;
        }
    }
}
