﻿using System.Drawing;

namespace Project3 {
    /// <summary>
    /// An Image made of a Bitmap used for Signal processing of Images
    /// </summary>
    public class Image {
        /// <summary>
        /// Gets the Bitmap Image
        /// </summary>
        public Bitmap BMap { get; }
        /// <summary>
        /// Gets the Width of the Image
        /// </summary>
        public double Width => BMap.Width;
        /// <summary>
        /// Gets the Height of the Image
        /// </summary>
        public double Height => BMap.Height;
        /// <summary>
        /// Creates a new Image based on the given dimensions
        /// </summary>
        /// <param name="width">The width of the new Image</param>
        /// <param name="height">The height of the new Image</param>
        public Image(int width, int height) {
            BMap = new Bitmap(width, height);
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    Set(i, j, Color.Black);
                }
            }
        }
        /// <summary>
        /// Creates a new Image based on a Signal2D
        /// </summary>
        /// <param name="matrix">The Signal2D to base off of</param>
        public Image(Signal2D matrix) {
            BMap = new Bitmap(matrix.Width, matrix.Height);
            for(int i = 0; i < matrix.Height; i++) {
                for(int j = 0; j < matrix[0].Length; j++) {
                    Set(j, i, Color.FromArgb((int)matrix[i][j].Real));
                }
            }
        }
        /// <summary>
        /// Creates a new Image based on a Image file
        /// </summary>
        /// <param name="fileName">The Image file to base off of</param>
        public Image(string fileName) {
            if(!fileName.Contains(".csv"))
                BMap = new Bitmap(fileName);
            else {
                string[] lines = System.IO.File.ReadAllLines(fileName);
                ComplexNumber[][] matrix = new ComplexNumber[lines.Length][];
                for(int i = 0; i < lines.Length; i++) {
                    string[] line = lines[i].Split(',');
                    matrix[i] = new ComplexNumber[line.Length];
                    for(int j = 0; j < line.Length; j++) {
                        matrix[i][j] = double.Parse(line[j].Trim());
                    }

                }
                BMap = new Bitmap(matrix[0].Length, matrix.Length);
                for (int i = 0; i < matrix.Length; i++) {
                    for (int j = 0; j < matrix[0].Length; j++) {
                        int col = (int)((double)matrix[i][j]);
                        Set(j, i, Color.FromArgb(col,col,col));
                    }
                }
            }
        }
        /// <summary>
        /// Saves the Image to a file
        /// </summary>
        /// <param name="fileName">The name of the File to save to</param>
        public void Save(string fileName) {
            BMap.Save(fileName);
        }
        /// <summary>
        /// Changes the color of a certain pixel in the Image
        /// </summary>
        /// <param name="x">The x coordinate of the Pixel</param>
        /// <param name="y">The y coordinate of the Pixel</param>
        /// <param name="color">The Color to change to</param>
        public void Set(int x, int y, Color color) {
            BMap.SetPixel(x, y, color);
        }
        /// <summary>
        /// Gets the row at the given index
        /// </summary>
        /// <param name="index">The index to get</param>
        /// <returns>The row at the given index</returns>
        public Color[] this[int index] {
            get {
                Color[] arr = new Color[(int)Width];
                for (int i = 0; i < Width; i++)
                    arr[i] = BMap.GetPixel(i, index);
                return arr;
            }
        }
        /// <summary>
        /// Gets a Signal2D representation of the Image
        /// </summary>
        /// <returns>The Signal2D of the Image</returns>
        public Signal2D GetMatrix() {
            Signal2D matrix = new Signal2D((int)Height,(int)Width);
            for(int i = 0; i < Height; i++) {
                matrix[i] = new Signal(matrix.Width);
                for(int j = 0; j < Width; j++) {
                    matrix[i][j] = BMap.GetPixel(j,i).ToArgb();
                }
            }
            return matrix;
        }
        public Signal2D GetRedMatrix() {
            Signal2D matrix = new Signal2D((int)Height, (int)Width);
            for(int i = 0; i < Height; i++) {
                matrix[i] = new Signal(matrix.Width);
                for(int j = 0; j < Width; j++) {
                    matrix[i][j] = BMap.GetPixel(j, i).R;
                }
            }
            return matrix;
        }
        public Signal2D GetGreenMatrix() {
            Signal2D matrix = new Signal2D((int)Height, (int)Width);
            for (int i = 0; i < Height; i++) {
                matrix[i] = new Signal(matrix.Width);
                for (int j = 0; j < Width; j++) {
                    matrix[i][j] = BMap.GetPixel(j, i).G;
                }
            }
            return matrix;
        }
        public Signal2D GetBlueMatrix() {
            Signal2D matrix = new Signal2D((int)Height, (int)Width);
            for (int i = 0; i < Height; i++) {
                matrix[i] = new Signal(matrix.Width);
                for (int j = 0; j < Width; j++) {
                    matrix[i][j] = BMap.GetPixel(j, i).B;
                }
            }
            return matrix;
        }
        public Signal2D[] Deconstruct() {
            Signal2D[] rgb = new Signal2D[] {new Signal2D((int)Height,(int)Width), new Signal2D((int)Height, (int)Width), new Signal2D((int)Height, (int)Width) };
            for(int i = 0; i < Height; i++) {
                for(int j = 0; j < Width; j++) {
                    rgb[0][i][j] = BMap.GetPixel(j, i).R;
                    rgb[1][i][j] = BMap.GetPixel(j, i).G;
                    rgb[2][i][j] = BMap.GetPixel(j, i).B;
                }
            }
            return rgb;
        }
        public static Image Reconstruct(Signal2D red, Signal2D green, Signal2D blue) {
            Image image = new Image(red.Width, red.Height);
            for(int i = 0; i < image.Height; i++) {
                for(int j = 0; j < image.Width; j++) {
                    Color c = Color.FromArgb((int)red[i][j], (int)green[i][j], (int)blue[i][j]);
                    image.Set(j, i, c);
                }
            }
            return image;
        }
        /// <summary>
        /// Creates the Image A 
        /// </summary>
        /// <returns>Image A</returns>
        public static Image GetImageA() {
            Image a = new Image(512, 512);
            for(int row = 0; row < a.Height; row++) {
                for(int col = 0; col < a.Width; col++) {
                    if(row >= 180 && row < 320 && col >= 220 && col < 330)
                        a.Set(col,row, Color.White);
                    if (row >= ((320+180) / 2) - 45 && row < ((320 + 180) / 2) + 45 && col >= 330 - 30 && col < 330)
                        a.Set(col, row, Color.Black);
                }
            }
            return a;
        }
        /// <summary>
        /// Creates the Image B
        /// </summary>
        /// <returns>Image B</returns>
        public static Image GetImageB() {
            Image b = new Image(512, 512);
            for (int row = 0; row < b.Height; row++) {
                for (int col = 0; col < b.Width; col++) {
                    if (row < 120 && col < 30)
                        b.Set(col, row, Color.White);
                    if (row >= 15 && row < 105 && col >= 15 && col < 30)
                        b.Set(col, row, Color.Black);
                }
            }
            return b;
        }
    }
}
