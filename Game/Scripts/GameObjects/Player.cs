using Agar.io_sfml.Engine.Camera;
using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Game.Scripts.Input;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.GameObjects
{
    public class Player : GameObject
    {
        private PlayerInput inputHandler;
        private CircleShape shape;
        TextureManager textureManager = new TextureManager();
        private Texture playerTexture;
        private float speed = 200f;

        public Vector2f Position { get; set; }
        public RenderWindow Window { get; private set; }
        public float Radius { get; private set; }

        public Player(PlayerInput inputHandler, Vector2f startPosition, RenderWindow window)
        {
            this.inputHandler = inputHandler;
            Position = startPosition;
            Window = window;
            playerTexture = textureManager.LoadTexture("Game\\Textures\\Skin\\PlayerSkin.png");

            shape = new CircleShape(25)
            {
                Texture = playerTexture,
                Origin = new Vector2f(25, 25)
            };
        }


        public void Grow(float amount)
        {
            shape.Radius += amount;
            shape.Origin = new Vector2f(shape.Radius, shape.Radius);
        }

        public void SetRadius(float Radius)
        {
            this.Radius = Radius;
            shape.Radius = Radius;
            shape.Origin = new Vector2f(Radius, Radius);
        }

        public override void Update(float deltaTime)
        {
            Vector2f movement = inputHandler.GetMovement();
            Position += movement * speed * deltaTime;
            shape.Position = Position;

            CameraController cameraController = new CameraController(Window, this, new FloatRect(0, 0, 4000, 4000));
            cameraController.Update();
            cameraController.Apply();
        }

        public override void Render(RenderWindow window)
        {
            window.Draw(shape);
        }

        public float GetRadius() => shape.Radius;
    }
}
