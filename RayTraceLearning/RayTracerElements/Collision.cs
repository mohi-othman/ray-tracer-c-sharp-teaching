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

        //Constructor
        public Collision(bool isHit) //Default constructor used to resent collision
        {
            IsHit = isHit;
            HitObject = null;
            Distance = Globals.Infinity;
        }

        public Collision(bool isHit, IPrimitive hitObject, double distance)
        {
            IsHit = isHit;
            HitObject = hitObject;
            Distance = distance;
        }
    }
}
