using SFML.System;
using SFML.Window;

namespace Agar.io_sfml.Game.Scripts.Input
{
    public class PlayerInput
    {
        public Vector2f GetMovement()
        {
            float x = 0, y = 0;
            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) y -= 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) y += 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) x -= 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) x += 1;

            Vector2f direction = new Vector2f(x, y);
            if (direction != new Vector2f(0, 0))
                direction /= MathF.Sqrt(x * x + y * y);

            return direction;
        }
    }
}
