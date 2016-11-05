using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIVES;
using System.IO;
using ClientManagerPlugin;

namespace FFFMGBlockPlugin
{
    public class FFFMGBlockPluginInitializer : IPluginInitializer
    {
        #region Interface definitions
        public string Name
        {
            get
            {
                return "FivesFourFunMiningGameBlock";
            }
        }

        public List<string> ComponentDependencies
        {
            get
            {
                return new List<string>() { "Location" };
            }
        }

        public List<string> PluginDependencies
        {
            get
            {
                return new List<string>();
            }
        }

        public void Initialize()
        {
            Console.WriteLine("[FFFMGBlock] initializing");
            defineComponents();
            PluginManager.Instance.AddPluginLoadedHandler("ClientManager", registerClientServices);
        }

        public void Shutdown()
        {
            Console.WriteLine("[FFFMGBlock] shutting down");
        }
        #endregion

        private void defineComponents()
        {
            ComponentDefinition block = new ComponentDefinition("block");
            // can be added if enum is propagatable
            // block.AddAttribute<Material>("material");
            // workaround:
            block.AddAttribute<int>("material");
            block.AddAttribute<int>("durability");
            ComponentRegistry.Instance.Register(block);
        }

        private void registerClientServices()
        {
            string blockIdl = File.ReadAllText("block.kiara");
            SINFONIPlugin.SINFONIServerManager.Instance.SinfoniServer.AmendIDL(blockIdl);
            ClientManager.Instance.RegisterClientService("block", true, new Dictionary<string, Delegate> {
                {"updateDurability", (Action<string, int, int>) updateDurability},
            });
        }

        private void updateDurability(string guid, int updatedDurability, int timestamp)
        {
            // timestamp is not used yet but could be useful...
            Entity entity = World.Instance.FindEntity(guid);
            if (entity != null)
                entity["block"]["durability"].Suggest(updatedDurability);
        }

        // Example for Material
        enum Material
        {
            DIRT,
            STONE,
            COPPER_ORE,
            IRON_ORE,
            SILVER_ORE,
            GOLD_ORE,
            TOPAZ,
            RUBY,
            SAPPHIRE,
            EMERALD,
            DIAMOND
        }
    }
}
