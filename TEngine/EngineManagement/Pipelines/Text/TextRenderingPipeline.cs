using System;
using TEngine.Components.Transforms.Text;
using TEngine.Components.Animations.Text;
using TEngine.Components.Renderers.Text;
using TEngine.EngineManagement.RenderingEngines.Text;

using TEngine.GameObjects;
using TEngine.EngineManagement.Drivers.Text;
using TEngine.EngineManagement.Pipelines;
using TEngine.EngineManagement.Scenes;
using TEngine.GameObjects.Cameras.Text;
using System.Drawing;
using TEngine.GameObjects.Cameras;
using TEngine.Components.Transforms;
using TEngine.EngineManagement.Drivers;

namespace TEngine.EngineManagement.Pipelines.Text
{
    public class TextRenderingPipeline : RenderingPipeline
    {
        private readonly IDriver<string> _driver;
        public TextRenderingPipeline(IDriverFactory factory)
        : base(factory)
        {
            _driver = DriverFactory.Create<string>();
        }

        public override void Render(Scene scene)
        {
            if (scene.MainCamera is not CameraText camera)
                throw new InvalidOperationException("MainCamera is not CameraText");

            var gameObjects = CullAndSort(scene, camera);
            Preprocess(gameObjects, camera);
            Draw(gameObjects, camera);
        }

        public override List<GameObject> CullAndSort(Scene scene, Camera camera)
        {
            var list = new List<GameObject>();
            var viewBounds = new RectangleF(camera.Position.X, camera.Position.Y, camera.ViewSize.X, camera.ViewSize.Y);

            foreach (var go in scene.GetRenderableGameObjects())
            {
                if (!go.HasComponent<TextRenderer>() || !go.HasComponent<Transform>())
                    continue;

                var transform = go.GetComponent<Transform>();
                if (viewBounds.Contains(transform.GlobalPosition.X, transform.GlobalPosition.Y))
                    list.Add(go);

            }

            //Gameobjects are already sorted by Z index, so no need to sort again.
            return list;
        }

        public override void Preprocess(List<GameObject> gameObjects, Camera camera)
        {
           //Preprocessing is handled mostly by the animation class already.
           //For now, nothing to be done here.
        }

        public override void Draw(List<GameObject> gameObjects, Camera camera)
        {
            //Render each gameobject
            foreach (var go in gameObjects)
            {
                var renderer = go.GetComponent<TextRenderer>();
                var transform = go.GetComponent<Transform>();

                if (renderer == null || transform == null)
                    continue;

                _driver.Draw(renderer.GetRenderedData(), transform.GlobalPosition);
            }
        }
    }

}
