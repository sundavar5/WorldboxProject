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

namespace FinalRun.UI.GenWindows
{
    internal class WindowManager
    {
        public static Dictionary<string, GameObject> windowContents = new Dictionary<string, GameObject>();
        public static Dictionary<string, ScrollWindow> createdWindows = new Dictionary<string, ScrollWindow>();

        public static void init()
        {
            newWindow("SelectTraitWindow", NewUI.getLocalizednamedec("select_traits"));
            SelectTraitsWindow.init();
            newWindow("RaceWindow", NewUI.getLocalizednamedec("race_window"));
            RaceWindow.init();
        }

        private static void newWindow(string id, string title)
        {
            ScrollWindow window;
            GameObject content;
            window = Windows.CreateNewWindow(id, title);
            createdWindows.Add(id, window);

            GameObject scrollView = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View");
            scrollView.gameObject.SetActive(true);

            content = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View/Viewport/Content");
            if (content != null)
            {
                windowContents.Add(id, content);
            }
        }

        public static void updateScrollRect(GameObject content, int count, int size)
        {
            var scrollRect = content.GetComponent<RectTransform>();
            scrollRect.sizeDelta = new Vector2(0, count*size);
        }
        public static void openTraitWindow(string windowID)
        {
            Windows.ShowWindow(windowID);
        }
    }
}