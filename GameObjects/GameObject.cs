using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.GameObjects
{
    public abstract class GameObject
    {
        public Vector2f Position { get; protected set; }
        public abstract void Update(float deltaTime);
        public abstract void Render(RenderWindow window);

        public virtual bool IsEaten(Vector2f otherPosition, float otherRadius)
        {
            float distance = MathF.Sqrt(MathF.Pow(Position.X - otherPosition.X, 2) + MathF.Pow(Position.Y - otherPosition.Y, 2));
            return distance < otherRadius + (this is Enemy ? ((Enemy)this).Radius : 0);
        }
    }
}