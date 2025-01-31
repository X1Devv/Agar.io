using Agar.io_sfml.Engine.Core;
using Agar.io_sfml.Engine.Interfaces;
using Agar.io_sfml.Utils;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.Input
{
    public class EnemyController : Controller
    {
        private IInputHandler inputHandler;

        public EnemyController(IInputHandler inputHandler)
        {
            this.inputHandler = inputHandler;
        }

        public override Vector2f GetDirection(Vector2f currentPosition, float currentRadius, List<GameObject> gameObjects, float deltaTime)
        {
            return MathUtils.Normalize(inputHandler.GetInputDirection());
        }
    }
}