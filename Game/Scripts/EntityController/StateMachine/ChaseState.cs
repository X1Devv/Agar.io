using SFML.System;
using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Utils;
using Agar.io_sfml.Engine.Core;

namespace Agar.io_sfml.Game.Scripts.EntityController.StateMachine
{
    public class ChaseState : EntityState
    {
        private GameObject target;
        private Vector2f currentVelocity;

        public override void Enter(Entity entity)
        {
            target = null;
            currentVelocity = new Vector2f(0, 0);
        }

        public override Vector2f Update(Entity entity, List<GameObject> gameObjects, float deltaTime)
        {
            target = FindNearestFood(entity, gameObjects);
            if (target == null)
            {
                return new Vector2f(0, 0);
            }

            Vector2f direction = MathUtils.Normalize(target.Position - entity.Position);
            currentVelocity += (direction - currentVelocity);
            return currentVelocity;
        }

        private GameObject FindNearestFood(Entity entity, List<GameObject> gameObjects)
        {
            GameObject nearestFood = null;
            float minDistance = float.MaxValue;

            foreach (var obj in gameObjects)
            {
                if (obj is Food food)
                {
                    float distance = MathUtils.Distance(entity.Position, food.Position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestFood = food;
                    }
                }
            }
            return nearestFood;
        }
    }
}