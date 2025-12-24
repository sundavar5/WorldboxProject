using NeoModLoader.api.attributes;

namespace FinalRun.Content;

internal static class ExampleActions
{
    [Hotfixable]
    public static void ToggleCustomInheritance(bool isEnabled)
    {
        if (isEnabled)
            InheritanceConfigManager.Reload();
        else
            InheritanceConfigManager.RestoreDefaults();
    }

    [Hotfixable]
    public static void RegenerateConfig(bool shouldRegenerate)
    {
        if (!shouldRegenerate)
            return;

        InheritanceConfigManager.RegenerateConfig();
    }
}
