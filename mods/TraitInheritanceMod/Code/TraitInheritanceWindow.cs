using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace TraitInheritanceMod
{
    public class TraitInheritanceUI : MonoBehaviour
    {
        private bool showWindow = false;
        private Rect windowRect = new Rect(20, 20, 500, 600);
        private Vector2 scrollPosition;
        private string searchFilter = "";

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                showWindow = !showWindow;
            }
        }

        void OnGUI()
        {
            if (showWindow)
            {
                windowRect = GUI.Window(12345, windowRect, WindowFunction, "Trait Inheritance Settings");
            }
        }

        void WindowFunction(int windowID)
        {
            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Search:", GUILayout.Width(60));
            searchFilter = GUILayout.TextField(searchFilter);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            GUILayout.Label("Set inheritance rate (0-100%). 'Default' means game logic applies.");

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            var traits = AssetManager.traits.list;

            // Sort traits alphabetically for easier finding
            var sortedTraits = traits.OrderBy(t => t.id).ToList();

            foreach (var trait in sortedTraits)
            {
                if (!string.IsNullOrEmpty(searchFilter) && !trait.id.Contains(searchFilter.ToLower()))
                    continue;

                GUILayout.BeginHorizontal("box");

                // Icon (if possible, but text is fine)
                // GUILayout.Box(trait.path_icon ... ); // Skipping texture load for simplicity

                // Trait Name/ID
                GUILayout.Label(trait.id, GUILayout.Width(200));

                // Current Rate
                float currentRate = Main.GetInheritanceRate(trait.id);
                bool isCustom = currentRate >= 0;

                string displayRate = isCustom ? currentRate.ToString("F0") + "%" : "Default";
                GUILayout.Label(displayRate, GUILayout.Width(80));

                // Slider
                float sliderValue = isCustom ? currentRate : 50f;
                float newSliderValue = GUILayout.HorizontalSlider(sliderValue, 0f, 100f, GUILayout.Width(100));

                // If user touches slider for a Default entry, it becomes Custom
                if (Math.Abs(newSliderValue - sliderValue) > 0.01f)
                {
                    Main.inheritanceRates[trait.id] = newSliderValue;
                }
                else if (isCustom && Math.Abs(newSliderValue - currentRate) > 0.01f)
                {
                     Main.inheritanceRates[trait.id] = newSliderValue;
                }

                GUILayout.Space(10);

                if (GUILayout.Button(isCustom ? "Reset" : "Set", GUILayout.Width(50)))
                {
                    if (isCustom)
                        Main.inheritanceRates.Remove(trait.id);
                    else
                        Main.inheritanceRates[trait.id] = 50f;
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();

            GUILayout.Space(10);
            if (GUILayout.Button("Save Configuration"))
            {
                Main.SaveConfig();
            }

            if (GUILayout.Button("Close"))
            {
                showWindow = false;
            }

            GUILayout.EndVertical();
            GUI.DragWindow();
        }
    }
}
