using Agar.io_sfml.Engine.Interfaces;
using Agar.io_sfml.Utils;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.Input
{
    public class EnemyInputHandler : IInputHandler
    {
        private Random random = new();
        private Vector2f currentDirection;
        private int stepsToChangeDirection = 50;
        private int currentStep = 0;

        public EnemyInputHandler()
        {
            RandomizeDirection();
        }

        public Vector2f GetInputDirection()
        {
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
            currentDirection = MathUtils.Normalize(currentDirection);
        }
    }
}