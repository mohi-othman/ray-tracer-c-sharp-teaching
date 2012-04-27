﻿//Console application to test the ray tracing engine

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemDown.RayTracer;

namespace RayTracerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Timer to measure execution time
            var time = DateTime.Now;

            //Instantiate camera
            var camera = new VerySimpleCamera();
            
            //Instantiate shader
            var shader = new DiffuseShader();
                        
            //Create red sphere
            var redSphere = new Sphere(new Vector3D(0, 0, 2), 2);
            redSphere.PrimitiveMaterial = new Material(new Color(.8, .2, .2), 1.5);

            //Create blue sphere behind it
            var blueSphere = new Sphere(new Vector3D(3, 1.5, 5), 2);
            blueSphere.PrimitiveMaterial = new Material(new Color(.1, .1, .7), .7);

            //Create dark green plane under the spheres
            var greenPlane = new Plane(new Vector3D(0, 1, 0), 7);
            greenPlane.PrimitiveMaterial = new Material(new Color(0, .5, 0));

            //Add 3D objects to a list
            var objects = new List<IPrimitive>();
            objects.Add(redSphere);
            objects.Add(blueSphere);
            objects.Add(greenPlane);

            //Create directional light
            var dirLight = new DirectionalLight(new Vector3D(1, -1, 1), new Color(.7, .7, .7));

            //Add lights to a list
            var lights = new List<ILight>();
            lights.Add(dirLight);

            //Instantiate scene, using a very dark gray as the background color
            var scene = new Scene(camera, objects, new Color(.1, .1, .1), lights, shader);

            //Instantiate ray tracing engine to produce a 400 x 400 pixel image. Pixel size of 0.01 means the image will 10.0 x 10.0 in real world units.
            var engine = new RayTracer(scene, 400, 400, 0.025);

            //Render the scene
            var view = engine.RenderScene();

            //Create a GDI bitmap
            var bmp = view.ExportImage();

            //Save the image
            bmp.Save("output.bmp");

            Console.WriteLine("Ray tracing done. Execution time: {0:d} ms", (DateTime.Now - time).Milliseconds);
            Console.ReadKey();
        }
    }
}
