using System;
using NAudio.Wave;
using System.Collections.Generic;
using NAudio.Wave.SampleProviders;
using System.Linq;

namespace Project3 {
    /// <summary>
    /// Misc Functions used to generate and interact with Signals, mainly used for Signal generation
    /// </summary>
    public class Functions {
        /// <summary>
        /// The F function with only 1 S value
        /// </summary>
        /// <param name="s">The current S value</param>
        /// <param name="numSamples">The number of samples to use</param>
        /// <returns>The result in the 1st(and only) column of a Matrix</returns>
        public static Signal F(int s, int numSamples) {
            Signal m = new Signal(numSamples);
            for (int i = 0; i < numSamples; i++) {
                double sum = 0.0;
                double t = (double)i / numSamples;
                for (int k = 1; k <= s; k++) {
                    sum += (Math.Sin(2.0 * Math.PI * (2.0 * k - 1.0) * t)) / (2.0 * k - 1.0);
                }
                m[i] = sum;
            }
            return m;
        }
        /// <summary>
        /// The G function with only 1 S value
        /// </summary>
        /// <param name="s">The current S value</param>
        /// <param name="numSamples">The number of samples to use</param>
        /// <returns>The result in the 1st(and only) column of a Matrix</returns>
        public static Signal G(int s, int numSamples) {
            Signal m = new Signal(numSamples);
            for (int i = 0; i < numSamples; i++) {
                double sum = 0.0;
                double t = (double)i / numSamples;
                for (int k = 1; k <= s; k++) {
                    sum += (Math.Sin(2.0 * Math.PI * (2.0 * k) * t)) / (2.0 * k);
                }
                m[i] = sum;
            }
            return m;
        }
        /// <summary>
        /// The V function 
        /// </summary>
        /// <param name="freq">The current frequency</param>
        /// <param name="numSamples">The number of samples to use</param>
        /// <returns>An array containing the result of V</returns>
        public static Signal V(int freq, int numSamples) {
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
        public static Signal X(int numSamples) {
            double[] result = new double[numSamples];
            double[] v1 = (double[])V(13, numSamples);
            double[] v2 = (double[])V(31, numSamples);
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
        public static Signal Y(int numSamples) {
            double[] result = new double[numSamples];
            double[] v1 = (double[])V(13, numSamples);
            double[] v2 =(double[]) V(31, numSamples);
            for (int i = 0; i < numSamples; i++) {
                result[i] = v1[i] * v2[i];
            }
            return result;
        }
        /// <summary>
        /// The H function
        /// </summary>
        /// <param name="numSamples">The number of samples to use</param>
        /// <param name="c">The c value used in the function</param>
        /// <returns>/The H Signal</returns>
        public static Signal H(int numSamples, double c) {
            Signal signal = new Signal(numSamples);
            for(int i = 0; i < numSamples; i++) {
                double t = ((double)i / numSamples) - c;
                signal[i] = Math.Sin(20.0 * Math.PI * t);
            }
            return signal;
        }
        /// <summary>
        /// Reads a Signal from a .wav file
        /// </summary>
        /// <param name="fileName">The .wav file to read</param>
        /// <returns>The Signal from the .wav file</returns>
        public static Signal ReadWav(string fileName) {
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
            reader.Close();
            return result.ToArray();
        }
        /// <summary>
        /// Plays a .wav file 
        /// </summary>
        /// <param name="fileName">The .wav file</param>
        public static void PlayWav(string fileName) {
            WaveFileReader reader = new WaveFileReader(fileName);
            WaveOut waveOut = new WaveOut();
            waveOut.Init(reader);

            waveOut.Play();
            while (waveOut.PlaybackState == PlaybackState.Playing)
                System.Threading.Thread.Sleep(1000);
            reader.Close();
        }
        /// <summary>
        /// Creates a .wav File based on a Signal
        /// </summary>
        /// <param name="fileName">The name of the File to make</param>
        /// <param name="data">The Signal to write</param>
        /// <param name="rate">The sample rate of the Signal</param>
        public static void WriteWav(string fileName, Signal data, int rate) {
            float[] dataFloat = new float[data.Length];
            for (int i = 0; i < data.Length; i++)
                dataFloat[i] = (float)data[i].Real;
            WaveFormat format = new WaveFormat(rate,24,1);
            WaveFileWriter writer = new WaveFileWriter(fileName,format);
            writer.WriteSamples(dataFloat,0,data.Length);
            writer.Close();
        }
        /// <summary>
        /// Gets 2 Signals, a pulse and the recieved values from a file, index 0 contains the pulse, 1 contains the returned values
        /// </summary>
        /// <param name="fileName">The file containing the Signals</param>
        /// <returns>The pulse and returned Signals</returns>
        public static Signal[] GetDataFromFile(string fileName) {
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
            Signal[] result = new Signal[2];
            for (int i = 0; i < data.Length; i++)
                result[i] = data[i].ToArray();
            return result;
        }
        /// <summary>
        /// Calculates the distance to a reflector based on the speed of sound in water and the time the frequency was recieved
        /// </summary>
        /// <param name="velocity">The velocity of sound in water</param>
        /// <param name="time">The time the frequency was recieved</param>
        /// <returns>The distance to a reflector</returns>
        public static double CalcDistance(double velocity, double time) {
            return velocity * time / 2;
        }
    }
}
