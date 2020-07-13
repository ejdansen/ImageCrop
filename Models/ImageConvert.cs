using System.Drawing;
using System.Collections.Generic;
using System;

namespace ImageCrop.Models 
{
    public class ImageConvert
    {
        Bitmap bitmap;

        public Dictionary<string, double> TargetAspectWHDict;

        public ImageConvert(Image image)
        {
            this.bitmap = new Bitmap(image); 
            TargetAspectWHDict = new Dictionary<string, double>();
            TargetAspectWHDict.Add("extrawide", 3.0/1.0);
            TargetAspectWHDict.Add("wide", 3.0/2.0);
            TargetAspectWHDict.Add("tall", 2.0/3.0);
            TargetAspectWHDict.Add("extratall",1.0/3.0);
            TargetAspectWHDict.Add("square", 1.0);      
        }

        public Bitmap ConvertImage(string shape, double scale)
        {

            double targetAspectRatioWH;

            TargetAspectWHDict.TryGetValue(shape, out targetAspectRatioWH);
            Size s = new Size(
                Math.Min(Convert.ToInt32(targetAspectRatioWH*bitmap.Height), bitmap.Width),
                Math.Min(Convert.ToInt32(1.0/targetAspectRatioWH*bitmap.Width), bitmap.Height));
            
            Point p = new Point((bitmap.Width - s.Width)/2, (bitmap.Height - s.Height)/2);
            Size Scaled = new Size((int) (s.Width*(scale/100)), (int) (s.Height*(scale/100)));

            return new Bitmap(bitmap.Clone(new Rectangle(p,s), bitmap.PixelFormat), Scaled);          
        }
    }

}
