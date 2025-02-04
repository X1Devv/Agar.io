using Agar.io_sfml.Engine.Interfaces;
using Agar.io_sfml.Game.Scripts.EntityController.Enemy;
using Agar.io_sfml.Game.Scripts.GameObjects;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Engine.Factory
{
    public class EnemyFactory
    {
        private FloatRect _mapBorder;
        private float _minSize;
        private float _maxSize;
        private float _baseSpeed;
        private Random _random = new();

        public EnemyFactory(FloatRect mapBorder, float minSize, float maxSize, float baseSpeed)
        {
            _mapBorder = mapBorder;
            _minSize = minSize;
            _maxSize = maxSize;
            _baseSpeed = baseSpeed;
        }

        public Entity CreateEnemy()
        {
            float x = (float)(_random.NextDouble() * _mapBorder.Width + _mapBorder.Left);
            float y = (float)(_random.NextDouble() * _mapBorder.Height + _mapBorder.Top);
            float size = (float)(_random.NextDouble() * (_maxSize - _minSize) + _minSize);

            IInputHandler enemyInput = new EnemyInputHandler();
            EnemyController enemyController = new EnemyController(enemyInput, _baseSpeed);

            return new Entity(enemyController, new Vector2f(x, y), size, 200f, true);
        }
    }
}