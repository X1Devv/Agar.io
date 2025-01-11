﻿using AgarIO.GameObjects;
using SFML.Graphics;
using SFML.System;

namespace AgarIO.Factory
{
    public class FoodFactory
    {
        private FloatRect mapBorder;
        private Random random = new Random();

        public FoodFactory(FloatRect mapBorder)
        {
            this.mapBorder = mapBorder;
        }

        public Food CreateFood()
        {
            float x = (float)(random.NextDouble() * mapBorder.Width + mapBorder.Left);
            float y = (float)(random.NextDouble() * mapBorder.Height + mapBorder.Top);

            return new Food(new Vector2f(x, y));
        }
    }
}
