using SFML.Graphics;
using SFML.System;
using Agar.io_sfml.Engine.Factory;
using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Engine.UI;
using Agar.io_sfml.Engine.Camera;
using Agar.io_sfml.Engine.Managers;

namespace Agar.io_sfml.Game.Scripts.GameRule
{
    public class GameController
    {
        private Player player;
        private GameObjectManager gameObjectManager;
        
        private Clock clock = new();
        private Clock abilityCooldownClock = new();
        
        private const float FoodSpawnInterval = 0.01f;
        private float timeSinceLastFoodSpawn = 0f;
        
        private const float AbilityCooldown = 5f;
        private bool isAbilityReady = true;

        private const float minPlayerSize = 20f;

        private InteractionHandler interactionHandler;
        
        private PlayerUI playerUI;
        private CameraController cameraController;

        public GameController(Player player, FloatRect mapBorder, RenderWindow window)
        {
            this.player = player;

            var foodFactory = new FoodFactory(mapBorder, DefaultFoodConfigs());
            var enemyFactory = new EnemyFactory(mapBorder);

            gameObjectManager = new GameObjectManager(foodFactory, enemyFactory);

            interactionHandler = new InteractionHandler(minPlayerSize);
            cameraController = new CameraController(window, player, mapBorder);

            TextureManager textureManager = new TextureManager();

            playerUI = new PlayerUI(window, textureManager);

            playerUI.AddAbility("Game\\Textures\\AbilityButton\\SwapButton.png", () =>
            {
                if (isAbilityReady)
                {
                    player.PerformAbility(gameObjectManager.GetAllObjects());
                    isAbilityReady = false;
                    abilityCooldownClock.Restart();
                }
            });

            for (int i = 0; i < 30; i++)
                gameObjectManager.SpawnEnemy();
        }

        public void Update(RenderWindow window)
        {
            float deltaTime = clock.Restart().AsSeconds();
            player.Update(deltaTime);

            timeSinceLastFoodSpawn += deltaTime;
            if (timeSinceLastFoodSpawn >= FoodSpawnInterval)
            {
                timeSinceLastFoodSpawn = 0f;
                gameObjectManager.SpawnFood();
            }

            interactionHandler.HandleInteractions(player, gameObjectManager.GetAllObjects(), deltaTime);

            gameObjectManager.UpdateObjects(deltaTime);

            if (!isAbilityReady && abilityCooldownClock.ElapsedTime.AsSeconds() >= AbilityCooldown)
                isAbilityReady = true;

            cameraController.Update();
            playerUI.Update();
        }

        public void Render(RenderWindow window)
        {
            cameraController.Apply();
            player.Render(window);
            gameObjectManager.RenderObjects(window);

            window.SetView(window.DefaultView);
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
