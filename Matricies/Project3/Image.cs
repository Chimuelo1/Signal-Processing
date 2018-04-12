
using System.Drawing;
namespace Project3 {
    class Image {
        public Bitmap image { get; }
        public double Width => image.Width;
        public double Height => image.Height;
        public Image(int width, int height) {
            image = new Bitmap(width, height);
            for(int i = 0; i < width; i++) {
                for(int j = 0; j < height; j++) {
                    ChangeColor(i, j, Color.White);
                }
            }
        }
        public Image(string fileName) {
            image = new Bitmap(fileName);
        }
        public void Save(string fileName) {
            image.Save(fileName);
        }
        public void ChangeColor(int x, int y,Color color) {
            image.SetPixel(x, y, color);
        }
    }
}
