using SFML.System;

namespace Agar.io_sfml.Engine.Core
{
    public abstract class Controller
    {
        public abstract Vector2f GetDirection(Vector2f currentPosition, float currentRadius, List<GameObject> gameObjects, float deltaTime);
        public virtual void PerformAbility(GameObject gameObject, List<GameObject> gameObjects) { }
    }
}
