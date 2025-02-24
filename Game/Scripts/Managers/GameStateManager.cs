using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Game.Scripts.GameObjects;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.Managers
{
    public class GameStateManager
    {
        private enum GameState { Playing, Paused, GameOver }
        private GameState _currentState;
        private readonly RenderWindow _window;
        private readonly Text _stateText;
        private readonly Font _font;
        private Clock _pauseClock;
        private float _lastPauseTime;
        private const float PauseCooldown = 0.2f;

        public GameStateManager(RenderWindow window, TextureManager textureManager, Config.Config config)
        {
            _window = window;
            _font = textureManager.LoadFont(config.FontPath);
            _stateText = new Text("", _font, 40)
            {
                FillColor = Color.Black,
                OutlineColor = Color.White,
                OutlineThickness = 2f,
                Position = new Vector2f(window.Size.X / 2 - 100, window.Size.Y / 2 - 20)
            };
            _currentState = GameState.Playing;
            _pauseClock = new Clock();
        }

        public void Update(float deltaTime, Entity player)
        {
            if (player.GetRadius() < 5f)
            {
                _currentState = GameState.GameOver;
                _stateText.DisplayedString = "Game Over";
            }
            else if (_currentState == GameState.Paused)
            {
                _stateText.DisplayedString = "Paused";
            }
        }

        public void Render()
        {
            if (_currentState != GameState.Playing)
            {
                _window.SetView(_window.DefaultView);
                _window.Draw(_stateText);
            }
        }

        public void TogglePause()
        {
            float currentTime = _pauseClock.ElapsedTime.AsSeconds();
            if (currentTime - _lastPauseTime < PauseCooldown) return;

            _lastPauseTime = currentTime;
            if (_currentState == GameState.Playing)
            {
                _currentState = GameState.Paused;
            }
            else if (_currentState == GameState.Paused)
            {
                _currentState = GameState.Playing;
            }
        }
        public bool IsPaused()
        {
            return _currentState == GameState.Paused;
        }

        public bool IsPlaying()
        {
            return _currentState == GameState.Playing;
        }
    }
}