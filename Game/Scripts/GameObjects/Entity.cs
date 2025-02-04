using Agar.io_sfml.Engine.Camera;
using Agar.io_sfml.Engine.Core;
using Agar.io_sfml.Engine.Utils;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.GameObjects
{
    public class Entity : GameObject
    {
        private Controller controller;

        private TextureManager textureManager;
        private Texture playerTexture;

        private CameraController cameraController;

        public bool IsEnemy { get; private set; }
        private float speed;

        private CircleShape shape;

        public float Radius { get; private set; }

        public Entity(Controller controller, Vector2f position, float radius, float speed, bool isEnemy, RenderWindow window = null, ConfigLoader config = null)
        {
            this.controller = controller;
            this.speed = speed;
            IsEnemy = isEnemy;
            Radius = radius;
            Position = position;

            shape = new CircleShape(radius)
            {
                FillColor = isEnemy ? Color.Red : Color.Blue,
                Origin = new Vector2f(radius, radius),
                Position = position
            };

            if (!isEnemy && window != null)
            {
                textureManager = new TextureManager();
                playerTexture = textureManager.LoadTexture("Resources\\Textures\\Skin\\PlayerSkin.png");

                if (playerTexture != null)
                {
                    shape.Texture = playerTexture;
                }
                cameraController = new CameraController(window, this, new FloatRect(0, 0, 4000, 4000), config);
            }
        }

        public override void Update(float deltaTime)
        {
            var direction = controller.GetDirection(Position, Radius, new List<GameObject>(), deltaTime);
            Position += direction * speed * deltaTime;

            if (!IsEnemy)
            {
                cameraController.Update(deltaTime);
                cameraController.Apply();
            }

            shape.Position = Position;
        }

        public void Grow(float amount)
        {
            Radius += amount / (1 + Radius * 0.01f);
            UpdateShape();
        }

        public void SetRadius(float newRadius)
        {
            Radius = MathF.Max(newRadius, 0);
            UpdateShape();
        }

        private void UpdateShape()
        {
            shape.Radius = Radius;
            shape.Origin = new Vector2f(Radius, Radius);
        }

        public override void Render(RenderWindow window)
        {
            window.Draw(shape);
        }

        public override float GetCollisionRadius() => Radius;

        public Controller GetController() => controller;

        public float GetRadius() => Radius;

        public void SetPos(Vector2f newPosition)
        {
            Position = newPosition;
            shape.Position = newPosition;
        }
    }
}