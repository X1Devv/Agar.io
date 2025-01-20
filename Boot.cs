using Agar.io_sfml.Game;
public class Boot
{
    public static void Main()
    {
        GameConfig config = new GameConfig();
        GameLoop gameLoop = config.CreateGameLoop();
        gameLoop.Run();
    }
}
