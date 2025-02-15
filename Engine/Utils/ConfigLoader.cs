using Agar.io_sfml.Game.Scripts.Config;
using IniParser;
using IniParser.Model;
using SFML.Graphics;
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
        public string PauseButtonPath { get; private set; }
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
        public string BackgroundMusicPath { get; private set; }
        public string KillSoundPath { get; private set; }
        public string FirstBloodSoundPath { get; private set; }
        public string DoubleKillSoundPath { get; private set; }
        public string TripleKillSoundPath { get; private set; }
        public string UltraKillSoundPath { get; private set; }
        public string RampageSoundPath { get; private set; }
        public string HolyShitSoundPath { get; private set; }
        public string FontPath { get; private set; }

        private string projectRoot;

        public ConfigLoader()
        {
            projectRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");
            string configPath = Path.Combine(projectRoot, "Resources", "Config", "config.ini");
            var parser = new FileIniDataParser();
            IniData iniData = parser.ReadFile(configPath);
            LoadFromIni(iniData);
        }

        private void LoadFromIni(IniData iniData)
        {
            EnemyCount = GetValue(iniData, "Gameplay", "EnemyCount", DefaultConfig.EnemyCount);
            FoodSpawnInterval = GetValue(iniData, "Gameplay", "FoodSpawnInterval", DefaultConfig.FoodSpawnInterval);
            MinPlayerRadius = GetValue(iniData, "Gameplay", "MinPlayerRadius", DefaultConfig.MinPlayerRadius);
            SwapAbilityCooldown = GetValue(iniData, "Gameplay", "SwapAbilityCooldown", DefaultConfig.SwapAbilityCooldown);
            PlayerStartSpeed = GetValue(iniData, "Gameplay", "PlayerStartSpeed", DefaultConfig.PlayerStartSpeed);
            PlayerStartPosition = new Vector2f(
                GetValue(iniData, "Gameplay", "PlayerStartX", DefaultConfig.PlayerStartX),
                GetValue(iniData, "Gameplay", "PlayerStartY", DefaultConfig.PlayerStartY)
            );
            PlayerStartSize = GetValue(iniData, "Gameplay", "PlayerStartSize", DefaultConfig.PlayerStartSize);

            EnemyMinSize = GetValue(iniData, "Enemies", "MinSize", DefaultConfig.EnemyMinSize);
            EnemyMaxSize = GetValue(iniData, "Enemies", "MaxSize", DefaultConfig.EnemyMaxSize);
            EnemyBaseSpeed = GetValue(iniData, "Enemies", "BaseSpeed", DefaultConfig.EnemyBaseSpeed);

            StartZoom = GetValue(iniData, "Camera", "StartZoom", DefaultConfig.StartZoom);
            CameraMinZoom = GetValue(iniData, "Camera", "MinZoom", DefaultConfig.CameraMinZoom);
            CameraMaxZoom = GetValue(iniData, "Camera", "MaxZoom", DefaultConfig.CameraMaxZoom);
            CameraSmoothness = GetValue(iniData, "Camera", "Smoothness", DefaultConfig.CameraSmoothness);
            BackgroundMusicPath = GetFullPath(GetValue(iniData, "Audio", "BackgroundMusic", DefaultConfig.BackgroundMusicPath));
            KillSoundPath = GetFullPath(GetValue(iniData, "Audio", "Kill", DefaultConfig.KillSoundPath));
            FirstBloodSoundPath = GetFullPath(GetValue(iniData, "Audio", "FirstBloodSound", DefaultConfig.FirstBloodSoundPath));
            DoubleKillSoundPath = GetFullPath(GetValue(iniData, "Audio", "DoubleKillSound", DefaultConfig.DoubleKillSoundPath));
            TripleKillSoundPath = GetFullPath(GetValue(iniData, "Audio", "TripleKillSound", DefaultConfig.TripleKillSoundPath));
            UltraKillSoundPath = GetFullPath(GetValue(iniData, "Audio", "UltraKillSound", DefaultConfig.UltraKillSoundPath));
            RampageSoundPath = GetFullPath(GetValue(iniData, "Audio", "RampageSound", DefaultConfig.RampageSoundPath));
            HolyShitSoundPath = GetFullPath(GetValue(iniData, "Audio", "HolyShitSound", DefaultConfig.HolyShitSoundPath));

            FontPath = GetFullPath(GetValue(iniData, "UI", "FontPath", DefaultConfig.FontPath));

            MapBounds = new FloatRect(
                GetValue(iniData, "Map", "Left", DefaultConfig.MapLeft),
                GetValue(iniData, "Map", "Top", DefaultConfig.MapTop),
                GetValue(iniData, "Map", "Width", DefaultConfig.MapWidth),
                GetValue(iniData, "Map", "Height", DefaultConfig.MapHeight)
            );

            int foodTypesCount = GetValue(iniData, "Food", "FoodTypesCount", 4);
            FoodConfigs = new List<FoodConfig>();
            for (int i = 0; i < foodTypesCount; i++)
            {
                string section = $"FoodType{i + 1}";
                FoodConfigs.Add(new FoodConfig(
                    GetValue(iniData, section, "Probability", 0),
                    GetValue(iniData, section, "Size", 0f),
                    ParseColor(GetValue(iniData, section, "Color", "255,255,255")),
                    GetValue(iniData, section, "GrowthBonus", 0)
                ));
            }

            SwapAbilityButtonPath = GetValue(iniData, "UI", "SwapAbilityButtonPath", DefaultConfig.SwapAbilityButtonPath);
            PauseButtonPath = GetValue(iniData, "UI", "PauseButtonPath", DefaultConfig.PauseButtonPath);
        }

        private string GetFullPath(string relativePath)
        {
            return Path.Combine(projectRoot, relativePath);
        }

        private T GetValue<T>(IniData iniData, string section, string key, T defaultValue)
        {
            if (iniData[section].ContainsKey(key))
            {
                return (T)Convert.ChangeType(iniData[section][key], typeof(T));
            }
            return defaultValue;
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