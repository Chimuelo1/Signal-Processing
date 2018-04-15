using System;
using Matricies;
using NAudio.Wave;
using System.Collections.Generic;
using NAudio.Wave.SampleProviders;


namespace Project3 {
    public class Functions {
        /// <summary>
        /// The F function with a number of different S values
        /// </summary>
        /// <param name="s">The S values</param>
        /// <param name="numSamples">The number of samples to use</param>
        /// <returns>A matrix with the first column being the T values used</returns>
        public static Matrix F(int[] s, int numSamples) {
            Matrix m = new Matrix(numSamples, s.Length + 1);
            for (int j = 0; j < s.Length; j++) {
                for (int i = 0; i < numSamples; i++) {
                    double sum = 0.0;
                    double t = (double)i / numSamples;
                    if (j == 0)
                        m[i][0] = t;
                    for (int k = 1; k <= s[j]; k++) {
                        double val = (Math.Sin(2.0 * Math.PI * (2.0 * k - 1.0) * t)) / (2.0 * k - 1.0);
                        sum += val;
                    }
                    m[i][j + 1] = sum;
                }
            }
            m.WriteToFile("..\\..\\fMatrix.csv");
            return m;
        }
        /// <summary>
        /// The F function with only 1 S value
        /// </summary>
        /// <param name="s">The current S value</param>
        /// <param name="numSamples">The number of samples to use</param>
        /// <returns>The result in the 1st(and only) column of a Matrix</returns>
        public static Matrix F(int s, int numSamples) {
            Matrix m = new Matrix(numSamples, 1);
            for (int i = 0; i < numSamples; i++) {
                double sum = 0.0;
                double t = (double)i / numSamples;
                for (int k = 1; k <= s; k++) {
                    sum += (Math.Sin(2.0 * Math.PI * (2.0 * k - 1.0) * t)) / (2.0 * k - 1.0);
                }
                m[i][0] = sum;
            }
            m.WriteToFile("..\\..\\f2Matrix.csv");
            return m;
        }
        /// <summary>
        /// The G function with a number of different S values
        /// </summary>
        /// <param name="s">The S values</param>
        /// <param name="numSamples">The number of samples to use</param>
        /// <returns>A matrix with the first column being the T values used</returns>
        public static Matrix G(int[] s, int numSamples) {
            Matrix m = new Matrix(numSamples, s.Length + 1);
            for (int j = 0; j < s.Length; j++) {
                for (int i = 0; i < numSamples; i++) {
                    double sum = 0.0;
                    double t = (double)i / numSamples;
                    if (j == 0)
                        m[i][0] = t;
                    for (int k = 1; k <= s[j]; k++) {
                        double val = (Math.Sin(2.0 * Math.PI * (2.0 * k) * t)) / (2.0 * k);
                        sum += val;
                    }
                    m[i][j + 1] = sum;
                }
            }
            m.WriteToFile("..\\..\\gMatrix.csv");
            return m;
        }
        /// <summary>
        /// The G function with only 1 S value
        /// </summary>
        /// <param name="s">The current S value</param>
        /// <param name="numSamples">The number of samples to use</param>
        /// <returns>The result in the 1st(and only) column of a Matrix</returns>
        public static Matrix G(int s, int numSamples) {
            Matrix m = new Matrix(numSamples, 1);
            for (int i = 0; i < numSamples; i++) {
                double sum = 0.0;
                double t = (double)i / numSamples;
                for (int k = 1; k <= s; k++) {
                    sum += (Math.Sin(2.0 * Math.PI * (2.0 * k) * t)) / (2.0 * k);
                }
                m[i][0] = sum;
            }
            m.WriteToFile("..\\..\\f2Matrix.csv");
            return m;
        }
        /// <summary>
        /// The V function 
        /// </summary>
        /// <param name="freq">The current frequency</param>
        /// <param name="numSamples">The number of samples to use</param>
        /// <returns>An array containing the result of V</returns>
        public static double[] V(int freq, int numSamples) {
            double[] result = new double[numSamples];
            for (int i = 0; i < numSamples; i++) {
                double t = (double)i / numSamples;
                result[i] = Math.Sin(2.0 * freq * Math.PI * t);
            }
            return result;
        }
        /// <summary>
        /// The X function
        /// </summary>
        /// <param name="numSamples">The number of samples to use</param>
        /// <returns>An array containing the result of X</returns>
        public static double[] X(int numSamples) {
            double[] result = new double[numSamples];
            double[] v1 = V(13, numSamples);
            double[] v2 = V(31, numSamples);
            for (int i = 0; i < numSamples; i++) {
                result[i] = v1[i] + v2[i];
            }
            return result;
        }
        /// <summary>
        /// The Y function
        /// </summary>
        /// <param name="numSamples">The number of samples to use</param>
        /// <returns>An array containing the result of Y</returns>
        public static double[] Y(int numSamples) {
            double[] result = new double[numSamples];
            double[] v1 = V(13, numSamples);
            double[] v2 = V(31, numSamples);
            for (int i = 0; i < numSamples; i++) {
                result[i] = v1[i] * v2[i];
            }
            return result;
        }
        public static double[] ReadWav(string fileName) {
            List<double> result = new List<double>();
            AudioFileReader reader = new AudioFileReader(fileName);
            float[] buffer = new float[reader.WaveFormat.SampleRate];
            int read;
            do {
                read = reader.Read(buffer, 0, buffer.Length);
                for (int i = 0; i < read; i++) {
                    result.Add(buffer[i]);
                }
            } while (read > 0);
            return result.ToArray();
        }
        public static void WriteWav(string fileName, double[] data) {
            WaveFormat format = new WaveFormat(44100,32,1);
            WaveFileWriter writer = new WaveFileWriter(fileName,format);
            byte[] bytes = new byte[data.Length];
            for (int i = 0; i < data.Length; i++) {
                float f = (float)data[i];
                writer.WriteSample(f);
                
            }
        }
        public static double[][] GetDataFromFile(string fileName) {
            List<double>[] data = new List<double>[2];
            data[0] = new List<double>();
            data[1] = new List<double>();
            bool inReturn = false;
            string[] file = System.IO.File.ReadAllLines(fileName);
            for(int i = 0; i < file.Length; i++) {
                if(file[i].Contains("sample") || file[i].Trim().Length == 0) {
                    if (file[i].Contains("1024"))
                        inReturn = true;
                    continue;
                }
                double d = double.Parse(file[i].Trim());
                if (inReturn)
                    data[1].Add(d); //Recieved values
                else
                    data[0].Add(d); //Transmitted values
            }
            double[][] result = new double[2][];
            for (int i = 0; i < data.Length; i++)
                result[i] = data[i].ToArray();
            return result;
        }
        public static ComplexNumber[] PadWithZeroes(double[] arr, int desiredLength) {
            return PadWithZeroes(Fourier.ConvertArray(arr), desiredLength);
        }
        public static ComplexNumber[] PadWithZeroes(ComplexNumber[] arr, int desiredLength) {
            ComplexNumber[] result = new ComplexNumber[desiredLength];
            int j = 0;
            int index = (desiredLength - arr.Length) / 2;
            for (int i = 0; i < desiredLength; i++) {
                if (i >= index && i < index + arr.Length) {
                    result[i] = arr[j];
                    j++;
                }
                else
                    result[i] = 0;
            }
            return result;
        }
        public static double CalcDistance(double velocity, double time) {
            double v = velocity / 2;
            return v * time;
        }
    }
}
