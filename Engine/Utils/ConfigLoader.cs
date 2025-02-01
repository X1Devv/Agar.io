using System.Globalization;


namespace Agar.io_sfml.Engine.Utils
{
    public class ConfigLoader
    {
        public int EnemyCount { get; private set; }
        public float FoodSpawnInterval { get; private set; }
        public float CameraHeight { get; private set; }

        private string projectRoot;

        public ConfigLoader()
        {
            projectRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");
            string configPath = Path.Combine(projectRoot, "Game", "Scripts", "Config", "config.ini");

            LoadConfig(configPath);
        }

        private void LoadConfig(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var configValues = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                var parts = line.Split('=');
                if (parts.Length == 2)
                {
                    configValues[parts[0].Trim()] = parts[1].Trim();
                }
            }

            EnemyCount = int.Parse(configValues["EnemyCount"]);
            FoodSpawnInterval = float.Parse(configValues["FoodSpawnInterval"], CultureInfo.InvariantCulture);
            CameraHeight = float.Parse(configValues["CameraHeight"], CultureInfo.InvariantCulture);
        }
    }
}