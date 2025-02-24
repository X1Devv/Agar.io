using Agar.io_sfml.Engine.Core;
using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Game.Scripts.Abilities;
using Agar.io_sfml.Game.Scripts.Config;
using Agar.io_sfml.Game.Scripts.EntityController;
using Agar.io_sfml.Game.Scripts.EntityController.StateMachine;
using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Game.Scripts.UI;
using SFML.Graphics;
using SFML.Window;

namespace Agar.io_sfml.Game.Scripts.GameRule
{
    public class GameInit
    {
        private SkinSelectionMenu _skinMenu;

        public GameLoop CreateGameLoop()
        {
            var config = LoadConfig();
            var window = CreateWindow();
            var textureManager = new TextureManager();

            _skinMenu = new SkinSelectionMenu(window, textureManager, config);
            Dictionary<string, IntRect[]> selectedSkin = ShowSkinSelection(window);

            var player = CreatePlayer(config, window, textureManager, selectedSkin, _skinMenu.GetSelectedSkinPath());
            var gameController = new GameController(player, config.MapBounds, window, config, textureManager);

            return new GameLoop(gameController, window);
        }

        private Dictionary<string, IntRect[]> ShowSkinSelection(RenderWindow window)
        {
            while (window.IsOpen && _skinMenu.IsActive)
            {
                window.DispatchEvents();
                if (_skinMenu.Update())
                    return _skinMenu.GetSelectedSkin();

                window.Clear();
                _skinMenu.Render();
                window.Display();
            }
            return _skinMenu.GetSelectedSkin();
        }

        private Config.Config LoadConfig()
        {
            var configLoader = new ConfigLoader("Resources/Config/config.ini");
            return ConfigInitializer.Initialize(configLoader.Load());
        }

        private RenderWindow CreateWindow()
        {
            return new RenderWindow(new VideoMode(1200, 800), "Agar.io");
        }

        private Entity CreatePlayer(Config.Config config, RenderWindow window, TextureManager textureManager, Dictionary<string, IntRect[]> selectedSkin, string skinPath)
        {
            var abilitySystem = new AbilitySystem();
            abilitySystem.AddAbility(new SwapAbility(config.SwapAbilityCooldown));
            
            Entity player = new Entity(null, config.PlayerStartPosition, config.PlayerStartSize, config.PlayerStartSpeed, false, textureManager, config, selectedSkin, window, skinPath);
            player.SetController(new UniversalController(config.PlayerStartSpeed, new PlayerControlState(), player, abilitySystem));
            
            return player;
        }
    }
}