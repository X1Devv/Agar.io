﻿using Agar.io_sfml.Engine.Interfaces;
using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Utils;
using SFML.System;
using Agar.io_sfml.Engine.Core;

namespace Agar.io_sfml.Game.Scripts.Abilities
{
    public class SwapAbility : IAbility
    {
        public void Execute(Entity player, List<GameObject> gameObjects)
        {
            Entity nearestEnemy = null;
            float shortestDistance = float.MaxValue;

            foreach (var obj in gameObjects)
            {
                if (obj is Entity enemy && enemy.IsEnemy)
                {
                    float distance = MathUtils.Distance(player.Position, enemy.Position);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestEnemy = enemy;
                    }
                }
            }

            if (nearestEnemy != null)
            {
                Vector2f temp = player.Position;
                player.Position = nearestEnemy.Position;
                nearestEnemy.Position = temp;
            }
        }

        public void Execute(GameObject gameObject, List<GameObject> gameObjects)
        {
            if (gameObject is Entity player)
            {
                Execute(player, gameObjects);
            }
        }
    }
}
