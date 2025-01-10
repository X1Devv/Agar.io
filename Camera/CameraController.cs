using SFML.Graphics;
using AgarIO.GameObjects;
using SFML.System;

namespace Agar.io_sfml.Camera
{
    class CameraController
    {
        private View cameraView;
        private RenderWindow window;
        private Player player;
        private FloatRect mapBounds;

        public CameraController(RenderWindow window, Player player, FloatRect mapBounds)
        {
            this.window = window;
            this.player = player;
            this.mapBounds = mapBounds;
            cameraView = new View(window.GetView());
        }

        public void Update()
        {
            cameraView.Center = new Vector2f(player.Position.X, player.Position.Y);

            float cameraHalfWidth = cameraView.Size.X / 2;
            float cameraHalfHeight = cameraView.Size.Y / 2;

            float PosX = Math.Max(mapBounds.Left + cameraHalfWidth, Math.Min(cameraView.Center.X, mapBounds.Left + mapBounds.Width - cameraHalfWidth));
            float PosY = Math.Max(mapBounds.Top + cameraHalfHeight, Math.Min(cameraView.Center.Y, mapBounds.Top + mapBounds.Height - cameraHalfHeight));

            cameraView.Center = new Vector2f(PosX, PosY);

            cameraView.Size = new Vector2f(window.Size.X, window.Size.Y);
        }

        public void Apply()
        {
            window.SetView(cameraView);
        }
    }
}
