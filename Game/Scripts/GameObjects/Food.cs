using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.GameObjects
{
    public class Food : GameObject
    {
        private CircleShape shape;
        public Color FoodColor { get; private set; }
        public float Size { get; private set; }
        public int GrowthBonus { get; private set; }

        public Food(Vector2f position, float size, Color color, int growthBonus)
        {
            Position = position;
            Size = size;
            FoodColor = color;
            GrowthBonus = growthBonus;

            shape = new CircleShape(Size)
            {
                FillColor = FoodColor,
                Origin = new Vector2f(Size / 2, Size / 2),
                Position = position
            };
        }

        public int GetGrowthBonus()
        {
            return GrowthBonus;
        }

        public override void Update(float deltaTime) { }

        public override void Render(RenderWindow window)
        {
            window.Draw(shape);
        }
    }
}
