using SFML.System;
using Agar.io_sfml.Engine.Core;
using Agar.io_sfml.Game.Scripts.Abilities;
using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Game.Scripts.EntityController.StateMachine;

namespace Agar.io_sfml.Game.Scripts.EntityController
{
    public class UniversalController : Controller
    {
        private EntityState _currentState;
        private readonly Entity _controlledEntity;
        private readonly float _baseSpeed;
        private readonly AbilitySystem _abilitySystem;

        public UniversalController(float baseSpeed, EntityState initialState, Entity entity, AbilitySystem abilitySystem = null)
        {
            _baseSpeed = baseSpeed;
            _currentState = initialState;
            _controlledEntity = entity;
            _abilitySystem = abilitySystem;
            _currentState.Enter(_controlledEntity);
        }

        public override Vector2f GetDirection(Vector2f currentPosition, float currentRadius, List<GameObject> gameObjects, float deltaTime)
        {
            Vector2f direction = _currentState.Update(_controlledEntity, gameObjects, deltaTime);
            return direction * _baseSpeed * deltaTime;
        }

        public override void PerformAbility(GameObject gameObject, List<GameObject> gameObjects)
        {
            if (_abilitySystem != null && gameObject is Entity entity)
            {
                _abilitySystem.ActivateAbility(entity, gameObjects, 0);
            }
        }

        public void SetState(EntityState newState)
        {
            _currentState = newState;
            _currentState.Enter(_controlledEntity);
        }
    }
}