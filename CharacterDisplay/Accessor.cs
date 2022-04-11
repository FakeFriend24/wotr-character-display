using HarmonyLib;
using Kingmaker;
using Kingmaker.UI.MVVM._PCView.CharGen;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.FeatureSelector;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.Portrait;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.Total;
using Kingmaker.UI.MVVM._PCView.CharGen.Portrait;
using Kingmaker.UI.MVVM._PCView.Common;
using Kingmaker.UI.MVVM._PCView.Common.MessageModal;
using Kingmaker.UnitLogic.Class.LevelUp;
using Owlcat.Runtime.UI.Controls.Button;
using Owlcat.Runtime.UI.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CharacterDisplayWotR
{
    public static class Accessor
    {
        public static Transform StaticRoot
        {
            get
            {
                if (HasStaticCanvas)
                    return Game.Instance.UI.Canvas.transform;
                else
                    return Game.Instance.UI.Common.transform;
            }
        }
         
        public static Transform FadeCanvas
        {
            get
            {
                return Game.Instance.UI.FadeCanvas.transform;
            }
        }

        public static bool HasStaticCanvas
        {
            get => Game.Instance.UI.Canvas != null;
        }

        // Token: 0x17000024 RID: 36
        // (get) Token: 0x060000DA RID: 218 RVA: 0x00008A87 File Offset: 0x00006C87
        public static T Current<T> () where T : MonoBehaviour
        {
                T view = StaticRoot.GetComponentInDescendant<T>();
#if DEBUG
                if (view != null)
                {
                    Log.Write("Found Current (= first) of Type "+typeof(T).ToString());
                }
#endif
                return view;
            
        }

        public static GameObject PortraitFullPrefab
        {
            get
            {
                GameObject prefab = Traverse.Create(StaticRoot.GetComponentInDescendant<CharGenPortraitPhaseDetailedPCView>()).Field<CharGenPortraitPCView>("m_PortraitView").Value.gameObject;
#if DEBUG
                if (prefab != null)
                {
                    Log.Write("Found PortraitFull-Prefab");
                }
#endif
                return prefab;
                
            }
        }



        //public static Settings settings = Main.settings;
         


    }
}
