using SFML.Graphics;
using SFML.System;
using Agar.io_sfml.Engine.Factory;
using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Game.Scripts.Input;
using SFML.Window;
using System.Numerics;

namespace Agar.io_sfml.Game.Scripts.GameRule
{
    public class GameController
    {
        private Player player;
        private List<GameObject> gameObjects = new();

        private FoodFactory foodFactory;
        private EnemyFactory enemyFactory;

        private Clock clock = new();

        private const int InitialEnemyCount = 30;
        private const float FoodSpawnInterval = 0.05f;

        private float timeSinceLastFoodSpawn = 0f;
        private const float MinPlayerSize = 20f;

        private InteractionHandler interactionHandler;

        public GameController(Player player, FloatRect mapBorder)
        {
            this.player = player;
            foodFactory = new FoodFactory(mapBorder, DefaultFoodConfigs());
            enemyFactory = new EnemyFactory(mapBorder);
            interactionHandler = new InteractionHandler(MinPlayerSize);

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

            interactionHandler.HandleInteractions(player, gameObjects, deltaTime);

            foreach (var obj in gameObjects)
            {
                if (obj is Enemy enemy)
                {
                    enemy.SetGameObjects(gameObjects);
                    enemy.Update(deltaTime);
                }
                else
                {
                    obj.Update(deltaTime);
                }
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.F))
            {
                Swap();
            }
        }

        private void Swap()
        {
            Enemy nearestEnemy = null;
            float shortestDistance = float.MaxValue;

            foreach (var obj in gameObjects)
            {
                if (obj is Enemy enemy)
                {
                    float distance = Distance(player.Position, enemy.Position);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestEnemy = enemy;
                    }
                }
            }
                Vector2f temp = player.Position;
                player.SetPos(nearestEnemy.Position);
                nearestEnemy.SetPos(temp);
        }

        private float Distance(Vector2f a, Vector2f b)
        {
            return MathF.Sqrt(MathF.Pow(a.X - b.X, 2) + MathF.Pow(a.Y - b.Y, 2));
        }

        public void Render(RenderWindow window)
        {
            player.Render(window);
            foreach (var obj in gameObjects)
            {
                obj.Render(window);
            }
        }

        private List<FoodConfig> DefaultFoodConfigs()
        {
            return new List<FoodConfig>
            {
                new FoodConfig(70, 10, Color.Magenta, 2),
                new FoodConfig(20, 15, new Color(204, 95, 45), 8),
                new FoodConfig(10, 15,Color.Green, 25),
                new FoodConfig(3, 50, Color.Black, -30)
            };
        }
    }
}
