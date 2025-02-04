using Agar.io_sfml.Engine.Core;
using Agar.io_sfml.Engine.Interfaces;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.EntityController.Enemy
{
    public class EnemyController : Controller
    {
        private readonly IInputHandler _inputHandler;
        private readonly float _baseSpeed;

        public EnemyController(IInputHandler inputHandler, float baseSpeed)
        {
            _inputHandler = inputHandler;
            _baseSpeed = baseSpeed;
        }

        public override Vector2f GetDirection(
            Vector2f currentPosition,
            float radius,
            List<GameObject> objects,
            float deltaTime
        )
        {
            Vector2f direction = _inputHandler.GetInputDirection();
            return direction * _baseSpeed;
        }
    }
}