using SFML.Graphics;
using SFML.System;

namespace AgarIO.GameObjects
{
    public abstract class GameObject
    {
        public Vector2f Position { get; protected set; }
        public abstract void Update(float deltaTime);
        public abstract void Render(RenderWindow window);
    }
}