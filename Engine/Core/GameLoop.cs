using SFML.Graphics;
using Agar.io_sfml.Engine.Interfaces;

namespace Agar.io_sfml.Engine.Core
{
    public class GameLoop
    {
        private readonly RenderWindow _window;
        private readonly IGameController _gameController;

        /// <summary>
        /// Initializes a new instance of the GameLoop class
        /// </summary>
        public GameLoop(IGameController gameController, RenderWindow window)
        {
            _gameController = gameController;
            _window = window;
            _window.SetFramerateLimit(180);
            _window.Closed += (sender, e) => _window.Close();
        }

        /// <summary>
        /// Runs the game loop
        /// </summary>
        public void Run()
        {
            while (_window.IsOpen)
            {
                _window.DispatchEvents();
                _gameController.Update(_window);
                _gameController.Render(_window);
                _window.Display();
            }
        }
    }
}