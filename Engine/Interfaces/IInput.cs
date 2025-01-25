using Agar.io_sfml.Game.Scripts.GameObjects;
using SFML.System;

namespace Agar.io_sfml.Engine.Interfaces
{
    public interface IInput
    {
        Vector2f GetDirection(Vector2f currentPosition, float currentRadius, List<GameObject> gameObjects);
        void PerformAbility(Character character, List<GameObject> gameObjects);
    }

}