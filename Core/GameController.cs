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
        private Clock clock = new();
        private float foodSpawnTimer = 0f;
        private const float FoodSpawnInterval = 0.05f;
        private const float MinPlayerSize = 10f;

        public GameController(Player player, FloatRect mapBounds)
        {
            this.player = player;
            foodFactory = new FoodFactory(mapBounds);
        }

        public void Update()
        {
            float deltaTime = clock.Restart().AsSeconds();
            player.Update(deltaTime);

            foodSpawnTimer += deltaTime;
            if (foodSpawnTimer >= FoodSpawnInterval)
            {
                foodSpawnTimer = 0;
                gameObjects.Add(foodFactory.CreateFood());
            }

            for (int i = gameObjects.Count - 1; i >= 0; i--)
            {
                if (gameObjects[i] is Food food && food.IsEaten(player.Position, player.GetRadius()))
                {
                    player.Grow(food.GetGrowthBonus());

                    if (player.GetRadius() < MinPlayerSize)
                    {
                        player.SetRadius(MinPlayerSize);
                    }

                    gameObjects.RemoveAt(i);
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
