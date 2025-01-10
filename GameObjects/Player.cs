using Agar.io_sfml.Camera;
using Agar.io_sfml.Input;
using SFML.Graphics;
using SFML.System;

namespace AgarIO.GameObjects
{
    public class Player : GameObject
    {
        private Input inputHandler;
        private CircleShape shape;
        private float speed = 100f;

        public Vector2f Position { get; set; }
        public RenderWindow Window { get; private set; }
        public float Radius { get; private set; }

        public Player(Input inputHandler, Vector2f startPosition, RenderWindow window)
        {
            this.inputHandler = inputHandler;
            Position = startPosition;
            Window = window;
            shape = new CircleShape(20)
            {
                FillColor = Color.Blue,
                Origin = new Vector2f(20, 20)
            };
        }

        public void Grow(float amount)
        {
            shape.Radius += amount;
            shape.Radius = shape.Radius;
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
