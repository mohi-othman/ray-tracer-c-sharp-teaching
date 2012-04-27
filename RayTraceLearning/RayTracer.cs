//This class contains the core engine implementation of the ray tracer

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public class RayTracer
    {
        //The scene object to be rendered
        private Scene SceneToRender { get; set; }

        //Width of the result view plane in pixels
        public int Width { get; set; }

        //Height of the result view plane in pixels
        public int Height { get; set; }

        //Size of the pixel in real world units
        public double PixelSize { get; set; }

        //Constructor
        public RayTracer(Scene sceneToRender, int width, int height, double pixelSize)
        {
            SceneToRender = sceneToRender;
            Width = width;
            Height = height;
            PixelSize = pixelSize;
        }

        //The main rendering function. Takes a scene object, and the width, height and pixel size of result view plane
        public ViewPlane RenderScene()
        {
            //Create the blank view plane as seen by the camera
            var resultView = SceneToRender.SceneCamera.CreateViewPlane(Width, Height, PixelSize);

            //Iterate through all the pixels and ray trace trough them
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var targetPixel = resultView.PixelArray[x, y];
                    
                    //Create ray from camera that passes through the pixel
                    var cameraRay = SceneToRender.SceneCamera.CreateRay(targetPixel);

                    //Ray trace
                    RayTrace(cameraRay, targetPixel);
                }
            }
            return resultView;
        }

        //Main ray tracing function. Takes a ray and the target pixel 
        //Changes the contents of pixel according to the result of the ray tracing.
        private void RayTrace(Ray ray, Pixel TargetPixel)
        {
            //Trace the ray
            var cameraCollision = Trace(ray);

            if (cameraCollision.IsHit)  //The ray has hit an object
            {
                var pixelColor = new Color();   //Reset pixel's color to black

                //Iterate through the lights and calculate their contribution
                foreach (ILight light in SceneToRender.SceneLights)
                {
                    var lightDirection = light.GetLightDirection(cameraCollision.HitPoint);

                    //Calculate the light's contribution to the color of the pixel using the scene's shader
                    pixelColor += SceneToRender.SceneShader.GetColor(cameraCollision.HitObject,
                                                                        light,
                                                                        ray.Direction,
                                                                        lightDirection,
                                                                        cameraCollision.Normal);
                }

                //Set the pixel's color to be the calculated color
                TargetPixel.PixelColor = pixelColor;
            }
            else //The ray did not hit anything
            {
                //Set the pixel's color to be the background color
                TargetPixel.PixelColor = SceneToRender.BackgroundColor;
            }
        }

        //Tracing function. Calls the Intersect function on all primitives in the scene. Finds out if there is an intersection, and what object we hit
        private Collision Trace(Ray ray)
        {
            //Set the minimum distance to infinity
            var minDistance = Globals.Infinity;

            //Reset the collision
            var rayCollision = new Collision(false);

            //Iterate through all the primitives in the scene
            foreach (IPrimitive obj in SceneToRender.ScenePrimitives)
            {
                //Check if the ray intersects with the 3D primitive
                var collision = obj.Intersect(ray);

                if (collision.IsHit && collision.Distance < minDistance)    //intesection has occured, and the distance is less than the latest minimum distance
                {
                    //Set the new values
                    minDistance = collision.Distance;
                    rayCollision = collision;
                }
            }

            //Return the result
            return rayCollision;
        }
    }
}
