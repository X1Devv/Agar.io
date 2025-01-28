using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Engine.Core;
using SFML.Graphics;

namespace Agar.io_sfml.Engine.Managers
{
    public class GameObjectManager
    {
        private List<GameObject> gameObjects = new();

        public List<GameObject> GetAllObjects() => gameObjects;

        public void AddObject(GameObject obj)
        {
            gameObjects.Add(obj);
        }

        public void RemoveObject(GameObject obj)
        {
            gameObjects.Remove(obj);
        }

        public void SpawnFood(GameObject food)
        {
            gameObjects.Add(food);
        }

        public void SpawnEnemy(GameObject enemy)
        {
            gameObjects.Add(enemy);
        }

        public void UpdateObjects(float deltaTime)
        {
            foreach (var obj in gameObjects)
            {
                if (obj is Entity entity && entity.IsEnemy)
                {
                    var direction = entity.GetController().GetDirection(entity.Position, entity.GetRadius(), gameObjects, deltaTime);
                    entity.Position += direction * deltaTime * entity.GetRadius();
                    entity.Update(deltaTime);
                }
                else
                {
                    obj.Update(deltaTime);
                }
            }
        }

        public void RenderObjects(RenderWindow window)
        {
            foreach (var obj in gameObjects)
                obj.Render(window);
        }
    }
}
