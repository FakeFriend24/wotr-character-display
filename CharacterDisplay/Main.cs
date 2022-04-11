// Copyright (c) 2019 v1ld.git@gmail.com
// Copyright (c) 2019 Jennifer Messerly
// This code is licensed under MIT license (see LICENSE for details)

using System; 
using System.IO;
using System.Linq;
using System.Reflection;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Root.Strings.GameLog;
using Kingmaker.GameModes;
using Kingmaker.Globalmap;
using Kingmaker.Globalmap.Blueprints;
using Kingmaker.Globalmap.View;
using Kingmaker.PubSubSystem;
using Kingmaker.UI.ServiceWindow.LocalMap;
using Kingmaker.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityModManagerNet;
using Owlcat.Runtime.Core;
using Owlcat.Runtime.Core.Logging; 
using Kingmaker.UI;
using Kingmaker.UI.MVVM._PCView.ServiceWindows.LocalMap;
using Kingmaker.UI.MVVM._VM.ServiceWindows.LocalMap;
using HarmonyLib;
using Kingmaker.EntitySystem.Stats;

namespace CharacterDisplayWotR
{
#if DEBUG
    [EnableReloading]
#endif

    public class Main
    {

        public static bool enabled;

        public static System.Random randomGenerator;

        public static UnityModManager.ModEntry.ModLogger logger;


        static Harmony harmonyInstance;


        [System.Diagnostics.Conditional("DEBUG")]
        static void EnableGameLogging()
        {
            if (Owlcat.Runtime.Core.Logging.Logger.Instance.Enabled) return;

            // Code taken from GameStarter.Awake(). PF:K logging can be enabled with command line flags,
            // but when developing the mod it's easier to force it on.
            var dataPath = ApplicationPaths.persistentDataPath;
            Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
            Owlcat.Runtime.Core.Logging.Logger.Instance.Enabled = true;
            var text = Path.Combine(dataPath, "GameLog.txt");
            if (File.Exists(text))
            {
                File.Copy(text, Path.Combine(dataPath, "GameLogPrev.txt"), overwrite: true);
                File.Delete(text);
            }
            Owlcat.Runtime.Core.Logging.Logger.Instance.AddLogger(new UberLoggerFile("GameLogFull.txt", dataPath));
            Owlcat.Runtime.Core.Logging.Logger.Instance.AddLogger(new UberLoggerFilter(new UberLoggerFile("GameLog.txt", dataPath), LogSeverity.Warning, "MatchLight"));

            Owlcat.Runtime.Core.Logging.Logger.Instance.Enabled = true;
        }

        internal static void NotifyPlayer(string message, bool warning = false)
        {
            if (warning)
            {
                EventBus.RaiseEvent<IWarningNotificationUIHandler>((IWarningNotificationUIHandler h) => h.HandleWarning(message, true));
            }
            else
            {
                // Game.Instance.UI.DBattleLogManager.LogView.AddLogEntry(message, GameLogStrings.Instance.DefaultColor);
            }
        }



        // mod entry point, invoked from UMM
        static bool Load(UnityModManager.ModEntry modEntry)
        {
#if DEBUG
            try
            {
#endif
                logger = modEntry.Logger;
                modEntry.OnToggle = OnToggle;
#if DEBUG
                modEntry.OnUnload = Unload;
#endif
                harmonyInstance = new Harmony(modEntry.Info.Id);
                harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
                StartMod();
#if DEBUG
            }
            catch (Exception e)
            {
                Log.Write(e.ToString());
            }
#endif

            return true;
        }

#if DEBUG
        static bool Unload(UnityModManager.ModEntry modEntry)
        {
            harmonyInstance.UnpatchAll();

            return true;
        }
#endif


        static void StartMod()
        {
        }

        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            enabled = value;
            if(UIElements.ImageInstance) 
                UIElements.ImageInstance.enabled = enabled;
            return true;
        }


        static void OnGUI(UnityModManager.ModEntry modEntry)
        {
        }
        static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
        }


    } 



}
