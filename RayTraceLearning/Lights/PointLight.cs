//Class to describe a point light
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public class PointLight : ILight
    {
        public Color LightColor { get; set; }

        //Location of the point light
        public Vector3D Location { get; set; }

        //Constructor
        public PointLight(Vector3D location, Color lightColor)
        {
            Location = location;
            LightColor = lightColor;
        }

        public Vector3D GetLightDirection(Vector3D targetPoint)
        {
            return (Location - targetPoint).Normalize();
        }

        public double GetDistance(Vector3D targetPoint)
        {
            return Vector3D.Distance(targetPoint, Location);
        }
    }
}
