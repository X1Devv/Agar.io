using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Game.Scripts.Config;
using Agar.io_sfml.Game.Scripts.EntityController;
using Agar.io_sfml.Game.Scripts.EntityController.StateMachine;
using Agar.io_sfml.Game.Scripts.GameObjects;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Engine.Factory
{
    public class EnemyFactory
    {
        private readonly FloatRect _mapBorder;
        private readonly float _minSize;
        private readonly float _maxSize;
        private readonly float _baseSpeed;
        private readonly TextureManager _textureManager;
        private readonly Config _config;
        private readonly Random _random = new();

        public EnemyFactory(FloatRect mapBorder, float minSize, float maxSize, float baseSpeed, TextureManager textureManager, Config config)
        {
            _mapBorder = mapBorder;
            _minSize = minSize;
            _maxSize = maxSize;
            _baseSpeed = baseSpeed;
            _textureManager = textureManager;
            _config = config;
        }

        public Entity CreateEnemy()
        {
            float x = (float)(_random.NextDouble() * _mapBorder.Width + _mapBorder.Left);
            float y = (float)(_random.NextDouble() * _mapBorder.Height + _mapBorder.Top);
            float size = (float)(_random.NextDouble() * (_maxSize - _minSize) + _minSize);

            Entity enemy = new Entity(null, new Vector2f(x, y), size, _baseSpeed, true, _textureManager,_config);
            enemy.SetController(new UniversalController(_baseSpeed, new ChaseState(), enemy));
            
            return enemy;
        }
    }
}