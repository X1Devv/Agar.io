using Agar.io_sfml.Engine.Camera;
using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Game.Scripts.Audio;
using Agar.io_sfml.Game.Scripts.GameObjects;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.GameRule
{
    public class StreakSystem
    {
        private int killStreak;
        private AudioSystem audioSystem;
        private RenderWindow window;
        private Clock streakClock;
        private CameraController cameraController;
        private bool firstBloodPlayed = false;

        public StreakSystem(AudioSystem audioSystem, RenderWindow window, ConfigLoader config, CameraController cameraController)
        {
            this.audioSystem = audioSystem;
            this.cameraController = cameraController;
            this.window = window;
            killStreak = 0;
            streakClock = new Clock();

            //font = new Font("Resources\\Fonts\\arial.ttf");
            //streakText = new Text("", font, 20)
            //{
            //    FillColor = Color.Red,
            //    Position = new Vector2f(10, 10)
            //};
        }

        public void OnKill(Entity killer, Entity victim)
        {
            if (streakClock.ElapsedTime.AsSeconds() > 10)
            {
                killStreak = 0;
                firstBloodPlayed = true;
            }

            killStreak++;
            streakClock.Restart();

            if (killStreak == 1 && !firstBloodPlayed)
            {
                audioSystem.PlayStreakSound("FirstBlood");
                firstBloodPlayed = true;
            }
            else if (killStreak == 2)
                audioSystem.PlayStreakSound("DoubleKill");
            else if (killStreak == 3)
                audioSystem.PlayStreakSound("TripleKill");
            else if (killStreak == 4)
                audioSystem.PlayStreakSound("UltraKill");
            else if (killStreak == 5)
                audioSystem.PlayStreakSound("Rampage");
            else if (killStreak >= 6)
                audioSystem.PlayStreakSound("HolyShit");

            audioSystem.PlayStreakSound("Kill");
            ApplyBloodEffect();
        }

        private void ApplyBloodEffect()
        {
            window.Clear(new Color(255, 0, 0, 50));
        }

        public void Update()
        {
            if (cameraController == null) return;

            Vector2f cameraCenter = cameraController.GetView().Center;
            Vector2f cameraSize = cameraController.GetView().Size;
        }
    }
}