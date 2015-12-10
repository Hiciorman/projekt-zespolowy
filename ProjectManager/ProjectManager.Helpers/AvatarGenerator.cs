using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

namespace ProjectManager.Helpers
{
    public class AvatarGenerator
    {
        private readonly List<string> _backgroundColours;

        public AvatarGenerator()
        {
            _backgroundColours = new List<string> { "B26126", "FFF7F2", "FFE8D8", "74ADB2", "D8FCFF" };
        }

        public byte[] Generate(string username)
        {
            var avatarString = $"{username[0]}".ToUpper();

            var randomIndex = new Random().Next(0, _backgroundColours.Count - 1);
            var bgColour = _backgroundColours[randomIndex];

            var bmp = new Bitmap(32, 32);
            var sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            var font = new Font("Arial", 32, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(bmp);

            var convertFromString = new ColorConverter().ConvertFromString("#" + bgColour);
            if (convertFromString != null)
            {
                graphics.Clear((Color)convertFromString);
            }

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.DrawString(avatarString, font, new SolidBrush(Color.WhiteSmoke), new RectangleF(0, 0, 32, 32), sf);
            graphics.Flush();

            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);

            return ms.ToArray();
        }
    }
}