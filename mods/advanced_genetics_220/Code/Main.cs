using System;
using System.Collections.Generic;
using UnityEngine;
using NeoModLoader.api;
using HarmonyLib;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace TraitInheritanceMod
{
    public class Main : IMod
    {
        public static GameObject obj;
        public static string modPath;
        public static Dictionary<string, float> inheritanceRates = new Dictionary<string, float>();
        public static bool isInitialized = false;

        public void onLoad(ModDeclare pMod)
        {
            modPath = pMod.FolderPath;
            obj = new GameObject("TraitInheritanceMod");
            obj.AddComponent<TraitInheritanceUI>();

            LoadConfig();

            var harmony = new Harmony(pMod.UID);
            harmony.PatchAll();

            isInitialized = true;
            Debug.Log("Trait Inheritance Mod Loaded!");
        }

        public static void SaveConfig()
        {
            string configPath = Path.Combine(modPath, "inheritance_rates.json");
            string json = JsonConvert.SerializeObject(inheritanceRates, Formatting.Indented);
            File.WriteAllText(configPath, json);
        }

        public static void LoadConfig()
        {
            string configPath = Path.Combine(modPath, "inheritance_rates.json");
            if (File.Exists(configPath))
            {
                try
                {
                    string json = File.ReadAllText(configPath);
                    inheritanceRates = JsonConvert.DeserializeObject<Dictionary<string, float>>(json);
                }
                catch (Exception e)
                {
                    Debug.LogError("Failed to load inheritance rates: " + e.Message);
                }
            }
        }

        public static float GetInheritanceRate(string traitId)
        {
            if (inheritanceRates.ContainsKey(traitId))
            {
                return inheritanceRates[traitId];
            }
            return -1f; // Not custom
        }

        public void onUnload()
        {
            isInitialized = false;
        }
    }

    [HarmonyPatch(typeof(BabyHelper), "traitsInherit")]
    public static class BabyHelper_traitsInherit_Patch
    {
        public static bool Prefix(Actor pActorTarget, Actor pParent1, Actor pParent2)
        {
            if (!Main.isInitialized) return true;

            // 1. Handle Custom Traits (Deterministic check based on percentage)
            HashSet<string> uniqueTraits = new HashSet<string>();
            if (pParent1 != null && pParent1.data.traits != null)
                foreach(var t in pParent1.data.traits) uniqueTraits.Add(t);
            if (pParent2 != null && pParent2.data.traits != null)
                foreach(var t in pParent2.data.traits) uniqueTraits.Add(t);

            foreach (string traitId in uniqueTraits)
            {
                float customRate = Main.GetInheritanceRate(traitId);
                if (customRate >= 0)
                {
                    // It's a custom trait. Roll for it.
                    if (UnityEngine.Random.Range(0f, 100f) < customRate)
                    {
                        pActorTarget.addTrait(traitId, false);
                    }
                }
            }

            // 2. Handle Default Traits (Replicate original weighted lottery)
            // Use ListPool<ActorTrait> as original code does to be consistent/performant.
            using (ListPool<ActorTrait> tPossibleTraits = new ListPool<ActorTrait>(128))
            {
                int tTotalParentTraits = 0;
                int tTotalParentTraits2 = 0;

                AddTraitsFromParentToDefaultPool(pParent1, tPossibleTraits, out tTotalParentTraits);
                if (pParent2 != null)
                {
                    AddTraitsFromParentToDefaultPool(pParent2, tPossibleTraits, out tTotalParentTraits2);
                }

                if (tPossibleTraits.Count != 0)
                {
                    // Calculate number of traits to inherit.
                    // Note: tTotalParentTraits counts all valid default traits (unique per parent).
                    int tTotalParentTraits3 = (int)((float)(tTotalParentTraits + tTotalParentTraits2) * 0.25f);
                    tTotalParentTraits3 = Mathf.Max(1, tTotalParentTraits3);

                    for (int i = 0; i < tTotalParentTraits3; i++)
                    {
                        ActorTrait tTrait = tPossibleTraits.GetRandom<ActorTrait>();
                        // Avoid adding if we already added it (e.g. from Custom logic? No, custom logic traits are excluded from pool).
                        // Avoid adding duplicates if the lottery picks the same one twice?
                        // pActorTarget.addTrait handles duplicate checks internally (returns false if exists).
                        pActorTarget.addTrait(tTrait.id, false);
                    }
                }
            }

            return false; // Skip original method
        }

        private static void AddTraitsFromParentToDefaultPool(Actor pActor, ListPool<ActorTrait> pList, out int pCounter)
        {
            int tResultCounter = 0;
            foreach (ActorTrait tTrait in pActor.getTraits())
            {
                // If it's a custom trait, skip it here. It's handled separately.
                if (Main.GetInheritanceRate(tTrait.id) >= 0) continue;

                if (tTrait.rate_inherit != 0 || tTrait.rate_birth != 0)
                {
                    tResultCounter++;
                    pList.AddTimes(tTrait.rate_birth, tTrait);
                    pList.AddTimes(tTrait.rate_inherit, tTrait);
                }
            }
            pCounter = tResultCounter;
        }
    }
}
