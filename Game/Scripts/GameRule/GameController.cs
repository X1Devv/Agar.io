﻿using Agar.io_sfml.Engine.Camera;
using Agar.io_sfml.Engine.Factory;
using Agar.io_sfml.Engine.Managers;
using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Game.Scripts.Abilities;
using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Game.Scripts.UI;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.GameRule
{
    public class GameController
    {
        private Entity player;
        private GameObjectManager gameObjectManager = new();

        private AbilitySystem abilitySystem;

        private FoodFactory foodFactory;
        private EnemyFactory enemyFactory;

        private InteractionHandler interactionHandler;

        private PlayerUI playerUI;
        private CameraController cameraController;

        private Clock clock = new();

        private float timeSinceLastFoodSpawn;
        private float foodSpawnInterval;

        public GameController(Entity player, FloatRect mapBorder, RenderWindow window, ConfigLoader config)
        {
            this.player = player;
            foodSpawnInterval = config.FoodSpawnInterval;
            foodFactory = new FoodFactory(mapBorder, config.FoodConfigs);

            enemyFactory = new EnemyFactory(
                mapBorder,
                config.EnemyMinSize,
                config.EnemyMaxSize,
                config.EnemyBaseSpeed
            );

            for (int i = 0; i < config.EnemyCount; i++)
                gameObjectManager.SpawnEnemy(enemyFactory.CreateEnemy());

            abilitySystem = new AbilitySystem();
            abilitySystem.AddAbility(new SwapAbility(config.SwapAbilityCooldown));
            interactionHandler = new InteractionHandler(config.MinPlayerRadius);

            cameraController = new CameraController(window, player, mapBorder, config);

            playerUI = new PlayerUI(window, new TextureManager(), cameraController);
            playerUI.AddAbility(config.SwapAbilityButtonPath, () =>
            {
                abilitySystem.ActivateAbility(player, gameObjectManager.GetAllObjects(), 0);
            });
        }

        public void Update(RenderWindow window)
        {
            float deltaTime = clock.Restart().AsSeconds();

            player.Update(deltaTime);
            cameraController.Update(deltaTime);

            timeSinceLastFoodSpawn += deltaTime;
            if (timeSinceLastFoodSpawn >= foodSpawnInterval)
            {
                timeSinceLastFoodSpawn = 0f;
                gameObjectManager.SpawnFood(foodFactory.CreateFood());
            }

            interactionHandler.HandleInteractions(player, gameObjectManager.GetAllObjects(), deltaTime);
            gameObjectManager.UpdateObjects(deltaTime);
            abilitySystem.Update(deltaTime);
            playerUI.Update();
        }

        public void Render(RenderWindow window)
        {
            cameraController.Apply();
            player.Render(window);
            gameObjectManager.RenderObjects(window);
            playerUI.Render();
        }
    }
}