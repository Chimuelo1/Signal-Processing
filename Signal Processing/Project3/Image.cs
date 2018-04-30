using System.Drawing;

namespace SignalProcessing {
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
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    Set(j,i, Color.Black);
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
                    matrix[i][j] = BMap.GetPixel(i,j).ToArgb();
                }
            }
            return matrix;
        }
        /// <summary>
        /// Gets all red values in a Signal2D of the Image
        /// </summary>
        /// <returns>The red values in a Signal2D</returns>
        public Signal2D GetRedMatrix() {
            Signal2D matrix = new Signal2D((int)Height, (int)Width);
            for(int i = 0; i < Height; i++) {
                matrix[i] = new Signal(matrix.Width);
                for(int j = 0; j < Width; j++) {
                    matrix[i][j] = BMap.GetPixel(i,j).R;
                }
            }
            return matrix;
        }
        /// <summary>
        /// Gets all green values in a Signal2D of the Image
        /// </summary>
        /// <returns>The green values in a Signal2D</returns>
        public Signal2D GetGreenMatrix() {
            Signal2D matrix = new Signal2D((int)Height, (int)Width);
            for (int i = 0; i < Height; i++) {
                matrix[i] = new Signal(matrix.Width);
                for (int j = 0; j < Width; j++) {
                    matrix[i][j] = BMap.GetPixel(i,j).G;
                }
            }
            return matrix;
        }
        /// <summary>
        /// Gets all blue values in a Signal2D of the Image
        /// </summary>
        /// <returns>The blue values in a Signal2D</returns>
        public Signal2D GetBlueMatrix() {
            Signal2D matrix = new Signal2D((int)Height, (int)Width);
            for (int i = 0; i < Height; i++) {
                matrix[i] = new Signal(matrix.Width);
                for (int j = 0; j < Width; j++) {
                    matrix[i][j] = BMap.GetPixel(i,j).B;
                }
            }
            return matrix;
        }
        /// <summary>
        /// Deconstructs the Image into 3 Signal2Ds: red, green, and blue
        /// </summary>
        /// <returns>A Signal2D array with 3 Signal2Ds for each color</returns>
        public Signal2D[] Deconstruct() {
            Signal2D[] rgb = new Signal2D[] {new Signal2D((int)Height,(int)Width), new Signal2D((int)Height, (int)Width), new Signal2D((int)Height, (int)Width) };
            for(int i = 0; i < Height; i++) {
                for(int j = 0; j < Width; j++) {
                    rgb[0][i][j] = BMap.GetPixel(i,j).R;
                    rgb[1][i][j] = BMap.GetPixel(i,j).G;
                    rgb[2][i][j] = BMap.GetPixel(i,j).B;
                }
            }
            return rgb;
        }
        /// <summary>
        /// Reconstructs the Image from 3 Signal2Ds each representing R G or B
        /// </summary>
        /// <param name="red">The red signal</param>
        /// <param name="green">The green signal</param>
        /// <param name="blue">The blue signal</param>
        /// <returns>An Image created from the rgb signals</returns>
        public static Image Reconstruct(Signal2D red, Signal2D green, Signal2D blue) {
            Image image = new Image(red.Width, red.Height);
            for(int i = 0; i < image.Height; i++) {
                for(int j = 0; j < image.Width; j++) {
                    int r = (int)red[i][j].Real;
                    if (red[i][j].Real > 255)
                        red[i][j] = 255;
                    if (green[i][j].Real > 255)
                        green[i][j] = 255;
                    if (blue[i][j].Real > 255)
                        blue[i][j] = 255;
                    if (red[i][j].Real < 0)
                        red[i][j] = 0;
                    if (green[i][j].Real < 0)
                        green[i][j] = 0;
                    if (blue[i][j].Real < 0)
                        blue[i][j] = 0;
                    Color c = Color.FromArgb((int)red[i][j], (int)green[i][j], (int)blue[i][j]);
                    image.Set(i,j, c);
                }
            }
            return image;
        }
        /// <summary>
        /// Creates the Image A 
        /// </summary>
        /// <returns>Image A</returns>
        public static Image GetSignalImage() {
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
        public static Image GetPulseImage() {
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
        /// <summary>
        /// Marks the values within 10% of the max as red
        /// </summary>
        /// <returns>The image with the 10% highest values marked red</returns>
        public Image MarkRed() {
            Image im = this;
            double index = 255.0 - (255.0 * .1);
            for(int i = 0; i < Height; i++) {
                System.Console.Write(i+"\r");
                for(int j = 0; j < Width; j++) {
                    int val = BMap.GetPixel(j, i).R;
                    if(val > index) {
                        BMap.SetPixel(j, i, Color.FromArgb(val, 0, 0));
                    }
                }
            }
            return im;
        }
    }
}
