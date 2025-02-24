using SFML.Graphics;
using SFML.System;
using Agar.io_sfml.Utils;

namespace Agar.io_sfml.Engine.Core
{
    public abstract class GameObject
    {
        public Vector2f Position { get; set; }

        /// <summary>
        /// Updates the game object
        /// </summary>
        public abstract void Update(float deltaTime);

        /// <summary>
        /// Renders the game object
        /// </summary>
        public abstract void Render(RenderWindow window);

        /// <summary>
        /// Gets the collision radius of the object
        /// </summary>
        public virtual float GetCollisionRadius() => 0f;

        /// <summary>
        /// Checks if this object collides with another
        /// </summary>
        public bool CollidesWith(GameObject other)
        {
            float distance = MathUtils.Distance(Position, other.Position);
            return distance <= GetCollisionRadius() + other.GetCollisionRadius();
        }
    }
}
