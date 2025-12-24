using System;
using NCMS;
//using NCMS.Utils.Windows;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;
using FinalRun.UI;
using FinalRun.UI.GenWindows;
using ReflectionUtility;
using FinalRun;

using System.Text;
using System.Threading.Tasks;

using System.Globalization;


using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace FinalRun.UI
{
    class NewUI : MonoBehaviour
    {

        public static void getLocalization(string id, ref string name, ref string desc, string descAddOn)
        {
            if (LocalizedTextManager.stringExists($"{id}"))
            {
                name = LocalizedTextManager.getText($"{id}");
            }
            if (LocalizedTextManager.stringExists($"{id}{descAddOn}"))
            {
                desc = LocalizedTextManager.getText($"{id}{descAddOn}");
            }
        }

        public static Button createBGWindowButton(GameObject parent, int posY, string iconName, string buttonName, string buttonTitle, 
        string buttonDesc, UnityAction call)
        {
            // UnityEngine.Debug.Log("Windows()>> Start");

            PowerButton button = PowerButtons.CreateButton(
                buttonName,
                Resources.Load<Sprite>("ui/icons/"+iconName),
                //Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icons.{iconName}.png"),
                buttonTitle,
                buttonDesc,
                new Vector2(118, posY),
                ButtonType.Click,
                parent.transform,
                call
            );
            // UnityEngine.Debug.Log("Button Made");
            Image buttonBG = button.gameObject.GetComponent<Image>();
            buttonBG.sprite = Resources.Load<Sprite>("ui/backgroundTabButton");
            //buttonBG.sprite = Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.UI.backgroundTabButton.png");
            Button buttonButton = button.gameObject.GetComponent<Button>();
            buttonBG.rectTransform.localScale = Vector3.one;

            return buttonButton;
        }
        public static string getLocalizednamedec(string textID){
            // UnityEngine.Debug.Log("Getting Localization of" + textID);
            string loacalized = LocalizedTextManager.getText($"{textID}");
            return(loacalized);
        }

    }
}