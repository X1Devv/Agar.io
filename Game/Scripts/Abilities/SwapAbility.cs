using Agar.io_sfml.Engine.Interfaces;
using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Utils;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.Abilities
{
    public class SwapAbility : IAbility
    {
        public void Execute(Character character, List<GameObject> gameObjects)
        {
            Enemy nearestEnemy = null;
            float shortestDistance = float.MaxValue;

            foreach (var obj in gameObjects)
            {
                if (obj is Enemy enemy)
                {
                    float distance = MathUtils.Distance(character.Position, enemy.Position);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestEnemy = enemy;
                    }
                }
            }

            if (nearestEnemy != null)
            {
                Vector2f temp = character.Position;
                character.Position = nearestEnemy.Position;
                nearestEnemy.Position = temp;
            }
        }
    }
}