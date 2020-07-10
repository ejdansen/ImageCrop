using System.Drawing;
using System.Collections.Generic;

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
            TargetAspectWHDict.Add("wide", 3.0/2.0);
            TargetAspectWHDict.Add("tall", 2.0/3.0);
            TargetAspectWHDict.Add("square", 1.0);         
        }

        //scale 1-100, shape wide,tall,square
        public Bitmap ConvertImage(string shape, double scale)
        {

            double targetAspectRatioWH;
            TargetAspectWHDict.TryGetValue(shape, out targetAspectRatioWH);
            float originAspectRatioWH = (float) bitmap.Width/bitmap.Height;         
            Size s = new Size(bitmap.Width, bitmap.Height);
            //cut width if image is wider than target
            if (targetAspectRatioWH < (double) originAspectRatioWH)
            {
                double WnormOrig = (double) bitmap.Width/bitmap.Height;
                double WnormTarget = targetAspectRatioWH;
                double fractionToRemove = (WnormOrig - WnormTarget)/WnormOrig;
                int numPixToCrop = (int) (fractionToRemove*bitmap.Width);
                s.Width = bitmap.Width - numPixToCrop;
                s.Height = bitmap.Height;
            }

            //cut height if image is taller than target
            if (targetAspectRatioWH > originAspectRatioWH)
            {
                double HnormOrig = (double) bitmap.Height/bitmap.Width;
                double HnormTarget = 1/targetAspectRatioWH;
                double fractionToRemove = (HnormOrig - HnormTarget)/HnormOrig;
                int numPixToCrop = (int) (fractionToRemove*bitmap.Height);
                s.Height = bitmap.Height - numPixToCrop;
                s.Width = bitmap.Width;
            }

            //default keep origin proportions
            Point p = new Point((int) 0.5*(bitmap.Height - s.Height), (int) 0.5*(bitmap.Width - s.Width));
            s.Width = (int) (s.Width*(scale/100));
            // Discuss!
            if (s.Width == 0) {s.Width = 1;}
            s.Height = (int) (s.Height*(scale/100));
            if (s.Height == 0) {s.Width = 1;}
            return new Bitmap(bitmap.Clone(new Rectangle(p,s), bitmap.PixelFormat));          
        }
    }

}
