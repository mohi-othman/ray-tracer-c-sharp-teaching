//Interface to be used by all 3D Primitives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public interface IPrimitive
    {
        //The material used for the 3D primitive
        Material PrimitiveMaterial { get; set; }

        //Takes a Ray object, and checks to see if it intersects with the 3D primitive
        Collision Intersect(Ray ray);
    }
}
