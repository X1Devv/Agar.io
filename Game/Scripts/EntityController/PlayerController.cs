using SFML.System;
using Agar.io_sfml.Engine.Core;
using Agar.io_sfml.Engine.Interfaces;
using Agar.io_sfml.Game.Scripts.Abilities;
using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Utils;

namespace Agar.io_sfml.Game.Scripts.Input
{
    public class PlayerController : Controller
    {
        private IInputHandler inputHandler;
        private AbilitySystem abilitySystem;

        public PlayerController(IInputHandler inputHandler, AbilitySystem abilitySystem)
        {
            this.inputHandler = inputHandler;
            this.abilitySystem = abilitySystem;
        }

        public override Vector2f GetDirection(Vector2f currentPosition, float currentRadius, List<GameObject> gameObjects, float deltaTime)
        {
            return MathUtils.Normalize(inputHandler.GetInputDirection());
        }

        public override void PerformAbility(GameObject gameObject, List<GameObject> gameObjects)
        {
            if (gameObject is Entity player)
            {
                abilitySystem.ActivateAbility(player, gameObjects, 0);
            }
        }
    }
}