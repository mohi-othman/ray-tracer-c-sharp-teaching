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

                    //Ray trace and get result color
                    targetPixel.PixelColor = RayTrace(cameraRay, 1);
                }
            }
            return resultView;
        }

        //Main ray tracing function. Takes a ray and the target pixel 
        //Returns the color of the result of the ray tracing.
        private Color RayTrace(Ray ray, int Level)
        {
            //Exit function if we have exceeded maximum recursion depth
            if (Level > Globals.MaxRecursionDepth)
            {
                return new Color(); //Stop tracing
            }

            //Trace the ray
            var cameraCollision = Trace(ray);

            if (cameraCollision.IsHit)  //The ray has hit an object
            {
                var pixelColor = new Color();   //Reset light pixel's color to black

                //Iterate through the lights and calculate their contribution
                foreach (ILight light in SceneToRender.SceneLights)
                {
                    var lightDirection = light.GetLightDirection(cameraCollision.HitPoint);
                    var lightDistance = light.GetDistance(cameraCollision.HitPoint);

                    //Construct ray from a point just outside the intersection point (to avoid hitting the same body) to light source. Used to find if object is in the shadows.
                    var shadowRay = new Ray(cameraCollision.HitPoint + lightDirection * Globals.Epsilon, lightDirection);

                    var shadowCollision = Trace(shadowRay);

                    //When no collision occurs, it means there is nothing between the light source and the hit object. 
                    //When collision occurs, but the collision distance is larger than the distance to the light source, it means there is nothing between the light source and the hit object. 
                    //Calculate light contribution to color
                    if (!shadowCollision.IsHit || shadowCollision.Distance > lightDistance)
                    {
                        //Calculate the light's contribution to the color of the pixel using the scene's shader
                        pixelColor += SceneToRender.SceneShader.GetColor(cameraCollision.HitObject,
                                                                            light,
                                                                            ray.Direction,
                                                                            lightDirection,
                                                                            cameraCollision.Normal);
                    }
                }

                //If reflection coeffecient is larger than 0, trace for reflection
                if (cameraCollision.HitObject.PrimitiveMaterial.ReflectionCoeff > 0)
                {
                    //Add results of the reflection tracing
                    pixelColor += TraceReflection(ray, cameraCollision.Normal, cameraCollision.HitPoint, cameraCollision.HitObject, Level);
                }

                //If refractuib index is larger than 0, trace for refraction
                if (cameraCollision.HitObject.PrimitiveMaterial.RefractionIndex > 0)
                {
                    //Add results of the reflection tracing
                    pixelColor += TraceRefraction(ray, cameraCollision.Normal, cameraCollision.HitPoint, cameraCollision.HitObject, cameraCollision.IsInside, Level);
                }

                return pixelColor;
            }
            else //The ray did not hit anything
            {
                //return the background color
                return SceneToRender.BackgroundColor;
            }
        }

        //Reflection rendering function
        private Color TraceReflection(Ray ray, Vector3D normal, Vector3D hitPoint, IPrimitive hitObject,  int Level)
        {
            //Calculate reflection direction
            var reflectionDir = (ray.Direction - (2 * (ray.Direction * normal) * normal)).Normalize();

            //Create reflection ray from just outside the intersection point, and trace it
            var reflectionRay = new Ray(hitPoint + reflectionDir * Globals.Epsilon, reflectionDir);

            //Get the color from the reflection
            var reflectionColor = RayTrace(reflectionRay, Level + 1);

            //Calculate final color
            var resultColor = reflectionColor * hitObject.PrimitiveMaterial.ReflectionCoeff;

            return resultColor;
        }

        //Refraction rendering function
        private Color TraceRefraction(Ray ray, Vector3D normal, Vector3D hitPoint, IPrimitive hitObject, bool isInside, int Level)
        {
            double originalRefIndex, newRefIndex;
            Vector3D N;

            if (!isInside)
            {
                //Ray is coming from outside the object
                originalRefIndex = 1;
                newRefIndex = hitObject.PrimitiveMaterial.RefractionIndex;
                N = normal;
            }
            else
            {
                //Ray is coming from inside the object
                originalRefIndex = hitObject.PrimitiveMaterial.RefractionIndex;
                newRefIndex = 1;
                N = -normal; //flip normal
            }
            
            //Calculate refraction direction
            //http://www.cs.unc.edu/~rademach/xroads-RT/RTarticle.html
            var n = originalRefIndex / newRefIndex;
            var c1 = -(N * ray.Direction);
            var c2 = Math.Sqrt(1 - Math.Pow(n, 2) * (1 - Math.Pow(c1, 2)));

            var refractionDirection = ((n * ray.Direction) + (n * c1 - c2) * N).Normalize();

            //Create refraction ray from just outside the intersection point, and trace it
            var refractionRay = new Ray(hitPoint + refractionDirection * Globals.Epsilon, refractionDirection);

            //Get the color from the refraction
            var resultColor = RayTrace(refractionRay, Level + 1);

            return resultColor;
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
