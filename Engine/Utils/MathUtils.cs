using SFML.System;

namespace Agar.io_sfml.Utils
{
    public static class MathUtils
    {
        public static float Distance(Vector2f a, Vector2f b)
        {
            return MathF.Sqrt(MathF.Pow(a.X - b.X, 2) + MathF.Pow(a.Y - b.Y, 2));
        }

        public static Vector2f Normalize(Vector2f vector)
        {
            float magnitude = MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            return magnitude > 0 ? vector / magnitude : new Vector2f(0, 0);
        }
    }
}