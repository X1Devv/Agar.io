using Agar.io_sfml.Game.Scripts.GameObjects;

namespace Agar.io_sfml.Engine.Interfaces
{
    public interface IAbility
    {
        void Execute(Character character, List<GameObject> gameObjects);
    }
}