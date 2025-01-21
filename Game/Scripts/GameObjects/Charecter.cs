using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.GameObjects
{
    public abstract class Character : GameObject
    {
        protected CircleShape shape;
        protected float speed;
        public float Radius { get; protected set; }

        public Character(Vector2f position, float radius, float speed, Color color)
        {
            Radius = radius;
            Position = position;
            this.speed = speed;

            shape = new CircleShape(Radius)
            {
                FillColor = color,
                Origin = new Vector2f(Radius, Radius),
                Position = Position
            };
        }

        public void Grow(float amount, bool isPlayer = false)
        {
            if (isPlayer)
                SetRadius(Radius + amount);
            else
                SetRadius(Radius + amount / (1 + Radius * 0.02f));
        }


        public void SetRadius(float newRadius)
        {
            Radius = MathF.Max(newRadius, 0);
            shape.Radius = Radius;
            shape.Origin = new Vector2f(Radius, Radius);
        }

        public override void Render(RenderWindow window)
        {
            window.Draw(shape);
        }
    }
}
