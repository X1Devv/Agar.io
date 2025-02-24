using SFML.System;
using SFML.Window;
using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Utils;
using Agar.io_sfml.Engine.Core;

namespace Agar.io_sfml.Game.Scripts.EntityController.StateMachine
{
    public class PlayerControlState : EntityState
    {
        public override void Enter(Entity entity) { }

        public override Vector2f Update(Entity entity, List<GameObject> gameObjects, float deltaTime)
        {
            float x = 0f, y = 0f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) y -= 1f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) y += 1f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) x -= 1f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) x += 1f;
            return MathUtils.Normalize(new Vector2f(x, y));
        }

    }
}