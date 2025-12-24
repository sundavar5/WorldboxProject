using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NeoModLoader.General;

namespace BetterInheritance
{
    public static class SimpleUI
    {
        public static ScrollWindow CreateScrollWindow(string id, string title)
        {
            // Check if window already exists
            if (ScrollWindow.checkWindowExist(id))
            {
                return ScrollWindow.get(id);
            }

            // Try to clone an existing simple window, e.g., "inspect_unit" or "traits"
            // The method ScrollWindow.checkWindowExist actually attempts to load a prefab from Resources
            // and if not found loads "windows/not_found".
            // We want to clone a working window prefab.

            // Let's use the game's way:
            // 1. Find a reference window (e.g., "inspect_unit")
            // 2. Instantiate it
            // 3. Customize it

            // However, ScrollWindow.checkWindowExist does logic to load prefabs.
            // If we want a custom window, we should manually instantiate one if we don't have a prefab.

            // For NML/NCMS mods, usually `Windows.CreateNewWindow` does this:
            // It clones the "inspect_unit" window or similar.

            GameObject canvasWindows = GameObject.Find("Canvas Container Main/Canvas - Windows");
            if (canvasWindows == null) return null;

            Transform windowsTransform = canvasWindows.transform.Find("windows");

            // Try to find a reference window to clone
            ScrollWindow referenceWindow = null;
            foreach (Transform t in windowsTransform)
            {
                ScrollWindow w = t.GetComponent<ScrollWindow>();
                if (w != null && w.name != "not_found") // Avoid broken ones
                {
                    referenceWindow = w;
                    break;
                }
            }

            if (referenceWindow == null)
            {
                // Fallback: try to load from resources
                referenceWindow = Resources.Load<ScrollWindow>("windows/inspect_unit");
            }

            if (referenceWindow == null)
            {
                Debug.LogError("SimpleUI: Could not find reference window to clone.");
                return null;
            }

            // Instantiate
            GameObject newWindowObj = UnityEngine.Object.Instantiate(referenceWindow.gameObject, windowsTransform);
            newWindowObj.name = id;

            ScrollWindow newWindow = newWindowObj.GetComponent<ScrollWindow>();
            newWindow.screen_id = id;

            // Clean up content
            // Assuming the standard structure: Background/Scroll View/Viewport/Content
            Transform background = newWindowObj.transform.Find("Background");
            if (background != null)
            {
                Transform titleTransform = background.Find("Title");
                if (titleTransform != null)
                {
                    Text titleText = titleTransform.GetComponent<Text>();
                    if (titleText != null) titleText.text = title;
                }

                Transform scroll = background.Find("Scroll View");
                if (scroll != null)
                {
                    Transform viewport = scroll.Find("Viewport");
                    if (viewport != null)
                    {
                        Transform content = viewport.Find("Content");
                        if (content != null)
                        {
                            // Destroy existing children
                            foreach (Transform child in content)
                            {
                                UnityEngine.Object.Destroy(child.gameObject);
                            }
                            // Reset size
                            RectTransform contentRect = content.GetComponent<RectTransform>();
                            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, 0);
                        }
                    }
                }
            }

            // Initialize the window using its internal method if needed,
            // but usually Awake/Start handles it.
            // We might need to register it in ScrollWindow._all_windows via reflection or just rely on it being there.
            // Actually ScrollWindow.checkWindowExist adds it to _all_windows if found.
            // Since we instantiated it manually, we should add it.

            // Reflection to add to _all_windows
            try
            {
                var field = typeof(ScrollWindow).GetField("_all_windows", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
                var dict = (Dictionary<string, ScrollWindow>)field.GetValue(null);
                if (!dict.ContainsKey(id))
                {
                    dict.Add(id, newWindow);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("SimpleUI: Failed to register window: " + e.Message);
            }

            return newWindow;
        }

    }
}
