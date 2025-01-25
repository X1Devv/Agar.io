using Agar.io_sfml.Game.Scripts.GameRule;
using SFML.Graphics;

namespace Agar.io_sfml.Game
{
    public class GameLoop
    {
        private RenderWindow window;
        private GameController gameController;

        public GameLoop(GameController gameController, RenderWindow window)
        {
            this.gameController = gameController;
            this.window = window;
            window.SetFramerateLimit(180);
            window.Closed += WindowClosed;
        }

        public void Run()
        {
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(new Color(46, 47, 48));
                gameController.Update(window);
                gameController.Render(window);
                window.Display();
            }
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}