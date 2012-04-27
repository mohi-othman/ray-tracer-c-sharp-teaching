//Class to represent a scene to be rendered.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public class Scene
    {
        //The camera used to render the scene
        public ICamera SceneCamera { get; set; }

        //List of all 3D primitives in the scene
        public List<IPrimitive> ScenePrimitives { get; set; }

        //Background color of the secne
        public Color BackgroundColor { get; set; }

        //List of all light sources in the scene
        public List<ILight> SceneLights { get; set; }

        //Shader to be used in the scene
        public IShader SceneShader { get; set; }

        //Constructor
        public Scene(ICamera camera, List<IPrimitive> primitives, Color backgroundColor, List<ILight> sceneLights, IShader sceneShader)
        {
            SceneCamera = camera;
            ScenePrimitives = primitives;
            BackgroundColor = backgroundColor;
            SceneLights = sceneLights;
            SceneShader = sceneShader;
        }
    }
}
