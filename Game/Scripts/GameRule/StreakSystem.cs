using Agar.io_sfml.Game.Scripts.Audio;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.GameRule
{
    public class StreakSystem
    {
        private int _killStreak;
        private SoundManager _soundManager;
        private RenderWindow _window;
        private Clock _streakClock;
        private bool _firstBloodPlayed = false;
        private RectangleShape _bloodEffect;
        private float _bloodEffectAlpha = 0f;

        public StreakSystem(SoundManager soundManager, RenderWindow window)
        {
            _soundManager = soundManager;
            _window = window;
            _killStreak = 0;
            _streakClock = new Clock();
            _bloodEffect = new RectangleShape(new Vector2f(5000, 5000))
            {
                FillColor = new Color(255, 0, 0, 0)
            };
        }

        public void OnKill()
        {
            if (_streakClock.ElapsedTime.AsSeconds() > 10)
            {
                _killStreak = 0;
                _firstBloodPlayed = true;
            }

            _killStreak++;
            _streakClock.Restart();

            string soundKey = _killStreak switch
            {
                1 when !_firstBloodPlayed => "FirstBlood",
                2 => "DoubleKill",
                3 => "TripleKill",
                4 => "UltraKill",
                5 => "Rampage",
                _ => _killStreak > 5 ? "HolyShit" : ""
            };

            if (!string.IsNullOrEmpty(soundKey))
            {
                _soundManager.PlaySound(soundKey);
                if (soundKey == "FirstBlood") _firstBloodPlayed = true;
            }

            _soundManager.PlaySound("Kill");
            _bloodEffectAlpha = 100f;
        }

        public void Update()
        {
            if (_bloodEffectAlpha > 0)
            {
                _bloodEffectAlpha -= 1f;
                _bloodEffect.FillColor = new Color(255, 0, 0, (byte)_bloodEffectAlpha);
            }
        }

        public void Render()
        {
            if (_bloodEffectAlpha > 0)
                _window.Draw(_bloodEffect);
        }
    }
}