//Class to represent the view plane
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public class ViewPlane
    {
        //Width of the view in pixels
        public int Width { get; set; }

        //Height of the view in pixels
        public int Height { get; set; }
                
        //Two dimensional array of all pixels in the view
        public Pixel[,] PixelArray {get; set;}

        //Constructor
        public ViewPlane(int width, int height)
        {
            Width = width;
            Height = height;

            PixelArray = new Pixel[width,height];
        }

        //Export to a GDI bitmap
        public System.Drawing.Bitmap ExportImage()
        {
            var picture = new System.Drawing.Bitmap(this.Width, this.Height);

            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    picture.SetPixel(x, y, PixelArray[x, y].PixelColor.ConvertTo32Bit());
                }
            }

            return picture;
        }
    }
}
