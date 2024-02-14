using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FoilInspection
{

    public static class ButtonExtension
    {
        public static void FitImage(this Button button, string imagePath, Size size)
        {
            Bitmap bitmap;

            bitmap = new Bitmap(imagePath);

            var cam_ico_new = new Bitmap(bitmap, size);
            button.Image = cam_ico_new;
        }
    }

    public static class ConsoleExtension
    {
        public static void WriteWithColor(dynamic dynamic, ConsoleColor consoleColor = ConsoleColor.Blue) 
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(dynamic);
            Console.ResetColor();
        }
    }

    public static class BitmapExtension
    {
        public static Bitmap DeepClone(this Bitmap bitmap, PixelFormat pixelFormat = PixelFormat.Format24bppRgb)
        {
            return bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), pixelFormat);
        }

        //public static Bitmap TransformAndCrop(this Bitmap inputBitmap)
        //{
        //    // Define the source and destination points for perspective transformation
        //    PointF[] sourcePoints = new PointF[]
        //    {
        //    new PointF(0, 420),
        //    new PointF(1200, 0),
        //    new PointF(0, 1800),
        //    new PointF(1200, 1450)
        //    };

        //    PointF[] destPoints = new PointF[]
        //    {
        //    new PointF(0, 0),
        //    new PointF(950, 0),
        //    new PointF(0, 610),
        //    new PointF(950, 610)
        //    };

        //    // Create a new Bitmap for the transformed image
        //    Bitmap transformedBitmap = new Bitmap(950, 610);

        //    using (Graphics g = Graphics.FromImage(transformedBitmap))
        //    {
        //        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        g.SmoothingMode = SmoothingMode.HighQuality;

        //        // Create the transformation matrix
        //        Matrix perspectiveMatrix = new Matrix(
        //            sourcePoints[0].X, sourcePoints[1].X, sourcePoints[0].Y,
        //            sourcePoints[2].Y, destPoints[0].X, destPoints[0].Y);

        //        // Perform the perspective transformation
        //        g.Transform = perspectiveMatrix;
        //        g.DrawImage(inputBitmap, new PointF[3] { PointF.Empty, new PointF(950, 0), new PointF(0, 610) });
        //    }

        //    // Crop the transformed image
        //    Rectangle cropRect = new Rectangle(100, 0, 750, 610);
        //    Bitmap croppedBitmap = new Bitmap(40, 40);

        //    using (Graphics g = Graphics.FromImage(croppedBitmap))
        //    {
        //        g.DrawImage(transformedBitmap, new Rectangle(Point.Empty, croppedBitmap.Size), cropRect, GraphicsUnit.Pixel);
        //    }

        //    return croppedBitmap;
        //}
    }

    public static class StringExtension 
    {
        public static string AddSpacesBeforeUppercase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            StringBuilder result = new StringBuilder();
            result.Append(input[0]); // Add the first character as is

            for (int i = 1; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]))
                {
                    result.Append(' '); // Add a space before uppercase letters
                }
                result.Append(input[i]); // Add the current character
            }
            return result.ToString();
        }
    }
   

    public static class ComboBoxExtension 
    {
        public static void LoadDirectoryNames(this ComboBox comboBox, string dirPath) 
        {
            try
            {
                foreach (string dirName in Directory.GetDirectories(dirPath))
                {
                    comboBox.Items.Clear();
                    comboBox.Items.Add(Path.GetFileName(dirName));
                }
                if (comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
