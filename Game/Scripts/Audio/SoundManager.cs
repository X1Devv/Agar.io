using SFML.Audio;

namespace Agar.io_sfml.Game.Scripts.Audio
{
    public class SoundManager
    {
        private Dictionary<string, Sound> _sounds = new Dictionary<string, Sound>();
        private Music _backgroundMusic;

        public float MusicVolume { get; private set; } = 20f;
        public float SoundVolume { get; private set; } = 30f;

        public void LoadSound(string key, string path)
        {
            if (_sounds.ContainsKey(key))
            {
                return;
            }

            SoundBuffer buffer = new SoundBuffer(path);
            Sound sound = new Sound(buffer) { Volume = SoundVolume };
            _sounds[key] = sound;
        }

        public void PlaySound(string key)
        {
            if (_sounds.TryGetValue(key, out Sound sound))
                sound.Play();
        }

        public void StopSound(string key)
        {
            if (_sounds.TryGetValue(key, out Sound sound))
                sound.Stop();
        }

        public void LoadBackgroundMusic(string path)
        {
            _backgroundMusic = new Music(path) { Loop = true, Volume = MusicVolume };
        }

        public void PlayBackgroundMusic() => _backgroundMusic?.Play();

        public void StopBackgroundMusic() => _backgroundMusic?.Stop();

        public void Dispose()
        {
            foreach (var sound in _sounds.Values)
            {
                sound.Dispose();
            }
            _backgroundMusic?.Dispose();
        }
    }
}