//Class to represent a ray collision
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public class Collision
    {
        //Has the ray hit anything?
        public bool IsHit { get; set; }

        //What did the ray hit?
        public IPrimitive HitObject { get; set; }        

        //Distance from origin of the ray to the intersection point
        public double Distance { get; set; }

        //Coordinates of the intersection point
        public Vector3D HitPoint { get; set; }

        //Normal of the hit object at the intersection point
        public Vector3D Normal { get; set; }

        //Is the ray originating from inside the 3D primitive?
        public bool IsInside { get; set; }

        //Constructor
        public Collision(bool isHit) //Default constructor used to resent collision
        {
            IsHit = isHit;
            HitObject = null;
            Distance = Globals.Infinity;            
        }

        public Collision(bool isHit, IPrimitive hitObject, double distance, Vector3D normal, Vector3D hitPoint, bool isInside)
        {
            IsHit = isHit;
            HitObject = hitObject;
            Distance = distance;
            Normal = normal;
            HitPoint = hitPoint;
            IsInside = isInside;
        }
    }
}
