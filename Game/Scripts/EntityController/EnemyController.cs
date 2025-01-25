using Agar.io_sfml.Engine.Interfaces;
using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Utils;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.Input
{
    public class EnemyController : IInput
    {
        private Random random = new();
        private Vector2f currentDirection;
        private int stepsToChangeDirection = 50;
        private int currentStep = 0;

        public EnemyController()
        {
            RandomizeDirection();
        }

        public Vector2f GetDirection(Vector2f currentPosition, float currentRadius, List<GameObject> gameObjects)
        {
            GameObject nearestFood = null;
            float nearestDistance = float.MaxValue;

            foreach (var obj in gameObjects)
            {
                if (obj is Food food)
                {
                    float distanceToFood = MathUtils.Distance(currentPosition, food.Position);
                    if (distanceToFood < nearestDistance)
                    {
                        nearestDistance = distanceToFood;
                        nearestFood = food;
                    }
                }
            }

            if (nearestFood != null && nearestDistance <= 300)
            {
                return MathUtils.Normalize(nearestFood.Position - currentPosition);
            }

            if (currentStep >= stepsToChangeDirection)
            {
                RandomizeDirection();
                currentStep = 0;
            }

            currentStep++;
            return currentDirection;
        }

        public void PerformAbility(Character character, List<GameObject> gameObjects){ }

        private void RandomizeDirection()
        {
            currentDirection = new Vector2f(
                (float)(random.NextDouble() * 2 - 1),
                (float)(random.NextDouble() * 2 - 1)
            );
            currentDirection = MathUtils.Normalize(currentDirection);
        }
    }
}