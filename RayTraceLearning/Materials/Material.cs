//Class used to represent the material used by 3D primitives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public class Material
    {
        //The diffuse color of the material
        public Color DiffuseColor { get; set; }

        //Constructor
        public Material(Color diffuseColor)
        {
            DiffuseColor = diffuseColor;
        }
    }
}
