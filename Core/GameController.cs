using SFML.Graphics;
using AgarIO.GameObjects;
using AgarIO.Factory;
using SFML.System;

namespace AgarIO.Core
{
    public class GameController
    {
        private Player player;
        private List<GameObject> gameObjects = new();
        private FoodFactory foodFactory;
        private EnemyFactory enemyFactory;
        private Clock clock = new();
        private const int InitialEnemyCount = 60;
        private const float FoodSpawnInterval = 0.05f;
        private float timeSinceLastFoodSpawn = 0f;
        private const float MinPlayerSize = 10f;

        public GameController(Player player, FloatRect mapBorder)
        {
            this.player = player;
            foodFactory = new FoodFactory(mapBorder);
            enemyFactory = new EnemyFactory(mapBorder);
            for (int i = 0; i < InitialEnemyCount; i++)
            {
                gameObjects.Add(enemyFactory.CreateEnemy());
            }
        }

        public void Update()
        {
            float deltaTime = clock.Restart().AsSeconds();
            player.Update(deltaTime);

            timeSinceLastFoodSpawn += deltaTime;
            if (timeSinceLastFoodSpawn >= FoodSpawnInterval)
            {
                timeSinceLastFoodSpawn = 0f;
                gameObjects.Add(foodFactory.CreateFood());
            }

            for (int i = gameObjects.Count - 1; i >= 0; i--)
            {
                if (gameObjects[i] is Food food)
                {
                    if (player.GetRadius() >= MinPlayerSize && food.IsEaten(player.Position, player.GetRadius()))
                    {
                        player.Grow(food.GetGrowthBonus());

                        if (player.GetRadius() < MinPlayerSize)
                        {
                            player.SetRadius(MinPlayerSize);
                        }

                        gameObjects.RemoveAt(i);
                    }
                }
                else if (gameObjects[i] is Enemy enemy)
                {
                    if (player.GetRadius() >= MinPlayerSize && player.GetRadius() > enemy.GetRadius())
                    {
                        if (enemy.IsEaten(player.Position, player.GetRadius()))
                        {
                            player.Grow(enemy.GetRadius());
                            gameObjects.RemoveAt(i);
                        }
                    }
                    else if (enemy.IsEaten(player.Position, enemy.GetRadius()) && enemy.GetRadius() > player.GetRadius())
                    {
                        player.SetRadius(MinPlayerSize);
                    }
                }
            }

            foreach (var obj in gameObjects)
            {
                obj.Update(deltaTime);
            }
        }

        public void Render(RenderWindow window)
        {
            player.Render(window);
            foreach (var obj in gameObjects)
            {
                obj.Render(window);
            }
        }
    }
}
