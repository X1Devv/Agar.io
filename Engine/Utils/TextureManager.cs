using SFML.Graphics;

namespace Agar.io_sfml.Engine.Utils
{
    public class TextureManager
    {
        private string projectRoot;
        private Dictionary<string, Texture> textures = new();
        private Dictionary<string, IntRect[]> textureRegions = new();
        private Dictionary<string, Font> fonts = new();

        /// <summary>
        /// Initializes a new instance of the TextureManager class
        /// </summary>
        public TextureManager()
        {
            projectRoot = FindProjectRoot();
        }

        private string FindProjectRoot()
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string targetDir = "Agar.io_sfml";

            while (!string.IsNullOrEmpty(currentDir))
            {
                if (currentDir.EndsWith(targetDir + Path.DirectorySeparatorChar) || currentDir.EndsWith(targetDir))
                {
                    return currentDir;
                }
                currentDir = Path.GetFullPath(Path.Combine(currentDir, ".."));
            }

            return null;
        }

        /// <summary>
        /// Loads a texture from a file
        /// </summary>
        public Texture LoadTexture(string relativePath)
        {
            if (!textures.TryGetValue(relativePath, out Texture texture))
            {
                string fullPath = Path.Combine(projectRoot, relativePath);
                if (File.Exists(fullPath))
                {
                    texture = new Texture(fullPath);
                    textures[relativePath] = texture;
                }
                else
                {
                    return null;
                }
            }
            return texture;
        }

        /// <summary>
        /// Loads a font from a file
        /// </summary>
        public Font LoadFont(string relativePath)
        {
            if (!fonts.TryGetValue(relativePath, out Font font))
            {
                string fullPath = Path.Combine(projectRoot, relativePath);
                if (File.Exists(fullPath))
                {
                    font = new Font(fullPath);
                    fonts[relativePath] = font;
                }
                else
                {
                    return null;
                }
            }
            return font;
        }

        /// <summary>
        /// Loads an atlas texture with regions
        /// </summary>
        public void LoadAtlas(string atlasPath, Dictionary<string, IntRect[]> regions)
        {
            Texture atlasTexture = LoadTexture(atlasPath);
            if (atlasTexture != null)
            {
                textureRegions = regions;
            }
        }

        /// <summary>
        /// Gets a texture region by animation key and frame index
        /// </summary>
        public IntRect GetTextureRegion(string animationKey, int frameIndex)
        {
            if (textureRegions.TryGetValue(animationKey, out IntRect[] regions))
            {
                if (frameIndex >= 0 && frameIndex < regions.Length)
                {
                    return regions[frameIndex];
                }
            }
            return new IntRect(0, 0, 800, 626);
        }
    }
}
