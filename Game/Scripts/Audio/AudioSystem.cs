using SFML.Audio;
using static Agar.io_sfml.Game.Scripts.Config.Config;

namespace Agar.io_sfml.Game.Scripts.Audio
{
    public class AudioSystem
    {
        private Music backgroundMusic;
        private Dictionary<string, Sound> streakSounds;

        public AudioSystem(AudioConfig audioConfig)
        {
            streakSounds = new Dictionary<string, Sound>();
        }

        public void LoadBackgroundMusic(string path)
        {
            backgroundMusic = new Music(path);
            backgroundMusic.Loop = true;
        }

        public void PlayBackgroundMusic()
        {
            backgroundMusic?.Play();
        }

        public void StopBackgroundMusic()
        {
            backgroundMusic.Stop();
        }

        public void LoadStreakSound(string key, string path)
        {
            SoundBuffer buffer = new SoundBuffer(path);
            Sound sound = new Sound(buffer);
            streakSounds[key] = sound;
        }

        public void PlayStreakSound(string key)
        {
            if (streakSounds.ContainsKey(key))
            {
                streakSounds[key].Play();
            }
        }
    }
}