using SFML.Graphics;
using SFML.System;


namespace Agar.io_sfml.Factory
{
    public class EnemyFactory
    {
        private FloatRect mapBorder;
        private Random random = new();

        public EnemyFactory(FloatRect mapBorder)
        {
            this.mapBorder = mapBorder;
        }

        public Enemy CreateEnemy()
        {
            float x = (float)(random.NextDouble() * mapBorder.Width + mapBorder.Left);
            float y = (float)(random.NextDouble() * mapBorder.Height + mapBorder.Top);
            float size = (float)(random.NextDouble() * 20 + 10);

            return new Enemy(new Vector2f(x, y), size);
        }
    }
}
