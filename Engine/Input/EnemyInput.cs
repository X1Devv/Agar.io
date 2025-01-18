using Agar.io_sfml.Game.GameObjects;
using SFML.System;

namespace Agar.io_sfml.Engine.Input
{
    public class EnemyInput
    {
        private Random random = new();
        private Vector2f currentDirection;
        private int stepsToChangeDirection = 50;
        private int currentStep = 0;

        public EnemyInput()
        {
            RandomizeDirection();
        }

        public Vector2f GetDirection(Vector2f currentPosition, float currentRadius, List<GameObject> gameObjects)
        {
            Food nearestFood = null;
            float nearestDistance = float.MaxValue;

            foreach (var obj in gameObjects)
            {
                if (obj is Food food)
                {
                    float distanceToFood = Distance(currentPosition, food.Position);
                    if (distanceToFood < nearestDistance)
                    {
                        nearestDistance = distanceToFood;
                        nearestFood = food;
                    }
                }
            }

            if (nearestFood != null && nearestDistance <= 300)
            {
                return Normalize(nearestFood.Position - currentPosition);
            }

            if (currentStep >= stepsToChangeDirection)
            {
                RandomizeDirection();
                currentStep = 0;
            }

            currentStep++;
            return currentDirection;
        }

        private void RandomizeDirection()
        {
            currentDirection = new Vector2f(
                (float)(random.NextDouble() * 2 - 1),
                (float)(random.NextDouble() * 2 - 1)
            );
            currentDirection = Normalize(currentDirection);
        }

        private Vector2f Normalize(Vector2f vector)
        {
            float magnitude = MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            return magnitude > 0 ? vector / magnitude : new Vector2f(0, 0);
        }

        private float Distance(Vector2f a, Vector2f b)
        {
            return MathF.Sqrt(MathF.Pow(a.X - b.X, 2) + MathF.Pow(a.Y - b.Y, 2));
        }
    }
}
