using System;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;
using System.Drawing;

namespace BitbucketBrowser.UI
{
    public class CellBackground : UIView
    {

        static CGGradient bottomGradient, topGradient;

        static CellBackground ()
        {
            using (var rgb = CGColorSpace.CreateDeviceRGB()){
                float [] colorsBottom = {
                    1, 1, 1, .5f,
                    0.91f, 0.91f, 0.91f, .5f
                };
                bottomGradient = new CGGradient (rgb, colorsBottom, null);
                float [] colorsTop = {
                    0.94f, 0.94f, 0.94f, .5f,
                    1, 1, 1, 0.5f
                };
                topGradient = new CGGradient (rgb, colorsTop, null);
            }
        }


        public CellBackground() : base() { }

        public override void Draw(RectangleF rect)
        {
            bool highlighted = (this.Superview as UITableViewCell).Highlighted;

            var context = UIGraphics.GetCurrentContext();
            var bounds = Bounds;
            var midx = bounds.Width/2;
            if (!highlighted){
                UIColor.White.SetColor ();
                context.FillRect (bounds);
                context.DrawLinearGradient (bottomGradient, new PointF (midx, bounds.Height-17), new PointF (midx, bounds.Height), 0);
                context.DrawLinearGradient (topGradient, new PointF (midx, 1), new PointF (midx, 3), 0);
            }
        }
          
    }
}

