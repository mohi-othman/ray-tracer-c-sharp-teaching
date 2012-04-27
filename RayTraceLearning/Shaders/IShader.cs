//Interface to be used by all shaders and illumination processors
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public interface IShader
    {
        //Gets the color we see, according the 3D object's properties, the light source properties, direction of the viewing ray, 
        //the direction from the intersection point to the light source, and the normal vector on the surface of the 3D object at
        //the intersection point.
        Color GetColor(IPrimitive HitObject, ILight Light, Vector3D ViewDirection, Vector3D LightDirection, Vector3D Normal);
    }
}
