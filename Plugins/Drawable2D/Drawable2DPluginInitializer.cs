using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FIVES;

namespace Drawable2DPlugin
{
    public class Drawable2DPluginInitializer : IPluginInitializer
    {
        #region IPluginInitializer implementation
        public string Name
        {
            get
            {
                return "Drawable2D";
            }
        }

        public List<string> ComponentDependencies
        {
            get
            {
                return new List<String>();
            }
        }

        public List<string> PluginDependencies
        {
            get
            {
                return new List<String>();
            }
        }

        public void Initialize()
        {
            Console.WriteLine("[Drawable2D] initializing");
            DefineComponents();
        }

        public void Shutdown()
        {
            Console.WriteLine("[Drawable2D] shutting down");
        }
        #endregion

        void DefineComponents()
        {
            ComponentDefinition sprite = new ComponentDefinition("sprite");
            sprite.AddAttribute<String>("textureFile");
            sprite.AddAttribute<Point>("upperLeftPositionInTextureFile");
            sprite.AddAttribute<Rectangle>("size");
            ComponentRegistry.Instance.Register(sprite);
        }
    }
}
