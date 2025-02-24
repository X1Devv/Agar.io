using SFML.Graphics;

namespace Agar.io_sfml.Engine.Interfaces
{
    public interface IGameController
    {
        void Update(RenderWindow window);
        void Render(RenderWindow window);
    }
}
