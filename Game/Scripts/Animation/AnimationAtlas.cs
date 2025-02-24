using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.Animation
{
    public class AnimationAtlas
    {
        private readonly Dictionary<string, IntRect[]> _regions;
        private readonly Texture _atlasTexture;
        private string _currentAnimation;
        public int _currentFrame;
        private float _frameTime;
        private float _elapsedTime;
        private bool _isPlaying;

        private readonly float _frameWidth = 800f;
        private readonly float _frameHeight = 626f;

        public AnimationAtlas(Texture atlasTexture, Dictionary<string, IntRect[]> regions, float frameDuration = 0.1f)
        {
            _atlasTexture = atlasTexture;
            _regions = regions;
            _frameTime = frameDuration;
            _elapsedTime = 0f;
            _isPlaying = true;
            _currentAnimation = "Idle";
            _currentFrame = 0;
        }

        public void Play(string animationKey)
        {
            if (_currentAnimation != animationKey)
            {
                _currentAnimation = animationKey;
                _currentFrame = 0;
                _elapsedTime = 0f;
            }
            _isPlaying = true;
        }

        public void Update(float deltaTime, Vector2f velocity)
        {
            if (!_isPlaying) return;

            string newAnimation = velocity.X != 0 || velocity.Y != 0 ? "Moving" : "Idle";

            if (_currentAnimation != newAnimation)
            {
                Play(newAnimation);
            }

            _elapsedTime += deltaTime;

            if (_elapsedTime >= _frameTime)
            {
                _currentFrame = (_currentFrame + 1) % _regions[_currentAnimation].Length;
                _elapsedTime = 0f;
            }
        }

        public void Draw(RenderTarget target, Vector2f position, float entityRadius)
        {
            float scale = entityRadius / (_frameWidth / 2);
            Sprite sprite = new Sprite(_atlasTexture) { TextureRect = _regions[_currentAnimation][_currentFrame], Position = position, Origin = new Vector2f(_frameWidth / 2, _frameHeight / 2), Scale = new Vector2f(scale, scale) };

            CircleShape mask = new CircleShape(entityRadius) { Position = position - new Vector2f(entityRadius, entityRadius), FillColor = Color.White };

            RenderStates spriteStates = new RenderStates(BlendMode.Alpha);
            RenderStates maskStates = new RenderStates(BlendMode.Multiply);

            target.Draw(sprite, spriteStates);
            target.Draw(mask, maskStates);
        }
    }
}