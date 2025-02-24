using Agar.io_sfml.Engine.Camera;
using Agar.io_sfml.Engine.Core;
using Agar.io_sfml.Engine.Managers;
using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Game.Scripts.Animation;
using Agar.io_sfml.Game.Scripts.EntityController;
using Agar.io_sfml.Game.Scripts.EntityController.StateMachine;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.GameObjects
{
    public class Entity : GameObject
    {
        private Controller _controller;
        private AnimationAtlas _animationAtlas;
        private readonly float _baseSpeed;
        public bool IsEnemy { get; }
        private float radius;
        public float Mass { get; private set; }
        public CameraController CameraController { get; private set; }
        private Text nameText;
        private string entityName;

        public Entity(Controller controller, Vector2f position, float radius, float speed, bool isEnemy, TextureManager textureManager, Config.Config config, Dictionary<string, IntRect[]> atlasRegions = null, RenderWindow window = null, string skinPath = null)
        {
            _controller = controller;
            _baseSpeed = speed;
            IsEnemy = isEnemy;
            this.radius = radius;
            Mass = radius * radius;
            Position = position;

            entityName = GenerateName();
            nameText = new Text(entityName, textureManager.LoadFont(config.FontPath), (uint)(12 + radius / 2)) { FillColor = Color.White, OutlineColor = Color.Black, OutlineThickness = 1f };

            string atlasPath = skinPath != null ? skinPath : (isEnemy ? config.DefaultEnemySkinPath + "/atlas.png" : config.DefaultPlayerSkinPath + "/atlas.png");
            Texture atlasTexture = textureManager.LoadTexture(atlasPath);

            if (atlasRegions == null)
            {
                atlasRegions = new Dictionary<string, IntRect[]> { { "Idle", GenerateRegions(0, 0, 800, 626, 1) }, { "Moving", GenerateRegions(0, 0, 800, 626, 20) } };
            }

            _animationAtlas = new AnimationAtlas(atlasTexture, atlasRegions);
            if (!isEnemy && window != null)
            {
                CameraController = new CameraController(window, this, config.MapBounds, config.CameraSmoothness, config.StartZoom, config.CameraMinZoom, config.CameraMaxZoom);
            }
        }

        private IntRect[] GenerateRegions(int startX, int startY, int frameWidth, int frameHeight, int frameCount)
        {
            IntRect[] regions = new IntRect[frameCount];
            int columns = 5;
            int rows = 4;

            for (int i = 0; i < frameCount; i++)
            {
                int row = i / columns;
                int col = i % columns;
                regions[i] = new IntRect(startX + (col * frameWidth), startY + (row * frameHeight), frameWidth, frameHeight);
            }
            return regions;
        }

        private string GenerateName()
        {
            string[] prefixes = { "True", "Life", "Gurren", "Mega", "Ark" };
            string[] suffixes = { "Enjoyer", "Lina", "Warden", "Ruiner", "Lagann" };
            Random rand = new();
            return prefixes[rand.Next(prefixes.Length)] + suffixes[rand.Next(suffixes.Length)];
        }

        public override void Update(float deltaTime)
        {
            var gameObjects = GameObjectManager.Instance.GetAllObjects();
            Vector2f velocity = _controller.GetDirection(Position, radius, gameObjects, deltaTime);
            Position += velocity;

            _animationAtlas.Update(deltaTime, velocity);
            nameText.Position = Position - new Vector2f(nameText.GetLocalBounds().Width / 2, radius + 15);
            nameText.CharacterSize = (uint)(12 + radius / 2);

            if (!IsEnemy && CameraController != null)
            {
                CameraController.Update(deltaTime);
                CameraController.Apply();
            }

            if (!IsEnemy)
            {
                Mass -= deltaTime * 0.1f;
                radius = (float)Math.Sqrt(Mass);
                if (radius < 20f) radius = 20f;
            }
        }

        public void Grow(float amount, bool fromKill = false)
        {
            if (fromKill)
            {
                radius += amount * 0.5f;
            }
            else
            {
                float growthModifier = 1f / (1f + radius / 100f);
                radius += amount * 0.5f * growthModifier;
            }
            Mass = radius * radius;
            if (!IsEnemy && CameraController != null)
            {
                CameraController.AdjustZoom(0);
            }
        }

        public override void Render(RenderWindow window)
        {
            _animationAtlas.Draw(window, Position, radius * 2f);
            window.Draw(nameText);
        }

        public override float GetCollisionRadius()
        {
            return radius;
        }

        public Controller GetController()
        {
            return _controller;
        }

        public float GetRadius()
        {
            return radius;
        }

        public void SetPos(Vector2f newPosition)
        {
            Position = newPosition;
        }

        public void SetController(Controller controller)
        {
            _controller = controller;
        }

        public string GetName()
        {
            return entityName;
        }

        public void SetRadius(float newRadius)
        {
            radius = newRadius;
            Mass = radius * radius;
            if (!IsEnemy && CameraController != null)
            {
                CameraController.AdjustZoom(0);
            }
        }

        public void SetState(EntityState newState)
        {
            if (_controller is UniversalController universalController)
            {
                universalController.SetState(newState);
            }
        }
    }
}