//Class to describe a directional light source
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public class DirectionalLight:ILight
    {        
        public Color LightColor{get;set;}

        //Vector of the direction the light shines
        public Vector3D Direction { get; set; }

        //Constructor
        public DirectionalLight(Vector3D direction, Color lightColor)
        {
            Direction = direction.Normalize();
            LightColor = lightColor;
        }
        
        public Vector3D GetLightDirection(Vector3D targetPoint)
        {
            return -Direction;
        }

    }
}
