using Agar.io_sfml.Engine.Core;
using Agar.io_sfml.Game.Scripts.GameObjects;

namespace Agar.io_sfml.Game.Scripts.Abilities
{
    public class AbilitySystem
    {
        private List<Ability> abilities = new List<Ability>();

        public void AddAbility(Ability ability)
        {
            abilities.Add(ability);
        }

        public void Update(float deltaTime)
        {
            foreach (var ability in abilities)
            {
                ability.Update(deltaTime);
            }
        }

        public void ActivateAbility(Entity player, List<GameObject> gameObjects, int abilityIndex)
        {
            if (abilityIndex >= 0 && abilityIndex < abilities.Count)
            {
                abilities[abilityIndex].Activate(player, gameObjects);
            }
        }
    }
}