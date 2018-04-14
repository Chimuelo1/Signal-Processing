
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
                    ChangeColor(i, j, Color.White);
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
    }
}
