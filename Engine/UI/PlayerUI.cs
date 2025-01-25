using Agar.io_sfml.Engine.Utils;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agar.io_sfml.Engine.UI
{
    public class PlayerUI
    {
        private List<(Sprite button, Action onClick)> abilityButtons = new();
        private TextureManager textureManager;
        private RenderWindow window;

        public PlayerUI(RenderWindow window, TextureManager textureManager)
        {
            this.window = window;
            this.textureManager = textureManager;
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

        public void Update()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2i mousePosition = Mouse.GetPosition(window);
                foreach (var (button, onClick) in abilityButtons)
                {
                    if (button.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y))
                    {
                        onClick?.Invoke();
                        break;
                    }
                }
            }
        }

        public void Render()
        {
            foreach (var (button, _) in abilityButtons)
            {
                window.Draw(button);
            }
        }

        private void UpdateButtonPositions()
        {
            Vector2f basePosition = new Vector2f(10, window.Size.Y - 160);
            
            for (int i = 0; i < abilityButtons.Count; i++)
                abilityButtons[i].button.Position = basePosition + new Vector2f(0, -i * 60);
        }
    }
}
