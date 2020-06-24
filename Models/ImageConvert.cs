using System.Drawing;

namespace ImageCrop.Models 
{
    public class ImageConvert
    {
        Bitmap bitmap;
        Bitmap cropped;

        public ImageConvert(Image image)
        {
            this.bitmap = new Bitmap(image);          
        }

        public void ConvertImage(string shape, double scale)
        {
            
            Rectangle r = new Rectangle();
            scale = scale/10.0;
            //square default
            Size s = GetSquare(scale);
            int targetW = 300;
            int targetH = 300;
            switch(shape)
            {
                case "wide":
                    s.Height = (int) (s.Height*(2.0/3.0));
                    targetW = 300;
                    targetH = 200;
                    break;
                case "tall":
                    s.Width = (int) (s.Width*(2.0/3.0));
                    targetW = 200;
                    targetH = 300;
                    break;
            } 
            r = new Rectangle(GetCenterCrop(s), s);            
            cropped = new Bitmap(bitmap.Clone(r, bitmap.PixelFormat), targetW, targetH);          
        }

        public Size GetSquare(double scale) 
        {
            double maxW = bitmap.Width;
            double maxH = bitmap.Height;
            if (maxW < maxH) {
                maxH = maxW;
            } else {
                maxW = maxH;
            }
            int scaledW = (int) (maxW*scale);
            int scaledH = (int) (maxH*scale);
            return new Size(scaledW, scaledH);
        }

        public Point GetCenterCrop(Size s)
        {
            int wCenter = bitmap.Width/2;
            int hCenter = bitmap.Height/2;
            int wOffCenter = wCenter - s.Width/2;
            int hOffCenter = hCenter - s.Height/2;
            return (new Point(wOffCenter, hOffCenter));           
        }

        public Bitmap GetCroppedImage() 
        { 
            return cropped;
        }
    }

}
