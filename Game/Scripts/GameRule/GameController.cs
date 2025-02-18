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
using GameConfig = Agar.io_sfml.Game.Scripts.Config.Config;

namespace Agar.io_sfml.Game.Scripts.GameRule
{
    public class GameController
    {
        private Entity _player;
        private GameObjectManager _gameObjectManager = new();
        private AbilitySystem _abilitySystem;
        private FoodFactory _foodFactory;
        private EnemyFactory _enemyFactory;
        private InteractionHandler _interactionHandler;
        private PlayerUI _playerUI;
        private CameraController _cameraController;
        private SoundManager _soundManager;
        private StreakSystem _streakSystem;
        private Clock _gameClock = new();
        private float _timeSinceLastFoodSpawn;
        private Clock _pauseClock = new();
        private float _lastPauseTime;
        private const float PauseCooldown = 0.2f;
        private bool _isPaused;

        public bool IsPaused => _isPaused;

        public GameController(Entity player, FloatRect mapBorder, RenderWindow window, GameConfig config)
        {
            _player = player;
            _foodFactory = new FoodFactory(mapBorder, config.FoodConfigs);
            _enemyFactory = new EnemyFactory(mapBorder, config.EnemyMinSize, config.EnemyMaxSize, config.EnemyBaseSpeed);
            _abilitySystem = new AbilitySystem();
            _soundManager = new SoundManager();
            _cameraController = new CameraController(window, _player, mapBorder, config);
            _streakSystem = new StreakSystem(_soundManager, window);
            _interactionHandler = new InteractionHandler(config.MinPlayerRadius, _streakSystem, () => IsPaused);
            _playerUI = new PlayerUI(window, new TextureManager(), _cameraController);
            _playerUI.AddAbility(config.SwapAbilityButtonPath, () =>
                _abilitySystem.ActivateAbility(_player, _gameObjectManager.GetAllObjects(), 0));
            _playerUI.AddPauseButton(config.PauseButtonPath, TogglePause);

            _soundManager.LoadBackgroundMusic(config.Audio.BackgroundMusic);
            _soundManager.PlayBackgroundMusic();
            _soundManager.SetMusicVolume(_soundManager.MusicVolume);
            _soundManager.SetSoundVolume(_soundManager.SoundVolume);
            foreach (var sound in config.Audio.StreakSounds)
            {
                _soundManager.LoadSound(sound.Key, sound.Value);
            }

            for (int i = 0; i < config.EnemyCount; i++)
            {
                _gameObjectManager.SpawnEnemy(_enemyFactory.CreateEnemy());
            }

            _abilitySystem.AddAbility(new SwapAbility(config.SwapAbilityCooldown));
        }

        public void Update(RenderWindow window)
        {
            if (_isPaused) return;

            float deltaTime = _gameClock.Restart().AsSeconds();

            _player.Update(deltaTime);
            _cameraController.Update(deltaTime);

            _timeSinceLastFoodSpawn += deltaTime;
            if (_timeSinceLastFoodSpawn >= _foodFactory.SpawnInterval)
            {
                _timeSinceLastFoodSpawn = 0f;
                _gameObjectManager.SpawnFood(_foodFactory.CreateFood());
            }

            _interactionHandler.HandleInteractions(_player, _gameObjectManager.GetAllObjects(), deltaTime);
            _gameObjectManager.UpdateObjects(deltaTime);
            _abilitySystem.Update(deltaTime);
            _streakSystem.Update();
            _playerUI.Update();
        }

        public void Render(RenderWindow window)
        {
            _cameraController.Apply();
            window.Clear(new Color(46, 47, 48));
            _gameObjectManager.RenderObjects(window);
            _player.Render(window);
            _playerUI.Render();
            _streakSystem.Render();
        }

        public void TogglePause()
        {
            float currentTime = _pauseClock.ElapsedTime.AsSeconds();
            if (currentTime - _lastPauseTime < PauseCooldown) return;
            _lastPauseTime = currentTime;
            _isPaused = !_isPaused;
            if (_isPaused)
                _soundManager.StopBackgroundMusic();
            else
                _soundManager.PlayBackgroundMusic();
        }
    }
}