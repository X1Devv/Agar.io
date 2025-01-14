using SFML.Graphics;

namespace Agar.io_sfml.Factory
{
    public class FoodConfig
    {
        public int Probability { get; }
        public float Size { get; }
        public Color Color { get; }
        public int GrowthBonus { get; }

        public FoodConfig(int probability, float size, Color color, int growthBonus)
        {
            Probability = probability;
            Size = size;
            Color = color;
            GrowthBonus = growthBonus;
        }
    }
}
