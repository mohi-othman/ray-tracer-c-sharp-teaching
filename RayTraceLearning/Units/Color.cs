//Struct to represent a color using RGB values
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public struct Color
    {
        //Value of red
        public double R { get; set; }

        //Value of green
        public double G { get; set; }

        //Value of blue
        public double B { get; set; }

        //Constructors       
        public Color(double r, double g, double b) : this()
        {
            R = CorrectValue(r);
            G = CorrectValue(g);
            B = CorrectValue(b);
        }

        //Convert to 32-bit RGB color
        public System.Drawing.Color ConvertTo32Bit()
        {
            var r = (int)Math.Ceiling(R * 255);
            var g = (int)Math.Ceiling(G * 255);
            var b = (int)Math.Ceiling(B * 255);

            return System.Drawing.Color.FromArgb(r, g, b);
        }

        //Function to make sure the values are always between 0 and 1 inclusive.
        private double CorrectValue(double a)
        {
            if (a < 0)
            {
                return 0;
            }
            else if (a > 1)
            {
                return 1;
            }
            else
            {
                return a;
            }
        }

        //Color arithmatic
        public static Color operator +(Color a, Color b)
        {
            return new Color(a.R + b.R, a.G + b.G, a.B + b.B);
        }
        public static Color operator -(Color a, Color b)
        {
            return new Color(a.R - b.R, a.G - b.G, a.B - b.B);
        }
        public static Color operator *(Color a, Color b)
        {
            return new Color(a.R * b.R, a.G * b.G, a.B * b.B);
        }
        public static Color operator *(double x, Color C)
        {
            return new Color(C.R * x, C.G * x, C.B * x);
        }
        public static Color operator *(Color C, double x)
        {
            return x * C;
        }
    }
}
