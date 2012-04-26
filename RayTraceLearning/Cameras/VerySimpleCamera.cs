//A class to describe a very very simple camera. It has a fixed location at (0,0,-5) its view plane is always at (0,0,0)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public class VerySimpleCamera : ICamera
    {
        //Location of the camera is always fixed
        private readonly Vector3D CameraLocation = new Vector3D(0, 0, -5);

        //Calculate the view plane for our simple fixed camera
        public ViewPlane CreateViewPlane(int width, int height, double PixelSize)
        {
            //Calculate the width and height in real world units
            var realWidth = width * PixelSize;
            var realHeight = height * PixelSize;

            //The center of the view plane is always at (0,0,0) with the z-axis always fixed at 0
            //Calculate the coordinate of the top left corner of the view plane
            //Remeber that left is negative X, and up is positive Y
            var topLeft = new Vector3D(-realWidth / 2, realHeight / 2, 0);

            //Create the view plane, and assign real coordinates to all pixels
            var view = new ViewPlane(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //Calculate real world coordinate and assign it to the pixel with the color black
                    var realCoordinate = topLeft;
                    realCoordinate.x+= x * PixelSize;
                    realCoordinate.y-= y * PixelSize;

                    view.PixelArray[x, y] = new Pixel(new Color(), realCoordinate);
                }
            }

            return view;
        }

        public Ray CreateRay(Pixel targetPixel)
        {
            //Calculate direction from camera location to the target pixel
            var direction = targetPixel.RealCoordinates - CameraLocation;

            //Create ray object
            var cameraRay = new Ray(CameraLocation, direction);

            return cameraRay;
        }

    }
}
