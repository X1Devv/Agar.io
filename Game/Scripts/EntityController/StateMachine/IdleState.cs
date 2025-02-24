using SFML.System;
using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Engine.Core;
using Agar.io_sfml.Utils;

namespace Agar.io_sfml.Game.Scripts.EntityController.StateMachine
{
    public class IdleState : EntityState
    {
        private float idleTime;
        private float currentTime;
        private Vector2f randomDirection;

        public IdleState(float duration)
        {
            idleTime = duration;
            currentTime = 0f;
            Random rand = new();
            randomDirection = MathUtils.Normalize(new Vector2f((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1));
        }

        public override void Enter(Entity entity)
        {
            currentTime = 0f;
        }

        public override Vector2f Update(Entity entity, List<GameObject> gameObjects, float deltaTime)
        {
            currentTime += deltaTime;
            if (currentTime >= idleTime)
            {
                entity.SetState(new ChaseState());
                return new Vector2f(0, 0);
            }

            return randomDirection;
        }
    }
}