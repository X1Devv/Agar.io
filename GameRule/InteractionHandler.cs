using Agar.io_sfml.GameObjects;
using SFML.System;


namespace Agar.io_sfml.GameRule
{
    public class InteractionHandler
    {
        private float MinPlayerSize;

        public InteractionHandler(float minPlayerSize)
        {
            MinPlayerSize = minPlayerSize;
        }

        public void HandleInteractions(Player player, List<GameObject> gameObjects, float deltaTime)
        {
            for (int i = gameObjects.Count - 1; i >= 0; i--)
            {
                var obj = gameObjects[i];

                switch (obj)
                {
                    case Food food:
                        HandleFoodInteraction(player, gameObjects, food, i);
                        break;
                    case Enemy enemy:
                        HandleEnemyInteraction(player, gameObjects, enemy, i, deltaTime);
                        break;
                }
            }
        }

        private void HandleFoodInteraction(Player player, List<GameObject> gameObjects, Food food, int index)
        {
            if (food.IsEaten(player.Position, player.GetRadius()))
            {
                player.Grow(ReductionOfGrowthBeyondTheRadius(player.GetRadius(), food.GetGrowthBonus()));
                gameObjects.RemoveAt(index);
                return;
            }

            foreach (var obj in gameObjects)
            {
                if (obj is Enemy enemy && food.IsEaten(enemy.Position, enemy.GetRadius()))
                {
                    enemy.Grow(ReductionOfGrowthBeyondTheRadius(enemy.Radius, food.GetGrowthBonus()));
                    gameObjects.RemoveAt(index);
                    break;
                }
            }
        }
        private void HandleEnemyInteraction(Player player, List<GameObject> gameObjects, Enemy enemy, int index, float deltaTime)
        {
            float playerRadius = player.GetRadius();
            float enemyRadius = enemy.Radius;
            float distanceToEnemy = Distance(player.Position, enemy.Position);

            if (distanceToEnemy <= playerRadius + enemyRadius)
            {
                float overlap = (playerRadius + enemyRadius) - distanceToEnemy;

                if (playerRadius > enemyRadius)
                {
                    float growthAmount = ReductionOfGrowthBeyondTheRadius(playerRadius, overlap);
                    player.Grow(growthAmount);
                    enemy.SetRadius(enemyRadius - overlap);

                    if (enemyRadius <= MinPlayerSize)
                    {
                        gameObjects.RemoveAt(index);
                    }
                }
                else if (enemyRadius > playerRadius)
                {
                    float shrinkAmount = ReductionOfGrowthBeyondTheRadius(enemyRadius, overlap);
                    player.SetRadius(playerRadius - shrinkAmount);

                    if (playerRadius <= MinPlayerSize)
                    {
                        player.SetRadius(MinPlayerSize);
                    }
                }
            }
        }
        private float ReductionOfGrowthBeyondTheRadius(float currentRadius, float initialGrowth)
        {
            return initialGrowth / (1 + currentRadius * 0.05f);
        }

        private float Distance(Vector2f a, Vector2f b)
        {
            return MathF.Sqrt(MathF.Pow(a.X - b.X, 2) + MathF.Pow(a.Y - b.Y, 2));
        }
    }
}



