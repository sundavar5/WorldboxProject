using System;
using NeoModLoader.api;
using UnityEngine;
using HarmonyLib;
using System.Reflection;

namespace BetterInheritance
{
    public class InheritanceMod : BasicMod<InheritanceMod>, IReloadable
    {
        public static string ModPath;

        protected override void OnModLoad()
        {
            ModPath = GetDeclaration().FolderPath;
            InheritanceConfig.Load();

            Harmony harmony = new Harmony("com.jules.betterinheritance");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Debug.Log("BetterInheritance: Loaded.");
        }

        private void Update()
        {
            InheritanceUI.Init();
        }

        public void Reload()
        {
            InheritanceConfig.Load();
            Debug.Log("BetterInheritance: Reloaded config.");
        }
    }
}
