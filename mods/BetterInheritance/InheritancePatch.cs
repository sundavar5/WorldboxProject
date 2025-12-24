using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace BetterInheritance
{
    [HarmonyPatch(typeof(BabyHelper), "traitsInherit")]
    public static class TraitsInheritPatch
    {
        public static bool Prefix(Actor pActorTarget, Actor pParent1, Actor pParent2)
        {
            // Collect all unique traits from parents that are inheritable
            HashSet<string> parentTraits = new HashSet<string>();

            // Parent 1
            if (pParent1 != null)
            {
                foreach (ActorTrait trait in pParent1.getTraits())
                {
                    parentTraits.Add(trait.id);
                }
            }

            // Parent 2
            if (pParent2 != null)
            {
                foreach (ActorTrait trait in pParent2.getTraits())
                {
                    parentTraits.Add(trait.id);
                }
            }

            // Iterate and check inheritance
            foreach (string traitId in parentTraits)
            {
                ActorTrait trait = AssetManager.traits.get(traitId);
                if (trait == null) continue;

                // Skip if trait is not inheritable by default mechanism (unless we force it via config?)
                // The user wants to change inheritance rates.
                // We should check the config.

                int chance = InheritanceConfig.GetRate(traitId);

                // Roll the dice
                if (UnityEngine.Random.Range(0, 100) < chance)
                {
                    // Ensure the child doesn't already have it (though addTrait usually handles this)
                    pActorTarget.addTrait(traitId, false);
                }
            }

            // Return false to skip the original method
            return false;
        }
    }
}
