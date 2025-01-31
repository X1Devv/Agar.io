using Agar.io_sfml.Engine.Interfaces;
using SFML.System;
using SFML.Window;

namespace Agar.io_sfml.Game.Scripts.Input
{
    public class PlayerInputHandler : IInputHandler
    {
        public Vector2f GetInputDirection()
        {
            float x = 0, y = 0;
            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) y -= 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) y += 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) x -= 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) x += 1;

            return new Vector2f(x, y);
        }
    }
}