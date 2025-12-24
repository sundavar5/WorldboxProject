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
            // Do NOT call ScrollWindow.checkWindowExist(id) because it triggers the "Under Development" fallback window if the ID is missing.
            // Instead, check internal registry via reflection or just try to find it in the scene if we made it before.

            // Check if we already created it (it should be in the scene)
            GameObject existingObj = GameObject.Find("Canvas Container Main/Canvas - Windows/windows/" + id);
            if (existingObj != null)
            {
                return existingObj.GetComponent<ScrollWindow>();
            }

            GameObject canvasWindows = GameObject.Find("Canvas Container Main/Canvas - Windows");
            if (canvasWindows == null) return null;

            Transform windowsTransform = canvasWindows.transform.Find("windows");

            // Try to find a reference window to clone
            ScrollWindow referenceWindow = null;

            // Try specific stable windows first
            Transform inspectUnit = windowsTransform.Find("inspect_unit");
            if (inspectUnit != null) referenceWindow = inspectUnit.GetComponent<ScrollWindow>();

            if (referenceWindow == null)
            {
                foreach (Transform t in windowsTransform)
                {
                    ScrollWindow w = t.GetComponent<ScrollWindow>();
                    if (w != null && w.name != "not_found" && w.gameObject.activeSelf == false)
                    {
                        // Clone an inactive valid window to avoid glitches
                        referenceWindow = w;
                        break;
                    }
                }
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

            // Register it
            try
            {
                var field = typeof(ScrollWindow).GetField("_all_windows", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
                var dict = (Dictionary<string, ScrollWindow>)field.GetValue(null);
                if (dict != null && !dict.ContainsKey(id))
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
