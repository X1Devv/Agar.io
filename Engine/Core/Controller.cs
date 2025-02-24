using SFML.System;

namespace Agar.io_sfml.Engine.Core
{
    public abstract class Controller
    {
        protected float Speed { get; set; }

        /// <summary>
        /// Gets the direction for the controlled object
        /// </summary>
        public abstract Vector2f GetDirection(Vector2f currentPosition, float currentRadius, List<GameObject> gameObjects, float deltaTime);

        /// <summary>
        /// Performs an ability for the controlled object
        /// </summary>
        public virtual void PerformAbility(GameObject gameObject, List<GameObject> gameObjects) { }

        /// <summary>
        /// Gets the speed of the controller
        /// </summary>
        public float GetSpeed() => Speed;
    }
}