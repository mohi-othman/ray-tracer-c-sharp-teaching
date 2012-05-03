//Shader to calculate specular component
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public class SpecularShader:IShader
    {     
        public  Color GetColor(IPrimitive HitObject, ILight Light, Vector3D ViewDirection, Vector3D LightDirection, Vector3D Normal)
        {
            //Caulcate reflection vector
            var reflectionDirection = (LightDirection - (2 * LightDirection * Normal) * Normal).Normalize();

            var dot = reflectionDirection * ViewDirection; //if the dot product is zero or less that means the angle between the two vectors is 90 or more and no highlighting occurs.

            if (dot > 0)
            {
                var specularPower = HitObject.PrimitiveMaterial.SpecularCoeff * Math.Pow(dot, HitObject.PrimitiveMaterial.SpecularExponent);
                var highlightColor = Light.LightColor * specularPower;
                
                return highlightColor;
            }

            return new Color();
        }
             
    }
}
