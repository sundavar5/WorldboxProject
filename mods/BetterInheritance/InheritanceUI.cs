using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using NeoModLoader.General;

namespace BetterInheritance
{
    public class InheritanceUI : MonoBehaviour
    {
        private static ScrollWindow window;
        private static GameObject content;
        private static bool _initialized = false;

        public static void Init()
        {
            if (_initialized) return;

            // Check if game UI is ready
            GameObject canvasMain = GameObject.Find("Canvas Container Main");
            if (canvasMain == null) return;

            try
            {
                // Create the window
                window = SimpleUI.CreateScrollWindow("BetterInheritanceWindow", "Inheritance Rates");
                if (window == null)
                {
                    Debug.LogError("BetterInheritance: Failed to create window.");
                    return;
                }

                content = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View/Viewport/Content");

                // Populate the window
                if (content != null)
                {
                    PopulateList();
                }

                // Create a button to open the window.
                // We'll place it in the "Other" tab of the power bar, or just under the power bar if easier.
                // Finding the "Other" tab content:
                // Usually: Canvas Container Main/Canvas - Bottom/Bottom Bar/Scroll View/Viewport/Content/Tab_Other (or similar)

                // For simplicity, we'll try to use PowerButtonCreator if available in NML,
                // but since we are barebones, let's try to clone a button from the bottom bar.

                CreateOpenButton();

                _initialized = true;
                Debug.Log("BetterInheritance: UI Initialized.");
            }
            catch (Exception e)
            {
                Debug.LogError("BetterInheritance: Error initializing UI: " + e.Message);
            }
        }

        private static void CreateOpenButton()
        {
            // Find a place to put the button.
            // Let's try to append it to the "Other" tab grid.
            GameObject tabOther = GameObject.Find("Canvas Container Main/Canvas - Bottom/Bottom Bar/Scroll View/Viewport/Content/Tab_Other");
            if (tabOther == null)
            {
                 // Fallback to "Tab_System" or just the content root
                 tabOther = GameObject.Find("Canvas Container Main/Canvas - Bottom/Bottom Bar/Scroll View/Viewport/Content");
            }

            if (tabOther != null)
            {
                // Clone an existing button (first child)
                if (tabOther.transform.childCount > 0)
                {
                    Transform refButton = tabOther.transform.GetChild(0);
                    GameObject newBtn = Instantiate(refButton.gameObject, tabOther.transform);
                    newBtn.name = "Button_BetterInheritance";

                    // Remove existing PowerButton component if it interferes, or just use Button
                    // Most buttons have a custom component. We just want the Unity Button.
                    Button btnComp = newBtn.GetComponent<Button>();
                    if (btnComp != null)
                    {
                        btnComp.onClick.RemoveAllListeners();
                        btnComp.onClick.AddListener(() => ScrollWindow.showWindow("BetterInheritanceWindow"));
                    }

                    // Change Icon
                    Transform iconTr = newBtn.transform.Find("Icon");
                    if (iconTr != null)
                    {
                        Image iconImg = iconTr.GetComponent<Image>();
                        if (iconImg != null)
                        {
                            // Load custom icon or use a default one (e.g. dna)
                            // Since we don't have a loaded sprite easily, let's just color it for now or leave as is.
                            // iconImg.color = Color.cyan;
                            // Try to load a sprite from Resources if possible
                            Sprite dnaSprite = Resources.Load<Sprite>("ui/icons/icon_trait_immortal"); // Example
                            if (dnaSprite != null) iconImg.sprite = dnaSprite;
                        }
                    }

                    // Cleanup tooltips if possible (remove PowerButton component logic)
                    // ...
                }
            }
        }

        private static void PopulateList()
        {
            // Sort traits by ID
            var traits = AssetManager.traits.list.Cast<ActorTrait>().OrderBy(t => t.id).ToList();

            int y = 0;
            int height = 40; // Height of each row

            // Adjust content height
            RectTransform contentRect = content.GetComponent<RectTransform>();
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, traits.Count * height);

            foreach (var trait in traits)
            {
                CreateTraitRow(trait, y);
                y -= height;
            }
        }

