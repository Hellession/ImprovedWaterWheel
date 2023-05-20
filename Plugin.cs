using BepInEx;
using UnityEngine;
using HarmonyLib;
using BepInEx.Logging;
using System.Reflection;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using System.Linq;
using System.Xml;
using Hellession;
using System.IO;

namespace UnpredictableWaterWheel
{
    [BepInPlugin(PluginGUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGUID = "com.hellession.unpredictablewaterwheel";

        public static Dictionary<string, Sprite> ModdedSprites { get; set; } = new Dictionary<string, Sprite>();

        public static JsonWWSettings Settings { get; set; }

        public static HLSNAnimator MyAnimator { get; set; } = new HLSNAnimator()
        {
            MyDelayMode = HLSNAnimator.DelayMode.NextFrame,
            MyTimeMode = HLSNAnimator.TimeMode.Time
        };

        //Water wheel variables. Not great but I also don't know how else I'd do it, unless I process role derp groups in SetSelectedRole()

        public static Dictionary<Role, RoleLotInfo> RoleLotInfos { get; set; } = new Dictionary<Role, RoleLotInfo>();

        public static List<List<int>> CustomRoleGroups { get; set; } = new List<List<int>>();

        /// <summary>
        /// private field
        /// </summary>
        public static Role SelectedRole { get; set; }

        public static List<string> RandomBlabs { get; set; } = new List<string>()
        {
            "TT Arsonist",
            "Furry",
            "Tina",
            "The Dic",
            "Cus",
            "!ret",
            "Will Die N1",
            "Wood ELO",
            "VTuber",
            "Elon Musk",
            "BMG Employee",
            "Cat",
            "OBJECTION!",
            "Virgin"
        };

        internal static ManualLogSource Log;

        private void Awake()
        {
            // Plugin startup logic
            Plugin.Log = base.Logger;
            Logger.LogDebug($"Unpredictable Water Wheel: activating the mod - loading assets...");

            LoadAssets();

            
            //Settings = JsonUtility.FromJson<JsonWWSettings>(File.ReadAllText(Application.dataPath + "/Hellession/Data/iww_settings.json"));

            

            if (!Settings.disabled)
            {
                var harmony = new Harmony(PluginGUID);
                harmony.PatchAll(Assembly.GetExecutingAssembly());
                Logger.LogInfo($"Unpredictable Water Wheel has been activated!");
            }
            else
                Logger.LogInfo($"Unpredictable Water Wheel is DISABLED. To enable, change 'disabled' of the file {Application.dataPath}/Hellession/Data/iww_settings.json!");
        }
        
        void LoadAssets()
        {
            //here's how we do it
            //images: LOAD TO MEMORY
            //audio: Dump to StreamingAssets
            //assetbundle: Dump to StreamingAssets
            //Blabs: Generate and read from file. On all OS except Mac generate that file in the Data folder
            
            var assembly = typeof(Plugin).GetTypeInfo().Assembly;

            //images
            LoadSprite("UnpredictableWaterWheel.img.solidmask.png", "solidmask");
            //Stream resource = assembly.GetManifestResourceStream("UnpredictableWaterWheel._fonts.OpenSans.ttf");
            
            //audio files

            var audioFiles = new string[] { "OutOfScrolls", "ScrollConsumed" };
            var audioExtensions = new string[] { "ogg", "wav", "mp3" };
            foreach(var fileName in audioFiles)
            {
                foreach(var fileExt in audioExtensions)
                    DumpFileTo($"UnpredictableWaterWheel.audio.{fileName}.{fileExt}", Path.Combine(Application.streamingAssetsPath, $"res/WebAssets/Sound/Hellession/{fileName}.{fileExt}"));
            }

            //assetbundle
            DumpFileTo($"UnpredictableWaterWheel.assetbundle.waterwheelext.waterwheelext", Path.Combine(Application.streamingAssetsPath, $"waterwheelext.waterwheelext"));

            string customRoleGroupText = "# Enter a list of role IDs on each line separated by a command (you can add as many spaces you want) to create a role group!\n# Lines starting with a # don't count!\n# You can find role IDs by looking up the localization file StringTable.en-US.xml located in TownOfSalem_Data/StreamingAssets/res/WebAssets/XMLData. Look at the number after GUI_ROLE_ starting at line 5146.\n# Here is an example that creates a role group of Investigator, Blackmailer, Jailor and Bodyguard:\n# 3, 15, 4, 0";
            //Settings and Blabs
            if(Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
            {
                if (!Directory.Exists("Hellession"))
                    Directory.CreateDirectory("Hellession");
                if (!File.Exists("Hellession/iww_settings.json"))
                    File.WriteAllText("Hellession/iww_settings.json", JsonUtility.ToJson(new JsonWWSettings { oddsMode = OddsMode.Gigabrain, disabled = false, debugMode = false, logMode = true, enableMockGameDebug = false }, true));
                Settings = JsonUtility.FromJson<JsonWWSettings>(File.ReadAllText("Hellession/iww_settings.json"));
                //Blabs here
                RandomBlabs = GetFileWithDefaults("Hellession/iww_blabs.txt", string.Join("\n", RandomBlabs)).Split('\n').ToList();
                customRoleGroupText = GetFileWithDefaults("Hellession/iww_customRoleGroups.txt", customRoleGroupText);
            }
            else
            {
                if (!Directory.Exists(Application.dataPath + "/Hellession/Data"))
                    Directory.CreateDirectory(Application.dataPath + "/Hellession/Data");
                if (!File.Exists(Application.dataPath + "/Hellession/Data/iww_settings.json"))
                    File.WriteAllText(Application.dataPath + "/Hellession/Data/iww_settings.json", JsonUtility.ToJson(new JsonWWSettings { oddsMode = OddsMode.Gigabrain, disabled = false, debugMode = false, logMode = true, enableMockGameDebug = false }, true));
                Settings = JsonUtility.FromJson<JsonWWSettings>(File.ReadAllText(Application.dataPath + "/Hellession/Data/iww_settings.json"));
                //Blabs here
                RandomBlabs = GetFileWithDefaults(Application.dataPath + "/Hellession/Data/iww_blabs.txt", string.Join("\n", RandomBlabs)).Split('\n').ToList();
                customRoleGroupText = GetFileWithDefaults(Application.dataPath + "/Hellession/Data/iww_customRoleGroups.txt", customRoleGroupText);
            }
            //check the settings
            Settings.immediateRespinChance = Math.Max(Math.Min(0.95, Settings.immediateRespinChance), 0);
            Settings.delayedRespinChance = Math.Max(Math.Min(0.95, Settings.delayedRespinChance), 0);
            Settings.maxImmediateRespins = Math.Max(Math.Min(100, Settings.maxImmediateRespins), 0);
            Settings.maxDelayedRespins = Math.Max(Math.Min(100, Settings.maxDelayedRespins), 0);

            Settings.minImmediateFlatlineFactor = Math.Max(Math.Min(3, Settings.minImmediateFlatlineFactor), 0);
            Settings.minDelayedFlatlineFactor = Math.Max(Math.Min(3, Settings.minDelayedFlatlineFactor), 0);
            Settings.maxImmediateFlatlineFactor = Math.Max(Math.Min(3, Settings.maxImmediateFlatlineFactor), 0);
            Settings.maxDelayedFlatlineFactor = Math.Max(Math.Min(3, Settings.maxDelayedFlatlineFactor), 0);
            if(Settings.minImmediateFlatlineFactor > Settings.maxImmediateFlatlineFactor)
            {
                var pocket = Settings.minImmediateFlatlineFactor;
                Settings.minImmediateFlatlineFactor = Settings.maxImmediateFlatlineFactor;
                Settings.maxImmediateFlatlineFactor = pocket;
            }
            if (Settings.minDelayedFlatlineFactor > Settings.maxDelayedFlatlineFactor)
            {
                var pocket = Settings.minDelayedFlatlineFactor;
                Settings.minDelayedFlatlineFactor = Settings.maxDelayedFlatlineFactor;
                Settings.maxDelayedFlatlineFactor = pocket;
            }

            Settings.minImmediateSpinStrength = Math.Max(Math.Min(13000, Settings.minImmediateSpinStrength), 200);
            Settings.minDelayedSpinStrength = Math.Max(Math.Min(13000, Settings.minDelayedSpinStrength), 200);
            Settings.maxImmediateSpinStrength = Math.Max(Math.Min(13000, Settings.maxImmediateSpinStrength), 200);
            Settings.maxDelayedSpinStrength = Math.Max(Math.Min(13000, Settings.maxDelayedSpinStrength), 200);
            if (Settings.minImmediateSpinStrength > Settings.maxImmediateSpinStrength)
            {
                var pocket = Settings.minImmediateSpinStrength;
                Settings.minImmediateSpinStrength = Settings.maxImmediateSpinStrength;
                Settings.maxImmediateSpinStrength = pocket;
            }
            if (Settings.minDelayedSpinStrength > Settings.maxDelayedSpinStrength)
            {
                var pocket = Settings.minDelayedSpinStrength;
                Settings.minDelayedSpinStrength = Settings.maxDelayedSpinStrength;
                Settings.maxDelayedSpinStrength = pocket;
            }
            Settings.customRoleGroupWeight = Math.Max(Settings.customRoleGroupWeight, 0);
            Settings.investResultWeight = Math.Max(Settings.investResultWeight, 0);
            Settings.subalignmentWeight = Math.Max(Settings.subalignmentWeight, 0);
            Settings.factionWeight = Math.Max(Settings.factionWeight, 0);
            Settings.roleGroupSpawnChance = Math.Max(Math.Min(0.95, Settings.roleGroupSpawnChance), 0);
            Settings.maxRoleGroups = Math.Max(Math.Min(30, Settings.maxRoleGroups), 0);
            Settings.blabSpawnChance = Math.Max(Math.Min(1, Settings.blabSpawnChance), 0);

            Settings.minArrowheadSlotCoverage = Math.Max(Math.Min(0.5, Settings.minArrowheadSlotCoverage), 0);
            Settings.maxArrowheadSlotCoverage = Math.Max(Math.Min(0.5, Settings.maxArrowheadSlotCoverage), 0);
            if (Settings.minArrowheadSlotCoverage > Settings.maxArrowheadSlotCoverage)
            {
                var pocket = Settings.minArrowheadSlotCoverage;
                Settings.minArrowheadSlotCoverage = Settings.maxArrowheadSlotCoverage;
                Settings.maxArrowheadSlotCoverage = pocket;
            }
            Settings.minFlatlineFalloffSpeed = Math.Max(Math.Min(50, Settings.minFlatlineFalloffSpeed), 1);
            Settings.maxFlatlineFalloffSpeed = Math.Max(Math.Min(50, Settings.maxFlatlineFalloffSpeed), 1);
            if (Settings.minFlatlineFalloffSpeed > Settings.maxFlatlineFalloffSpeed)
            {
                var pocket = Settings.minFlatlineFalloffSpeed;
                Settings.minFlatlineFalloffSpeed = Settings.maxFlatlineFalloffSpeed;
                Settings.maxFlatlineFalloffSpeed = pocket;
            }
            CustomRoleGroups = new List<List<int>>();
            //parse the custom role group file
            customRoleGroupText = customRoleGroupText.Replace(" ", "");
            foreach(var kv in customRoleGroupText.Split('\n'))
            {
                if(!string.IsNullOrEmpty(kv) && !kv.StartsWith("#"))
                {
                    var splitUpRoleIDs = kv.Split(',');
                    List<int> roleIDs = new List<int>();
                    foreach(var roleIDStr in splitUpRoleIDs)
                    {
                        if(int.TryParse(roleIDStr, out int roleID))
                            roleIDs.Add(roleID);
                        else
                            Plugin.Log.LogWarning($"Failed to parse {roleIDStr} as an integer for a custom role group.");
                    }
                    CustomRoleGroups.Add(roleIDs);
                }
            }

            Log.LogInfo($"- Current settings for the Improved Water Wheel mod -\n"
            + $"OddsMode = {Settings.oddsMode}\n"
            + $"RoleAppearanceMode = {Settings.roleApparanceMode}\n"
            + $"immediateRespinChance = {Settings.immediateRespinChance}\n"
            + $"delayedRespinChance = {Settings.delayedRespinChance}\n"
            + $"maxImmediateRespins = {Settings.maxImmediateRespins}\n"
            + $"maxDelayedRespins = {Settings.maxDelayedRespins}\n"
            + $"minImmediateFlatlineFactor = {Settings.minImmediateFlatlineFactor}\n"
            + $"minDelayedFlatlineFactor = {Settings.minDelayedFlatlineFactor}\n"
            + $"maxImmediateFlatlineFactor = {Settings.maxImmediateFlatlineFactor}\n"
            + $"maxDelayedFlatlineFactor = {Settings.maxDelayedFlatlineFactor}\n"
            + $"customRoleGroupWeight = {Settings.customRoleGroupWeight}\n"
            + $"investResultWeight = {Settings.investResultWeight}\n"
            + $"subalignmentWeight = {Settings.subalignmentWeight}\n"
            + $"factionWeight = {Settings.factionWeight}\n"
            + $"roleGroupSpawnChance = {Settings.roleGroupSpawnChance}\n"
            + $"maxRoleGroups = {Settings.maxRoleGroups}\n"
            + $"blabSpawnChance = {Settings.blabSpawnChance}\n"
            + $"customRoleGroups Count = {CustomRoleGroups.Count}\n"
            + $"RandomBlabs Count = {RandomBlabs.Count}\n"
            + $"minArrowheadSlotCoverage = {Settings.minArrowheadSlotCoverage}\n"
            + $"maxArrowheadSlotCoverage = {Settings.maxArrowheadSlotCoverage}\n"
            + $"minFlatlineFalloffSpeed = {Settings.minFlatlineFalloffSpeed}\n"
            + $"maxFlatlineFalloffSpeed = {Settings.maxFlatlineFalloffSpeed}");
        }
        
        public static void DumpFileTo(string manifest, string to)
        {
            if(File.Exists(to))
                return;
            var assembly = Assembly.GetExecutingAssembly();
            byte[] bytes = ReadFully(assembly.GetManifestResourceStream(manifest));
            //make sure the directory exists
            string housingDirectory = Path.GetDirectoryName(to);
            if(!Directory.Exists(housingDirectory))
                Directory.CreateDirectory(housingDirectory);
            File.WriteAllBytes(to, bytes);
        }

        public static string GetFileWithDefaults(string loc, string defaultVal)
        {
            if (!Directory.Exists(Path.GetDirectoryName(loc)))
                Directory.CreateDirectory(Path.GetDirectoryName(loc));
            if (!File.Exists(loc))
                File.WriteAllText(loc, defaultVal);
            return File.ReadAllText(loc);
        }
        
        public static Sprite LoadSprite(string loc, string key)
        {
            var assembly = Assembly.GetExecutingAssembly();
            byte[] bytes = ReadFully(assembly.GetManifestResourceStream(loc));
            var tex2d = new Texture2D(1, 1);
            bool status = tex2d.LoadImage(bytes);
            Sprite result;
            if(status)
            {
                result = Sprite.Create(tex2d, new Rect(0f, 0f, tex2d.width, tex2d.height), new Vector2(0, 0));
                Plugin.ModdedSprites.Add(key, result);
            }
            else
            {
                result = Sprite.Create(Texture2D.redTexture, new Rect(0f, 0f, 1f, 1f), new Vector2(0, 0));
                Plugin.ModdedSprites.Add(key, result);
                Log.LogWarning($"Failed to load embedded sprite {loc}!");
            }
            return result;
        }
        
        public static byte[] ReadFully(Stream stream)
        {
            byte[] result;
            using(var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                result = memoryStream.ToArray();
            }
            return result;
        }
    }
}
