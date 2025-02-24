using Agar.io_sfml.Engine.Camera;
using Agar.io_sfml.Engine.Factory;
using Agar.io_sfml.Engine.Interfaces;
using Agar.io_sfml.Engine.Managers;
using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Game.Scripts.Abilities;
using Agar.io_sfml.Game.Scripts.Audio;
using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Game.Scripts.Managers;
using Agar.io_sfml.Game.Scripts.UI;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.GameRule
{
    public class GameController : IGameController
    {
        private readonly Entity _player;
        private readonly AbilitySystem _abilitySystem;
        private readonly FoodFactory _foodFactory;
        private readonly EnemyFactory _enemyFactory;
        private readonly InteractionHandler _interactionHandler;
        private readonly PlayerUI _playerUI;
        private readonly CameraController _cameraController;
        private readonly SoundManager _soundManager;
        private readonly StreakSystem _streakSystem;
        private readonly Sprite _mapBackground;
        private readonly GameStateManager _gameStateManager;
        private readonly Clock _gameClock = new();
        private float _timeSinceLastFoodSpawn;
        private bool _musicPlaying;

        public GameController(Entity player, FloatRect mapBorder, RenderWindow window, Config.Config config, TextureManager textureManager)
        {
            _player = player;
            _foodFactory = new FoodFactory(mapBorder, config.FoodConfigs);
            _enemyFactory = new EnemyFactory(mapBorder, config.EnemyMinSize, config.EnemyMaxSize, config.EnemyBaseSpeed, textureManager, config);
            _abilitySystem = new AbilitySystem();
            _soundManager = new SoundManager();
            _cameraController = new CameraController(window, _player, mapBorder, config.CameraSmoothness, config.StartZoom, config.CameraMinZoom, config.CameraMaxZoom);
            _streakSystem = new StreakSystem(_soundManager, window);
            _interactionHandler = new InteractionHandler(config.MinPlayerRadius, _streakSystem, () => _gameStateManager.IsPaused());
            _playerUI = new PlayerUI(window, textureManager, _cameraController);
            _gameStateManager = new GameStateManager(window, textureManager, config);

            GameObjectManager.Instance.AddObject(_player);

            _playerUI.AddAbility(config.SwapAbilityButtonPath, () =>
            {
                if (_gameStateManager.IsPlaying())
                    _abilitySystem.ActivateAbility(_player, GameObjectManager.Instance.GetAllObjects(), 0);
            });
            _playerUI.AddPauseButton(config.PauseButtonPath, _gameStateManager.TogglePause);

            _soundManager.LoadBackgroundMusic(config.Audio.BackgroundMusic);
            _soundManager.PlayBackgroundMusic();
            _musicPlaying = true;
            foreach (var sound in config.Audio.StreakSounds)
                _soundManager.LoadSound(sound.Key, sound.Value);

            var texture = textureManager.LoadTexture(config.BackgroundTexturePath);
            _mapBackground = new Sprite(texture) { Position = new Vector2f(config.MapBounds.Left, config.MapBounds.Top), Scale = new Vector2f(config.MapBounds.Width / texture.Size.X, config.MapBounds.Height / texture.Size.Y) };

            for (int i = 0; i < config.EnemyCount; i++)
            {
                GameObjectManager.Instance.AddObject(_enemyFactory.CreateEnemy());
            }

            _abilitySystem.AddAbility(new SwapAbility(config.SwapAbilityCooldown));

            for (int i = 0; i < 10; i++)
            {
                GameObjectManager.Instance.AddObject(_foodFactory.CreateFood());
            }
        }

        public void Update(RenderWindow window)
        {
            float deltaTime = _gameClock.Restart().AsSeconds();

            _playerUI.Update();

            if (_gameStateManager.IsPlaying())
            {
                _player.Update(deltaTime);
                _cameraController.Update(deltaTime);

                _timeSinceLastFoodSpawn += deltaTime;
                if (_timeSinceLastFoodSpawn >= _foodFactory.SpawnInterval)
                {
                    _timeSinceLastFoodSpawn = 0f;
                    GameObjectManager.Instance.AddObject(_foodFactory.CreateFood());
                }

                _interactionHandler.HandleInteractions(_player, GameObjectManager.Instance.GetAllObjects(), deltaTime);
                GameObjectManager.Instance.UpdateObjects(deltaTime);
                _abilitySystem.Update(deltaTime);
                _streakSystem.Update();

                if (!_musicPlaying)
                {
                    _soundManager.PlayBackgroundMusic();
                    _musicPlaying = true;
                }
            }
            else if (_musicPlaying)
            {
                _soundManager.StopBackgroundMusic();
                _musicPlaying = false;
            }

            _gameStateManager.Update(deltaTime, _player);
        }

        public void Render(RenderWindow window)
        {
            _cameraController.Apply();
            window.Draw(_mapBackground);
            GameObjectManager.Instance.RenderObjects(window);
            _player.Render(window);

            foreach (var obj in GameObjectManager.Instance.GetAllObjects())
            {
                if (obj is Entity entity)
                {
                    CircleShape hitbox = new CircleShape(entity.GetRadius()) { Position = entity.Position - new Vector2f(entity.GetRadius(), entity.GetRadius()), FillColor = Color.Transparent, OutlineColor = entity.IsEnemy ? Color.Red : Color.Green, OutlineThickness = 2f };
                    window.Draw(hitbox);
                }
            }

            window.SetView(window.DefaultView);
            _playerUI.Render();
            _streakSystem.Render();
            _gameStateManager.Render();
            window.SetView(_cameraController.GetView());
        }
    }
}