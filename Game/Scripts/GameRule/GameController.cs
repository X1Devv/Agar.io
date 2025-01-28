using Agar.io_sfml.Game.Scripts.GameObjects;
using SFML.Graphics;
using SFML.System;
using Agar.io_sfml.Engine.Managers;
using Agar.io_sfml.Engine.Factory;
using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Engine.Camera;
using Agar.io_sfml.Game.Scripts.Input;
using Agar.io_sfml.Engine.Interfaces;
using Agar.io_sfml.Game.Scripts.Config;

namespace Agar.io_sfml.Game.Scripts.GameRule
{
    public class GameController
    {
        private Entity player;
        private GameObjectManager gameObjectManager;

        private PlayerController playerController;
        private FoodFactory foodFactory;

        private InteractionHandler interactionHandler;
        private PlayerUI playerUI;
        private CameraController cameraController;

        private Clock clock = new();
        private const float FoodSpawnInterval = 0.01f;
        private float timeSinceLastFoodSpawn = 0f;

        private float abilityCooldownTime = 5f;
        private float timeSinceLastAbilityUse = 0f;

        public GameController(Entity player, FloatRect mapBorder, RenderWindow window, IAbility ability)
        {
            this.player = player;
            gameObjectManager = new GameObjectManager();
            interactionHandler = new InteractionHandler(20f);

            playerController = new PlayerController(ability);

            foodFactory = new FoodFactory(mapBorder, DefaultFoodConfigs());
            EnemyFactory enemyFactory = new EnemyFactory(mapBorder);

            for (int i = 0; i < 30; i++)
                gameObjectManager.SpawnEnemy(enemyFactory.CreateEnemy());

            gameObjectManager.SpawnFood(foodFactory.CreateFood());

            cameraController = new CameraController(window, player, mapBorder);

            TextureManager textureManager = new TextureManager();
            playerUI = new PlayerUI(window, textureManager, cameraController);

            playerUI.AddAbility("Game\\Textures\\AbilityButton\\SwapButton.png", () =>
            {
                if (timeSinceLastAbilityUse >= abilityCooldownTime)
                {
                    playerController.PerformAbility(player, gameObjectManager.GetAllObjects());
                    timeSinceLastAbilityUse = 0f;
                }
            });
        }


        public void Update(RenderWindow window)
        {
            if (foodFactory == null)
                return;

            float deltaTime = clock.Restart().AsSeconds();
            player.Update(deltaTime);

            cameraController.Update();

            timeSinceLastAbilityUse += deltaTime;

            timeSinceLastFoodSpawn += deltaTime;
            if (timeSinceLastFoodSpawn >= FoodSpawnInterval)
            {
                timeSinceLastFoodSpawn = 0f;

                var food = foodFactory.CreateFood();
                gameObjectManager.SpawnFood(food);
            }

            interactionHandler.HandleInteractions(player, gameObjectManager.GetAllObjects(), deltaTime);

            gameObjectManager.UpdateObjects(deltaTime);

            playerUI.Update();
        }

        public void Render(RenderWindow window)
        {
            cameraController.Apply();
            player.Render(window);
            gameObjectManager.RenderObjects(window);
            playerUI.Render();
        }

        private List<FoodConfig> DefaultFoodConfigs()
        {
            return new List<FoodConfig>
            {
                new FoodConfig(70, 10, Color.Magenta, 10),
                new FoodConfig(20, 15, new Color(204, 95, 45), 15),
                new FoodConfig(10, 15, Color.Green, 35),
                new FoodConfig(3, 50, Color.Black, -30)
            };
        }
    }
}
