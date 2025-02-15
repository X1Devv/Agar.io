﻿using Agar.io_sfml.Engine.Camera;
using Agar.io_sfml.Engine.Utils;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agar.io_sfml.Game.Scripts.UI
{
    public class PlayerUI
    {
        private List<(Sprite button, Action onClick)> abilityButtons = new();
        private Sprite pauseButton;
        private Action onPauseClick;
        private TextureManager textureManager;
        private RenderWindow window;
        private CameraController cameraController;

        public PlayerUI(RenderWindow window, TextureManager textureManager, CameraController cameraController)
        {
            this.window = window;
            this.textureManager = textureManager;
            this.cameraController = cameraController;
        }

        public void AddAbility(string texturePath, Action onClick)
        {
            Texture buttonTexture = textureManager.LoadTexture(texturePath);
            Sprite button = new Sprite(buttonTexture)
            {
                Scale = new Vector2f(0.2f, 0.2f)
            };

            abilityButtons.Add((button, onClick));
            UpdateButtonPositions();
        }

        public void AddPauseButton(string texturePath, Action onClick)
        {
            Texture pauseTexture = textureManager.LoadTexture(texturePath);
            pauseButton = new Sprite(pauseTexture) { Scale = new Vector2f(0.1f, 0.1f) };
            onPauseClick = onClick;
            UpdatePauseButtonPosition();
        }

        public void Update()
        {
            Vector2i mousePosition = Mouse.GetPosition(window);
            Vector2f worldMousePosition = window.MapPixelToCoords(mousePosition);

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (pauseButton.GetGlobalBounds().Contains(worldMousePosition.X, worldMousePosition.Y))
                {
                    onPauseClick?.Invoke();
                    return;
                }

                foreach (var (button, onClick) in abilityButtons)
                {
                    if (button.GetGlobalBounds().Contains(worldMousePosition.X, worldMousePosition.Y))
                    {
                        onClick?.Invoke();
                        break;
                    }
                }
            }

            UpdateButtonPositions();
            UpdatePauseButtonPosition();
        }


        public void Render()
        {
            foreach (var (button, _) in abilityButtons)
            {
                window.Draw(button);
            }

            window.Draw(pauseButton);
        }

        private void UpdateButtonPositions()
        {
            Vector2f cameraPosition = cameraController.GetView().Center;

            for (int i = 0; i < abilityButtons.Count; i++)
            {
                float offsetX = -window.Size.X / 2 + 30;
                float offsetY = -window.Size.Y / 2 + 580 + i * 70;
                Vector2f buttonPosition = new Vector2f(cameraPosition.X + offsetX, cameraPosition.Y + offsetY);
                abilityButtons[i].button.Position = buttonPosition;
            }
        }

        private void UpdatePauseButtonPosition()
        {
            Vector2f cameraPosition = cameraController.GetView().Center;
            float offsetX = window.Size.X / 2 - 50;
            float offsetY = -window.Size.Y / 2 + 50;
            pauseButton.Position = new Vector2f(
                cameraPosition.X + offsetX,
                cameraPosition.Y + offsetY
            );
        }
    }
}