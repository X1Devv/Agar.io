using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Game.Scripts.Input;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Engine.Factory
{
    public class EnemyFactory
    {
        private FloatRect mapBorder;
        private Random random = new();

        public EnemyFactory(FloatRect mapBorder)
        {
            this.mapBorder = mapBorder;
        }

        public Entity CreateEnemy()
        {
            float x = (float)(random.NextDouble() * mapBorder.Width + mapBorder.Left);
            float y = (float)(random.NextDouble() * mapBorder.Height + mapBorder.Top);
            float size = (float)(random.NextDouble() * 20 + 10);

            return new Entity(new EnemyController(), new Vector2f(x, y), size, 200f, true);
        }

    }
}
