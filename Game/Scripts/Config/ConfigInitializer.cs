using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.Config
{
    public static class ConfigInitializer
    {
        public static Config Initialize(Dictionary<string, Dictionary<string, string>> configData)
        {
            var config = new Config();

            config.EnemyCount = GetValue(configData, "Gameplay", "EnemyCount", 50);
            config.FoodSpawnInterval = GetValue(configData, "Gameplay", "FoodSpawnInterval", 0.05f);
            config.PlayerStartSpeed = GetValue(configData, "Gameplay", "PlayerStartSpeed", 200f);
            config.PlayerStartPosition = new Vector2f(
                GetValue(configData, "Gameplay", "PlayerStartX", 300f),
                GetValue(configData, "Gameplay", "PlayerStartY", 300f)
            );
            config.PlayerStartSize = GetValue(configData, "Gameplay", "PlayerStartSize", 40f);
            config.MinPlayerRadius = GetValue(configData, "Gameplay", "MinPlayerRadius", 20f);
            config.SwapAbilityCooldown = GetValue(configData, "Gameplay", "SwapAbilityCooldown", 1f);

            config.EnemyMinSize = GetValue(configData, "Enemies", "MinSize", 20f);
            config.EnemyMaxSize = GetValue(configData, "Enemies", "MaxSize", 20f);
            config.EnemyBaseSpeed = GetValue(configData, "Enemies", "BaseSpeed", 1f);

            config.StartZoom = GetValue(configData, "Camera", "StartZoom", 800f);
            config.CameraMinZoom = GetValue(configData, "Camera", "MinZoom", 50f);
            config.CameraMaxZoom = GetValue(configData, "Camera", "MaxZoom", 2000f);
            config.CameraSmoothness = GetValue(configData, "Camera", "Smoothness", 1f);

            config.MapBounds = new FloatRect(
                GetValue(configData, "Map", "Left", 0f),
                GetValue(configData, "Map", "Top", 0f),
                GetValue(configData, "Map", "Width", 4000f),
                GetValue(configData, "Map", "Height", 4000f)
            );

            config.FoodConfigs = LoadFoodConfigs(configData);

            config.Audio = new Config.AudioConfig
            {
                BackgroundMusic = GetValue(configData, "Audio", "Music", "Resources/Audio/music.wav"),
                StreakSounds = new Dictionary<string, string>
                {
                    ["Kill"] = GetValue(configData, "Audio", "Kill", "Resources/Audio/StreakAlert/Kill.wav"),
                    ["FirstBlood"] = GetValue(configData, "Audio", "FirstBloodSound", "Resources/Audio/StreakAlert/FirstBlood.wav"),
                    ["DoubleKill"] = GetValue(configData, "Audio", "DoubleKillSound", "Resources/Audio/StreakAlert/DoubleKill.wav"),
                    ["TripleKill"] = GetValue(configData, "Audio", "TripleKillSound", "Resources/Audio/StreakAlert/TripleKill.wav"),
                    ["UltraKill"] = GetValue(configData, "Audio", "UltraKillSound", "Resources/Audio/StreakAlert/UltraKill.wav"),
                    ["Rampage"] = GetValue(configData, "Audio", "RampageSound", "Resources/Audio/StreakAlert/Rampage.wav"),
                    ["HolyShit"] = GetValue(configData, "Audio", "HolyShitSound", "Resources/Audio/StreakAlert/HolyShit.wav")
                }
            };

            config.SwapAbilityButtonPath = GetValue(configData, "UI", "SwapAbilityButtonPath", "Resources/Textures/AbilityButton/SwapButton.png");
            config.PauseButtonPath = GetValue(configData, "UI", "PauseButtonPath", "Resources/Textures/AbilityButton/PauseButton.png");
            config.FontPath = GetValue(configData, "UI", "FontPath", "Resources/Fonts/arial.ttf");

            return config;
        }

        private static T GetValue<T>(Dictionary<string, Dictionary<string, string>> configData, string section, string key, T defaultValue)
        {
            if (configData.TryGetValue(section, out var sectionData))
            {
                if (sectionData.TryGetValue(key, out var value))
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }
            return defaultValue;
        }

        private static List<Config.FoodConfig> LoadFoodConfigs(Dictionary<string, Dictionary<string, string>> configData)
        {
            var foodConfigs = new List<Config.FoodConfig>();

            int foodTypesCount = GetValue(configData, "Food", "FoodTypesCount", 4);
            for (int i = 1; i <= foodTypesCount; i++)
            {
                string section = $"FoodType{i}";
                foodConfigs.Add(new Config.FoodConfig
                {
                    Probability = GetValue(configData, section, "Probability", 0),
                    Size = GetValue(configData, section, "Size", 0f),
                    Color = ParseColor(GetValue(configData, section, "Color", "255,255,255")),
                    GrowthBonus = GetValue(configData, section, "GrowthBonus", 0)
                });
            }

            return foodConfigs;
        }

        private static Color ParseColor(string colorStr)
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