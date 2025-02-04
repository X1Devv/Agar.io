using Agar.io_sfml.Game.Scripts.GameRule;

public class Boot
{
    public static void Main()
    {
        var gameInit = new GameInit();
        var gameLoop = gameInit.CreateGameLoop();
        gameLoop.Run();
    }
}
