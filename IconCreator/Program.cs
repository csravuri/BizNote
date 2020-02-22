using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IconCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            var logo = Image.FromFile(@"D:\WhoKnows\Courses\Applications\BusinessAnalyst\icons\icon_applogo_org.png");

            saveResizeImage(logo, new Size(72, 72), @"D:\WhoKnows\Courses\Applications\BusinessAnalyst\icons\resizeImages\mipmap-hdpi\icon.png");
            saveResizeImage(logo, new Size(48, 48), @"D:\WhoKnows\Courses\Applications\BusinessAnalyst\icons\resizeImages\mipmap-mdpi\icon.png");
            saveResizeImage(logo, new Size(96, 96), @"D:\WhoKnows\Courses\Applications\BusinessAnalyst\icons\resizeImages\mipmap-xhdpi\icon.png");
            saveResizeImage(logo, new Size(144, 144), @"D:\WhoKnows\Courses\Applications\BusinessAnalyst\icons\resizeImages\mipmap-xxhdpi\icon.png");
            saveResizeImage(logo, new Size(192, 192), @"D:\WhoKnows\Courses\Applications\BusinessAnalyst\icons\resizeImages\mipmap-xxxhdpi\icon.png");

            var foreground = Image.FromFile(@"D:\WhoKnows\Courses\Applications\BusinessAnalyst\icons\icon_foreground_org.png");

            saveResizeImage(foreground, new Size(162, 162), @"D:\WhoKnows\Courses\Applications\BusinessAnalyst\icons\resizeImages\mipmap-hdpi\launcher_foreground.png");
            saveResizeImage(foreground, new Size(108, 108), @"D:\WhoKnows\Courses\Applications\BusinessAnalyst\icons\resizeImages\mipmap-mdpi\launcher_foreground.png");
            saveResizeImage(foreground, new Size(216, 216), @"D:\WhoKnows\Courses\Applications\BusinessAnalyst\icons\resizeImages\mipmap-xhdpi\launcher_foreground.png");
            saveResizeImage(foreground, new Size(324, 324), @"D:\WhoKnows\Courses\Applications\BusinessAnalyst\icons\resizeImages\mipmap-xxhdpi\launcher_foreground.png");
            saveResizeImage(foreground, new Size(432, 432), @"D:\WhoKnows\Courses\Applications\BusinessAnalyst\icons\resizeImages\mipmap-xxxhdpi\launcher_foreground.png");


        }

        public static void saveResizeImage(Image imgToResize, Size size, string imagePath)
        {
            Image newImage = (Image)(new Bitmap(imgToResize, size));
            newImage.Save(imagePath);
        }
    }
}
