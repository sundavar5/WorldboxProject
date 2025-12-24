using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;
using NeoModLoader.General;

namespace FinalRun;

internal static class InheritanceConfigManager
{
    private const string ConfigFileName = "inheritance_config.json";
    private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
    {
        Formatting = Formatting.Indented
    };

    private static readonly Dictionary<string, int> OriginalInheritValues = new();
    private static Dictionary<string, int> _configuredRates = new();

    private static string ConfigPath => Path.Combine(ResolveConfigDirectory(), ConfigFileName);

    public static void Load()
    {
        CaptureOriginalInheritValues();
        _configuredRates = LoadOrCreateConfig();
        ApplyConfiguredRates();
        ExampleModMain.LogInfo($"Applied custom inheritance rates for {_configuredRates.Count} traits.");
    }

    public static void Reload()
    {
        _configuredRates = LoadOrCreateConfig();
        ApplyConfiguredRates();
        ExampleModMain.LogInfo("Reloaded inheritance_config.json.");
    }

    public static void RestoreDefaults()
    {
        foreach (var trait in AssetManager.traits.list.OfType<ActorTrait>())
        {
            if (OriginalInheritValues.TryGetValue(trait.id, out var inherit))
            {
                trait.rate_inherit = inherit;
            }
        }
    }

    public static void RegenerateConfig()
    {
        var freshConfig = BuildDefaultConfig();
        SaveConfig(freshConfig);
        _configuredRates = freshConfig;
        ApplyConfiguredRates();
        ExampleModMain.LogInfo("Regenerated inheritance_config.json from current traits.");
    }

    private static void CaptureOriginalInheritValues()
    {
        if (OriginalInheritValues.Count > 0)
        {
            return;
        }

        foreach (var trait in AssetManager.traits.list.OfType<ActorTrait>())
        {
            OriginalInheritValues[trait.id] = trait.rate_inherit;
        }
    }

    private static Dictionary<string, int> LoadOrCreateConfig()
    {
        if (!File.Exists(ConfigPath))
        {
            var defaultConfig = BuildDefaultConfig();
            SaveConfig(defaultConfig);
            return defaultConfig;
        }

        try
        {
            var json = File.ReadAllText(ConfigPath);
            var config = JsonConvert.DeserializeObject<Dictionary<string, int>>(json) ?? new Dictionary<string, int>();
            var updated = AppendMissingTraits(config);

            if (updated)
            {
                SaveConfig(config);
            }

            return config;
        }
        catch (Exception ex)
        {
            ExampleModMain.LogError($"Failed to read {ConfigPath}. Regenerating defaults. Error: {ex}");
            var defaultConfig = BuildDefaultConfig();
            SaveConfig(defaultConfig);
            return defaultConfig;
        }
    }

    private static Dictionary<string, int> BuildDefaultConfig()
    {
        var rates = new Dictionary<string, int>();

        foreach (var trait in AssetManager.traits.list.OfType<ActorTrait>())
        {
            var inheritValue = trait.rate_inherit;

            if (OriginalInheritValues.TryGetValue(trait.id, out var originalValue))
            {
                inheritValue = originalValue;
            }

            rates[trait.id] = inheritValue;
        }

        return rates;
    }

    private static bool AppendMissingTraits(IDictionary<string, int> existingConfig)
    {
        var updated = false;

        foreach (var trait in AssetManager.traits.list.OfType<ActorTrait>())
        {
            if (existingConfig.ContainsKey(trait.id))
            {
                continue;
            }

            var inheritValue = trait.rate_inherit;

            if (OriginalInheritValues.TryGetValue(trait.id, out var originalValue))
            {
                inheritValue = originalValue;
            }

            existingConfig[trait.id] = inheritValue;
            updated = true;
        }

        return updated;
    }

    private static void SaveConfig(Dictionary<string, int> config)
    {
        try
        {
            var directory = Path.GetDirectoryName(ConfigPath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonConvert.SerializeObject(config, JsonSettings);
            File.WriteAllText(ConfigPath, json);
        }
        catch (Exception ex)
        {
            ExampleModMain.LogError($"Failed to save inheritance config: {ex}");
        }
    }

    private static void ApplyConfiguredRates()
    {
        foreach (var trait in AssetManager.traits.list.OfType<ActorTrait>())
        {
            if (_configuredRates.TryGetValue(trait.id, out var configuredRate))
            {
                trait.rate_inherit = Mathf.Clamp(configuredRate, 0, 100);
            }
            else if (OriginalInheritValues.TryGetValue(trait.id, out var defaultValue))
            {
                trait.rate_inherit = defaultValue;
            }
        }
    }

    private static string ResolveConfigDirectory()
    {
        // Prefer the known Steam install path for this mod if it exists.
        try
        {
            var steamPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Steam",
                "steamapps",
                "common",
                "worldbox",
                "worldbox_Data",
                "StreamingAssets",
                "mods",
                "advanced_genetics_220");

            if (Directory.Exists(steamPath))
            {
                return steamPath;
            }
        }
        catch (Exception ex)
        {
            ExampleModMain.LogError($"Failed to resolve Steam mods path: {ex}");
        }

        // Fallback: current assembly location.
        try
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            if (!string.IsNullOrEmpty(assemblyLocation))
            {
                var dir = Path.GetDirectoryName(assemblyLocation);
                if (!string.IsNullOrEmpty(dir))
                {
                    return dir;
                }
            }
        }
        catch (Exception ex)
        {
            ExampleModMain.LogError($"Failed to read assembly location: {ex}");
        }

        // Additional fallback: AppDomain base directory.
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        if (!string.IsNullOrEmpty(baseDir))
        {
            return baseDir;
        }

        // Last resort: current working directory.
        return Directory.GetCurrentDirectory();
    }
}
