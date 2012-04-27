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

        //The diffuse coefficient of the material
        public double  DiffuseCoeff { get; set; }

        //Constructors
        public Material(Color diffuseColor)
        {
            DiffuseColor = diffuseColor;
            DiffuseCoeff = 1;
        }

        public Material(Color diffuseColor, double diffuseCoeff)
        {
            DiffuseColor = diffuseColor;
            DiffuseCoeff = diffuseCoeff;
        }
    }
}
