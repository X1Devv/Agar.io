using Agar.io_sfml.Game.Scripts.GameObjects;
using SFML.System;
using Agar.io_sfml.Engine.Interfaces;
using Agar.io_sfml.Engine.Core;
using Agar.io_sfml.Game.Scripts.Input;
using Agar.io_sfml.Game.Scripts.Abilities;
using Agar.io_sfml.Game.Scripts.GameRule;
using SFML.Graphics;
using SFML.Window;

namespace Agar.io_sfml.Game
{
    public class GameConfig
    {
        public GameLoop CreateGameLoop()
        {
            FloatRect mapBorder = new FloatRect(0, 0, 4000, 4000);
            RenderWindow window = new RenderWindow(new VideoMode(1200, 800), "Agar.io");
            IAbility ability = new SwapAbility();
            Controller playerController = new PlayerController(ability);
            Entity player = new Entity(playerController, new Vector2f(400, 300), 40f, 200f, false, window);
            GameController gameController = new GameController(player, mapBorder, window, ability);

            return new GameLoop(gameController, window);
        }
    }
}