using Agar.io_sfml.Engine.Core;
using Agar.io_sfml.Engine.Interfaces;
using Agar.io_sfml.Game.Scripts.GameObjects;

namespace Agar.io_sfml.Game.Scripts.Abilities
{
    public abstract class Ability : IAbility
    {
        public float Cooldown { get; protected set; }
        public float CurrentCooldown { get; protected set; }

        protected Ability(float cooldown)
        {
            Cooldown = cooldown;
            CurrentCooldown = 0f;
        }

        public virtual bool CanActivate(Entity player, List<GameObject> gameObjects)
        {
            return CurrentCooldown <= 0f;
        }

        public virtual void Activate(Entity player, List<GameObject> gameObjects)
        {
            if (CanActivate(player, gameObjects))
            {
                Execute(player, gameObjects);
                CurrentCooldown = Cooldown;
            }
        }

        public abstract void Execute(Entity player, List<GameObject> gameObjects);

        public void Execute(GameObject gameObject, List<GameObject> gameObjects)
        {
            if (gameObject is Entity player)
            {
                Execute(player, gameObjects);
            }
        }

        public void Update(float deltaTime)
        {
            if (CurrentCooldown > 0f)
            {
                CurrentCooldown -= deltaTime;
            }
        }
    }
}