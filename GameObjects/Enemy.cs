using SFML.Graphics;
using SFML.System;

namespace AgarIO.GameObjects
{
    public class Enemy : GameObject
    {
        private CircleShape shape;
        public float Radius { get; private set; }
        public Vector2f Position { get; private set; }
        
        private FloatRect mapBorder;
        private Vector2f direction;

        private const uint MaxMovementCount = 50;
        private uint movementCounter = 0;
        private const float Speed = 100f;

        public Enemy(Vector2f position, float initialSize, FloatRect mapBorder)
        {
            Radius = initialSize;
            Position = position;
            this.mapBorder = mapBorder;

            shape = new CircleShape(Radius)
            {
                FillColor = Color.Red,
                Origin = new Vector2f(Radius, Radius),
                Position = Position
            };

            RandomizeDirection();
        }
        public override void Update(float deltaTime)
        {
            if (movementCounter >= MaxMovementCount)
            {
                RandomizeDirection();
                movementCounter = 0;
            }

            Position += direction * Speed * deltaTime;

            if (Position.X < mapBorder.Left || Position.X > mapBorder.Left + mapBorder.Width)
                direction.X = -direction.X;

            if (Position.Y < mapBorder.Top || Position.Y > mapBorder.Top + mapBorder.Height)
                direction.Y = -direction.Y;

            shape.Position = Position;

            movementCounter++;
        }

        public override void Render(RenderWindow window)
        {
            window.Draw(shape);
        }

        public float GetRadius() => Radius;


        public bool IsEaten(Vector2f playerPosition, float playerRadius)
        {
            float distance = MathF.Sqrt(MathF.Pow(Position.X - playerPosition.X, 2) + MathF.Pow(Position.Y - playerPosition.Y, 2));
            return distance < playerRadius + Radius;
        }

        private void RandomizeDirection()
        {
            direction = new Vector2f(
                (float)(Random.Shared.NextDouble() * 2 - 1),
                (float)(Random.Shared.NextDouble() * 2 - 1)
            );

            if (direction != new Vector2f(0, 0))
                direction /= MathF.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
        }
    }
}
