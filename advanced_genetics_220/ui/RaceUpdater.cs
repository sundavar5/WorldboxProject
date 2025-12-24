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

namespace FinalRun.UI
{
    public class RaceUpdater
    {
        private static RaceUpdater instance;
        private Dictionary<string, bool> traitStatus;

        private RaceUpdater()
        {
            traitStatus = InitializeTraitStatus();
        }

        public static RaceUpdater Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RaceUpdater();
                }

                return instance;
            }
        }

        public bool IsTraitEnabled(string traitId)
        {
            return traitStatus.TryGetValue(traitId, out bool isEnabled) && isEnabled;
        }

        public void ToggleTraitStatus(string traitId)
        {
            if (traitStatus.ContainsKey(traitId))
            {
                traitStatus[traitId] = !traitStatus[traitId];
            }
        }
        public Dictionary<string, bool> GetTraitValues()
        {
            return traitStatus;
        }
        private Dictionary<string, bool> InitializeTraitStatus()
        {
            int CurrentactorTraitCount = 0;
            int actorTraitCount = AssetManager.traits.list.Count;
            string[,] idArry = new string[actorTraitCount, 2];

            foreach (var trait in AssetManager.traits.list)
            {
                if (trait is ActorTrait)
                {
                    idArry[CurrentactorTraitCount, 0] = trait.id;
                    idArry[CurrentactorTraitCount, 1] = trait.path_icon;
                    CurrentactorTraitCount++;
                }
            }

            Dictionary<string, bool> traitStatus = new Dictionary<string, bool>();

            for (int i = 0; i < actorTraitCount; i++)
            {
                traitStatus[idArry[i, 0]] = false;
            }

            return traitStatus;
        }
    }
}
