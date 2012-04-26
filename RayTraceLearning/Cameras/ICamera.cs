//Interface to be used by all camera objects
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public interface ICamera
    {
        //Creates a view plane as seen by the camera with the following pixel width and height and size of a pixel in real world units
        ViewPlane CreateViewPlane(int width, int height, double PixelSize);

        //Creates a ray that passes through the target pixel
        Ray CreateRay(Pixel targetPixel);
    }
}
