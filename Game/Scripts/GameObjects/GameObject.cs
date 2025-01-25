using Agar.io_sfml.Utils;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.GameObjects
{
    public abstract class GameObject
    {
        public Vector2f Position { get; set; }
        public abstract void Update(float deltaTime);
        public abstract void Render(RenderWindow window);

        public virtual bool IsEaten(Vector2f otherPosition, float otherRadius)
        {
            return MathUtils.Distance(Position, otherPosition) < otherRadius + (this is Character character ? character.Radius : 0);
        }
    }
}