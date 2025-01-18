using SFML.Graphics;

namespace Agar.io_sfml.Engine.Utils
{
    public class TextureManager
    {
        private string projectRoot;
        private Dictionary<string, Texture> textures = new();

        public TextureManager()
        {
            projectRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");
        }

        public Texture LoadTexture(string relativePath)
        {
            if (!textures.TryGetValue(relativePath, out Texture texture))
            {
                string fullPath = Path.Combine(projectRoot, relativePath);

                texture = new Texture(fullPath);
                textures[relativePath] = texture;
            }

            return texture;
        }

        public void PreloadAllTextures(string relativeDirectory)
        {
            string fullDirectoryPath = Path.Combine(projectRoot, relativeDirectory);

            foreach (string file in Directory.GetFiles(fullDirectoryPath, "*.png", SearchOption.AllDirectories))
            {
                string relativePath = Path.GetRelativePath(projectRoot, file);
                LoadTexture(relativePath);
            }
        }
    }
}
