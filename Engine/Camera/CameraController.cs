﻿using Agar.io_sfml.Game.Scripts.GameObjects;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Engine.Camera
{
    public class CameraController
    {
        private View cameraView;
        private RenderWindow window;
        private Entity player;
        private FloatRect mapBorder;

        public CameraController(RenderWindow window, Entity player, FloatRect mapBorder)
        {
            this.window = window;
            this.player = player;
            this.mapBorder = mapBorder;
            cameraView = new View(window.GetView());
        }

        public void Update()
        {
            cameraView.Center = new Vector2f(player.Position.X, player.Position.Y);

            float cameraHalfWidth = cameraView.Size.X / 2;
            float cameraHalfHeight = cameraView.Size.Y / 2;

            float PosX = Math.Max(mapBorder.Left + cameraHalfWidth, Math.Min(cameraView.Center.X, mapBorder.Left + mapBorder.Width - cameraHalfWidth));
            float PosY = Math.Max(mapBorder.Top + cameraHalfHeight, Math.Min(cameraView.Center.Y, mapBorder.Top + mapBorder.Height - cameraHalfHeight));

            cameraView.Center = new Vector2f(PosX, PosY);

            cameraView.Size = new Vector2f(window.Size.X, window.Size.Y);
        }

        public void Apply()
        {
            if (cameraView != null)
                window.SetView(cameraView);
        }

        public View GetView()
        {
            return cameraView;
        }
    }
}
