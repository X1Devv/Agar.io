using SFML.Graphics;
using Agar.io_sfml.Engine.Core;

namespace Agar.io_sfml.Engine.Managers
{
    public class GameObjectManager
    {
        private static GameObjectManager _instance;
        public static GameObjectManager Instance => _instance ??= new GameObjectManager();

        private List<GameObject> gameObjects = new();

        private GameObjectManager() { }

        /// <summary>
        /// Gets all game objects
        /// </summary>
        public List<GameObject> GetAllObjects() => gameObjects;

        /// <summary>
        /// Adds a game object to the manager
        /// </summary>
        public void AddObject(GameObject obj)
        {
            gameObjects.Add(obj);
        }

        /// <summary>
        /// Removes a game object from the manager
        /// </summary>
        public void RemoveObject(GameObject obj)
        {
            gameObjects.Remove(obj);
        }

        /// <summary>
        /// Updates all game objects
        /// </summary>
        public void UpdateObjects(float deltaTime)
        {
            foreach (var obj in gameObjects.ToList())
            {
                obj.Update(deltaTime);
            }
        }

        /// <summary>
        /// Renders all game objects
        /// </summary>
        public void RenderObjects(RenderWindow window)
        {
            foreach (var obj in gameObjects)
                obj.Render(window);
        }
    }
}
