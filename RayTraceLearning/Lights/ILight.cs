//Interface to be used by all light sources
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public interface ILight
    {
        //Color of the light
        Color LightColor { get; set; }

        //Get direction vector from the target point to the light source 
        Vector3D GetLightDirection(Vector3D targetPoint);
    }
}
