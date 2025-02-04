using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Game.Scripts.GameObjects;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agar.io_sfml.Engine.Camera
{
    public class CameraController
    {
        private View _cameraView;
        private RenderWindow _window;

        private Entity _player;
        private FloatRect _mapBorder;

        private float _cameraSmoothness;

        public float StartZoom { get; private set; }
        public float MinZoom { get; }
        public float MaxZoom { get; }

        public CameraController(RenderWindow window, Entity player, FloatRect mapBorder, ConfigLoader config)
        {
            _window = window;
            _player = player;
            _mapBorder = mapBorder;
            _cameraSmoothness = config.CameraSmoothness;
            MinZoom = config.CameraMinZoom;
            MaxZoom = config.CameraMaxZoom;
            StartZoom = config.StartZoom;

            _cameraView = new View(_window.GetView());
            UpdateViewSize();

            _window.MouseWheelScrolled += OnMouseWheelScrolled;
        }

        private void OnMouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            if (e.Delta > 0) AdjustZoom(-50);
            else AdjustZoom(50);
        }

        public void AdjustZoom(float delta)
        {
            StartZoom = Math.Clamp(StartZoom + delta, MinZoom, MaxZoom);
            UpdateViewSize();
        }

        public View GetView() => _cameraView;

        public void Update(float deltaTime)
        {
            Vector2f targetPosition = _player.Position;
            Vector2f currentCenter = _cameraView.Center;
            Vector2f newCenter = currentCenter + (targetPosition - currentCenter) * _cameraSmoothness * deltaTime;

            float halfWidth = _cameraView.Size.X / 2;
            float halfHeight = _cameraView.Size.Y / 2;
            newCenter.X = Math.Clamp(newCenter.X, _mapBorder.Left + halfWidth, _mapBorder.Left + _mapBorder.Width - halfWidth);
            newCenter.Y = Math.Clamp(newCenter.Y, _mapBorder.Top + halfHeight, _mapBorder.Top + _mapBorder.Height - halfHeight);

            _cameraView.Center = newCenter;
        }

        public void Apply() => _window.SetView(_cameraView);

        private void UpdateViewSize()
        {
            float aspectRatio = (float)_window.Size.X / _window.Size.Y;
            _cameraView.Size = new Vector2f(StartZoom * aspectRatio, StartZoom);
        }
    }
}