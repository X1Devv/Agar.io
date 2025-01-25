using Agar.io_sfml.Game.Scripts.Input;
using SFML.Graphics;
using SFML.System;


namespace Agar.io_sfml.Game.Scripts.GameObjects
{
    public class Enemy : Character
    {
        private readonly EnemyController input;
        private List<GameObject> gameObjects;

        public Enemy(Vector2f initialPosition, float initialRadius) : base(initialPosition, initialRadius, 200f, Color.Red)
        {
            this.input = new EnemyController();
        }

        public override void Update(float deltaTime)
        {
            if (gameObjects != null)
            {
                Position += input.GetDirection(Position, Radius, gameObjects) * speed * deltaTime;
                shape.Position = Position;
            }
        }

        public void SetGameObjects(List<GameObject> objects)
        {
            gameObjects = objects;
        }

        public void SetPos(Vector2f newPosition)
        {
            Position = newPosition;
            shape.Position = newPosition;
        }

        public float GetRadius() => Radius;
    }
}