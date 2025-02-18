using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Engine.Interfaces;
using Agar.io_sfml.Game.Scripts.Abilities;
using SFML.Graphics;
using SFML.Window;
using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Game.Scripts.EntityController.Player;
using Agar.io_sfml.Game.Scripts.Config;

namespace Agar.io_sfml.Game.Scripts.GameRule
{
    public class GameInit
    {
        public GameLoop CreateGameLoop()
        {
            var configLoader = new ConfigLoader("Resources/Config/config.ini");
            var configData = configLoader.Load();
            var config = ConfigInitializer.Initialize(configData);

            FloatRect mapBorder = config.MapBounds;
            RenderWindow window = new RenderWindow(new VideoMode(1200, 800), "Agar.io");

            AbilitySystem abilitySystem = new AbilitySystem();
            abilitySystem.AddAbility(new SwapAbility(config.SwapAbilityCooldown));

            IInputHandler playerInput = new PlayerInputHandler();
            PlayerController playerController = new PlayerController(playerInput, abilitySystem);

            Entity player = new Entity(playerController, config.PlayerStartPosition, config.PlayerStartSize, config.PlayerStartSpeed, false, window, config);

            GameController gameController = new GameController(player, mapBorder, window, config);
            
            return new GameLoop(gameController, window);
        }
    }
}