        private static void CreateTraitRow(ActorTrait trait, int y)
        {
            // Create a container for the row
            GameObject row = new GameObject("TraitRow_" + trait.id);
            row.transform.SetParent(content.transform);
            row.transform.localScale = Vector3.one;

            RectTransform rowRect = row.AddComponent<RectTransform>();
            rowRect.anchorMin = new Vector2(0, 1);
            rowRect.anchorMax = new Vector2(1, 1);
            rowRect.pivot = new Vector2(0.5f, 1);
            rowRect.anchoredPosition = new Vector2(0, y);
            rowRect.sizeDelta = new Vector2(0, 40);

            // Trait Name Text
            GameObject nameObj = new GameObject("Name");
            nameObj.transform.SetParent(row.transform);
            Text nameText = nameObj.AddComponent<Text>();
            nameText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            string localizedName = LocalizedTextManager.getText(trait.id);
            if (string.IsNullOrEmpty(localizedName)) localizedName = trait.id;
            nameText.text = localizedName;
            nameText.color = Color.white;
            nameText.alignment = TextAnchor.MiddleLeft;

            RectTransform nameRect = nameObj.GetComponent<RectTransform>();
            nameRect.anchorMin = new Vector2(0, 0);
            nameRect.anchorMax = new Vector2(0.7f, 1);
            nameRect.offsetMin = new Vector2(10, 0);
            nameRect.offsetMax = new Vector2(0, 0);

            // Controls Container
            GameObject controls = new GameObject("Controls");
            controls.transform.SetParent(row.transform);
            RectTransform controlsRect = controls.AddComponent<RectTransform>();
            controlsRect.anchorMin = new Vector2(0.7f, 0);
            controlsRect.anchorMax = new Vector2(1, 1);
            controlsRect.offsetMin = Vector2.zero;
            controlsRect.offsetMax = Vector2.zero;

            // Current Value Text
            GameObject valObj = new GameObject("Value");
            valObj.transform.SetParent(controls.transform);
            Text valText = valObj.AddComponent<Text>();
            valText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            valText.alignment = TextAnchor.MiddleCenter;
            valText.color = Color.yellow;
            RectTransform valRT = valObj.GetComponent<RectTransform>();
            valRT.anchorMin = new Vector2(0.3f, 0);
            valRT.anchorMax = new Vector2(0.7f, 1);
            valRT.offsetMin = Vector2.zero;
            valRT.offsetMax = Vector2.zero;

            int currentRate = InheritanceConfig.GetRate(trait.id);
            valText.text = currentRate + "%";

            // Minus Button
            CreateButton("-", new Vector2(0, 0), new Vector2(0.3f, 1), controls.transform, () => {
                currentRate = Mathf.Max(0, currentRate - 10);
                InheritanceConfig.SetRate(trait.id, currentRate);
                valText.text = currentRate + "%";
                InheritanceConfig.Save();
            });

            // Plus Button
            CreateButton("+", new Vector2(0.7f, 0), new Vector2(1, 1), controls.transform, () => {
                currentRate = Mathf.Min(100, currentRate + 10);
                InheritanceConfig.SetRate(trait.id, currentRate);
                valText.text = currentRate + "%";
                InheritanceConfig.Save();
            });
        }

        private static void CreateButton(string text, Vector2 anchorMin, Vector2 anchorMax, Transform parent, UnityEngine.Events.UnityAction onClick)
        {
            GameObject btnObj = new GameObject("Btn_" + text);
            btnObj.transform.SetParent(parent);
            btnObj.transform.localScale = Vector3.one;

            Image img = btnObj.AddComponent<Image>();
            img.color = new Color(0.5f, 0.5f, 0.5f);

            Button btn = btnObj.AddComponent<Button>();
            btn.onClick.AddListener(onClick);

            RectTransform rt = btnObj.GetComponent<RectTransform>();
            rt.anchorMin = anchorMin;
            rt.anchorMax = anchorMax;
            rt.offsetMin = new Vector2(2, 2);
            rt.offsetMax = new Vector2(-2, -2);

            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(btnObj.transform);
            textObj.transform.localScale = Vector3.one;
            Text t = textObj.AddComponent<Text>();
            t.text = text;
            t.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            t.alignment = TextAnchor.MiddleCenter;
            t.color = Color.black;

            RectTransform textRT = textObj.GetComponent<RectTransform>();
            textRT.anchorMin = Vector2.zero;
            textRT.anchorMax = Vector2.one;
            textRT.offsetMin = Vector2.zero;
            textRT.offsetMax = Vector2.zero;
        }
    }
}
