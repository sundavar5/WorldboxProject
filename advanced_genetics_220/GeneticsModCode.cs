using System;
using NeoModLoader.api;
using NeoModLoader.api.attributes;
using NeoModLoader.General;

namespace FinalRun;


public class ExampleModMain : BasicMod<ExampleModMain>, IReloadable, IUnloadable
{   
    [Hotfixable]

    public void Reload()
    {
        LM.ApplyLocale();
        InheritanceConfigManager.Reload();
    }

    public void OnUnload()
    {
        InheritanceConfigManager.RestoreDefaults();
    }

    protected override void OnModLoad()
    {
        LogInfo("Genetics()>> Started!");
        InheritanceConfigManager.Load();
        if (Environment.UserName == "Tactical")
        {
            LogInfo("Admin()>>You Are The Editor");
            Config.isEditor = true;
        }
    }

    public static void Called()
    {
        LogInfo("Hello World From Another!");
    }
}
