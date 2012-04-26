//Class to represent a pixel in the view plane

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public class Pixel
    {
        //Color of the pixel
        public Color PixelColor { get; set; }

        //Coordinates of the pixel in real world coordinates
        public Vector3D RealCoordinates { get; set; }

        //Constructor
        public Pixel(Color pixelColor, Vector3D coordinates)
        {
            PixelColor = pixelColor;
            RealCoordinates = coordinates;
        }
    }
}
