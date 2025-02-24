using Agar.io_sfml.Game.Scripts.Audio;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.GameRule
{
    public class StreakSystem
    {
        private int _killStreak;
        private SoundManager _soundManager;
        private Clock _streakClock;
        private bool _firstBloodPlayed = false;

        public StreakSystem(SoundManager soundManager, RenderWindow window)
        {
            _soundManager = soundManager;
            _killStreak = 0;
            _streakClock = new Clock();
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
        }

        public void Update() { }

        public void Render() { }
    }
}