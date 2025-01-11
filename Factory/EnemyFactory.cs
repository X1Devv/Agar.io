using AgarIO.GameObjects;
using SFML.Graphics;
using SFML.System;

namespace AgarIO.Factory
{
    public class EnemyFactory
    {
        private FloatRect mapBorder;
        private Random random = new Random();

        public EnemyFactory(FloatRect mapBorder)
        {
            this.mapBorder = mapBorder;
        }

        public Enemy CreateEnemy()
        {
            float x = (float)(random.NextDouble() * mapBorder.Width + mapBorder.Left);
            float y = (float)(random.NextDouble() * mapBorder.Height + mapBorder.Top);
            float size = (float)(random.Next(10, 30));
            Color color = Color.Red;

            return new Enemy(new Vector2f(x, y), size, mapBorder);
        }
    }
}
