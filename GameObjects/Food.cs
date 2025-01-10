using SFML.Graphics;
using SFML.System;

namespace AgarIO.GameObjects
{
    public class Food : GameObject
    {
        private static Random random = new Random();
        private CircleShape shape;
        public Color FoodColor { get; private set; }
        public float Size { get; private set; }
        public int GrowthBonus { get; private set; }

        public Food(Vector2f position)
        {
            Position = position;

            int chance = random.Next(0, 101);
            if (chance <= 70)
            {
                FoodColor = Color.Magenta;
                Size = 10;
                GrowthBonus = 2;
            }
            else if (chance <= 90)
            {
                FoodColor = new Color(204, 95, 45);
                Size = 15;
                GrowthBonus = 8;
            }
            else if (chance <= 95)
            {
                FoodColor = Color.Black;
                Size = 20;
                GrowthBonus = -10;
            }

            shape = new CircleShape(Size)
            {
                FillColor = FoodColor,
                Origin = new Vector2f(Size / 2, Size / 2),
                Position = position
            };
        }

        public override void Update(float deltaTime) { }

        public override void Render(RenderWindow window)
        {
            window.Draw(shape);
        }

        public bool IsEaten(Vector2f playerPosition, double playerRadius)
        {
            float distance = MathF.Sqrt(MathF.Pow(Position.X - playerPosition.X, 2) + MathF.Pow(Position.Y - playerPosition.Y, 2));
            return distance < playerRadius + Size / 2;
        }

        public int GetGrowthBonus()
        {
            return GrowthBonus;
        }
    }
}
