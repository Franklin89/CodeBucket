using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Dialog;

namespace BitbucketBrowser.UI
{
    public class HeaderView : UIView
    {
        private static float XPad = 14f;
        private static float YPad = 10f;
        private static readonly UIFont TitleFont = UIFont.BoldSystemFontOfSize(18);
        private static readonly UIFont SubtitleFont = UIFont.SystemFontOfSize(13);

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public UIImage Image { get; set; }

        public bool ShadowImage { get; set; }
        
        public HeaderView(float width)
            : base(new RectangleF(0, 0, width, 60f))
        {
            ShadowImage = true;
            Layer.MasksToBounds = false;
            Layer.ShadowColor = UIColor.Gray.CGColor;
            Layer.ShadowOpacity = 1.0f;
            Layer.ShadowOffset = new SizeF(0, 2f);
            BackgroundColor = UIColor.FromRGB(0.98f, 0.98f, 0.98f);
        }

        public override void Draw(RectangleF rect)
        {
            base.Draw(rect);

            var context = UIGraphics.GetCurrentContext();
            float titleY = string.IsNullOrWhiteSpace(Subtitle) ? rect.Height / 2 - TitleFont.LineHeight / 2 : YPad;
            float contentWidth = rect.Width - XPad * 2;

            if (Image != null)
            {
                var height = Image.Size.Height > 36 ? 36 : Image.Size.Height;
                var width = Image.Size.Width > 36 ? 36 : Image.Size.Width;
                var top = rect.Height / 2 - height / 2;
                var left = rect.Width - XPad * 2 - width;

                if (ShadowImage)
                {
                    context.SaveState();
                    context.SetFillColor(UIColor.White.CGColor);
                    context.TranslateCTM(left, top);
                    context.SetLineWidth(1.0f);
                    context.SetShadowWithColor(new SizeF(0, 0), 5, UIColor.DarkGray.CGColor);
                    context.AddPath(GraphicsUtil.MakeRoundedPath(width, 4));
                    context.FillPath();
                    context.RestoreState();
                }


                Image.Draw(new RectangleF(left, top, width, height));
                contentWidth -= (width + 4f); 
            }


            if (!string.IsNullOrEmpty(Title))
            {
                DrawString(
                        Title,
                        new RectangleF(XPad, titleY, contentWidth, TitleFont.LineHeight),
                        TitleFont,
                        UILineBreakMode.TailTruncation
                );
            }

            if (!string.IsNullOrWhiteSpace(Subtitle))
            {
                UIColor.FromRGB(0.3f, 0.3f, 0.3f).SetColor();

                DrawString(
                    Subtitle,
                    new RectangleF(XPad, YPad + TitleFont.LineHeight + 2f, contentWidth, SubtitleFont.LineHeight),
                    SubtitleFont,
                    UILineBreakMode.TailTruncation
                );
            }


        }
    }
}
