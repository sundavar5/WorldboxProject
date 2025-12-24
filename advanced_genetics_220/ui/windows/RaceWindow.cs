using System;
using NCMS.Utils;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;
using ReflectionUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinalRun.UI;
using FinalRun.UI.GenWindows;
using System.Threading.Tasks;
using UnityEngine.UI;
using System.Security.Cryptography;
using UnityEngine.UI;
using UnityEngine;
using NeoModLoader.General;
using NeoModLoader.General.UI.Tab;

namespace FinalRun.UI.GenWindows
{
    internal class RaceWindow
    {
        private static Dictionary<string, bool> raceSelections = new Dictionary<string, bool>();
        public static void init()
        {
            if (raceSelections.Count == 0)
            {
                InitializeSelectedRaces();
            }

            var scrollView = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/RaceWindow/Background/Scroll View");

            Button addElfButton = NewUI.createBGWindowButton(
                scrollView,
                50,
                "saveIcon4",
                "LoadTraits",

                NewUI.getLocalizednamedec("load_name"),
                NewUI.getLocalizednamedec("load_desc"),

                () => RaceWindow.LoadSelectedTraits()
            );

            initTiles();

        }
        private static void InitializeSelectedRaces()
        {
            raceSelections.Add("unit_human", false);
            raceSelections.Add("unit_elf", false);
            raceSelections.Add("unit_dwarf", false);
            raceSelections.Add("unit_orc", false);
        }
        private static void initTiles()
        {
            // NewUI.getLocalization($"trait_{tileName}", ref name, ref desc, "_info");
            var sprite1 = Resources.Load<Sprite>("ui/icons/humanIcon");
            createTraitButton("unit_human", "race", sprite1, 0); 
            var sprite2 = Resources.Load<Sprite>("ui/icons/elfIcon");
            createTraitButton("unit_elf", "race", sprite2, 1); 
            var sprite3 = Resources.Load<Sprite>("ui/icons/dwarfIcon");
            createTraitButton("unit_dwarf", "race", sprite3, 2);
            var sprite4 = Resources.Load<Sprite>("ui/icons/orcIcon"); 
            createTraitButton("unit_orc", "race", sprite4, 3); 
        }
        private static void createTraitButton(string tileName, string tileTypeType, Sprite sprite, int index)
        {

            string desc = "";
            string name = NewUI.getLocalizednamedec(tileName);

            var racebutton = PowerButtons.CreateButton(
                tileName,
                sprite,
                name,
                desc,

                // Getting button position by its index
                getPositionByIndex(index),

                ButtonType.Toggle,
                WindowManager.windowContents["RaceWindow"].transform,

                // Setting on click callback with parameter
                () => ToggleRaceSelection(tileName)
            );
        }
        private static void ToggleRaceSelection(string raceName)
        {
            raceSelections[raceName] = !raceSelections[raceName]; // Toggle race selection
        }
        public static Dictionary<string, bool> GetRaceSelections()
        {
            return raceSelections;//returns race selection for other funtions to call and get the values saved
        }
        private static void LoadSelectedTraits()
        {
            var raceSelections = RaceWindow.GetRaceSelections(); // Get selected races from RaceUpdater
            Dictionary<string, bool> traitSelections = RaceUpdater.Instance.GetTraitValues(); // Get selected traits from SelectTraitsWindow
            
            foreach (var race in raceSelections)
            {
                if (race.Value)
                {
                    TraitConfiguration traitConfig = TraitConfiguration.GetInstance(race.Key);

                    // Apply traits for the selected race using traitSelections
                    foreach (var trait in traitSelections)
                    {
                        if (trait.Value)
                        {
                            // UnityEngine.Debug.Log("RaceWindow()>> traitID: "+trait.Key);
                            if(traitConfig.IsTraitEnabled(trait.Key))
                            {
                                continue;
                            }
                            else
                            {
                                traitConfig.ToggleTraitStatus(trait.Key);
                            }
                        }
                        else
                        {
                            // Set the unselected traits to false
                            traitConfig.ClearTraitStatus(trait.Key);
                        }
                    }
                }
            }
            // Close the window after applying traits
            // Windows.CloseWindow("RaceWindow");
        }
        /*
        Getting button position by its index
        By this formules we dont need any additional if-else constructions. 
        */
        internal static Vector2 getPositionByIndex(int index)
        {

            // Starting position by x
            float startX = 50;

            // Starting position by y
            float startY = -20;

            // Buttons size + gap between
            float sizeWithGap = 40;

            // Buttons per row
            int buttonsPerRow = 5;

            // Calculating points
            float positionX = startX + (index * sizeWithGap) - ((Mathf.Floor(index / buttonsPerRow) * sizeWithGap) * buttonsPerRow);
            float positionY = startY - (Mathf.Floor(index / buttonsPerRow) * sizeWithGap);

            return new Vector2(positionX, positionY);
        }
    }

}
