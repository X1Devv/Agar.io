using SFML.System;
using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Engine.Core;

namespace Agar.io_sfml.Game.Scripts.EntityController.StateMachine
{
    public abstract class EntityState
    {
        public abstract void Enter(Entity entity);
        public abstract Vector2f Update(Entity entity, List<GameObject> gameObjects, float deltaTime);
    }
}