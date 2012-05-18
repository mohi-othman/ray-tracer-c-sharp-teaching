//Class used to describe infinite 2D plane

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public class Plane: IPrimitive
    {        
        public Material PrimitiveMaterial{get;set;}

        //Normal vector of the plane
        public Vector3D Normal { get; set; }
        
        //Offset from origin point
        public double Offset { get; set; }

        public Plane(Vector3D normal, double offset)
        {
            Normal = normal.Normalize();
            Offset = offset;
        }

        public Collision Intersect(Ray ray)
        {
            var dot = Normal * ray.Direction;     //When the dot product is zero the ray is parallel to the plane, and we can't see the plane.
            if (dot != 0)
            {
                var distance = -((Normal * ray.Origin) + Offset) / dot;
                if (distance > 0)   //when the distance is positive it means the plane is in front of us
                {
                    var hitPoint = ray.Origin + (distance * ray.Direction);
                    return new Collision(true, this, distance, Normal, hitPoint, false);
                }
            }
            return new Collision(false);
        }
    }
}
