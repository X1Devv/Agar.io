using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.Config
{
    public class Config
    {
        public int EnemyCount { get; set; }
        public float FoodSpawnInterval { get; set; }
        public float PlayerStartSpeed { get; set; }
        public Vector2f PlayerStartPosition { get; set; }
        public float PlayerStartSize { get; set; }
        public float MinPlayerRadius { get; set; }
        public float SwapAbilityCooldown { get; set; }

        public float EnemyMinSize { get; set; }
        public float EnemyMaxSize { get; set; }
        public float EnemyBaseSpeed { get; set; }

        public float StartZoom { get; set; }
        public float CameraMinZoom { get; set; }
        public float CameraMaxZoom { get; set; }
        public float CameraSmoothness { get; set; }

        public FloatRect MapBounds { get; set; }

        public List<FoodConfig> FoodConfigs { get; set; }

        public AudioConfig Audio { get; set; }

        public string SwapAbilityButtonPath { get; set; }
        public string PauseButtonPath { get; set; }
        public string FontPath { get; set; }
        public string BackgroundTexturePath { get; set; }

        public class AudioConfig
        {
            public string BackgroundMusic { get; set; }
            public Dictionary<string, string> StreakSounds { get; set; }
        }

        public class FoodConfig
        {
            public int Probability { get; set; }
            public float Size { get; set; }
            public Color Color { get; set; }
            public int GrowthBonus { get; set; }
        }
    }
}