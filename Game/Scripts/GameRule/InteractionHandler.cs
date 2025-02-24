using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Utils;
using Agar.io_sfml.Engine.Core;

namespace Agar.io_sfml.Game.Scripts.GameRule
{
    public class InteractionHandler
    {
        private readonly float _minPlayerSize;
        private readonly StreakSystem _streakSystem;
        private readonly Func<bool> _isPaused;

        public InteractionHandler(float minPlayerSize, StreakSystem streakSystem, Func<bool> isPaused)
        {
            _minPlayerSize = minPlayerSize;
            _streakSystem = streakSystem;
            _isPaused = isPaused;
        }

        public void HandleInteractions(Entity player, List<GameObject> gameObjects, float deltaTime)
        {
            if (_isPaused()) return;

            for (int i = gameObjects.Count - 1; i >= 0; i--)
            {
                var obj = gameObjects[i];

                if (obj is Food food)
                {
                    HandleFoodInteraction(player, gameObjects, food, i);
                }
                else if (obj is Entity enemy && enemy.IsEnemy)
                {
                    HandleEnemyInteraction(player, gameObjects, enemy, i, deltaTime);
                }
            }
        }

        private void HandleFoodInteraction(Entity player, List<GameObject> gameObjects, Food food, int index)
        {
            if (food.CollidesWith(player))
            {
                player.Grow(food.GetGrowthBonus(), false);
                gameObjects.RemoveAt(index);
                return;
            }

            foreach (var obj in gameObjects)
            {
                if (obj is Entity enemy && enemy.IsEnemy && food.CollidesWith(enemy))
                {
                    enemy.Grow(food.GetGrowthBonus(), false);
                    gameObjects.RemoveAt(index);
                    break;
                }
            }
        }

        private void HandleEnemyInteraction(Entity player, List<GameObject> gameObjects, Entity enemy, int index, float deltaTime)
        {
            float playerRadius = player.GetRadius();
            float enemyRadius = enemy.GetRadius();
            float distanceToEnemy = MathUtils.Distance(player.Position, enemy.Position);

            if (distanceToEnemy <= playerRadius + enemyRadius)
            {
                float overlap = playerRadius + enemyRadius - distanceToEnemy;

                if (playerRadius > enemyRadius)
                {
                    float growthAmount = overlap;
                    player.Grow(growthAmount, true);
                    enemy.SetRadius(enemyRadius - overlap);

                    if (enemy.GetRadius() <= _minPlayerSize)
                    {
                        gameObjects.RemoveAt(index);
                        _streakSystem.OnKill();
                    }
                }
                else if (enemyRadius > playerRadius)
                {
                    float shrinkAmount = overlap;
                    player.SetRadius(playerRadius - shrinkAmount);

                    if (player.GetRadius() <= _minPlayerSize)
                    {
                        player.SetRadius(_minPlayerSize);
                    }
                }
            }
        }
    }
}