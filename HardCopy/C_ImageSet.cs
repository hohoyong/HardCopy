
namespace HardCopy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;
    class C_ImageSet
    {
        public static Bitmap ImageCreate(int width, int height, byte[] buff)
        {
            Bitmap bit = new Bitmap(width, height);


            for (int j = 0; j < height; j++)
            {
                int set_color = 0;
                if (buff.Length == 0)
                {
                    set_color = (int)(255.0 / height * j);
                }
                if (height * width == buff.Length)
                {
                    for (int i = 0; i < width; i++)
                    {
                        set_color = buff[j * width + i];
                        bit.SetPixel(i, j, Color.FromArgb(255, set_color, set_color, set_color));
                    }
                }

            }
            return bit;
        }
        public static Bitmap ImageCut(Bitmap bit, int width, int height)
        {


            for (int j = 0; j < bit.Height; j++)
            {
                for (int i = 0; i < bit.Width; i++)
                {
                    if (i <= width || j <= height)
                    {
                        bit.SetPixel(i, j, Color.FromArgb(255, 255, 255, 255));
                    }

                }
            }
            return bit;
        }

        public static Bitmap ImageCreate(int width, int height, int left, int top, int right, int bottom, byte[] buff)
        {
            Bitmap bit = new Bitmap(width, height);

            int set_color = 0;
            for (int j = 0; j < height; j++)
            {

                for (int i = 0; i < width; i++)
                {
                    set_color = buff[j * width + i];
                    bit.SetPixel(i, j, Color.FromArgb(255, set_color, set_color, set_color));
                }
            }

            return C_ImageSet.cropImage(bit, new Rectangle(new Point(left, top), new Size(width - left - right, height - top - bottom))); ;
        }

        public static Bitmap cropImage(Bitmap img, Rectangle cropArea)
        {
            return img.Clone(cropArea, img.PixelFormat);
        }
    }
}
