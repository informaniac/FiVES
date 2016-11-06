using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FIVES;
using System.Configuration;
using FFFMGBlockPlugin;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace FFFMGWorldGeneratorPlugin
{
    public class FFFMGWorldGeneratorPluginInitializer : IPluginInitializer
    {
        #region IPluginInitializer implementation
        public string Name
        {
            get
            {
                return "FivesFourFunMiningGameWorldGenerator";
            }
        }

        public List<string> ComponentDependencies
        {
            get
            {
                return new List<string>();
            }
        }

        public List<string> PluginDependencies
        {
            get
            {
                return new List<string>() { "FivesFourFunMiningGameBlock" };
            }
        }

        public void Initialize()
        {
            readConfiguration();
            if (isPersistent)
                loadWorld();

            Console.WriteLine("[FFFMGWorldGenerator] initializing. Persistence: {0}, World creation: {1} (will not generate world if persistence is true and a world exists). Size (WxH): {2}x{3}",
                isPersistent, generateWorld && gameWorld == null, widthInBlocks, heightInBlocks);

            if (generateWorld && gameWorld == null)
                generateRandomWorld();

            createEntitiesFromWorld();
        }

        public void Shutdown()
        {
            if (isPersistent)
            {
                Console.WriteLine("[FFFMGWorldGenerator] saving world");
                saveWorld();
            }
        }
        #endregion

        private void readConfiguration()
        {
            string configurationPath = this.GetType().Assembly.Location;
            Configuration config = ConfigurationManager.OpenExeConfiguration(configurationPath);

            string persistenceSetting = config.AppSettings.Settings["isPersistent"].Value;
            bool.TryParse(persistenceSetting, out isPersistent);
            string generationSetting = config.AppSettings.Settings["generateWorld"].Value;
            bool.TryParse(generationSetting, out generateWorld);
            string widthSetting = config.AppSettings.Settings["widthInBlocks"].Value;
            int.TryParse(widthSetting, out widthInBlocks);
            string heightSetting = config.AppSettings.Settings["heightInBlocks"].Value;
            int.TryParse(heightSetting, out heightInBlocks);
        }

        private void loadWorld()
        {
            if (File.Exists("FFFMGWorld.dat"))
            {
                gameWorld = new Material[widthInBlocks, heightInBlocks];

                FileStream fs = new FileStream("FFFMGWorld.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    // Deserialize the hashtable from the file and 
                    // assign the reference to the local variable.
                    gameWorld = (Material[,])formatter.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("[FFFMGWorldGenerator] Failed to deserialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        private void saveWorld()
        {
            FileStream fs = new FileStream("FFFMGWorld.dat", FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, gameWorld);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("[FFFMGWorldGenerator] Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        // TODO: create a good randomize world algorithm
        private void generateRandomWorld()
        {
            gameWorld = new Material[heightInBlocks, widthInBlocks];
            for(int i = 0; i < heightInBlocks; i++)
            {
                for(int j = 0; j < widthInBlocks; j++)
                {
                    gameWorld[i, j] = (Material)randomizer.Next((int)Material.DIRT, (int)Material.DIAMOND + 1);
                }
            }
        }

        private void createEntitiesFromWorld()
        {
            Material material;
            Entity entity;
            // vector position ==> pixels?
            Vector position;
            for (int i = 0; i < heightInBlocks; i++)
            {
                for (int j = 0; j < widthInBlocks; j++)
                {
                    material = gameWorld[i, j];
                    entity = new Entity();
                }
            }
        }

        private Random randomizer = new Random();
        private bool isPersistent = false;
        private bool generateWorld = true;
        private int widthInBlocks = 0;
        private int heightInBlocks = 0;
        private Material[,] gameWorld = null;
    }
}
