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
    public class TraitConfiguration
    {

        private static Dictionary<string, TraitConfiguration> instances = new Dictionary<string, TraitConfiguration>();
        private Dictionary<string, bool> traitStatus;

        private TraitConfiguration()
        {
            traitStatus = InitializeTraitStatus();
        }
        public static TraitConfiguration GetInstance(string race)
        {
            if (!instances.ContainsKey(race))
            {
                instances[race] = new TraitConfiguration();
            }
            return instances[race];
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
        public void ClearTraitStatus(string traitID)
        {
            if(traitStatus[traitID]==true){
                traitStatus[traitID] = !traitStatus[traitID];
            }
        }

        private Dictionary<string, bool> InitializeTraitStatus()
        {
            int CurrentactorTraitCount = 0;
            int actorTraitCount = AssetManager.traits.list.Count;
            string[] idArry = new string[actorTraitCount];

            foreach (var trait in AssetManager.traits.list)
            {
                if (trait is ActorTrait)
                {
                    idArry[CurrentactorTraitCount] = trait.id;
                    // idArry[CurrentactorTraitCount, 1] = trait.path_icon;
                    CurrentactorTraitCount++;
                }
            }

            Dictionary<string, bool> traitStatus = new Dictionary<string, bool>();

            for (int i = 0; i < actorTraitCount; i++)
            {
                traitStatus[idArry[i]] = false;
            }

            return traitStatus;
        }
    }
}
