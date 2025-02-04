using Agar.io_sfml.Game.Scripts.Config;
using Agar.io_sfml.Game.Scripts.GameObjects;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Engine.Factory
{
    public class FoodFactory
    {
        private FloatRect mapBorder;
        private Random random = new Random();

        private List<FoodConfig> foodConfigs;

        public FoodFactory(FloatRect mapBorder, List<FoodConfig> configs)
        {
            this.mapBorder = mapBorder;
            foodConfigs = configs;
        }


        public Food CreateFood()
        {
            int totalWeight = foodConfigs.Sum(config => config.Probability);
            int randomValue = random.Next(0, totalWeight);

            FoodConfig selectedConfig = null;
            foreach (var config in foodConfigs)
            {
                if (randomValue < config.Probability)
                {
                    selectedConfig = config;
                    break;
                }
                randomValue -= config.Probability;
            }

            float x = (float)(random.NextDouble() * mapBorder.Width + mapBorder.Left);
            float y = (float)(random.NextDouble() * mapBorder.Height + mapBorder.Top);

            return new Food(new Vector2f(x, y), selectedConfig.Size, selectedConfig.Color, selectedConfig.GrowthBonus);
        }
    }
}
