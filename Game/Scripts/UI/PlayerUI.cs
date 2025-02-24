using Agar.io_sfml.Engine.Camera;
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
            Vector2f screenMousePosition = window.MapPixelToCoords(mousePosition, window.DefaultView);

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (pauseButton.GetGlobalBounds().Contains(screenMousePosition.X, screenMousePosition.Y))
                {
                    onPauseClick?.Invoke();
                    return;
                }

                foreach (var (button, onClick) in abilityButtons)
                {
                    if (button.GetGlobalBounds().Contains(screenMousePosition.X, screenMousePosition.Y))
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
            var defaultView = window.DefaultView;
            window.SetView(defaultView);
            foreach (var (button, _) in abilityButtons)
            {
                window.Draw(button);
            }
            window.Draw(pauseButton);
            window.SetView(cameraController.GetView());
        }

        private void UpdateButtonPositions()
        {
            for (int i = 0; i < abilityButtons.Count; i++)
            {
                abilityButtons[i].button.Position = new Vector2f(30, window.Size.Y - 70 * (abilityButtons.Count - i));
            }
        }

        private void UpdatePauseButtonPosition()
        {
            pauseButton.Position = new Vector2f(window.Size.X - 50, 50);
        }
    }
}