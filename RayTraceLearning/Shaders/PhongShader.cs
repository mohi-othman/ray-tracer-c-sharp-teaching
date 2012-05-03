//Calulcates phong shading, which is the combination of diffuse shading and specular highlighting
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public class PhongShader : IShader
    {
        private DiffuseShader diffuse;
        private SpecularShader specular;

        public PhongShader()
        {
            diffuse = new DiffuseShader();
            specular = new SpecularShader();
        }

        public Color GetColor(IPrimitive HitObject, ILight Light, Vector3D ViewDirection, Vector3D LightDirection, Vector3D Normal)
        {
            var result = diffuse.GetColor(HitObject, Light, ViewDirection, LightDirection, Normal)
                        + specular.GetColor(HitObject, Light, ViewDirection, LightDirection, Normal);

            return result;
        }

    }
}
