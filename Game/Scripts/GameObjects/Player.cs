using Agar.io_sfml.Engine.Camera;
using Agar.io_sfml.Engine.Interfaces;
using Agar.io_sfml.Engine.Utils;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.GameObjects
{
    public class Player : Character
    {
        private IInput input;

        private TextureManager textureManager;
        private Texture playerTexture;
        
        private CameraController cameraController;

        public Player(IInput input, Vector2f initialPosition, float initialRadius, RenderWindow window) : base(initialPosition, initialRadius, 200f, Color.Blue)
        {
            this.input = input;
            textureManager = new TextureManager();
            playerTexture = textureManager.LoadTexture("Game\\Textures\\Skin\\PlayerSkin.png");

            shape.Texture = playerTexture;

            cameraController = new CameraController(window, this, new FloatRect(0, 0, 4000, 4000));
        }


        public override void Update(float deltaTime)
        {
            Position += input.GetDirection(Position, Radius, null) * speed * deltaTime;
            shape.Position = Position;

            cameraController.Update();
            cameraController.Apply();
        }

        public void PerformAbility(List<GameObject> gameObjects)
        {
            input.PerformAbility(this, gameObjects);
        }

        public void SetPos(Vector2f newPosition)
        {
            Position = newPosition;
            shape.Position = newPosition;
        }

        public float GetRadius() => Radius;
    }
}
