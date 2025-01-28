using Agar.io_sfml.Utils;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Engine.Core
{
    public abstract class GameObject
    {
        public Vector2f Position { get; set; }
        public abstract void Update(float deltaTime);
        public abstract void Render(RenderWindow window);

        public virtual float GetCollisionRadius() => 0f;

        public bool CollidesWith(GameObject other)
        {
            float distance = MathUtils.Distance(Position, other.Position);
            return distance <= GetCollisionRadius() + other.GetCollisionRadius();
        }
    }
}
