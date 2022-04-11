using HarmonyLib;
using Kingmaker;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UI;
using Kingmaker.UI.Common;
using Kingmaker.UI.MVVM._PCView.CharGen;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.AbilityScores;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.Appearance;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.FeatureSelector;
using Kingmaker.UI.MVVM._PCView.CharGen.Portrait;
using Kingmaker.UI.MVVM._VM.CharGen.Phases.AbilityScores;
using Kingmaker.UnitLogic.Class.LevelUp;
using Owlcat.Runtime.UI.Controls.Button;
using Owlcat.Runtime.UI.Controls.SelectableState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CharacterDisplayWotR
{
    [HarmonyLib.HarmonyPatch(typeof(CharGenAppearancePhaseDetailedPCView), "BindViewImplementation")]
    public static class CharGenAppearancePhaseDetailedPCView_BindViewImplementation_Patch
    {
        public static void Postfix(CharGenAppearancePhaseDetailedPCView __instance)
        {
            GameObject Selector = __instance.gameObject;
            UIElements.CleanUpPortraitView(Selector);
#if DEBUG
            Log.Write("Instantiate CharacterDisplayPortrait.");
#endif
            if (Main.enabled )
            {
 #if DEBUG
               Log.Write("CharacterPortraitView should be added.");
 #endif
               UIElements.InitializePortraitView(Selector.GetComponent<RectTransform>(), __instance);
            }
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(CharGenAppearancePhaseDetailedPCView), "DestroyViewImplementation")]
    public static class CharGenAppearancePhaseDetailedPCView_DestroyViewImplementation_Patch
    {
        public static void Postfix(CharGenAppearancePhaseDetailedPCView __instance)
        {
            GameObject Selector = __instance.gameObject;
            UIElements.CleanUpPortraitView(Selector);
        }
    }


    class UIElements
    {
        public static CharGenPortraitPCView ImageInstance;
         

        public static void CleanUpPortraitView( GameObject target, string targetName = "CharacterDisplay")
        {
            for (int i = 0; i < target.transform.childCount; i++)
            {
                if (target.transform.GetChild(i).name.StartsWith(targetName))
                {
                    if (UIElements.ImageInstance && UIElements.ImageInstance.gameObject == target.transform.GetChild(i).gameObject)
                        UIElements.ImageInstance = null;
                    UnityEngine.Object.Destroy(target.transform.GetChild(i).gameObject);
#if DEBUG
                    Log.Write("Cleaned up trash.");
#endif
                }
            }
            UIElements.ImageInstance = null;

        }

        internal static void InitializePortraitView(RectTransform transform, CharGenAppearancePhaseDetailedPCView instance)
        {
            if(ImageInstance == null)
            {
                ImageInstance = InitializePortraitFull(transform);
            }
        }

        public static CharGenPortraitPCView InitializePortraitFull(RectTransform parent, string name = "CharacterDisplay_PortraitView")
        {
            CharGenPortraitPCView portraitView =  GameObject.Instantiate(Accessor.PortraitFullPrefab, parent).GetComponent<CharGenPortraitPCView>();
            portraitView.gameObject.name = name;
            portraitView.gameObject.AddComponent<ChangeSideUIBehaviour>();
            portraitView.GetComponent<CanvasGroup>().blocksRaycasts = true;
            return portraitView;
        }
    }
}
