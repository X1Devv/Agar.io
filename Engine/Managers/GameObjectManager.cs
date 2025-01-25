using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Engine.Factory;
using SFML.Graphics;

namespace Agar.io_sfml.Engine.Managers
{
    public class GameObjectManager
    {
        private List<GameObject> gameObjects = new();
        
        private FoodFactory foodFactory;
        private EnemyFactory enemyFactory;

        public GameObjectManager(FoodFactory foodFactory, EnemyFactory enemyFactory)
        {
            this.foodFactory = foodFactory;
            this.enemyFactory = enemyFactory;
        }

        public List<GameObject> GetAllObjects() => gameObjects;

        public void AddObject(GameObject obj)
        {
            gameObjects.Add(obj);
        }

        public void RemoveObject(GameObject obj)
        {
            gameObjects.Remove(obj);
        }

        public void SpawnFood()
        {
            gameObjects.Add(foodFactory.CreateFood());
        }

        public void SpawnEnemy()
        {
            gameObjects.Add(enemyFactory.CreateEnemy());
        }

        public void UpdateObjects(float deltaTime)
        {
            foreach (var obj in gameObjects)
            {
                if (obj is Enemy enemy)
                {
                    enemy.SetGameObjects(gameObjects);
                    enemy.Update(deltaTime);
                }
                else
                    obj.Update(deltaTime);
            }
        }

        public void RenderObjects(RenderWindow window)
        {
            foreach (var obj in gameObjects)
                obj.Render(window);
        }
    }
}
