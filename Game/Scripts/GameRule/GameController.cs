using Agar.io_sfml.Engine.Camera;
using Agar.io_sfml.Engine.Factory;
using Agar.io_sfml.Engine.Managers;
using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Game.Scripts.Abilities;
using Agar.io_sfml.Game.Scripts.Audio;
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
        private Clock pauseClock = new();
        private float timeSinceLastFoodSpawn;
        private float foodSpawnInterval;
        private AudioSystem audioSystem;
        private StreakSystem streakSystem;
        private bool isPaused = false;
        private RectangleShape pauseOverlay;
        private float pauseCooldown = 0.2f;
        private float lastPauseTime = 0f;

        public GameController(Entity player, FloatRect mapBorder, RenderWindow window, ConfigLoader config)
        {
            this.player = player;
            foodSpawnInterval = config.FoodSpawnInterval;
            foodFactory = new FoodFactory(mapBorder, config.FoodConfigs);
            enemyFactory = new EnemyFactory(mapBorder, config.EnemyMinSize, config.EnemyMaxSize, config.EnemyBaseSpeed);

            for (int i = 0; i < config.EnemyCount; i++)
                gameObjectManager.SpawnEnemy(enemyFactory.CreateEnemy());

            abilitySystem = new AbilitySystem();
            abilitySystem.AddAbility(new SwapAbility(config.SwapAbilityCooldown));

            audioSystem = new AudioSystem();
            audioSystem.LoadBackgroundMusic(config.BackgroundMusicPath);
            audioSystem.LoadStreakSound("Kill", config.KillSoundPath);
            audioSystem.LoadStreakSound("FirstBlood", config.FirstBloodSoundPath);
            audioSystem.LoadStreakSound("DoubleKill", config.DoubleKillSoundPath);
            audioSystem.LoadStreakSound("TripleKill", config.TripleKillSoundPath);
            audioSystem.LoadStreakSound("UltraKill", config.UltraKillSoundPath);
            audioSystem.LoadStreakSound("Rampage", config.RampageSoundPath);
            audioSystem.LoadStreakSound("HolyShit", config.HolyShitSoundPath);
            audioSystem.PlayBackgroundMusic();

            cameraController = new CameraController(window, player, mapBorder, config);
            streakSystem = new StreakSystem(audioSystem, window, config, cameraController);
            interactionHandler = new InteractionHandler(config.MinPlayerRadius, streakSystem, this);

            playerUI = new PlayerUI(window, new TextureManager(), cameraController);
            playerUI.AddAbility(config.SwapAbilityButtonPath, () =>
            {
                abilitySystem.ActivateAbility(player, gameObjectManager.GetAllObjects(), 0);
            });

            playerUI.AddPauseButton(config.PauseButtonPath, TogglePause);
            CreatePauseOverlay(window);
        }

        private void CreatePauseOverlay(RenderWindow window)
        {
            pauseOverlay = new RectangleShape(new Vector2f(window.Size.X, window.Size.Y))
            {
                FillColor = new Color(40, 40, 40, 150)
            };
        }

        public void TogglePause()
        {
            float currentTime = pauseClock.ElapsedTime.AsSeconds();
            if (currentTime - lastPauseTime < pauseCooldown)
                return;

            lastPauseTime = currentTime;
            isPaused = !isPaused;
        }

        public bool IsPaused => isPaused;

        public void Update(RenderWindow window)
        {
            playerUI.Update();

            if (isPaused) return;

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
            streakSystem.Update();
        }

        public void Render(RenderWindow window)
        {
            cameraController.Apply();
            player.Render(window);
            gameObjectManager.RenderObjects(window);

            if (isPaused)
            {
                window.SetView(window.DefaultView);
                window.Draw(pauseOverlay);
                window.SetView(cameraController.GetView());
            }
            playerUI.Render();
        }
    }
}
