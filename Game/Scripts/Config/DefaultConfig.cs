namespace Agar.io_sfml.Game.Scripts.Config
{
    public static class DefaultConfig
    {
        public static int EnemyCount = 30;
        public static float FoodSpawnInterval = 0.05f;
        public static float PlayerStartSpeed = 200;
        public static float PlayerStartX = 300;
        public static float PlayerStartY = 300;
        public static float PlayerStartSize = 40;
        public static float MinPlayerRadius = 20;
        public static float SwapAbilityCooldown = 1.0f;
        public static float EnemyMinSize = 20;
        public static float EnemyMaxSize = 20;
        public static float EnemyBaseSpeed = 1;
        public static float StartZoom = 800;
        public static float CameraMinZoom = 50;
        public static float CameraMaxZoom = 2000;
        public static float CameraSmoothness = 1;
        public static float MapLeft = 0;
        public static float MapTop = 0;
        public static float MapWidth = 4000;
        public static float MapHeight = 4000;
        public static string SwapAbilityButtonPath = "Resources/Textures/AbilityButton/SwapButton.png";
        public static string PauseButtonPath = "Resources\\Textures\\AbilityButton\\PauseButton.png";
        public static string BackgroundMusicPath = "Resources/Audio/music.wav";
        public static string KillSoundPath = "Resources/Audio/StreakAlert/Kill.wav";
        public static string FirstBloodSoundPath = "Resources/Audio/StreakAlert/FirstBlood.wav";
        public static string DoubleKillSoundPath = "Resources/Audio/StreakAlert/DoubleKill.wav";
        public static string TripleKillSoundPath = "Resources/Audio/StreakAlert/TripleKill.wav";
        public static string UltraKillSoundPath = "Resources/Audio/StreakAlert/UltraKill.wav";
        public static string RampageSoundPath = "Resources/Audio/StreakAlert/Rampage.wav";
        public static string HolyShitSoundPath = "Resources/Audio/StreakAlert/HolyShit.wav";
        public static string FontPath = "Resources/Fonts/arial.ttf";
    }
}