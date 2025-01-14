using Agar.io_sfml.Input;
using SFML.Graphics;
using SFML.System;
using Agar.io_sfml.GameRule;
using Agar.io_sfml.GameObjects;
using SFML.Window;

namespace Agar.io_sfml.Game
{
    public class Boot
    {
        public static void Main()
        {
            FloatRect mapBorder = new FloatRect(0, 0, 4000, 4000);
            RenderWindow window = new RenderWindow(new VideoMode(1200, 800), "Agar.io");

            PlayerInput input = new PlayerInput();
            Player player = new Player(input, new Vector2f(400, 300), window);

            GameController gameController = new GameController(player, mapBorder);
            Game game = new Game(gameController, window);

            game.Run();
        }
    }
}
