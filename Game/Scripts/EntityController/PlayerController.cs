using SFML.System;
using Agar.io_sfml.Engine.Core;
using Agar.io_sfml.Engine.Interfaces;
using Agar.io_sfml.Utils;

namespace Agar.io_sfml.Game.Scripts.Input
{
    public class PlayerController : Controller
    {
        private IAbility ability;

        public PlayerController(IAbility ability)
        {
            this.ability = ability;
        }

        public override Vector2f GetDirection(Vector2f currentPosition, float currentRadius, List<GameObject> gameObjects, float deltaTime)
        {
            float x = 0, y = 0;
            if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.W)) y -= 1;
            if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.S)) y += 1;
            if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.A)) x -= 1;
            if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.D)) x += 1;

            Vector2f direction = new Vector2f(x, y);
            return MathUtils.Normalize(direction);
        }

        public override void PerformAbility(GameObject gameObject, List<GameObject> gameObjects)
        {
            ability.Execute(gameObject, gameObjects);
        }
    }
}
