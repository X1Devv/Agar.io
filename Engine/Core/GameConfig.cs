using SFML.Graphics;
using SFML.System;
using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Game.Scripts.GameRule;
using Agar.io_sfml.Game.Scripts.Input;
using SFML.Window;

namespace Agar.io_sfml.Game
{
    public class GameConfig
    {
        public GameLoop CreateGameLoop()
        {
            FloatRect mapBorder = new FloatRect(0, 0, 4000, 4000);
            RenderWindow window = new RenderWindow(new VideoMode(1200, 800), "Agar.io");

            PlayerInput input = new PlayerInput();
            Player player = new Player(input, new Vector2f(400, 300), window);

            GameController gameController = new GameController(player, mapBorder);
            return new GameLoop(gameController, window);
        }
    }
}