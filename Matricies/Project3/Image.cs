
using System.Drawing;
namespace Project3 {
    class Image {
        public Bitmap image { get; }
        public double Width => image.Width;
        public double Height => image.Height;
        public Image(int width, int height) {
            image = new Bitmap(width, height);
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    ChangeColor(i, j, Color.Black);
                }
            }
        }
        public Image(ComplexNumber[][] matrix) {
            image = new Bitmap(matrix[0].Length, matrix.Length);
            for(int i = 0; i < matrix.Length; i++) {
                for(int j = 0; j < matrix[0].Length; j++) {
                    ChangeColor(j, i, Color.FromArgb((int)matrix[i][j].GetReal()));
                }
            }
        }
        public Image(string fileName) {
            image = new Bitmap(fileName);
        }
        public void Save(string fileName) {
            image.Save(fileName);
        }
        public void ChangeColor(int x, int y, Color color) {
            image.SetPixel(x, y, color);
        }
        public Color[] this[int index] {
            get {
                Color[] arr = new Color[(int)Width];
                for (int i = 0; i < Width; i++)
                    arr[i] = image.GetPixel(i, index);
                return arr;
            }
        }
        public void Set(int x, int y, Color color) {
            image.SetPixel(x, y, color);
        }
        public ComplexNumber[][] GetMatrix() {
            ComplexNumber[][] matrix = new ComplexNumber[(int)Height][];
            for(int i = 0; i < Height; i++) {
                matrix[i] = new ComplexNumber[(int)Width];
                for(int j = 0; j < Width; j++) {
                    matrix[i][j] = image.GetPixel(j,i).ToArgb();
                }
            }
            return matrix;
        }
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
