using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public class DiffuseShader : IShader
    {
        public Color GetColor(IPrimitive HitObject, ILight Light, Vector3D ViewDirection, Vector3D LightDirection, Vector3D Normal)
        {
            //If diffuse coeffecient is zero, then no shading occurs
            if (HitObject.PrimitiveMaterial.DiffuseCoeff > 0)
            {
                var dot = LightDirection * Normal;  //if the dot product is zero or less that means the angle between the two vectors is 90 or more and no diffuse occurs.
                if (dot > 0)
                {
                    var diff = dot * HitObject.PrimitiveMaterial.DiffuseCoeff;                  //Calculate strength of the diffuse
                    return diff * HitObject.PrimitiveMaterial.DiffuseColor * Light.LightColor;  //Calculate result color 
                }
            }

            return new Color();            
        }
    }
}
