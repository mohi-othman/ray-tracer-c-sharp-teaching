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
        public double DiffuseCoeff { get; set; }
                
        //The reflection coefficient of the material
        public double ReflectionCoeff { get; set; }

        //Specular coefficient. Determines how much specular highlight contributes the material color.
        public double SpecularCoeff { get; set; }

        //Specular exponent. Determines how shiny the specular highlight is.
        public double SpecularExponent { get; set; }

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
            ReflectionCoeff = 0;
        }

        public Material(Color diffuseColor, double diffuseCoeff, double reflectionCoeff)
        {
            DiffuseColor = diffuseColor;
            DiffuseCoeff = diffuseCoeff;            
            ReflectionCoeff = reflectionCoeff;
        }
    }
}
