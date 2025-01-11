using AgarIO.GameObjects;
using SFML.Graphics;
using SFML.System;
using AgarIO.Core;
using Agar.io_sfml.Input;
using SFML.Window;

namespace AgarIO.Game
{
    public class Boot
    {
        public static void Main()
        {
            FloatRect mapBorder = new FloatRect(0, 0, 4000, 4000);
            RenderWindow window = new RenderWindow(new VideoMode(1200, 800), "Agar.io");
            
            Input input = new Input();
            Player player = new Player(input, new Vector2f(400, 300), window);
            
            GameController gameController = new GameController(player, mapBorder);
            Game game = new Game(gameController, window);
            
            game.Run();
        }
    }
}
