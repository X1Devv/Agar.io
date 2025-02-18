using Agar.io_sfml.Game.Scripts.Config;
using Agar.io_sfml.Game.Scripts.GameObjects;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Engine.Factory
{
    public class FoodFactory
    {
        private FloatRect _mapBorder;
        private Random _random = new();
        private List<Config.FoodConfig> _foodConfigs;

        public float SpawnInterval { get; }

        public FoodFactory(FloatRect mapBorder, List<Config.FoodConfig> configs)
        {
            _mapBorder = mapBorder;
            _foodConfigs = configs;
            SpawnInterval = 0.05f;
        }

        public Food CreateFood()
        {
            int totalWeight = _foodConfigs.Sum(config => config.Probability);
            int randomValue = _random.Next(0, totalWeight);

            Config.FoodConfig selectedConfig = null;
            foreach (var config in _foodConfigs)
            {
                if (randomValue < config.Probability)
                {
                    selectedConfig = config;
                    break;
                }
                randomValue -= config.Probability;
            }

            float x = (float)(_random.NextDouble() * _mapBorder.Width + _mapBorder.Left);
            float y = (float)(_random.NextDouble() * _mapBorder.Height + _mapBorder.Top);

            return new Food(new Vector2f(x, y), selectedConfig.Size, selectedConfig.Color, selectedConfig.GrowthBonus);
        }
    }
}