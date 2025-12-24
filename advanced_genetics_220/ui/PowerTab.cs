using System.Collections.Generic;
using NeoModLoader.General;
using NeoModLoader.General.UI.Tab;
using FinalRun.UI;
using FinalRun.UI.GenWindows;
using NCMS.Utils;
using UnityEngine.UI;
using UnityEngine;



namespace FinalRun.UI.GenWindows
{
internal static class PowerTab
    {
        public const string GENETICS = "Genetics";
        public static PowersTab tab;


        internal static void init(){
            tab = TabManager.CreateTab("Button_Genetics_Toggle", "Advanced_Genetics", "Modify_Inherit_Traits",
            SpriteTextureLoader.getSprite("ui/icons/Icondna"));
            tab.SetLayout(new List<string>()
            {
                GENETICS,
            });

            _addButtons();
            // string buttonID = "Button_Genetics_Toggle";
        }
        internal static PowerButton traitbutton;
        private static void _addButtons() {

            traitbutton = PowerButtons.CreateButton(
            //Button ID
            "anyTrait",

            // using Icondna from mod files as an icon
            Resources.Load<Sprite>("ui/icons/Icondna"),

            NewUI.getLocalizednamedec("trait_selector_name"),
            NewUI.getLocalizednamedec("trait_selector_desc"),

            //gets position
            Vector2.zero,

            // Button Type, is click so it presses once
            ButtonType.Click,

            null,

            // On click action
            OpenWindow);
        
            //Adds NCMS button to NML Tab
            PowerButtonCreator.AddButtonToTab(traitbutton, tab, new Vector2(116, 18));
        }
        private static void OpenWindow()
        {
            //UnityEngine.Debug.Log("OpenWindow()>> Start");
            Windows.ShowWindow("SelectTraitWindow");
            //UnityEngine.Debug.Log("OpenWindow()>> Success");
        }
    }
}
