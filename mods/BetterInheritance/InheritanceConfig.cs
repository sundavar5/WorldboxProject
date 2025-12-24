using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NeoModLoader.api;
using UnityEngine;

namespace BetterInheritance
{
    public static class InheritanceConfig
    {
        private static string ConfigPath => Path.Combine(InheritanceMod.ModPath, "inheritance_config.json");
        public static Dictionary<string, int> TraitRates = new Dictionary<string, int>();

        public static void Load()
        {
            if (File.Exists(ConfigPath))
            {
                try
                {
                    string json = File.ReadAllText(ConfigPath);
                    TraitRates = JsonConvert.DeserializeObject<Dictionary<string, int>>(json) ?? new Dictionary<string, int>();
                }
                catch (Exception e)
                {
                    Debug.LogError("BetterInheritance: Failed to load config. " + e.Message);
                }
            }

            // Ensure all traits are in the config with a default value if missing
            foreach (var trait in AssetManager.traits.list)
            {
                if (!TraitRates.ContainsKey(trait.id))
                {
                    // Default to rate_inherit if it's > 0, otherwise 0?
                    // Or maybe a flat default like 20% if it's inheritable?
                    // The game uses rate_inherit as a weight.
                    // Let's use rate_inherit directly if it's <= 100, otherwise 50.
                    // This is a rough heuristic.
                    int defaultRate = 0;
                    if (trait.rate_inherit > 0)
                    {
                        defaultRate = trait.rate_inherit <= 100 ? trait.rate_inherit : 50;
                    }
                    TraitRates[trait.id] = defaultRate;
                }
            }
        }

        public static void Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(TraitRates, Formatting.Indented);
                File.WriteAllText(ConfigPath, json);
            }
            catch (Exception e)
            {
                Debug.LogError("BetterInheritance: Failed to save config. " + e.Message);
            }
        }

        public static int GetRate(string traitId)
        {
            if (TraitRates.TryGetValue(traitId, out int rate))
            {
                return rate;
            }
            return 0;
        }

        public static void SetRate(string traitId, int rate)
        {
            TraitRates[traitId] = Mathf.Clamp(rate, 0, 100);
        }
    }
}
