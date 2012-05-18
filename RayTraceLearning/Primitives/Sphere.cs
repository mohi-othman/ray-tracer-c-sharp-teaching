//Class used to represent a sphere primitive
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public class Sphere : IPrimitive
    {
        //Coordinates of the center of the sphere
        public Vector3D Center { get; set; }

        //The radius of the sphere
        public double Radius { get; set; }

        //Constructor
        public Sphere(Vector3D center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public Material PrimitiveMaterial { get; set; }

        //Intersection formula was taken from http://wiki.cgsociety.org/index.php/Ray_Sphere_Intersection
        public Collision Intersect(Ray ray)
        {
            var A = ray.Direction * ray.Direction;
            var B = 2 * (ray.Origin - Center) * ray.Direction;
            var C = (ray.Origin - Center) * (ray.Origin - Center) - Radius * Radius;

            var d = B * B - 4 * A * C;
            if (d < 0)
                return new Collision(false);    //No collision. Ray does not hit sphere.

            var sqrtD = Math.Sqrt(d);

            double q;
            if (B < 0)
                q = (-B - sqrtD) / 2;
            else
                q = (-B + sqrtD) / 2;

            var t0 = q / A;
            var t1 = C / q;

            if (t0 > t1)
            {
                var temp = t0;
                t1 = t0;
                t0 = temp;
            }

            if (t1 < 0)
                return new Collision(false);    //No collision. Sphere is behind origin point.

            if (t0 < 0)
            {
                //Origin inside sphere                
                var hitPoint1 = ray.Origin + ray.Direction * t1;
                return new Collision(true, this, t1, GetNormal(hitPoint1), hitPoint1, true);   //Collision. Origin point is inside sphere.
            }

            var hitPoint0 = ray.Origin + ray.Direction * t0;
            return new Collision(true, this, t0, GetNormal(hitPoint0), hitPoint0, false);       //Collision. Origin point is outside sphere.
        }

        //Get normal vector of the sphere's surface at hit point location
        private Vector3D GetNormal(Vector3D hitPoint)
        {
            var n = hitPoint - Center;
            
            var temp = 1 / Math.Sqrt(n * n);

            n = temp * n;

            return n;
        }
    }
}
