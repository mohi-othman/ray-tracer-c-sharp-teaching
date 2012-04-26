//Struct to represent a three dimensional vector value

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public struct Vector3D
    {
        //Properties
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        //Constructor       
        public Vector3D(double X, double Y, double Z):this()
        {
            x = X;
            y = Y;
            z = Z;
        }

        //Returns a copy of the vector
        public Vector3D Copy()
        {
            return new Vector3D(x, y, z);
        }

        //Dot product of two vectors
        public static double operator *(Vector3D a, Vector3D b)
        {
            return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
        }

        //Cross product of two vectors
        public static Vector3D Cross(Vector3D a, Vector3D b)
        {
            var x = a.y * b.z - b.y * a.z;
            var y = a.z * b.x - b.z * a.x;
            var z = a.x * b.y - b.x * a.y;
            return new Vector3D(x, y, z);
        }

        //Vector arithmatic
        public static Vector3D operator *(double scalar, Vector3D vector)
        {
            var xd = scalar * vector.x;
            var yd = scalar * vector.y;
            var zd = scalar * vector.z;

            return new Vector3D(xd, yd, zd);
        }
        public static Vector3D operator *(Vector3D vector, double scalar)
        {
            return scalar * vector;
        }                
        public static Vector3D operator -(Vector3D start, Vector3D finish)
        {
            var xd = start.x - finish.x;
            var yd = start.y - finish.y;
            var zd = start.z - finish.z;

            return new Vector3D(xd, yd, zd);
        }
        public static Vector3D operator +(Vector3D start, Vector3D finish)
        {
            var xd = start.x + finish.x;
            var yd = start.y + finish.y;
            var zd = start.z + finish.z;

            return new Vector3D(xd, yd, zd);
        }

        //Distance between two coordinates
        public static double Distance(Vector3D start, Vector3D finish)
        {
            var xd = start.x - finish.x;
            var yd = start.y - finish.y;
            var zd = start.z - finish.z;

            return Math.Sqrt(xd * xd + yd * yd + zd * zd);
        }
                        
        //Returns magnitude of a vector
        public double Magnitude()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        //Returns a normalized vector (i.e. make it's magnitude equal to 1)
        public Vector3D Normalize()
        {
            var a = this.Magnitude();
            if (a == 0)
                return this;
            else
                return new Vector3D(x / a, y / a, z / a);
        }

        //Returns length of a vector
        public double Length()
        {
            return Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
        }
    }
}
