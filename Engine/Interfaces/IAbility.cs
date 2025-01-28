using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Engine.Core;

namespace Agar.io_sfml.Engine.Interfaces
{
    public interface IAbility
    {
        void Execute(Entity player, List<GameObject> gameObjects);
        void Execute(GameObject gameObject, List<GameObject> gameObjects);
    }
}
