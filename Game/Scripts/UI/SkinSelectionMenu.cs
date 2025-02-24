using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Game.Scripts.Animation;

namespace Agar.io_sfml.Game.Scripts.UI
{
    public class SkinSelectionMenu
    {
        private RenderWindow _window;
        private TextureManager _textureManager;
        private Config.Config _config;
        private int _selectedSkinIndex = 0;
        private bool _isActive = true;
        private RectangleShape _background;
        private Sprite _previewSprite;
        private Font _font;
        private Text _titleText;
        private Text _selectText;
        private AnimationAtlas _previewAnimation;

        public SkinSelectionMenu(RenderWindow window, TextureManager textureManager, Config.Config config)
        {
            _window = window;
            _textureManager = textureManager;
            _config = config;

            _window.MouseWheelScrolled += OnMouseWheelScrolled;

            _background = new RectangleShape(new Vector2f(window.Size.X, window.Size.Y))
            {
                FillColor = new Color(149, 150, 53, 150)
            };

            _font = _textureManager.LoadFont(_config.FontPath);

            if (_font != null)
            {
                _titleText = new Text("Scroll Your Skin", _font, 30)
                {
                    FillColor = Color.White,
                    Position = new Vector2f(window.Size.X / 2 - 150, 50)
                };
                _selectText = new Text("Click to equip", _font, 30)
                {
                    FillColor = Color.White,
                    Position = new Vector2f(400, 500)
                };
            }

            Texture atlasTexture = _textureManager.LoadTexture(_config.PlayerSkinPaths[1] + "/atlas.png");
            _previewAnimation = new AnimationAtlas(atlasTexture, GenerateFullRegions(), 0.1f);
            _previewSprite = new Sprite(atlasTexture)
            {
                TextureRect = GenerateFullRegions()["Idle"][0],
                Scale = new Vector2f(80f / 800, 80f / 626),
                Position = new Vector2f(_window.Size.X / 2 - 40f, _window.Size.Y / 2 - 40f)
            };
        }

        private Dictionary<string, IntRect[]> GenerateFullRegions()
        {
            return new Dictionary<string, IntRect[]>
            {
                { "Idle", GenerateRegions(0, 0, 800, 626, 1) },
                { "Moving", GenerateRegions(0, 0, 800, 626, 20) }
            };
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

        private void OnMouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            if (_isActive)
            {
                if (e.Delta > 0 && _selectedSkinIndex > 0)
                    _selectedSkinIndex--;
                else if (e.Delta < 0 && _selectedSkinIndex < _config.PlayerSkinCount - 1)
                    _selectedSkinIndex++;

                Texture atlasTexture = _textureManager.LoadTexture(_config.PlayerSkinPaths[_selectedSkinIndex + 1] + "/atlas.png");
                _previewAnimation = new AnimationAtlas(atlasTexture, GenerateFullRegions(), 0.1f);
                _previewSprite.Texture = atlasTexture;
                _previewSprite.TextureRect = GenerateFullRegions()["Idle"][0];
            }
        }

        public bool Update()
        {
            if (!_isActive) return true;

            Vector2i mousePos = Mouse.GetPosition(_window);
            Vector2f mappedPos = _window.MapPixelToCoords(mousePos);

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                FloatRect selectBounds = _selectText.GetGlobalBounds();
                if (selectBounds.Contains(mappedPos.X, mappedPos.Y))
                {
                    _isActive = false;
                    return true;
                }
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && _selectedSkinIndex > 0)
                _selectedSkinIndex--;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && _selectedSkinIndex < _config.PlayerSkinCount - 1)
                _selectedSkinIndex++;

            Texture atlasTexture = _textureManager.LoadTexture(_config.PlayerSkinPaths[_selectedSkinIndex + 1] + "/atlas.png");
            _previewAnimation = new AnimationAtlas(atlasTexture, GenerateFullRegions(), 0.1f);
            _previewSprite.Texture = atlasTexture;
            _previewSprite.TextureRect = GenerateFullRegions()["Idle"][0];

            return false;
        }

        public void Render()
        {
            if (!_isActive) return;

            _window.Draw(_background);
            if (_font != null)
            {
                _window.Draw(_titleText);
                _window.Draw(_selectText);
            }

            _previewAnimation.Draw(_window, _previewSprite.Position, 80f);

            FloatRect selectBounds = _selectText.GetGlobalBounds();
            RectangleShape selectButton = new RectangleShape(new Vector2f(selectBounds.Width + 20, selectBounds.Height + 10))
            {
                Position = new Vector2f(selectBounds.Left - 10, selectBounds.Top - 5),
                FillColor = new Color(0, 128, 0, 200)
            };
            _window.Draw(selectButton);
            _window.Draw(_selectText);
        }

        public Dictionary<string, IntRect[]> GetSelectedSkin()
        {
            return GenerateFullRegions();
        }

        public string GetSelectedSkinPath()
        {
            return _config.PlayerSkinPaths[_selectedSkinIndex + 1] + "/atlas.png";
        }

        public bool IsActive => _isActive;
    }
}