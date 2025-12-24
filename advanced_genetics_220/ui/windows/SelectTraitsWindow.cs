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

namespace FinalRun.UI.GenWindows
{
    internal class SelectTraitsWindow
    {
        public static void init()
        {

            
            var scrollView = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/SelectTraitWindow/Background/Scroll View");

            Button RaceButton = NewUI.createBGWindowButton(
                scrollView,
                50,
                "elfIcon",
                "SelectRaces",

                NewUI.getLocalizednamedec("race_tab_name"),
                NewUI.getLocalizednamedec("race_window_tab_desc"),

                () => WindowManager.openTraitWindow("RaceWindow")
            );

            initTiles();

        }
        private static void initTiles()
        {


            int CurrentactorTraitCount = 0;
            int actorTraitCount = AssetManager.traits.list.Count;
            string[,] idArry = new string[actorTraitCount, 3]; // Initialize the array with the correct size

            foreach (var trait in AssetManager.traits.list)
            {
                if (trait is ActorTrait)
                {
                    idArry[CurrentactorTraitCount, 0] = trait.id;
                    idArry[CurrentactorTraitCount, 1] = trait.path_icon;
                    idArry[CurrentactorTraitCount, 2] = trait.group_id;
                    CurrentactorTraitCount++;
                }
            }


            var rect = WindowManager.windowContents["SelectTraitWindow"].GetComponent<RectTransform>();
            rect.pivot = new Vector2(0, 1);
            rect.sizeDelta = new Vector2(0, Mathf.Abs(getPositionByIndex(actorTraitCount).y) + 40);

            for (int j = 0; j<actorTraitCount; j++)
            {
                //UnityEngine.Debug.Log("Button Create() id: " + idArry[j,0]+" path: "+idArry[j,1]+" j: "+j);
                if (idArry[j, 0]==null)
                {
                    continue;
                }
                var sprite = Resources.Load<Sprite>(idArry[j, 1]);
                createTraitButton(idArry[j, 0], idArry[j, 2], sprite, j); // Creating tile button
            }


        }
        private static void createTraitButton(string tileName, string tileTypeType, Sprite sprite, int index)
        {

            string name = tileName;
            string desc = "";
            //Getting Games localization for each trait for each different language support
            NewUI.getLocalization($"trait_{tileName}", ref name, ref desc, "_info");
            // Creating new button
            var button = PowerButtons.CreateButton(
                tileName,
                sprite,
                name,
                desc,

                // Getting button position by its index
                getPositionByIndex(index),

                ButtonType.Toggle,
                WindowManager.windowContents["SelectTraitWindow"].transform,

                // Setting on click callback with parameter
                () => traitButtonClick(tileName, tileTypeType)
            );
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

        // On click callback with parameter
        private static void traitButtonClick(string tileName, string tileTypeType)
        {
            RaceUpdater traitConfig = RaceUpdater.Instance;
            traitConfig.ToggleTraitStatus(tileName);
        }

    }

}
