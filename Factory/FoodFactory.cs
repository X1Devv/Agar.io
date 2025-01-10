using AgarIO.GameObjects;
using SFML.Graphics;
using SFML.System;

namespace AgarIO.Factory
{
    public class FoodFactory
    {
        private FloatRect mapBounds;
        private Random random = new Random();

        public FoodFactory(FloatRect mapBounds)
        {
            this.mapBounds = mapBounds;
        }

        public Food CreateFood()
        {
            float x = (float)(random.NextDouble() * mapBounds.Width + mapBounds.Left);
            float y = (float)(random.NextDouble() * mapBounds.Height + mapBounds.Top);

            return new Food(new Vector2f(x, y));
        }
    }
}
