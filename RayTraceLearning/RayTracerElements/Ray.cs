//Class to represent a ray
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public class Ray
    {
        private Vector3D _direction;

        //Coordinates of the origin point of the ray
        public Vector3D Origin { get; set; }

        //The direction vector of the ray
        public Vector3D Direction {
            get
            {
                return _direction;
            }
            set
            {
                //Always normalize the direction vector
                _direction = value.Normalize();
            }
        }

        //Constructor
        public Ray(Vector3D origin, Vector3D direction)
        {
            Origin = origin;
            Direction = direction;
        }
    }
}
