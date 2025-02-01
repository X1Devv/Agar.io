using Agar.io_sfml.Game.Scripts.Config;

public class Boot
{
    public static void Main()
    {
        var gameConfig = new GameConfig();
        var gameLoop = gameConfig.CreateGameLoop();
        gameLoop.Run();
    }
}
