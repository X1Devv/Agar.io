using IniParser;
using IniParser.Model;
using SFML.Graphics;
using Agar.io_sfml.Game.Scripts.Config;
using SFML.System;

namespace Agar.io_sfml.Engine.Utils
{
    public class ConfigLoader
    {
        public int EnemyCount { get; private set; }
        public float FoodSpawnInterval { get; private set; }
        public float MinPlayerRadius { get; private set; }
        public float SwapAbilityCooldown { get; private set; }
        public string SwapAbilityButtonPath { get; private set; }
        public float EnemyMinSize { get; private set; }
        public float EnemyMaxSize { get; private set; }
        public float EnemyBaseSpeed { get; private set; }
        public List<FoodConfig> FoodConfigs { get; private set; }
        public float StartZoom { get; private set; }
        public float CameraMinZoom { get; private set; }
        public float CameraMaxZoom { get; private set; }
        public float CameraSmoothness { get; private set; }
        public FloatRect MapBounds { get; private set; }
        public Vector2f PlayerStartPosition { get; private set; }
        public float PlayerStartSpeed { get; private set; }
        public float PlayerStartSize { get; private set; }

        public ConfigLoader()
        {
            string configPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "..", "..", "..", "Resources", "Config", "config.ini"
            );
            var parser = new FileIniDataParser();
            var iniData = parser.ReadFile(configPath);
            LoadFromIni(iniData);
        }

        private void LoadFromIni(IniData iniData)
        {
            EnemyCount = int.Parse(iniData["Gameplay"]["EnemyCount"]);
            FoodSpawnInterval = float.Parse(iniData["Gameplay"]["FoodSpawnInterval"]);
            MinPlayerRadius = float.Parse(iniData["Gameplay"]["MinPlayerRadius"]);
            SwapAbilityCooldown = float.Parse(iniData["Gameplay"]["SwapAbilityCooldown"]);
            PlayerStartSpeed = float.Parse(iniData["Gameplay"]["PlayerStartSpeed"]);
            PlayerStartPosition = new Vector2f(
                float.Parse(iniData["Gameplay"]["PlayerStartX"]),
                float.Parse(iniData["Gameplay"]["PlayerStartY"])
            );
            PlayerStartSize = float.Parse(iniData["Gameplay"]["PlayerStartSize"]);

            EnemyMinSize = float.Parse(iniData["Enemies"]["MinSize"]);
            EnemyMaxSize = float.Parse(iniData["Enemies"]["MaxSize"]);
            EnemyBaseSpeed = float.Parse(iniData["Enemies"]["BaseSpeed"]);

            StartZoom = float.Parse(iniData["Camera"]["StartZoom"]);
            CameraMinZoom = float.Parse(iniData["Camera"]["MinZoom"]);
            CameraMaxZoom = float.Parse(iniData["Camera"]["MaxZoom"]);
            CameraSmoothness = float.Parse(iniData["Camera"]["Smoothness"]);

            MapBounds = new FloatRect(
                float.Parse(iniData["Map"]["Left"]),
                float.Parse(iniData["Map"]["Top"]),
                float.Parse(iniData["Map"]["Width"]),
                float.Parse(iniData["Map"]["Height"])
            );

            int foodTypesCount = int.Parse(iniData["Food"]["FoodTypesCount"]);
            FoodConfigs = new List<FoodConfig>();
            for (int i = 0; i < foodTypesCount; i++)
            {
                string section = $"FoodType{i + 1}";
                FoodConfigs.Add(new FoodConfig(
                    int.Parse(iniData[section]["Probability"]),
                    float.Parse(iniData[section]["Size"]),
                    ParseColor(iniData[section]["Color"]),
                    int.Parse(iniData[section]["GrowthBonus"])
                ));
            }
            SwapAbilityButtonPath = iniData["UI"]["SwapAbilityButtonPath"];
        }

        private Color ParseColor(string colorStr)
        {
            string[] parts = colorStr.Split(',');
            return new Color(
                byte.Parse(parts[0]),
                byte.Parse(parts[1]),
                byte.Parse(parts[2])
            );
        }
    }
}