using BepInEx;
using UnityEngine;
using HarmonyLib;
using BepInEx.Logging;
using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using System.Linq;
using UnityEngine.U2D;
using UnityEngine.UI;
using Hellession;
using System.IO;
using TMPro;

namespace UnpredictableWaterWheel
{

    /*
    [HarmonyPatch(typeof(ApplicationController), "InitializeEnvironment")]
    public class PatchInitializeEnvironment
    {
        public static GameObject PrefabToInstantiate { get; set; }

        static void Postfix(ApplicationController __instance, ref GameObject ___DebugPanel)
        {
            //var go = GameObject.Find("Login");
            Plugin.Log.LogInfo($"Postfix ApplicationController.InitializeEnvironment() FIRED.");
            if(__instance.DebugPanelPrefab is null)
                Plugin.Log.LogInfo($"__instance.DebugPanelPrefab is null :(");
            else
            {
                Plugin.Log.LogInfo($"__instance.DebugPanelPrefab ISN'T NULL!!");
                Plugin.Log.LogInfo($"__instance.DebugPanelPrefab active: {__instance.DebugPanelPrefab.activeInHierarchy}");
                Plugin.Log.LogInfo($"__instance.DebugPanelPrefab active self: {__instance.DebugPanelPrefab.activeSelf}");
                if(__instance.DebugPanelPrefab.transform.parent is null)
                    Plugin.Log.LogInfo($"__instance.DebugPanelPrefab PARENT is NULL!");
                else
                Plugin.Log.LogInfo($"__instance.DebugPanelPrefab PARENT: {__instance.DebugPanelPrefab.transform.parent}");
                var allMyComponents = __instance.DebugPanelPrefab.GetComponents<Component>();
                var allChildComponents = __instance.DebugPanelPrefab.GetComponentsInChildren<Component>();
                foreach(var kv in allMyComponents)
                    Plugin.Log.LogInfo($"__instance.DebugPanelPrefab Direct Component {kv.GetType()}, named {kv.name}");
                try
                {
                    foreach (var kv in allChildComponents)
                    {
                        if(kv is null)
                            Plugin.Log.LogInfo($"__instance.DebugPanelPrefab CHILD Component NULL?!");
                        else if(kv is Behaviour bh)
                        {
                            if (kv.transform is null)
                                Plugin.Log.LogInfo($"__instance.DebugPanelPrefab CHILD Component {kv.GetType()}, named {kv.name}, enabled {bh.enabled} on NULL Transform?!");
                            else if (kv.transform.parent is null)
                                Plugin.Log.LogInfo($"__instance.DebugPanelPrefab CHILD Component {kv.GetType()}, named {kv.name}, enabled {bh.enabled}, on object sibling index {kv.transform.GetSiblingIndex()} under NULL Parent!");
                            else
                                Plugin.Log.LogInfo($"__instance.DebugPanelPrefab CHILD Component {kv.GetType()}, named {kv.name}, enabled {bh.enabled}, on object sibling index {kv.transform.GetSiblingIndex()} under parent of {kv.transform.parent.name}");
                        }
                        else if (kv.transform is null)
                            Plugin.Log.LogInfo($"__instance.DebugPanelPrefab CHILD Component {kv.GetType()}, named {kv.name} on NULL Transform?!");
                        else if (kv.transform.parent is null)
                            Plugin.Log.LogInfo($"__instance.DebugPanelPrefab CHILD Component {kv.GetType()}, named {kv.name}, on object sibling index {kv.transform.GetSiblingIndex()} under NULL Parent!");
                        else
                            Plugin.Log.LogInfo($"__instance.DebugPanelPrefab CHILD Component {kv.GetType()}, named {kv.name}, on object sibling index {kv.transform.GetSiblingIndex()} under parent of {kv.transform.parent.name}");
                    }
                }
                catch(Exception e)
                {
                    Plugin.Log.LogWarning($"__instance.DebugPanelPrefab indexing error: {e}");
                }
                __instance.DebugPanelPrefab.SetActive(true);
                PrefabToInstantiate = __instance.DebugPanelPrefab;
            }
            return;
            if (___DebugPanel is null)
                Plugin.Log.LogInfo($"___DebugPanel is null :(");
            else
            {
                Plugin.Log.LogInfo($"___DebugPanel ISN'T NULL!!");
                Plugin.Log.LogInfo($"___DebugPanel active: {___DebugPanel.activeInHierarchy}");
                Plugin.Log.LogInfo($"___DebugPanel active self: {___DebugPanel.activeSelf}");
                var allMyComponents = ___DebugPanel.GetComponents<Component>();
                var allChildComponents = ___DebugPanel.GetComponentsInChildren<Component>();
                foreach (var kv in allMyComponents)
                    Plugin.Log.LogInfo($"___DebugPanel Direct Component {kv.GetType()}, named {kv.name}");
                try
                {
                    foreach (var kv in allChildComponents)
                        Plugin.Log.LogInfo($"___DebugPanel CHILD Component {kv.GetType()}, named {kv.name}, on object sibling index {kv.transform.GetSiblingIndex()} under parent of {kv.transform.parent.name}");
                }
                catch (Exception e)
                {
                    Plugin.Log.LogWarning($"___DebugPanel indexing error: {e}");
                }
            }
        }
    }

    [HarmonyPatch(typeof(BaseLoginSceneController), nameof(BaseLoginSceneController.Start))]
    public class PatchLoginControllerStart
    {
        static void Postfix(BaseLoginSceneController __instance)
        {
            //var go = GameObject.Find("Login");
            Plugin.Log.LogInfo($"Postfix BaseLoginSceneController.Start() FIRED.");
            var ttt = GameObject.Instantiate(PatchInitializeEnvironment.PrefabToInstantiate,
            __instance.LoginPanel.transform);
            MonoBehaviour.DontDestroyOnLoad(ttt);
        }
    }
    */
    
    /*
    [HarmonyPatch(typeof(BaseHomeSceneController), "Start")]
    public class PatchHomeControllerStart
    {
        static void Postfix(BaseHomeSceneController __instance)
        {
            //var go = GameObject.Find("Login");
            Plugin.Log.LogInfo($"Postfix BaseHomeSceneController.Start() FIRED.");
            __instance.StartCoroutine(WaitABit(__instance));
        }
        
        static IEnumerator WaitABit(BaseHomeSceneController __instance)
        {
            yield return new WaitForSeconds(5);
            __instance.gameObject.AddComponent<GameSimulator>();
        }
    }

    [HarmonyPatch(typeof(ApplicationController), nameof(ApplicationController.InitializeApplicationController))]
    public class PatchInitializeApplicationControllerDebug
    {

        static void Prefix(ApplicationController __instance)
        {
            Plugin.Log.LogInfo($"Postfix ApplicationController.InitializeApplicationController() FIRED.");
            __instance.ServiceLocator = new TypeReferences.ClassTypeReference(typeof(DebugServiceLocator));
        }

        static void Postfix(ApplicationController __instance)
        {
            Plugin.Log.LogInfo($"The Global service locator is of type {GlobalServiceLocator.Instance.GetType()}");
        }
    }
    */
    
    //DEBUG CODE HERE
    [HarmonyPatch(typeof(AchievementsGenreListItem), "OnGenreButtonClick")]
    class PatchAchievementGenreClick
    {
        //DEBUG: Immediately send the player to a mock game to test the water wheel.
        static void Postfix()
        {
            if(!Plugin.Settings.enableMockGameDebug)
                return;
            Plugin.Log.LogInfo($"Postfix AchievementsGenreListItem.OnGenreButtonClick() FIRED. Trying to start a mock game...");
            var newGO = GameObject.Instantiate(new GameObject("Attacherrr", new Type[] { typeof(DummyMB) })).GetComponent<DummyMB>();
            var gr = GlobalServiceLocator.GameRulesService.GetGameRules();
            var game = GlobalServiceLocator.GameService;
            if (gr.GameModes.ContainsKey(8))
            {
                game.ActiveGameState = new GameState();
                game.ActiveGameState.GameMode = gr.GameModes[8];
                int num = game.ActiveGameState.GameMode.TimingProfile["pick_names"];
                DateTime pickNamesEndTime = DateTime.Now.Add(TimeSpan.FromSeconds((double)num));
                game.PickNamesEndTime = pickNamesEndTime;
                Traverse.Create(game).Method("RaiseOnGameStarted", 8).GetValue(8);
                newGO.StartCoroutine(Waiter(game, gr));
                return;
            }
            Plugin.Log.LogInfo($"INVALID GAMEMODE");
        }
        
        static IEnumerator Waiter(IGameService myService, GameRules gr)
        {
            yield return new WaitForSeconds(2);
            //pretend as if the water wheel is starting
            myService.ActiveGameState.RoleList = gr.GameModes[8].RoleList;
            List<RoleLotInfo> roleLotInfos = new List<RoleLotInfo>();
            HLSNRandomable<int> myRoleRandomable = new HLSNRandomable<int>();
            for (int i = 0; i < 33;i++)
            {
                int individual = 10;
                int total = 150;
                /*
                switch(HLSNUtil.GetRandomInt(1,15))
                {
                    case 1:
                    case 2:
                        total = 240;
                        break;
                    case 3:
                        individual = 100;
                        total = 240;
                        break;
                    case 4:
                        total = 330;
                        break;
                }
                if(HLSNUtil.GetRandomInt(1, 40) == 40)
                {
                    individual = 100;
                    total = 330;
                }
                if (HLSNUtil.GetRandomInt(1, 70) == 70)
                {
                    individual = 100;
                    total = 420;
                }
                if (HLSNUtil.GetRandomInt(1, 50) == 50)
                {
                    total = 420;
                }
                if (HLSNUtil.GetRandomInt(1, 70) == 70)
                {
                    total = 510;
                }*/
                myRoleRandomable.chanceTable.Add(i, individual);
                roleLotInfos.Add(new RoleLotInfo(i, total, individual));
            }
            for (int i = 46; i < 62; i++)
            {
                int individual = 10;
                int total = 150;
                /*
                switch (HLSNUtil.GetRandomInt(1, 15))
                {
                    case 1:
                    case 2:
                        total = 240;
                        break;
                    case 3:
                        individual = 100;
                        total = 240;
                        break;
                    case 4:
                        total = 330;
                        break;
                }
                if (HLSNUtil.GetRandomInt(1, 40) == 40)
                {
                    individual = 100;
                    total = 330;
                }
                if (HLSNUtil.GetRandomInt(1, 70) == 70)
                {
                    individual = 100;
                    total = 420;
                }
                if (HLSNUtil.GetRandomInt(1, 50) == 50)
                {
                    total = 420;
                }
                if (HLSNUtil.GetRandomInt(1, 70) == 70)
                {
                    total = 510;
                }*/
                myRoleRandomable.chanceTable.Add(i, individual);
                roleLotInfos.Add(new RoleLotInfo(i, total, individual));
            }
            //TUBA'S MOD TEST!!! REMOVE LATER!
            //for (int i = 300; i < 304; i++)
            {
                //int individual = 10;
                //int total = 150;
                /*
                switch (HLSNUtil.GetRandomInt(1, 15))
                {
                    case 1:
                    case 2:
                        total = 240;
                        break;
                    case 3:
                        individual = 100;
                        total = 240;
                        break;
                    case 4:
                        total = 330;
                        break;
                }
                if (HLSNUtil.GetRandomInt(1, 40) == 40)
                {
                    individual = 100;
                    total = 330;
                }
                if (HLSNUtil.GetRandomInt(1, 70) == 70)
                {
                    individual = 100;
                    total = 420;
                }
                if (HLSNUtil.GetRandomInt(1, 50) == 50)
                {
                    total = 420;
                }
                if (HLSNUtil.GetRandomInt(1, 70) == 70)
                {
                    total = 510;
                }*/
                //myRoleRandomable.chanceTable.Add(i, individual);
                //roleLotInfos.Add(new RoleLotInfo(i, total, individual));
            }
            /*
            for (int i = 305; i < 306; i++)
            {
                int individual = 10;
                int total = 150;
                switch (HLSNUtil.GetRandomInt(1, 15))
                {
                    case 1:
                    case 2:
                        total = 240;
                        break;
                    case 3:
                        individual = 100;
                        total = 240;
                        break;
                    case 4:
                        total = 330;
                        break;
                }
                if (HLSNUtil.GetRandomInt(1, 40) == 40)
                {
                    individual = 100;
                    total = 330;
                }
                if (HLSNUtil.GetRandomInt(1, 70) == 70)
                {
                    individual = 100;
                    total = 420;
                }
                if (HLSNUtil.GetRandomInt(1, 50) == 50)
                {
                    total = 420;
                }
                if (HLSNUtil.GetRandomInt(1, 70) == 70)
                {
                    total = 510;
                }
                myRoleRandomable.chanceTable.Add(i, individual);
                roleLotInfos.Add(new RoleLotInfo(i, total, individual));
            }
            */
            myService.ActiveGameState.CachedRoleLotInfo = roleLotInfos;
            Plugin.Log.LogInfo($"Passing role lot infos with {roleLotInfos.Count} items!!!");
            Traverse.Create(myService).Method("RaiseOnRoleLotsInfo", roleLotInfos).GetValue(roleLotInfos);
            int characterId = 90;
            int petId = 45;
            int houseId = 0;
            for (int i = 0; i < 15; i++)
            {
                Player player = new Player(i);
                player.Name = "Player " + i.ToString();
                player.CharacterId = characterId;
                player.PetId = petId;
                player.HouseId = houseId;
                player.DeathAnimation = DeathAnimation.Rock;
                myService.ActiveGameState.Players.Add(player);
            }
            int myID = HLSNUtil.GetRandomInt(0, 14);
            myService.ActiveGameState.Me = myService.ActiveGameState.Players[myID];
            myService.ActiveGameState.Me.Name = "Hellession";
            int myRoleID = myRoleRandomable.GetRandom(false);
            myService.ActiveGameState.Me.CurrentRole = myService.ActiveGameRules.Roles[myRoleID];
            myService.ActiveGameState.Me.AbilitiesRemaining = myService.ActiveGameState.cachedAbilitiesRemaining;
            Plugin.Log.LogInfo($"You are role {myService.ActiveGameState.Me.CurrentRole.Name} and your position is {myID + 1}!!");
            /*
            if (roleAndPositionMessage.TargetPosition != null)
            {
                this.ActiveGameState.Players[roleAndPositionMessage.TargetPosition.Value].Tags.Set(PlayerTag.GoalTarget);
                this.ActiveGameState.Me.GoalTarget = this.ActiveGameState.Players[roleAndPositionMessage.TargetPosition.Value];
                if (this.ActiveGameState.Me.CurrentRole.Tags.IsSet(RoleTag.GoalTargetIsFixedTarget))
                {
                    this.ActiveGameState.Me.FixedTarget = this.ActiveGameState.Players[roleAndPositionMessage.TargetPosition.Value];
                }
            }*/
            if(GlobalServiceLocator.UserService.Selections.scrolls.Count() > 0)
                ScrollData = GlobalServiceLocator.CustomizationService.GetScrollData(myRoleID);
                //ScrollData = GlobalServiceLocator.CustomizationService.GetScrollData(3);
            else
                ScrollData = null;
            Traverse.Create(myService).Method("SetGamePhase", GameState.Phase.RoleInfo).GetValue(GameState.Phase.RoleInfo);
            Traverse.Create(myService).Method("RaiseOnRoleInfo").GetValue();
            yield return new WaitForSeconds(25);
            Traverse.Create(myService).Method("RaiseOnFirstDayTransition").GetValue();
        }

        public static CustomizationScrollData ScrollData { get; set; } = null;
    }

    [HarmonyPatch(typeof(WaterWheelController), nameof(WaterWheelController.SetSelectedRole))]
    class AnotherPatch
    {
        static void Postfix(Role role, Dictionary<Role, RoleLotInfo> ___roleLotInfo_)
        {
            Plugin.Log.LogInfo($"POSTFIX TRIGGERED: WaterWheelController.SetSelectedRole()");
            Plugin.SelectedRole = role;
        }
    }
    
    [HarmonyPatch(typeof(WaterWheelController), nameof(WaterWheelController.SetRoleLotInfo))]
    class PatchSetRoleLotInfo
    {
        static void Postfix(Dictionary<Role, RoleLotInfo> roleLotInfo, WaterWheelController __instance)
        {
            Plugin.RoleLotInfos = new Dictionary<Role, RoleLotInfo>(roleLotInfo);
            Plugin.Log.LogInfo($"Postfix WaterWheelController.SetRoleLotInfo() FIRED. RoleLotInfo has {roleLotInfo.Count} items");
        }
    }

    [HarmonyPatch(typeof(WaterWheelController), nameof(WaterWheelController.Spin))]
    class PatchSpin
    {
        static bool Prefix(WaterWheelController __instance, ref CustomizationScrollData ___consumedScroll_)
        {
            //SET THIS TO FALSE TO ALTER ANIMATION!!
            LogWaterWheelInfo($"Postfix WaterWheelController.Spin() FIRED.");
            Image woman = GameObject.Find("WW_Girl").GetComponent<Image>();
            Image arrowhead = GameObject.Find("Clicker").GetComponent<Image>();

            if (PatchAchievementGenreClick.ScrollData != null)
                __instance.SetScrollConsumed(PatchAchievementGenreClick.ScrollData);
                
                
            /*
            for (int i2 = 0; i2 < __instance.WheelAnimator.layerCount;i2++)
            {
                var clips = __instance.WheelAnimator.GetCurrentAnimatorClipInfo(i2);
                for (int i3 = 0; i3 < clips.Length;i3++)
                {
                    var clip = clips[i3];
                    Plugin.Log.LogInfo($"Animator Clip {i3} of layer {i2}. Weight: {clip.weight}. Name: {clip.clip.name}. Duration: {clip.clip.averageDuration}. Length: {clip.clip.length}");
                    for (int i4 = 0; i4 < clip.clip.events.Length;i4++)
                    {
                        var currEvent = clip.clip.events[i4];
                        Plugin.Log.LogInfo($"Event {i4}. Function name: {currEvent.functionName}. Message Options: {currEvent.messageOptions}. Int Parameter: {currEvent.intParameter}. Float Parameter: {currEvent.floatParameter}. String Parameter: {currEvent.stringParameter}. Object Reference Parameter: {currEvent.objectReferenceParameter}. Time: {currEvent.time}. Object Reference Parameter TYPE: {(currEvent.objectReferenceParameter != null ? currEvent.objectReferenceParameter.GetType().ToString() : "null")}.");
                    }
                }
            }
            int i = 0;
            foreach(var kv in __instance.WheelAnimator.parameters)
            {
                Plugin.Log.LogInfo($"Animator Parameter {i}! Type: {kv.type}, Name: {kv.name}, m_name: {kv.m_Name}, DefaultBool: {kv.defaultBool}, DefaultFloat: {kv.defaultFloat}, DefaultInt: {kv.defaultInt}");
                i++;
            }*/
            TextMeshProUGUI selectedRole = __instance.SelectedRoleSlot.OddsLabel;
            Graphic[] animatedGraphics = new Graphic[] { woman, selectedRole };
            __instance.StartCoroutine(AnimateWaterWheel(__instance, woman, arrowhead, ___consumedScroll_));
            return true;
        }

        public const double ImmediateSpinDelay = 0.5;
        public const double ImmediateSpinPeakTime = 0.327;
        public const float RoleSlotHeight = 175.3821f;

        public static double RoleSlotHeightReal { get; set; }

        public static double FlatlineFalloffPerSec { get; set; }

        public static double FlatlineFallOffPerFrame { get; set; }
        
        public static double ArrowheadOffset { get; set; }

        public static bool LogMode {get{
                return Plugin.Settings.logMode;
            }}
        public static bool DebugMode {get{
                return Plugin.Settings.debugMode;
        }}
        
        static void LogWaterWheelInfo(object message)
        {
            if(LogMode)
                Plugin.Log.LogDebug($"[{DateTime.Now.ToString("s")}] {message.ToString()}");
        }

        //public static List<string> WomanTextures { get; set; } = new List<string>();

        static List<Sprite> WaterWheelGirlAtlas { get; set; } = new List<Sprite>();

        public static List<Sprite> FireScreenwipeAtlas { get; set; } = new List<Sprite>();
        static List<Sprite> ScrollFireAtlas { get; set; } = new List<Sprite>();

        static IEnumerator AnimateWaterWheel(WaterWheelController __instance, Image woman, Image arrowhead, CustomizationScrollData consumedScroll_)
        {
            LogWaterWheelInfo($"Water wheel animation activated. Preloading sounds, textures, sprites and shaders...");

            __instance.PreloadSounds(new string[]
            {
            "Sound/Hellession/ScrollConsumed",
            "Sound/Hellession/OutOfScrolls"
            });
            AssetBundle myLoadedAssetBundle = null;
            foreach(var bundle in AssetBundle.GetAllLoadedAssetBundles())
            {
                if(bundle.name == "waterwheelext.waterwheelext")
                {
                    myLoadedAssetBundle = bundle;
                    break;
                }
            }
            if(myLoadedAssetBundle == null)
                myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "waterwheelext.waterwheelext"));
            if (myLoadedAssetBundle == null)
                Plugin.Log.LogError("Failed to load AssetBundle!");
            var allSprites = Resources.FindObjectsOfTypeAll<Sprite>();
            foreach (var kv in allSprites)
            {
                if (kv.name.StartsWith("ScrollFire_000"))
                    ScrollFireAtlas.Add(kv);
                if (ScrollFireAtlas.Count >= 6)
                    break;
            }
            ScrollFireAtlas = (from g in ScrollFireAtlas orderby g.name select g).ToList();
            var spriteAtlases = Resources.FindObjectsOfTypeAll<SpriteAtlas>();
            foreach(var kv in spriteAtlases)
            {
                /*
                Sprite[] sprs2 = new Sprite[kv.spriteCount];
                kv.GetSprites(sprs2);
                foreach(var sprite in sprs2)
                {
                    Plugin.Log.LogInfo($"Of atlas {kv.name}, sprite {sprite.name} is {sprite.rect}");
                }*/
                if(kv.name == "Game-WaterWheelGirl")
                {
                    Sprite[] sprs = new Sprite[kv.spriteCount];
                    kv.GetSprites(sprs);
                    WaterWheelGirlAtlas = (from g in sprs orderby g.name select g).ToList();
                }
                else if(kv.name == "FireScreenWipe")
                {
                    Sprite[] sprs = new Sprite[kv.spriteCount];
                    kv.GetSprites(sprs);
                    FireScreenwipeAtlas = (from g in sprs orderby g.name select g).ToList();
                }
                if(WaterWheelGirlAtlas.Count > 0 && FireScreenwipeAtlas.Count > 0)
                    break;
            }

            WaterWheelRoleController otherRole0 = __instance.OtherRoleSlots[0];
            WaterWheelRoleController otherRole1 = __instance.OtherRoleSlots[1];
            WaterWheelRoleController selectedRole = __instance.SelectedRoleSlot;
            WaterWheelRoleController otherRole25 = __instance.OtherRoleSlots[25];
            RectTransform gigaParent = (RectTransform)otherRole0.transform.parent.parent.parent;
            Rect gigaParentSizes = gigaParent.rect;
            RoleSlotHeightReal = ((RectTransform)selectedRole.transform).sizeDelta.y;
            LogWaterWheelInfo($"The real slot height has been calculated at {RoleSlotHeightReal}.");
            /*
            Plugin.Log.LogInfo($"Selected Role: Name {selectedRole.gameObject.name} Size Delta Y {((RectTransform)selectedRole.transform).sizeDelta.y} Anchor min {((RectTransform)selectedRole.transform).anchorMin} Anchor max {((RectTransform)selectedRole.transform).anchorMax}");
            Plugin.Log.LogInfo($"PARENT - Name {((RectTransform)selectedRole.transform.parent).name} Size {((RectTransform)selectedRole.transform.parent).sizeDelta} Anchor MIN {((RectTransform)selectedRole.transform.parent).anchorMin} Anchor MAX {((RectTransform)selectedRole.transform.parent).anchorMax}\n"+
            $"SUPER PARENT - Name {superParent.gameObject.name} Position {superParent.localPosition} Size {superParent.sizeDelta} Anchor MIN {superParent.anchorMin} Anchor MAX {superParent.anchorMax}");
            Component[] behaviors = selectedRole.transform.parent.gameObject.GetComponents<Component>();
            for (int mb = 0; mb < behaviors.Length; mb++)
                LogWaterWheelInfo($"Component {mb} on PARENT: Name {behaviors[mb].name} Type {behaviors[mb].GetType()}");
            behaviors = superParent.gameObject.GetComponents<Component>();
            for (int mb = 0; mb < behaviors.Length; mb++)
                LogWaterWheelInfo($"Component {mb} on SUPER PARENT: Name {behaviors[mb].name} Type {behaviors[mb].GetType()}");
            behaviors = superParent.parent.gameObject.GetComponents<Component>();
            for (int mb = 0; mb < behaviors.Length; mb++)
                LogWaterWheelInfo($"Component {mb} on MEGA GIGA PARENT: Name {behaviors[mb].name} Type {behaviors[mb].GetType()}");
            
            for (int i = 0; i < __instance.OtherRoleSlots.Length;i++)
            {
                WaterWheelRoleController myRoleController = __instance.OtherRoleSlots[i];
                Plugin.Log.LogInfo($"Other Role {i}: Name {myRoleController.gameObject.name} Anchor min {((RectTransform)myRoleController.transform).anchorMin} Anchor max {((RectTransform)myRoleController.transform).anchorMax}");
                behaviors = myRoleController.gameObject.GetComponents<Component>();
                for (int mb = 0; mb < behaviors.Length;mb++)
                    LogWaterWheelInfo($"Component {mb} on Other role {i}: Name {behaviors[mb].name} Type {behaviors[mb].GetType()}");
            }
            var listPlaces = UnityEngine.Object.FindObjectsOfType<RoleListItemController>();
            for (int mb = 0; mb < listPlaces.Length; mb++)
                LogWaterWheelInfo($"RoleListItemController {mb} on {listPlaces[mb].gameObject.name} game object under parent {listPlaces[mb].transform.parent.gameObject.name}: Name {listPlaces[mb].name} Type {listPlaces[mb].GetType()}");
            selectedRole.transform.parent.GetComponent<VerticalLayoutGroup>().enabled = true;
            */

            /*
            for (int i = 0; i < 800;i++)
            {
                Plugin.Log.LogInfo($"\"{woman.rectTransform.position}\",\"{woman.rectTransform.localPosition}\",\"{woman.rectTransform.sizeDelta}\",\"{woman.rectTransform.rect}\",\"{woman.rectTransform.anchorMin}\",\"{woman.rectTransform.anchorMax}\",\"{woman.sprite.name}\",\"{woman.sprite.rect}\",\"{woman.sprite.textureRect}\",\"{woman.sprite.textureRectOffset}\",\"{woman.sprite.texture.name}\""
                );
                yield return null;
            }
            */
            __instance.WheelAnimator.enabled = false;
            
            /*
            Transform scrollGO = gigaParent.Find("Scroll");
            Transform scrollFire1GO = gigaParent.Find("ScrollFire (1)");
            Transform scrollFireGO = gigaParent.Find("ScrollFire");
            if(scrollGO is null)
                Plugin.Log.LogInfo($"ScrollGO is null. It's probably inactive");
            else
            {
                Plugin.Log.LogInfo($"ScrollGO isn't null!");
                scrollGO.gameObject.SetActive(true);
                Image scrollImage = GameObject.Find("Scroll").GetComponent<Image>();
                if(scrollImage is null)
                    Plugin.Log.LogInfo($"...but it doesn't have an image");
                else
                    scrollImage.rectTransform.localPosition = new Vector2(-600, 400);
            }
            if (scrollFire1GO is null)
                Plugin.Log.LogInfo($"scrollFire1GO is null. It's probably inactive");
            else
            {
                Plugin.Log.LogInfo($"scrollFire1GO isn't null!");
                scrollFire1GO.gameObject.SetActive(true);
                Image scrollImage = GameObject.Find("ScrollFire (1)").GetComponent<Image>();
                if (scrollImage is null)
                    Plugin.Log.LogInfo($"...but it doesn't have an image");
                else
                    scrollImage.rectTransform.localPosition = new Vector2(-600, 0);
            }
            if (scrollFireGO is null)
                Plugin.Log.LogInfo($"scrollFireGO is null. It's probably inactive");
            else
            {
                Plugin.Log.LogInfo($"scrollFireGO isn't null!");
                scrollFireGO.gameObject.SetActive(true);
                Image scrollImage = GameObject.Find("ScrollFire").GetComponent<Image>();
                if (scrollImage is null)
                    Plugin.Log.LogInfo($"...but it doesn't have an image");
                else
                    scrollImage.rectTransform.localPosition = new Vector2(-600, -400);
            }
            */

            /*
                Arrowhead can tilt up to 25 Degress. However Arrowhead is NOT fully level by default!
                the Arrowhead can cover something like 35% of a role slot
                Size of each role slot on water wheel: 175,4 units HIGH
                NEVER worry about the X coordinate on the role slots, let every role slot's function run, because it does the job well with fucky wucky anchors.

                The water wheel completely halts before the spin for the final time on frame 190 or in 1,31944444444 seconds.
                The woman starts spinning the water wheel on frame 269 (on my screen) or in 1,86805555556 seconds.

                Peak water wheel spin speed (in vanilla) is 36 units per frame or 5184 units per second. (Frame 316)
                It takes 47 frames for it to hit its speed peak or 0,326388888889 seconds.
                The water wheel's speed rises parabolically until it hits the peak and falls parabolically until it hits 10 units per frame or 1440 units per second.
                The speed conitnues to decline at somewhere around a fixed 0.03 units per frame or 4,32 units per second.

                STEPS!
                1. Figure out how the woman will spin the water wheel (how many spins? Will she spin again and when?)
                2. Calculate where the selected role will be and how many roles total there will be.
                3. Create all the necessary role slots and put them all neatly into their places.
                4. Calculate and figure out all the fake roles.
                5. Play the animation!
                6. Animate the role information panel.
                7. Transition into the main game.
            */

            //STEP 1
            LogWaterWheelInfo($"STEP 1 STARTED");
            int immediateRespins = 1;
            int delayedRespins = 0;
            while (HLSNUtil.GetRandomDouble() < Plugin.Settings.immediateRespinChance && immediateRespins < Plugin.Settings.maxImmediateRespins + 1)
                immediateRespins++;
            int maximumDelayedRespins = Plugin.Settings.maxDelayedRespins - ((immediateRespins - 1) / 3);
            while (HLSNUtil.GetRandomDouble() < Plugin.Settings.delayedRespinChance && delayedRespins < maximumDelayedRespins)
                delayedRespins++;
            LogWaterWheelInfo($"Immediate respins: {immediateRespins}. Delayed Respins: {delayedRespins}");
            //STEP 2
            LogWaterWheelInfo($"STEP 2");
            //In order to do this well, I need to 'plot' the spins that are going to take place in function graphs.
            //get those ready...

            //the small pull the woman does downward at the start is completely ignored and not accounted for. Use a simple HLSNAnimator Move for that using the function EaseInOutQuad.
            //Afterward... well, we'll need to calculate...
            //this is hard, we need to use UNITS PER x. x!!! Not x. No, we use UNITS PER FRAME HERE.
            //Each spin strength will have its own 'flatline', which is a point at which the parabolic fall off stops. It is only generated for the last immediate spin and every delayed spin.

            //immediateSpinStrengths = new List<double>();
            immediateSpinStrengthPerFrame = new List<double>();
            immediateSpinFlatline = 0;
            //The spin delay of immediate spins is LOCKED, it is the same amount every time. It should be around -- seconds.
            //No, it is actually gonna be 0.5 seconds. Flat. My idiot ass lost all the documentation of the frames and when the woman's sprite changes,
            //but I think it's appropriate to reverse the animation at 0.5 seconds, because she stays in that state for a long enough period.

            //To be consistent, every single spin ever possible here will reach its peak in 0.327 seconds. NO MORE NO LESS.

            //delayedSpinStrengths = new List<double>();
            delayedSpinStrengthPerFrame = new List<double>();
            //this list contains the delay length (seconds) ever since the parabolic drop off ends.
            delayedSpinDelays = new List<double>();
            delayedSpinFlatlines = new List<double>();
            immediateSpinBs = new List<double>();
            delayedSpinBs = new List<double>();
            lockedUpdateTime = Time.deltaTime;
            FlatlineFalloffPerSec = HLSNUtil.GetRandomDouble(Plugin.Settings.minFlatlineFalloffSpeed, Plugin.Settings.maxFlatlineFalloffSpeed);
            FlatlineFallOffPerFrame = FlatlineFalloffPerSec * lockedUpdateTime / (1.0 / 144.0);
            ArrowheadOffset = HLSNUtil.GetRandomDouble(Plugin.Settings.minArrowheadSlotCoverage, Plugin.Settings.maxArrowheadSlotCoverage);
            LogWaterWheelInfo($"Locked update time: {lockedUpdateTime} FlatLineFalloffperframe: {FlatlineFallOffPerFrame}");
            //each consecutive respin needs to be STRONGER in length.
            for (int i = 0; i < immediateRespins; i++)
            {
                LogWaterWheelInfo($"Working immediate respin {i}");
                double spinStrengthPerFrame = HLSNUtil.GetRandomDouble(Plugin.Settings.minImmediateSpinStrength, Plugin.Settings.maxImmediateSpinStrength) * lockedUpdateTime;
                double startTime = i * ImmediateSpinDelay;
                immediateSpinFlatline = (spinStrengthPerFrame / 3.6) * HLSNUtil.GetRandomDouble(Plugin.Settings.minImmediateFlatlineFactor, Plugin.Settings.maxImmediateFlatlineFactor);
                LogWaterWheelInfo($"Spin strength per frame: {spinStrengthPerFrame}. Start time is {startTime}. Immediate spin flatline is {immediateSpinFlatline}");
                //immediateSpinStrengths.Add(spinStrength);
                if (i > 0)
                {
                    //add the last respin's value here.
                    double previousStrength = ComputeParabolic(startTime, immediateSpinStrengthPerFrame.Last(), immediateSpinBs.Last(), ImmediateSpinPeakTime + (i - 1) * ImmediateSpinDelay);
                    spinStrengthPerFrame += previousStrength;
                    LogWaterWheelInfo($"Not the first immediate spin, so adding previous strength of {previousStrength} to this function. Spin strength per frame is now {spinStrengthPerFrame}");
                }
                //let's calculate the Bs here so that we conserve processing power for later.
                double myB = GetParabolicFunctionB(spinStrengthPerFrame, startTime, ImmediateSpinPeakTime + startTime);
                LogWaterWheelInfo($"The B value is {myB}");
                immediateSpinStrengthPerFrame.Add(spinStrengthPerFrame);
                immediateSpinBs.Add(myB);
                lastImmediateSpinFlatlineTime = GetFlatlineTime(immediateSpinFlatline, spinStrengthPerFrame, myB, ImmediateSpinPeakTime + startTime);
                LogWaterWheelInfo($"And the immediate last spin flatline time computes to {lastImmediateSpinFlatlineTime}");
            }


            for (int i = 0; i < delayedRespins; i++)
            {
                LogWaterWheelInfo($"Working delayed respin {i}");
                double spinStrengthPerFrame = HLSNUtil.GetRandomDouble(Plugin.Settings.minDelayedSpinStrength, Plugin.Settings.maxDelayedSpinStrength) * lockedUpdateTime;
                delayedSpinFlatlines.Add((spinStrengthPerFrame / 3.6) * HLSNUtil.GetRandomDouble(Plugin.Settings.minDelayedFlatlineFactor, Plugin.Settings.maxDelayedFlatlineFactor));
                delayedSpinDelays.Add(HLSNUtil.GetRandomDouble(0.6, 1.9));
                //delayedSpinStrengths.Add(spinStrength);
                delayedSpinStrengthPerFrame.Add(spinStrengthPerFrame);

                //To not complicate shit, I believe it is not that important to count all the past time that passed.
                //We are trying to calculate the B coefficient, which only relates to how quickly the parabola rises.
                //It should NOT be affected by how far off to the right on the X axis it is.
                //Therefore, let's set x to 0 for when the parabola goes above 0.
                //Then we set the peak to the amount of time it takes to peak.
                delayedSpinBs.Add(GetParabolicFunctionB(spinStrengthPerFrame, 0, 0.327));
            }
            //double totalAnimationDuration = lastImmediateSpinFlatlineTime;

            LogWaterWheelInfo($"Finished picking spin values. Last immediate spin flatline time is: {lastImmediateSpinFlatlineTime}");

            //something something Time.deltaTime
            //we need to lock in this delta time for calculations later, because we need predictable outcomes.
            //Unfortunately this means that if deltaTime is abnormal when we lock it in, the predictions will likely suck.
            //The locked deltaTime is above because I need to use it to calculate B's immediately.
            double totalDistance = 0;
            double timeElapsed = 0;
            double lastSpeed = 0;
            //the proportion of the roleSlot that the arrow is 'currently' at, does not account for it turning
            double roleSlotPosition = 0.4;
            //the proportion of the roleSlot that the arrow is 'currently' at, DOES account for it turning
            double arrowheadPosition = roleSlotPosition - ArrowheadOffset;
            int roleSlotSlotsPassed = 0;
            int arrowheadSlotsPassed = 0;
            LogWaterWheelInfo($"Simulating spins to get final landing value...");
            while (!(timeElapsed > lastImmediateSpinFlatlineTime && lastSpeed <= 0))
            {
                lastSpeed = GetSpeedPerFrame(timeElapsed);
                //LogWaterWheelInfo($"At {timeElapsed} our speed is {lastSpeed} pixels per frame.");
                totalDistance += lastSpeed;
                timeElapsed += lockedUpdateTime;
                roleSlotPosition += lastSpeed / RoleSlotHeight;
                while (roleSlotPosition > 1)
                {
                    roleSlotSlotsPassed++;
                    roleSlotPosition -= 1;
                }
                arrowheadPosition += lastSpeed / RoleSlotHeight;
                while (arrowheadPosition > 1)
                {
                    arrowheadSlotsPassed++;
                    arrowheadPosition -= 1;
                }
            }

            int distanceSlots = (int)Math.Ceiling(totalDistance / RoleSlotHeight);
            LogWaterWheelInfo($"The simulation elapsed {totalDistance} total distance. Took {timeElapsed} seconds. The water wheel traversed {distanceSlots} slots, but the arrowhead passed {arrowheadSlotsPassed} slots.");
            // 6 extra slots - 3 for each side.
            int totalSlots = distanceSlots + 5;
            LogWaterWheelInfo($"There are {totalSlots} total slots.");
            //How slots are sorted:
            //3 fake slots in the beginning. The 2 very bottom ones have the arrow never land on them, and the 3rd gets the arrow after the jolt backwards.
            //The thing is, one of those 3 fake slots in the beginning is already included in the calculation, so you only add 2 at the bottom.
            //Then we have distanceSlots - the very final slot (in most cases, refer to arrowhead movements) is the role the player actually got.
            //3 extra fake slots at the very end for safety. The arrow should never land on them.

            //The animation begins with the arrowhead being on role 4 (from bottom), meaning it is up by 3 full role slots and then at 33.3% of the 4th slot.
            //It is then slightly retracted into being on role 3 (from bottom), from the very bottom its position is up by 2 full role slots and 40% of the 3rd slot.

            //We already know where the arrowhead will launch. The number of slots it passed over is in arrowheadSlotsPassed.
            //0-based index here, so we use 2 instead of 3.
            arrowheadSettleIndex = totalSlots - arrowheadSlotsPassed - 3;
            LogWaterWheelInfo($"The arrowhead will settle on slot {arrowheadSettleIndex}");
            //Includes BOTH the fake and the REAL slot!!!
            allSlots = new List<WaterWheelRoleController>();
            //__instance.SelectedRoleSlot is the REAL slot
            //__instance.OtherRoleSlots are FAKE slots
            List<WaterWheelRoleController> fakeSlots = (from g in __instance.OtherRoleSlots where g != null select g).ToList();
            
            LogWaterWheelInfo($"STEP 3. Prepared to create slot objects.");

            //STEP 3
            RectTransform parentTransform = (RectTransform) __instance.SelectedRoleSlot.gameObject.transform.parent;
            float parentHeight = RoleSlotHeight * totalSlots;
            parentTransform.sizeDelta = new Vector2(parentTransform.sizeDelta.x, parentHeight);
            //LogWaterWheelInfo($"The parent height is {parentHeight}. The parent transform is now {parentTransform.sizeDelta} big.");
            //LogWaterWheelInfo($"Selected Role: Name {selectedRole.gameObject.name} Size Delta Y {((RectTransform)selectedRole.transform).sizeDelta.y} Anchor min {((RectTransform)selectedRole.transform).anchorMin} Anchor max {((RectTransform)selectedRole.transform).anchorMax}");
            for (int i = 0; i < totalSlots;i++)
            {
                LogWaterWheelInfo($"Processing slot {i} in STEP 3.");
                WaterWheelRoleController currentSlot = null;
                int aboveSelectedRoleAddition = 0;
                if(i >= arrowheadSettleIndex)
                    aboveSelectedRoleAddition = -1;
                if (arrowheadSettleIndex == i)
                {
                    LogWaterWheelInfo($"This slot IS THE ROLE PLAYER SHOULD LAND ON.");
                    currentSlot = __instance.SelectedRoleSlot;
                }
                else if (i + aboveSelectedRoleAddition < fakeSlots.Count)
                {
                    LogWaterWheelInfo($"This slot already exists and represents a fake role that player will not get. Attributing slot...");
                    currentSlot = fakeSlots[i + aboveSelectedRoleAddition];
                }
                else
                {
                    LogWaterWheelInfo($"This slot doesn't exist. Instantiating from SelectedRoleSlot...");
                    currentSlot = GameObject.Instantiate(__instance.SelectedRoleSlot.gameObject, parentTransform).GetComponent<WaterWheelRoleController>();
                    fakeSlots.Add(currentSlot);
                }
                allSlots.Add(currentSlot);
                //((RectTransform)currentSlot.transform).sizeDelta = new Vector2(((RectTransform)currentSlot.transform).sizeDelta.x, RoleSlotHeight);
                currentSlot.transform.SetSiblingIndex(i);
                //currentSlot.transform.localPosition = new Vector2(0, -parentHeight / 2 + RoleSlotHeight / 2 + i * RoleSlotHeight);
                LogWaterWheelInfo($"Slot {i} has been placed into: {currentSlot.transform.localPosition}");
            }
            //due to VerticalLayoutGroup if the water wheel spins less than in vanilla ToS, the left over slots that aren't used will corrupt the water wheel.
            //Hence, all the leftover slots have to be destroyed...
            var otherRoleSlotsExtract = __instance.OtherRoleSlots.ToList();
            LogWaterWheelInfo($"There are {__instance.OtherRoleSlots.Length} other role slots on the water wheel. PRE-DELETION.");
            while (otherRoleSlotsExtract.Count > totalSlots - 1)
            {
                GameObject.Destroy(otherRoleSlotsExtract.Last().gameObject);
                LogWaterWheelInfo($"DESTROYING {otherRoleSlotsExtract.Last().gameObject.name}! IT'S NOT ON THE WATER WHEEL");
                otherRoleSlotsExtract.RemoveAt(otherRoleSlotsExtract.Count - 1);
            }
            LogWaterWheelInfo($"There are {(from g in __instance.OtherRoleSlots where g != null select g).Count()} non-null other role slots on the water wheel. POST-DELETION.");

            //STEP 4
            LogWaterWheelInfo($"STEP 4.");

            slotOccupancyState = new List<bool>();
            foreach(var kv in allSlots)
                slotOccupancyState.Add(false);

            RandomBlabs = new List<int>();
            //RandomBlabs.Add(arrowheadSettleIndex - 2);
            //slotOccupancyState[arrowheadSettleIndex - 2] = true;
            //RandomBlabs.Add(arrowheadSettleIndex + 2);
            //slotOccupancyState[arrowheadSettleIndex + 2] = true;
            if(Plugin.RandomBlabs.Count == 0 && Plugin.Settings.blabSpawnChance > 0)
            {
                Plugin.Log.LogWarning($"The list of blabs provided is empty, while the blab spawn rate is 0. The blab spawn rate will be forced to 0 for the rest of this session. To fix this, add at least one blab to the iww_blabs.txt file and restart the game.");
                Plugin.Settings.blabSpawnChance = 0;
            }
            for (int i = 0; i < allSlots.Count;i++)
                if (i != arrowheadSettleIndex && HLSNUtil.GetRandomDouble() <= Plugin.Settings.blabSpawnChance)
                {
                    LogWaterWheelInfo($"Random blab triggered on slot {i}!");
                    RandomBlabs.Add(i);
                    slotOccupancyState[i] = true;
                }
            LogWaterWheelInfo($"Random blab slots picked. There are {RandomBlabs.Count} blabs here.");
                
            //Construct all the various possible role groups!
            //use this to determine various role groups...
            GameRules gameRules = GlobalServiceLocator.GameRulesService.GetGameRules();
            RoleGroups = new List<List<Role>>();
            LogWaterWheelInfo($"I got game rules. Making invest result game rules...");

            //Unfortunately I can't find any spot where I can get invest results from, so I have to hardcode it...
            RoleGroups.Add(new List<Role>
            {
                gameRules.Roles[14],
                gameRules.Roles[13],
                gameRules.Roles[23],
                gameRules.Roles[55],
                gameRules.Roles[51]
            });
            RoleGroups.Add(new List<Role>
            {
                gameRules.Roles[7],
                gameRules.Roles[22],
                gameRules.Roles[8],
                gameRules.Roles[59],
                gameRules.Roles[48]
            });
            RoleGroups.Add(new List<Role>
            {
                gameRules.Roles[29],
                gameRules.Roles[12],
                gameRules.Roles[24],
                gameRules.Roles[61],
                gameRules.Roles[49]
            });
            RoleGroups.Add(new List<Role>
            {
                gameRules.Roles[10],
                gameRules.Roles[15],
                gameRules.Roles[4],
                gameRules.Roles[54]
            });
            RoleGroups.Add(new List<Role>
            {
                gameRules.Roles[9],
                gameRules.Roles[26],
                gameRules.Roles[31],
                gameRules.Roles[60]
            });
            RoleGroups.Add(new List<Role>
            {
                gameRules.Roles[20],
                gameRules.Roles[30],
                gameRules.Roles[27],
                gameRules.Roles[58]
            });
            RoleGroups.Add(new List<Role>
            {
                gameRules.Roles[5],
                gameRules.Roles[19],
                gameRules.Roles[53],
                gameRules.Roles[56]
            });
            RoleGroups.Add(new List<Role>
            {
                gameRules.Roles[2],
                gameRules.Roles[11],
                gameRules.Roles[17],
                gameRules.Roles[50]
            });
            RoleGroups.Add(new List<Role>
            {
                gameRules.Roles[1],
                gameRules.Roles[18],
                gameRules.Roles[28],
                gameRules.Roles[57]
            });
            RoleGroups.Add(new List<Role>
            {
                gameRules.Roles[3],
                gameRules.Roles[16],
                gameRules.Roles[6],
                gameRules.Roles[47],
                gameRules.Roles[52]
            });
            RoleGroups.Add(new List<Role>
            {
                gameRules.Roles[0],
                gameRules.Roles[21],
                gameRules.Roles[25],
                gameRules.Roles[46]
            });
            RoleGroups.Add(new List<Role>
            {
                gameRules.Roles[100]
            });
            //12 invest results total.
            LogWaterWheelInfo($"Done. Making faction role groups...");
            //alignments
            List<int> alignmentIds = new List<int> { 62, 64, 65, 66, 67, 69, 70, 71, 72, 73, 74, 76 };
            //12 alignments total.
            List<int> factionIds = new List<int> { 62, 63, 68, 71};
            //4 factions total.
            foreach(var kv in alignmentIds)
                RoleGroups.Add(ScourRoleGroup(kv, gameRules));
            //NOTE: Random Neutral DOESN'T contain jugg nor PB!
            //Same goes for NK...? Doesn't have Jugg. Everything else seems good.
            foreach (var kv in factionIds)
                RoleGroups.Add(ScourRoleGroup(kv, gameRules));
            if(Plugin.CustomRoleGroups.Count > 0)
            {
                LogWaterWheelInfo($"Custom roles detected!");
                int roleGroupNum = 1;
                foreach(var customRoleGroup in Plugin.CustomRoleGroups)
                {
                    List<Role> newRoleGroup = new List<Role>();
                    foreach(var customRoleID in customRoleGroup)
                    {
                        if(gameRules.Roles.ContainsKey(customRoleID))
                            newRoleGroup.Add(gameRules.Roles[customRoleID]);
                        else
                            Plugin.Log.LogWarning($"Unknown role ID {customRoleID} provided for custom role group {roleGroupNum}! Skipping...");
                    }
                    if(newRoleGroup.Count > 0)
                    {
                        RoleGroups.Add(newRoleGroup);
                        LogWaterWheelInfo($"Added custom role group {roleGroupNum}!");
                    }
                    else
                        Plugin.Log.LogWarning($"Custom role group {roleGroupNum} is empty! Skipping...");
                    roleGroupNum++;
                }
            }
            LogWaterWheelInfo($"Done. Sanitizing role groups...");

            //we got all the role groups.
            //but now we need to sanitize them.
            //2 things: First, remove all roles that are not present in the active role list, cause otherwise that'd be dumb.
            //The only exception to that is Pestillence, it remains only if Plaguebearer is also present.
            //Second, create a List for every RoleGroup to indicate whether it contains the SelectedRole.
            RoleGroupContainsPlayer = new List<bool>();
            foreach(var kv in RoleGroups)
            {
                for (int i = 0; i < kv.Count;i++)
                {
                    Role r = kv[i];
                    if(r.Id == 100)
                        r = gameRules.Roles[52];
                    if(!Plugin.RoleLotInfos.ContainsKey(r))
                    {
                        kv.RemoveAt(i);
                        i--;
                    }
                }
                RoleGroupContainsPlayer.Add(kv.Contains(Plugin.SelectedRole));
            }

            LogWaterWheelInfo($"Done. Placing up to {Plugin.Settings.maxRoleGroups} role groups with {Plugin.Settings.roleGroupSpawnChance * 100}% chance to keep getting a new role group.");
            //Here's how we do it. Once a 50% has rolled in favor of a role group, you pick the group.
            //Given the group size, you check if you can place it somewhere and then try to place it somewhere.
            RoleGroupPlacements = new Dictionary<int, int>();
            for (int i = 0; i < Plugin.Settings.maxRoleGroups && HLSNUtil.GetRandomDouble() <= Plugin.Settings.roleGroupSpawnChance;i++)
            {
                LogWaterWheelInfo($"Placing role group {i}.");
                //pick the group
                HLSNRandomable<int> randomable = new HLSNRandomable<int>();
                randomable.chanceTable.Add(0, Plugin.Settings.investResultWeight);
                randomable.chanceTable.Add(1, Plugin.Settings.subalignmentWeight);
                randomable.chanceTable.Add(2, Plugin.Settings.factionWeight);
                randomable.chanceTable.Add(3, Plugin.Settings.customRoleGroupWeight);
                int groupType = randomable.GetRandom(false);
                int groupID;
                switch(groupType)
                {
                    case 1:
                        LogWaterWheelInfo($"Investigator result role group!");
                        groupID = HLSNUtil.GetRandomInt(0, 11);
                        break;
                    case 0:
                        LogWaterWheelInfo($"Subalignment role group!");
                        groupID = HLSNUtil.GetRandomInt(12, 23);
                        break;
                    case 2:
                        LogWaterWheelInfo($"Faction role group!");
                        groupID = HLSNUtil.GetRandomInt(24, 27);
                        break;
                    case 3:
                        if(RoleGroups.Count > 28)
                        {
                            LogWaterWheelInfo($"Custom role group!");
                            groupID = HLSNUtil.GetRandomInt(28, RoleGroups.Count - 1);
                            break;
                        }
                        Plugin.Log.LogWarning($"Attempted to add a custom role group, but there are none of them! Adding a default investigator result...");
                        groupID = 0;
                        break;
                    default:
                        groupID = 0;
                        break;
                }
                List<Role> myGroup = RoleGroups[groupID];
                if (myGroup.Count == 0)
                {
                    LogWaterWheelInfo($"This group has no members. Skipping.");
                    continue;
                }
                var validPos = GetValidGroupPositions(myGroup, RoleGroupContainsPlayer[groupID]);
                LogWaterWheelInfo($"There are {validPos.Count} valid positions where this {myGroup.Count} role group can be placed.");
                if (validPos.Count == 0)
                    continue;
                int finalPosition = validPos[HLSNUtil.GetRandomInt(0, validPos.Count - 1)];
                RoleGroupPlacements.Add(finalPosition, groupID);
                LogWaterWheelInfo($"Placed group. Setting slot occupancy states...");
                for (int i2 = finalPosition; i2 < finalPosition + myGroup.Count && slotOccupancyState.Count > i2;i2++)
                    slotOccupancyState[i2] = true;
            }

            LogWaterWheelInfo($"Finished placing role groups, but going to define roles of each water wheel slot.");
            //We're almost done with this... now let's place an actual role behind every slot with the info we have.
            List<Role> finalizedSlotRoles = new List<Role>();
            //first let's add impostor values for every role so that it's easier to manage later.
            foreach(var kv in allSlots)
                finalizedSlotRoles.Add(null);
            //second let's add the role group derps
            foreach(var kv in RoleGroupPlacements)
            {
                List<Role> thisRoleGroup = RoleGroups[kv.Value].ToList();
                //if arrowheadSettle index is equal to or higher than kv.Key (starting position)
                //and kv.Key + thisRoleGroup.Count is higher than arrowheadSettleIndex, then the player's role is within the role group.
                bool containsPlayer = kv.Key <= arrowheadSettleIndex && kv.Key + thisRoleGroup.Count > arrowheadSettleIndex;
                if(containsPlayer)
                {
                    LogWaterWheelInfo($"This role group contains the player. Setting the role that will be landed on to the real role and the rest are randomized.");
                    finalizedSlotRoles[arrowheadSettleIndex] = Plugin.SelectedRole;
                    thisRoleGroup.Remove(Plugin.SelectedRole);
                    thisRoleGroup = thisRoleGroup.RandomizeArray().ToList();
                    //kv.Key is 2
                    //thisRoleGroup.Count is 9
                    int skipper = 0;
                    if (arrowheadSettleIndex == kv.Key)
                    {
                        LogWaterWheelInfo($"Slot {kv.Key} is where the water wheel settles! Detected before loop!");
                        skipper++;
                    }
                    for (int i = 0; i < thisRoleGroup.Count && finalizedSlotRoles.Count > kv.Key + i+ skipper; i++)
                    {
                        LogWaterWheelInfo($"The item {i} of the role group containing the player is slot {kv.Key + i + skipper} on the water wheel.");
                        finalizedSlotRoles[kv.Key + i+ skipper] = thisRoleGroup[i];
                        if (arrowheadSettleIndex == kv.Key + i + 1)
                        {
                            LogWaterWheelInfo($"Slot {kv.Key + i + 1} is where the water wheel settles!");
                            skipper++;
                        }
                    }
                }
                else
                {
                    LogWaterWheelInfo($"This role group doesn't contain the player and is completely randomized.");
                    thisRoleGroup = thisRoleGroup.RandomizeArray().ToList();
                    for (int i = 0; i < thisRoleGroup.Count && finalizedSlotRoles.Count > kv.Key + i; i++)
                        finalizedSlotRoles[kv.Key + i] = thisRoleGroup[i];
                }
            }
            //and now... for all roles that aren't role group derps, nor blabs, you pick a role based on RoleLots!

            LogWaterWheelInfo($"Done. Calculating the odds for each roles.");
            Dictionary<Role, double> rolePercentageOdds = new Dictionary<Role, double>();
            Dictionary<Role, double> preResultOdds = new Dictionary<Role, double>();

            LogWaterWheelInfo($"Gamemode: {GlobalServiceLocator.GameService.ActiveGameState.GameMode.Name}");
            LogWaterWheelInfo($"GlobalServiceLocator.GameService.ActiveGameState.RoleList:");
            foreach (var kv in GlobalServiceLocator.GameService.ActiveGameState.RoleList)
            {
                LogWaterWheelInfo(kv.Name + " - " + kv.Id);
            }
            if(GlobalServiceLocator.GameService.ActiveGameState.GameMode != null && GlobalServiceLocator.GameService.ActiveGameState.GameMode.RoleList != null)
            {
                LogWaterWheelInfo($"GlobalServiceLocator.GameService.ActiveGameState.GameMode.RoleList:");
                foreach (var kv in GlobalServiceLocator.GameService.ActiveGameState.RoleList)
                {
                    LogWaterWheelInfo(kv.Name + " - " + kv.Id);
                }
            }
            else
                LogWaterWheelInfo($"GlobalServiceLocator.GameService.ActiveGameState.GameMode.RoleList is null.");

            if (Plugin.Settings.oddsMode < OddsMode.Smart)
            {
                //int total = 0;
                //if(Plugin.Settings.oddsMode == OddsMode.DumbAlt)
                //    foreach(var kv in Plugin.RoleLotInfos)
                //        total += kv.Value.Lots;
                foreach (var kv in Plugin.RoleLotInfos)
                    rolePercentageOdds.Add(kv.Key, Mathf.Round((float)Plugin.RoleLotInfos[kv.Key].Lots / (float)Plugin.RoleLotInfos[kv.Key].TotalLots * 1000f) / 10f);
            }
            else
            {
                //Calculate every role's odds of appearing!
                Dictionary<Role, List<double>> roleSlotChances = new Dictionary<Role, List<double>>();
                foreach (var kv in Plugin.RoleLotInfos)
                    roleSlotChances.Add(kv.Key, new List<double>());
                //All unique roles:
                //4, 6, 8, 13, 21, 23, 31, 51, 52, 53, 55, 56, 57, 58, 59, 60, 61
                //VH: 12
                //Vampire: 30
                //All mafia roles: 15, 16, 17, 18, 19, 20, 21, 22, 23, 50, 51
                //Mafioso: 23
                //Godfather: 21
                List<Role> mafiaNonMKRoles = new List<Role>()
                {
                    gameRules.Roles[15],
                    gameRules.Roles[16],
                    gameRules.Roles[17],
                    gameRules.Roles[18],
                    gameRules.Roles[19],
                    gameRules.Roles[20],
                    gameRules.Roles[22],
                    gameRules.Roles[50],
                    gameRules.Roles[51]
                };
                var currentRoleList = GlobalServiceLocator.GameService.ActiveGameState.RoleList;
                if (currentRoleList.Count == 0 && GlobalServiceLocator.GameService.ActiveGameState.GameMode != null && GlobalServiceLocator.GameService.ActiveGameState.GameMode.RoleList != null)
                    currentRoleList = GlobalServiceLocator.GameService.ActiveGameState.GameMode.RoleList;
                List<List<Role>> expandedRoleList = new List<List<Role>>();
                foreach (var roleSlot in currentRoleList)
                {
                    var roleGroup = ScourRoleGroup(roleSlot.Id, gameRules);
                    if (roleGroup is null || roleGroup.Count == 0)
                        roleGroup = new List<Role>() { roleSlot };
                    expandedRoleList.Add(roleGroup);
                }
                //reorder it
                if (Plugin.Settings.oddsMode == OddsMode.Gigabrain)
                    expandedRoleList = (from g in expandedRoleList orderby g.Count, GetOrderingBasedOnRole(g.ElementAt(0)) select g).ToList();
                foreach (var roleGroup in expandedRoleList)
                {
                    foreach (var currentRoleChances in roleSlotChances)
                    {
                        if (!roleGroup.Contains(currentRoleChances.Key))
                        {
                            //LogWaterWheelInfo($"Role {currentRoleChances.Key.Id} doesn't appear in {kv.Id}");
                            currentRoleChances.Value.Add(0);
                        }
                        else
                        {
                            double myOdds = 1.0 / roleGroup.Count;
                            //LogWaterWheelInfo($"Role {currentRoleChances.Key.Id} has a base 1 in {roleGroup.Count} chance to appear in {kv.Id}");
                            if (IsUniqueRole(currentRoleChances.Key))
                            {
                                double notRolledChance = 1;
                                foreach (var lastRoleChances in currentRoleChances.Value)
                                    notRolledChance *= 1 - lastRoleChances;
                                myOdds *= notRolledChance;
                                //LogWaterWheelInfo($"It's a unique role, so in reality the chance is {myOdds}");
                            }
                            currentRoleChances.Value.Add(myOdds);
                        }
                    }
                    //account for the mafia here.
                    //calculate the chance that a mafioso doesn't exist at this point in the role list...
                    if (Plugin.Settings.oddsMode == OddsMode.Gigabrain)
                    {
                        double noMKChance = 1;
                        //if the mafioso exists...
                        if (roleSlotChances.ContainsKey(gameRules.Roles[23]))
                            //check every past chance
                            foreach (var lastRoleChances in roleSlotChances[gameRules.Roles[23]])
                                //and multiply the chance for no MK to exist by the chance that Mafioso was not rolled
                                noMKChance *= 1 - lastRoleChances;
                        if (roleSlotChances.ContainsKey(gameRules.Roles[21]) && noMKChance > 0)
                            //check every past chance
                            foreach (var lastRoleChances in roleSlotChances[gameRules.Roles[21]])
                                //and multiply the chance for no MK to exist by the chance that Mafioso was not rolled
                                noMKChance *= 1 - lastRoleChances;
                        //flip it now
                        double thereIsMK = 1 - noMKChance;
                        //now for every Mafia role, calculate its chances by multiplying it with thereIsMK
                        double extraMafiosoChance = 0;
                        foreach (var nonMK in mafiaNonMKRoles)
                            if (roleSlotChances.ContainsKey(nonMK))
                            {
                                var aids = roleSlotChances[nonMK];
                                extraMafiosoChance += noMKChance * aids[aids.Count - 1];
                                aids[aids.Count - 1] *= thereIsMK;
                            }
                        if (roleSlotChances.ContainsKey(gameRules.Roles[23]))
                        {
                            //add the chance of MK not existing to the mafioso
                            var aids = roleSlotChances[gameRules.Roles[23]];
                            aids[aids.Count - 1] += extraMafiosoChance;
                        }
                    }
                }
                foreach (var kv in roleSlotChances)
                {
                    double result = 1;
                    //calculate the chance of NOT rolling the role
                    foreach (var odds in kv.Value)
                    {
                        //LogWaterWheelInfo($"Odds for {kv.Key.Name}: current result: {result} and odds is {odds}");
                        result *= 1 - odds;
                    }
                    double playerVsPlayerOdds = Plugin.RoleLotInfos[kv.Key].Lots / (double)Plugin.RoleLotInfos[kv.Key].TotalLots;
                    double preresult = result;
                    result = (double)playerVsPlayerOdds * (1 - result);
                    preresult = 1 - preresult;
                    LogWaterWheelInfo($"The role {kv.Key.Name} has been calculated to have a {Math.Round(result * 100, 1)}% chance! The rolelist appearance odds are {Math.Round((double)preresult * 100, 1)}%! The player vs player odds are {Math.Round(playerVsPlayerOdds * 100, 1)}%!");
                    if (result <= 0 && kv.Key.Id > 200)
                    {
                        rolePercentageOdds.Add(kv.Key, playerVsPlayerOdds);
                        preResultOdds.Add(kv.Key, playerVsPlayerOdds);
                    }
                    else
                    {
                        rolePercentageOdds.Add(kv.Key, result);
                        preResultOdds.Add(kv.Key, preresult);
                    }
                }
                //VH checkup should take place here
                if (Plugin.Settings.oddsMode == OddsMode.Gigabrain)
                {
                    if (roleSlotChances.ContainsKey(gameRules.Roles[12]))
                    {
                        // I hope this works
                        if (roleSlotChances.ContainsKey(gameRules.Roles[30]))
                        {
                            //calculate the chance that Vamps spawn
                            rolePercentageOdds[gameRules.Roles[12]] *= preResultOdds[gameRules.Roles[30]];
                            LogWaterWheelInfo($"There is a Vampire in this role list - the real chance of VH spawning is {Math.Round(rolePercentageOdds[gameRules.Roles[12]]* 100, 1)}%.");
                        }
                        else
                        {
                            LogWaterWheelInfo($"There is a Vampire in this role list, but Vampires can't spawn. Setting the VH chance to 0%.");
                            //VH can't spawn
                            rolePercentageOdds[gameRules.Roles[12]] = 0;
                        }
                    }
                }
                for (int keyIndex = 0; keyIndex < rolePercentageOdds.Count;keyIndex++)
                {
                    var key = rolePercentageOdds.Keys.ElementAt(keyIndex);
                    rolePercentageOdds[key] = Math.Round(rolePercentageOdds[key] * 100, 1);
                }
            }

            //TODO: I don't like kv.Value.Lots, it doesn't accurately tell you what your odds are of getting the role.
            //Instead, we should calculate the REAL chance you have of getting the role
            //but that will come later

            //3 modes:
            //'Dumb' chance calculation uses vanilla ToS calculation
            //'Smart' chance calculation will pretend as if there's just one role that allows you to get all of the roles in RoleLotsInfo and then calculate your odds by deducting that role's lots with the ALL ROLES' combined total role lots.
            //'Gigabrain' chance calculation will completely account for the rolelist, adjusting the odds of your role for each slot. For example you have a higher chance of rolling Vigi from TK than RT and you also have a higher chance of rolling Vigi if there are 2 RTs rather than 1.

            LogWaterWheelInfo($"Done. Placing randomized roles based on role lots into unoccupied slots.");
            HLSNRandomable<Role> roleLotsRandomizer = new HLSNRandomable<Role>();
            switch (Plugin.Settings.roleApparanceMode)
            {
                case RoleAppearanceMode.AllEqual:
                    foreach (var kv in Plugin.RoleLotInfos)
                        roleLotsRandomizer.chanceTable.Add(kv.Key, 1);
                    break;
                case RoleAppearanceMode.AffectedByScrolls:
                    foreach (var kv in Plugin.RoleLotInfos)
                        roleLotsRandomizer.chanceTable.Add(kv.Key, kv.Value.Lots);
                    break;
                case RoleAppearanceMode.AffectedByOddsMode:
                    foreach (var kv in rolePercentageOdds)
                        roleLotsRandomizer.chanceTable.Add(kv.Key, Convert.ToSingle(kv.Value));
                    break;
            }
            for (int i = 0; i < finalizedSlotRoles.Count;i++)
            {
                if (!slotOccupancyState[i])
                {
                    LogWaterWheelInfo($"Slot {i} is unoccupied... placing a random role or the selected role...");
                    if(arrowheadSettleIndex == i)
                        finalizedSlotRoles[i] = Plugin.SelectedRole;
                    else
                        finalizedSlotRoles[i] = roleLotsRandomizer.GetRandom(false);
                }
            }

            LogWaterWheelInfo($"Done. Finally, setting the text for each role slot for their role and odds.");

            //Okay finally, let's place the roles into their slots.
            for (int i = 0; i < allSlots.Count;i++)
            {
                WaterWheelRoleController roleController = allSlots[i];
                //for random Blabs I will actually need to create Role objects because it will use them for 'CURRENT ROLE' on top left.
                if(finalizedSlotRoles[i] is null)
                {
                    //it's a blab!
                    int randomBlab = HLSNUtil.GetRandomInt(0, Plugin.RandomBlabs.Count - 1);
                    finalizedSlotRoles[i] = new Role(1000 + randomBlab, Plugin.RandomBlabs[randomBlab], new Color(1, 1, 1, 1), new Color32(0, 0, 0, 1));
                }
                Role role2 = finalizedSlotRoles[i];
                roleController.RoleLabel.SetText(role2.Name);
                roleController.RoleLabel.color = role2.Color;
                LogWaterWheelInfo($"Slot {i} represents {role2.Name} and is colored {role2.Color}.");
                //TODO: Change this when adding smarter odds calculation soon
                //roleController.OddsLabel.SetText(i.ToString());
                if (Plugin.RoleLotInfos.ContainsKey(role2))
                    roleController.OddsLabel.SetText(rolePercentageOdds[role2] + "%");
                else
                {
                    //small chance for any number between -2 - 100, otherwise make it look vanilla-like
                    if(HLSNUtil.GetRandomDouble() < 0.1)
                        roleController.OddsLabel.SetText(Mathf.Round(Convert.ToSingle(HLSNUtil.GetRandomDouble(-2, 100)) * 10f) / 10f + "%");
                    else
                        roleController.OddsLabel.SetText(Mathf.Round(Convert.ToSingle(HLSNUtil.GetRandomDouble(0, 30)) * 10f) / 10f + "%");
                }
            }

            LogWaterWheelInfo($"STEP 5.");
            //STEP 5
            //ooohh boy. It took me long enough...

            //So there are 2 real challenges here:
            //The arrowhead, we need it to bounce naturally
            //And the woman

            //right before the spin the parent is elevated by 408,6 pixels??

            parentTransform.localPosition = new Vector2(0, parentTransform.sizeDelta.y / 2 - 408.6f - RoleSlotHeight);
            HLSNAnimationRequest preSpinRequest = new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Move,
                AttachToBehaviour = __instance,
                EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                Vector2A = parentTransform.localPosition,
                Vector2B = new Vector2(0, parentTransform.sizeDelta.y / 2 - 408.6f),
                Duration = TimeSpan.FromSeconds(1.31944)
            };
            DateTime animationStartTime = DateTime.Now;

            LogWaterWheelInfo($"ARROWHEAD ROTATION: {arrowhead.transform.localRotation}");
            
            arrowheadSlotsPassed = totalSlots - 4;
            OnRoleChangedSmart(finalizedSlotRoles[arrowheadSlotsPassed], __instance, false);
            double arrowheadPositionOpposite = 0.4 + ArrowheadOffset;
            arrowheadPosition = 0.4 - ArrowheadOffset;
            LogWaterWheelInfo($"Preset the preSpin values before animation.");
            double arrowheadRotationPerFrame = 660 * lockedUpdateTime;
            double lastY = parentTransform.localPosition.y;
            bool arrowheadAtSlotBorder = false;
            //when in freefall, high speeds will cause the arrow to bounce up a bit
            double arrowRotationalVelocity = 0;
            double arrowRotationalChangePerFrame = 125 * lockedUpdateTime;
            double maximumRotationThreshold = ArrowheadOffset / 0.35 * 25;
            double inverseArrowheadOffset = 1 - ArrowheadOffset;
            Plugin.MyAnimator.AnimateObjects(preSpinRequest, parentTransform);
            timeElapsed = 0;
            while(DateTime.Now < animationStartTime + TimeSpan.FromSeconds(1.868056))
            {
                timeElapsed += lockedUpdateTime;
                //animate the woman here in the future.
                double differenceProportional = Math.Abs((parentTransform.localPosition.y - lastY) / RoleSlotHeight);
                arrowheadPositionOpposite -= differenceProportional;
                arrowheadPosition -= differenceProportional;
                lastY = parentTransform.localPosition.y;
                //arrowheadPositionOpposite is nearly identical to arrowheadPosition, except its offset is defined to be the opposite side,
                //so when it spins backwards, the arrowhead only slides backwards...
                //there is nothing else backwards about it though.
                //Because the water wheel spins backwards, BOTH the opposite and normal go down. They also BOTH go up when it spins normally.
                
                
                while(arrowheadPosition < 0)
                    arrowheadPosition++;
                while(arrowheadPositionOpposite < 0)
                {
                    arrowheadPositionOpposite++;
                    arrowheadSlotsPassed++;
                    //click! Update selected role label on top left.
                    OnRoleChangedSmart(finalizedSlotRoles[arrowheadSlotsPassed], __instance);
                }

                //handle arrow rotation
                Vector3 arrowheadEulerAngles = arrowhead.rectTransform.localEulerAngles;
                //TODO: Turn '25' to const.
                //TODO: Turn '660' to const.
                double newRotationZ = 0;
                if(arrowheadEulerAngles.z > 180)
                    arrowheadEulerAngles = new Vector3(arrowheadEulerAngles.x, arrowheadEulerAngles.y, arrowheadEulerAngles.z - 360);
                if (arrowheadEulerAngles.z < -maximumRotationThreshold)
                {
                    //LogWaterWheelInfo($"Arrowhead is more than -25 degrees");
                    //the arrow is in freefall!
                    //it turns +11 degrees per frame at 60 frames, aka 660 degrees per second
                    if (arrowheadEulerAngles.z + (arrowRotationalVelocity + arrowRotationalChangePerFrame) >= -maximumRotationThreshold)
                    {
                        //LogWaterWheelInfo($"Arrowhead would spin into the range of 25");
                        //the arrowhead, while turning to its normal position, will enter within the range of a slot hit.
                        //check if there is a slot hit for the proportion that may affect it.
                        if (arrowheadPositionOpposite < ArrowheadOffset && -(ArrowheadOffset - arrowheadPositionOpposite) / 0.35 * 25 < arrowheadEulerAngles.z + (arrowRotationalVelocity + arrowRotationalChangePerFrame))
                        {
                            if (DebugMode)
                                __instance.CurrentRoleLots.color = Color.cyan;
                            arrowRotationalVelocity = 0;
                            //LogWaterWheelInfo($"It will hit a slot that it'd attach to");
                            //the arrowhead, after being in freefall, hits a slot edge this frame
                            newRotationZ = -(ArrowheadOffset - arrowheadPositionOpposite) / 0.35 * 25;
                            arrowheadAtSlotBorder = true;
                            //! The 'click' sound should play at this point, if we count clicks by the time the slot border has been touched...
                        }
                        else
                        {
                            if (DebugMode)
                                __instance.CurrentRoleLots.color = Color.blue;
                            //LogWaterWheelInfo($"Freefall within 25");
                            //freefall
                            arrowRotationalVelocity += arrowRotationalChangePerFrame;
                            newRotationZ = arrowheadEulerAngles.z + arrowRotationalVelocity;
                        }
                    }
                    else
                    {
                        if (DebugMode)
                            __instance.CurrentRoleLots.color = Color.green;
                        //freefall
                        arrowRotationalVelocity += arrowRotationalChangePerFrame;
                        newRotationZ = arrowheadEulerAngles.z + arrowRotationalVelocity;
                    }
                }
                else
                {
                    //LogWaterWheelInfo($"Arrowhead is already within -25");
                    //LogWaterWheelInfo($"Arrowhead euler z is {arrowheadEulerAngles.z}");
                    //the arrowhead isn't floating above anything
                    if (arrowheadPositionOpposite < ArrowheadOffset && -(ArrowheadOffset - arrowheadPositionOpposite) / 0.35 * 25 < arrowheadEulerAngles.z + (arrowRotationalVelocity + arrowRotationalChangePerFrame))
                    {
                        if (DebugMode)
                            __instance.CurrentRoleLots.color = Color.yellow;
                        //LogWaterWheelInfo($"Arrowhead hit the border and is attached to it");
                        //the arrowhead is at the slot border
                        if (!arrowheadAtSlotBorder)
                        {
                            arrowheadAtSlotBorder = true;
                            //! The 'click' sound should play at this point, if we count clicks by the time the slot border has been touched...
                        }
                        arrowRotationalVelocity = 0;
                        newRotationZ = -(ArrowheadOffset - arrowheadPositionOpposite) / 0.35 * 25;
                    }
                    else
                    {
                        if (DebugMode)
                            __instance.CurrentRoleLots.color = Color.magenta;
                        //freefall
                        arrowRotationalVelocity += arrowRotationalChangePerFrame;
                        newRotationZ = arrowheadEulerAngles.z + arrowRotationalVelocity;
                    }
                }
                newRotationZ = Math.Min(0, newRotationZ);
                //LogWaterWheelInfo($"Rotating arrowhead to Z {newRotationZ}");
                arrowhead.rectTransform.localEulerAngles = new Vector3(0, 0, (float)newRotationZ);
                UpdateWWGirl(woman, timeElapsed, true);

                if (DebugMode)
                {
                    __instance.CurrentRoleLots.SetText($"{Math.Round(arrowheadPosition, 3)} / {arrowheadSlotsPassed}");
                    __instance.CurrentRoleTotalLots.SetText($"{Math.Round(lastSpeed, 3)} / {Math.Round(timeElapsed, 3)} sec");
                }
                yield return null;
            }
            LogWaterWheelInfo($"Starting main animation...");

            LogWaterWheelInfo($"There are {(from g in __instance.OtherRoleSlots where g != null select g).Count()} non-null other role slots on the water wheel. POST-DELETION, MAIN ANIMATION START.");
            animationStartTime = DateTime.Now;
            lastSpeed = 0;
            timeElapsed = 0;
            while (!(timeElapsed > lastImmediateSpinFlatlineTime && lastSpeed <= 0))
            {
                lastSpeed = GetSpeedPerFrame(timeElapsed);
                timeElapsed += lockedUpdateTime;
                Vector2 newPosition = parentTransform.localPosition - new Vector3(0, Convert.ToSingle(lastSpeed));
                double arrowheadLastPosition = arrowheadPosition;
                //every 'hit' is registered as a number between 0 to 1, indicating how far into the frame the hit happened here.
                List<double> arrowheadHitProportions = new List<double>();
                double arrowheadPositionChanged = lastSpeed / RoleSlotHeight;
                //double stackedPositionalChange = 0;
                arrowheadPosition += arrowheadPositionChanged;
                while (arrowheadPosition >= 1)
                {
                    arrowheadHitProportions.Add(1 - (Math.Floor(arrowheadPosition) - arrowheadLastPosition) / arrowheadPositionChanged);
                    arrowheadPosition--;
                    //stackedPositionalChange++;
                    arrowheadSlotsPassed--;
                    OnRoleChangedSmart(finalizedSlotRoles[arrowheadSlotsPassed], __instance);
                }

                Vector3 arrowheadEulerAngles = arrowhead.rectTransform.localEulerAngles;
                //TODO: Turn '25' to const.
                //TODO: Turn '660' to const.
                double newRotationZ = 0;
                //for the purposes of solid simulation, I'll make it so that the arrow drops 25 degrees in 0.2 seconds...
                //So the function to calculate the ACCUMULATED freefall is y=625x^2 here...
                //but I don't think accumulated freefall is enough...
                //actually scrap that, we're gonna use acceleration here.
                //the acceleration per second is 125... Let's just stick with that for now...
                if(arrowheadEulerAngles.z > maximumRotationThreshold)
                {
                    //LogWaterWheelInfo($"Arrowhead is more than -25 degrees");
                    //the arrow is in freefall!
                    //it turns +11 degrees per frame at 60 frames, aka 660 degrees per second
                    if(arrowheadEulerAngles.z - (arrowRotationalVelocity + arrowRotationalChangePerFrame) <= maximumRotationThreshold)
                    {
                        //LogWaterWheelInfo($"Arrowhead would spin into the range of 25");
                        //the arrowhead, while turning to its normal position, will enter within the range of a slot hit.
                        //check if there is a slot hit for the proportion that may affect it.
                        double proportionStart = (arrowheadEulerAngles.z - maximumRotationThreshold) / (arrowRotationalVelocity + arrowRotationalChangePerFrame);
                        var slotHits = (from g in arrowheadHitProportions where g > proportionStart select g);
                        if(slotHits.Count() > 0)
                        {
                            //LogWaterWheelInfo($"It will hit a slot");
                            if(DebugMode)
                                __instance.CurrentRoleLots.color = Color.red;
                            //the slot hit the arrowhead, so it should bounce back again, but only by the proportion of the speed that affects it
                            //! The 'click' sound should play at this point, if we count clicks by the time the slot border has been touched...

                            //because this depends on frames, wouldn't this mean that the arrowhead slot hit behavior would get influenced by the monitor framerate or nah?
                            arrowRotationalVelocity = lastSpeed / 15;
                            newRotationZ = maximumRotationThreshold + arrowRotationalVelocity * (1 - slotHits.ElementAt(0));
                            arrowheadAtSlotBorder = false;
                        }
                        else if (arrowheadPosition > inverseArrowheadOffset && (arrowheadPosition - inverseArrowheadOffset) / 0.35 * 25 > arrowheadEulerAngles.z - (arrowRotationalVelocity + arrowRotationalChangePerFrame))
                        {
                            if (DebugMode)
                                __instance.CurrentRoleLots.color = Color.cyan;
                            arrowRotationalVelocity = 0;
                            //LogWaterWheelInfo($"It will hit a slot that it'd attach to");
                            //the arrowhead, after being in freefall, hits a slot edge this frame
                            newRotationZ = (arrowheadPosition - inverseArrowheadOffset) / 0.35 * 25;
                            arrowheadAtSlotBorder = true;
                            //! The 'click' sound should play at this point, if we count clicks by the time the slot border has been touched...
                        }
                        else
                        {
                            if (DebugMode)
                                __instance.CurrentRoleLots.color = Color.blue;
                            //LogWaterWheelInfo($"Freefall within 25");
                            //freefall
                            arrowRotationalVelocity += arrowRotationalChangePerFrame;
                            newRotationZ = arrowheadEulerAngles.z - arrowRotationalVelocity;
                        }
                    }
                    else
                    {
                        if (DebugMode)
                            __instance.CurrentRoleLots.color = Color.green;
                        //freefall
                        arrowRotationalVelocity += arrowRotationalChangePerFrame;
                        newRotationZ = arrowheadEulerAngles.z - arrowRotationalVelocity;
                    }
                }
                else
                {
                    //LogWaterWheelInfo($"Arrowhead is already within 25");
                    //the arrowhead isn't floating above anything
                    if(arrowheadAtSlotBorder && arrowheadHitProportions.Count > 0)
                    {
                        if (DebugMode)
                            __instance.CurrentRoleLots.color = Color.gray;
                        //LogWaterWheelInfo($"Entering freefall as arrowhead gets released from border");
                        //bounce up
                        arrowRotationalVelocity = lastSpeed / 15;
                        newRotationZ = maximumRotationThreshold + arrowRotationalVelocity * (1 - arrowheadHitProportions[0]);
                        arrowheadAtSlotBorder = false;
                    }
                    else if (arrowheadPosition > inverseArrowheadOffset && (arrowheadPosition - inverseArrowheadOffset) / 0.35 * 25 > arrowheadEulerAngles.z - (arrowRotationalVelocity + arrowRotationalChangePerFrame))
                    {
                        if (DebugMode)
                            __instance.CurrentRoleLots.color = Color.yellow;
                        //LogWaterWheelInfo($"Arrowhead hit the border and is attached to it");
                        //the arrowhead is at the slot border
                        if (!arrowheadAtSlotBorder)
                        {
                            arrowheadAtSlotBorder = true;
                            //! The 'click' sound should play at this point, if we count clicks by the time the slot border has been touched...
                        }
                        arrowRotationalVelocity = 0;
                        newRotationZ = (arrowheadPosition - inverseArrowheadOffset) / 0.35 * 25;
                    }
                    else
                    {
                        if (DebugMode)
                            __instance.CurrentRoleLots.color = Color.magenta;
                        //freefall
                        arrowRotationalVelocity += arrowRotationalChangePerFrame;
                        newRotationZ = arrowheadEulerAngles.z - arrowRotationalVelocity;
                    }
                }
                newRotationZ = Math.Max(0, newRotationZ);
                //LogWaterWheelInfo($"Rotating arrowhead to Z {newRotationZ}");
                arrowhead.rectTransform.localEulerAngles = new Vector3(0, 0, (float) newRotationZ);

                if (DebugMode)
                {
                    __instance.CurrentRoleLots.SetText($"{Math.Round(arrowheadPosition, 3)} / {arrowheadSlotsPassed}");
                    __instance.CurrentRoleTotalLots.SetText($"{Math.Round(lastSpeed, 3)} / {Math.Round(timeElapsed, 3)} sec");
                }
                //LogWaterWheelInfo($"Changing parent transform from position {parentTransform.localPosition} to {newPosition}... If it doesn't work, then something's fucking with me.");
                //I think I have to set every single role slots transform to X 0 and Y whatever they have every frame.
                //foreach(var kv in allSlots)
                //    kv.transform.localPosition = new Vector2(0, kv.transform.localPosition.y);
                parentTransform.localPosition = newPosition;
                UpdateWWGirl(woman, timeElapsed, false);
                yield return null;
            }
            LogWaterWheelInfo($"Main animation finished...");
            if(arrowhead.rectTransform.localEulerAngles.z > 0)
            {
                if (arrowheadPosition > inverseArrowheadOffset)
                {
                    LogWaterWheelInfo($"The arrowhead is not cogged back to its normal position! Let's do it...");
                    //I want to use HLSNAnimator to rotate the arrowhead, but HLSNAnimator does not yet support rotations.
                    //I'll have to do Quaternion bumfuckery in order to be able to correctly rotate them... I think? I have to figure that part out.
                    Plugin.MyAnimator.Animate3DObjects(new HLSNAnimationRequest
                    {
                        Type = HLSNAnimator.AnimationType.Rotate,
                        AttachToBehaviour = __instance,
                        EasingMode = HLSNUtil.EasingMode.EASEIN2,
                        Vector3A = arrowhead.rectTransform.localEulerAngles,
                        Vector3B = new Vector3(0, 0, 0),
                        Duration = TimeSpan.FromSeconds(0.3)
                    }, arrowhead.transform);
                    yield return Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
                    {
                        Type = HLSNAnimator.AnimationType.Move,
                        AttachToBehaviour = __instance,
                        EasingMode = HLSNUtil.EasingMode.EASEIN2,
                        Vector2A = parentTransform.localPosition,
                        Vector2B = new Vector2(parentTransform.localPosition.x, parentTransform.localPosition.y + RoleSlotHeight * (float)(arrowheadPosition - inverseArrowheadOffset)),
                        Duration = TimeSpan.FromSeconds(.3)
                    }, parentTransform);
                }
                else
                {
                    //The arrowhead has to fall! Letting it fall...
                    LogWaterWheelInfo($"Arrow final freefall activated!");
                    __instance.StartCoroutine(ArrowFinalFreefall(arrowhead, arrowRotationalChangePerFrame, arrowRotationalVelocity));
                }
            }
            //here animate the selected water wheel slot and highlight the selected role.
            __instance.StartCoroutine(HandleSelectedRoleWiggle(__instance));
            __instance.StartCoroutine(HandleSelectedRoleJump(__instance));
            __instance.StartCoroutine(HandleSelectedRoleFlash(__instance));
            LogWaterWheelInfo($"RECT SIZES is {gigaParentSizes.width}x{gigaParentSizes.height}");
            Mask holderObject = null;
            if(consumedScroll_ != null)
            {
                //animate this first
                int scrollCount = GlobalServiceLocator.UserService.Inventory.GetOwnedCountOfItem(UserItemType.Scroll, consumedScroll_.id) + 1;
                //Plugin.Log.LogInfo($"The player has {scrollCount} scrolls of ID {consumedScroll_.id}");
                //I should probably not do this live??
                holderObject = GameObject.Instantiate(new GameObject("ScrollCountMask", new Type[] { typeof(RectTransform), typeof(Mask), typeof(Image) }), gigaParent).GetComponent<Mask>();
                holderObject.GetComponent<Image>().sprite = Plugin.ModdedSprites["solidmask"];
                holderObject.showMaskGraphic = false;
                //LogWaterWheelInfo($"Your screen is {Screen.width}x{Screen.height}");
                holderObject.rectTransform.sizeDelta = new Vector2(gigaParentSizes.width * 0.0976f, gigaParentSizes.height * 0.1f);
                var oldScrollCountText = TextMeshProUGUI.Instantiate(__instance.SelectedRoleSlot.OddsLabel, holderObject.rectTransform).GetComponent<TextMeshProUGUI>();
                oldScrollCountText.gameObject.SetActive(true);
                oldScrollCountText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                oldScrollCountText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                oldScrollCountText.rectTransform.sizeDelta = holderObject.rectTransform.sizeDelta;
                oldScrollCountText.rectTransform.localPosition = Vector2.zero;
                oldScrollCountText.margin = new Vector4(0, 0, 0, 0);
                oldScrollCountText.color = Color.white;
                oldScrollCountText.alignment = TextAlignmentOptions.Center;
                oldScrollCountText.enableAutoSizing = true;
                oldScrollCountText.fontSizeMin = 10;
                oldScrollCountText.fontSizeMax = 150;
                oldScrollCountText.text = scrollCount > 9 ? $"x{scrollCount}" : $"x0{scrollCount}";

                var newScrollCountText = TextMeshProUGUI.Instantiate(__instance.SelectedRoleSlot.OddsLabel, holderObject.rectTransform).GetComponent<TextMeshProUGUI>();
                newScrollCountText.gameObject.SetActive(true);
                newScrollCountText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                newScrollCountText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                newScrollCountText.rectTransform.sizeDelta = holderObject.rectTransform.sizeDelta;
                newScrollCountText.rectTransform.localPosition = new Vector2(0f, -newScrollCountText.rectTransform.sizeDelta.y);
                newScrollCountText.margin = new Vector4(0, 0, 0, 0);
                newScrollCountText.color = scrollCount == 1 ? Color.red : Color.white;
                newScrollCountText.alignment = TextAlignmentOptions.Center;
                newScrollCountText.enableAutoSizing = true;
                newScrollCountText.fontSizeMin = 10;
                newScrollCountText.fontSizeMax = 150;
                newScrollCountText.text = scrollCount - 1 > 9 ? $"x{scrollCount - 1}" : $"x0{scrollCount - 1}";
                
                holderObject.rectTransform.SetSiblingIndex(9);

                //hide the holderObject below a bit to the left of the scroll
                holderObject.rectTransform.localPosition = new Vector2(gigaParentSizes.width / 2 - gigaParentSizes.width * 0.1866f, -gigaParentSizes.height / 2 - gigaParentSizes.height * 0.111f);

                Image scrollImage = gigaParent.Find("Scroll").GetComponent<Image>();
                Image scrollFireImage = gigaParent.Find("ScrollFire (1)").GetComponent<Image>();
                //Image scrollFireImage = gigaParent.Find("ScrollFire").GetComponent<Image>();
                //Plugin.Log.LogInfo($"Scroll fire image sprite is: {scrollFireImage.sprite.name}, its rect is {scrollFireImage.sprite.rect}");
                scrollImage.gameObject.SetActive(true);
                scrollFireImage.gameObject.SetActive(false);
                //scrollFire1Image.gameObject.SetActive(false);

                Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
                {
                    Type = HLSNAnimator.AnimationType.Move,
                    AttachToBehaviour = __instance,
                    EasingMode = HLSNUtil.EasingMode.EASEOUT2,
                    Vector2A = new Vector2(gigaParentSizes.width / 2 + gigaParentSizes.width * 0.078f, -gigaParentSizes.height / 2 + gigaParentSizes.height * 0.1546f),
                    Vector2B = new Vector2(gigaParentSizes.width / 2 - gigaParentSizes.width * 0.086f, -gigaParentSizes.height / 2 + gigaParentSizes.height * 0.1546f),
                    Duration = TimeSpan.FromSeconds(.4)
                }, scrollImage.rectTransform);
                Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
                {
                    Type = HLSNAnimator.AnimationType.Move,
                    AttachToBehaviour = __instance,
                    EasingMode = HLSNUtil.EasingMode.EASEOUT3,
                    Vector2A = new Vector2(gigaParentSizes.width / 2 - gigaParentSizes.width * 0.1866f, -gigaParentSizes.height / 2 - gigaParentSizes.height * 0.111f),
                    Vector2B = new Vector2(gigaParentSizes.width / 2 - gigaParentSizes.width * 0.1866f, -gigaParentSizes.height / 2 + gigaParentSizes.height * 0.111f),
                    Duration = TimeSpan.FromSeconds(.5)
                }, holderObject.rectTransform);
                Plugin.MyAnimator.Animate3DObjects(new HLSNAnimationRequest
                {
                    Type = HLSNAnimator.AnimationType.Rotate,
                    AttachToBehaviour = __instance,
                    EasingMode = HLSNUtil.EasingMode.EASEOUT2,
                    Vector3A = new Vector3(0, 0, 60),
                    Vector3B = new Vector3(0, 0, 0),
                    Duration = TimeSpan.FromSeconds(.85)
                }, scrollImage.transform);
                yield return new WaitForSeconds(1f);
                scrollFireImage.rectTransform.localPosition = new Vector2(gigaParentSizes.width / 2 - gigaParentSizes.width * 0.086f, -gigaParentSizes.height / 2 + gigaParentSizes.height * 0.1546f);
                //buuurn
                scrollFireImage.gameObject.SetActive(true);
                Plugin.MyAnimator.AnimateGraphics(new HLSNAnimationRequest
                {
                    Type = HLSNAnimator.AnimationType.ChangeA,
                    AttachToBehaviour = __instance,
                    ColorA = new Color(0,0,0,0),
                    ColorB = new Color(0, 0, 0, 1),
                    Duration = TimeSpan.FromSeconds(.2)
                }, scrollFireImage);
                __instance.PlaySound("Sound/Common/FireSwitchLong");
                __instance.StartCoroutine(PlayBurnAnimation(scrollFireImage, ScrollFireAtlas, 25));
                yield return new WaitForSeconds(0.25f);
                Plugin.MyAnimator.AnimateGraphics(new HLSNAnimationRequest
                {
                    Type = HLSNAnimator.AnimationType.ColorAlphaless,
                    AttachToBehaviour = __instance,
                    ColorA = new Color(1, 1, 1, 1),
                    ColorB = new Color(0, 0, 0, 1),
                    Duration = TimeSpan.FromSeconds(.8)
                }, scrollImage);
                yield return new WaitForSeconds(0.4f);
                //animate the counter
                __instance.StartCoroutine(PlayScrollCounterAnimation(oldScrollCountText, newScrollCountText, scrollCount == 1, __instance));
                yield return new WaitForSeconds(0.6f);
                //LogWaterWheelInfo($"scrollimage size delta rn is: {scrollImage.rectTransform.sizeDelta}. Anchor min: {scrollImage.rectTransform.anchorMin}. Anchor max: {scrollImage.rectTransform.anchorMax}");
                Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
                {
                    Type = HLSNAnimator.AnimationType.Rescale,
                    AttachToBehaviour = __instance,
                    EasingMode = HLSNUtil.EasingMode.EASEIN2,
                    Vector2A = Vector2.one,
                    Vector2B = new Vector2(0, 1.1f),
                    Duration = TimeSpan.FromSeconds(.4)
                }, scrollImage.rectTransform);
                yield return new WaitForSeconds(0.2f);
                Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
                {
                    Type = HLSNAnimator.AnimationType.Rescale,
                    AttachToBehaviour = __instance,
                    EasingMode = HLSNUtil.EasingMode.EASEIN2,
                    Vector2A = Vector2.one,
                    Vector2B = new Vector2(0, 1.1f),
                    Duration = TimeSpan.FromSeconds(.25)
                }, scrollFireImage.rectTransform);
                Plugin.MyAnimator.AnimateGraphics(new HLSNAnimationRequest
                {
                    Type = HLSNAnimator.AnimationType.ChangeA,
                    AttachToBehaviour = __instance,
                    ColorA = new Color(0, 0, 0, 1),
                    ColorB = new Color(0, 0, 0, 0),
                    Duration = TimeSpan.FromSeconds(.25)
                }, scrollFireImage);
                yield return new WaitForSeconds(0.5f);
            }
            else
                //1.75 seconds later the fire animation should play.
                yield return new WaitForSeconds(1.75f);

            RoleRevealPanel = (RectTransform) gigaParent.Find("RoleRevealed");
            __instance.PlaySound("Sound/Common/FireSwitch");
            ScreenWiper = gigaParent.Find("ScreenWipe").GetComponent<Image>();
            ScreenWiper.gameObject.SetActive(true);
            ScreenWiper.enabled = true;
            ScreenWiper.color = new Color(1, 1, 1, 1);
            __instance.StartCoroutine(PlayBurnAnimation(ScreenWiper, FireScreenwipeAtlas));
            DateTime animationOver = DateTime.Now + TimeSpan.FromSeconds(1.7);
            yield return new WaitForSeconds(0.933f);
            var componentsToDisable = gigaParent.GetComponentsInChildren<RectTransform>();
            List<string> gameObjectsToDisable = new List<string>() { "RoleLotDetailsPanel", "Main", "WW_Girl", "More Info Button", "Current Role", "Details Toggle", "Scrolling Roles Mask", "Role", "Clicker", "Total Lots Value", "Your Lots", "Total Lots", "Your Lots Value" };
            foreach(var kv in componentsToDisable)
            {
                if(gameObjectsToDisable.Contains(kv.name))
                    kv.gameObject.SetActive(false);
            }
            
            if(holderObject != null)
                holderObject.gameObject.SetActive(false);
            RoleRevealPanel.gameObject.SetActive(true);
            RoleRevealController newController = gigaParent.transform.parent.GetComponentInChildren<RoleRevealController>();
            if(newController != null)
                newController.StartCoroutine(AnimateRoleRevealPanel(newController, myLoadedAssetBundle, gigaParentSizes));
            else
                LogWaterWheelInfo($"Failed to locate the RoleRevealController! Cannot continue with animation.");

            yield return new WaitForSeconds(0.767f);
            ScreenWiper.gameObject.SetActive(false);
        }
        
        static int GetOrderingBasedOnRole(Role role)
        {
            switch(role.Id)
            {
                case 21:
                case 23:
                    return 0;
            }
            if(IsUniqueRole(role))
                return 1;
            return 2;
        }
        
        /// <summary>
        /// Activated at the end of the animation if the arrowhead is still not fully level, but the animation has already stopped and it's in freefall.
        /// </summary>
        static IEnumerator ArrowFinalFreefall(Image arrowhead, double arrowRotationalChangePerFrame, double arrowRotationalVelocity)
        {
            while(arrowhead.rectTransform.localEulerAngles.z > 0)
            {
                Vector3 arrowheadEulerAngles = arrowhead.rectTransform.localEulerAngles;
                double newRotationZ = 0;
                //freefall
                arrowRotationalVelocity += arrowRotationalChangePerFrame;
                newRotationZ = arrowheadEulerAngles.z - arrowRotationalVelocity;
                newRotationZ = Math.Max(0, newRotationZ);
                //LogWaterWheelInfo($"Rotating arrowhead to Z {newRotationZ}");
                arrowhead.rectTransform.localEulerAngles = new Vector3(0, 0, (float)newRotationZ);
                yield return null;
            }
        }

        static IEnumerator AnimateRoleRevealPanel(RoleRevealController instance, AssetBundle myBundle, Rect sizes)
        {
            if (instance is null)
            {
                LogWaterWheelInfo($"AnimateRoleRevealPanel called with null RoleRevealController.");
                yield break;
            }
            //this coroutine starts off when the 'screen wipe' fire fills the screen
            //locate all objects for later

            //instance.RoleName = role name (Probably Text_1)
            //instance.RoleDescription = short role description (Probably Text_2)
            //instance.TipLabel = the tip at the bottom (Tip)
            //instance.RoleImages references BOTH the RoleIcon and the RoleIcon_Dupe! They are preset before this coroutine starts to their correct role.
            Image ghostRoleIcon = (from g in instance.RoleImages where g.name == "RoleIcon_Dupe" select g).ElementAt(0);
            Image largeRoleIcon = (from g in instance.RoleImages where g != ghostRoleIcon select g).ElementAt(0);
            Image grayerBackground = RoleRevealPanel.Find("Backing").GetComponent<Image>();
            Image roleBackground = RoleRevealPanel.Find("Role_Backing").GetComponent<Image>();
            TextMeshProUGUI yourRoleIs = RoleRevealPanel.Find("Text_1").GetComponent<TextMeshProUGUI>();

            //custom objects here

            //LogWaterWheelInfo($"{largeRoleIcon.name} is active itself? {largeRoleIcon.gameObject.activeSelf}. Active in hierarchy? {largeRoleIcon.gameObject.activeInHierarchy}. The rectTransform local position is: {largeRoleIcon.rectTransform.localPosition}, size delta is: {largeRoleIcon.rectTransform.sizeDelta}, anchor min: {largeRoleIcon.rectTransform.anchorMin}, anchor max: {largeRoleIcon.rectTransform.anchorMax}, scale: {largeRoleIcon.rectTransform.localScale}");
            //LogWaterWheelInfo($"{ghostRoleIcon.name} is active itself? {ghostRoleIcon.gameObject.activeSelf}. Active in hierarchy? {ghostRoleIcon.gameObject.activeInHierarchy}. The rectTransform local position is: {ghostRoleIcon.rectTransform.localPosition}, size delta is: {ghostRoleIcon.rectTransform.sizeDelta}, anchor min: {ghostRoleIcon.rectTransform.anchorMin}, anchor max: {ghostRoleIcon.rectTransform.anchorMax}, scale: {ghostRoleIcon.rectTransform.localScale}");
            //TODO: For whatever reason, despite both large role icons being stretched from 0.6 to 1 of the screen vertically and fully stretched horizontally, they only appear in their normal size. Their scale is normal (1,1). Their sizeDelta is set to stretch them out to the full borders... so how do they look normal?
            //IDEAS: Check for all the Components on those objects and make sure you are actually checking the real images concerned (very unlikely that I don't though)
            //ANOTHER IDEA: Maybe we should copy these role Icon objects, instead of making entirely new ones?

            //TODO: The mask doesn't work, because for SOME FUCKING REASON, every role has its background set to 1 alpha, instead of 0 (it's not fully transparent). Thanks BMG...
            //POTENTIAL SOLUTION: Create an entire fucking shader to deal with this. It has to be very small, just set all the color to full white when alpha color is above 1 / 255... Hopefully it works in combination with ChangeA of HLSNAnimator...

            //LogWaterWheelInfo($"t");
            //this is the role flash image and I decided to get rid of it for now.
            /*
            Image roleFlashImage = Image.Instantiate(largeRoleIcon, largeRoleIcon.transform);
            roleFlashImage.rectTransform.anchorMin = Vector2.zero;
            roleFlashImage.rectTransform.anchorMax = Vector2.one;
            roleFlashImage.rectTransform.sizeDelta = Vector2.zero;
            roleFlashImage.rectTransform.localPosition = Vector2.zero;
            roleFlashImage.sprite = largeRoleIcon.sprite;
            */
            //LogWaterWheelInfo($"tasdr");
            //var mat = myBundle.LoadAsset<Material>("BMGRoleFlash.mat");
            //if(mat is null)
            //    Plugin.Log.LogError($"MAT IS NULL! NOT FOUND!");
            //mat.SetFloat("_MaskCutoff", 0.01f);
            //LogWaterWheelInfo($"123123");
            //roleFlashImage.material = mat;
            largeRoleIcon.color = new Color(1, 1, 1, 0);
            ghostRoleIcon.color = new Color(1, 1, 1, 0);
            largeRoleIcon.rectTransform.localScale = new Vector2(1.4f, 1.25f);
            //LogWaterWheelInfo($"123asdfasdf123");

            //preset objects
            grayerBackground.gameObject.SetActive(false);
            instance.RoleDescription.gameObject.SetActive(false);
            instance.RoleName.gameObject.SetActive(false);
            instance.TipLabel.gameObject.SetActive(false);
            var roleCardPanelGO = GameObject.Find("RoleCardPanel");
            if(roleCardPanelGO is null)
                Plugin.Log.LogError($"RoleCardPanel wasn't FOUND!");
            //LogWaterWheelInfo($"rrr");
            
            var roleCardNewGO = GameObject.Instantiate(roleCardPanelGO, RoleRevealPanel);
            roleCardNewGO.transform.localScale = new Vector2(1.4f, 1.4f);
            var roleCardController = roleCardNewGO.GetComponentInChildren<RoleCardController>();
            roleCardController.SetRoleDetails(GlobalServiceLocator.GameService.ActiveGameState.Me);
            //LogWaterWheelInfo($"wtfff");

            var roleNamePanelTransform = roleCardNewGO.transform.Find("RoleNamePanel");
            roleCardNewGO.transform.Find("Minimize Button").gameObject.SetActive(false);
            var roleNameLabelText = roleNamePanelTransform.Find("RoleNameLabel").GetComponent<TextMeshProUGUI>();
            roleNameLabelText.text = Plugin.SelectedRole.Name;
            var roleVictoryConditionTransform = roleNamePanelTransform.Find("VictoryConditionsButton");
            roleVictoryConditionTransform.gameObject.SetActive(false);

            //LogWaterWheelInfo($"{roleCardNewGO.name} is active itself? {roleCardNewGO.gameObject.activeSelf}. Active in hierarchy? {roleCardNewGO.gameObject.activeInHierarchy}. The rectTransform local position is: {roleCardNewGO.transform.localPosition}, size delta is: {((RectTransform)roleCardNewGO.transform).sizeDelta}, anchor min: {((RectTransform)roleCardNewGO.transform).anchorMin}, anchor max: {((RectTransform)roleCardNewGO.transform).anchorMax}, scale: {((RectTransform)roleCardNewGO.transform).localScale}");
            //LogWaterWheelInfo($"{instance.RoleName.name} is active itself? {instance.RoleName.gameObject.activeSelf}. Active in hierarchy? {instance.RoleName.gameObject.activeInHierarchy}. The rectTransform local position is: {instance.RoleName.rectTransform.localPosition}, size delta is: {instance.RoleName.rectTransform.sizeDelta}, anchor min: {instance.RoleName.rectTransform.anchorMin}, anchor max: {instance.RoleName.rectTransform.anchorMax}, scale: {instance.RoleName.rectTransform.localScale}");
            roleCardNewGO.transform.localPosition = new Vector3(sizes.width / 2f + sizes.width / 3f, sizes.height / 2f - sizes.height * 0.05f);
            
            /*
            yield return Plugin.MyAnimator.AnimateGraphics(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.ChangeA,
                AttachToBehaviour = instance,
                ColorA = new Color(0, 0, 0, 0),
                ColorB = new Color(0, 0, 0, 1),
                Duration = TimeSpan.FromSeconds(.25)
            }, roleFlashImage);
            */
            largeRoleIcon.color = new Color(1, 1, 1, 1);


            /*
            Plugin.MyAnimator.AnimateGraphics(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.ChangeA,
                EasingMode = HLSNUtil.EasingMode.EASEIN2,
                AttachToBehaviour = instance,
                ColorA = new Color(0, 0, 0, 1),
                ColorB = new Color(0, 0, 0, 0),
                Duration = TimeSpan.FromSeconds(.4)
            }, roleFlashImage);
            */
            yield return Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Rescale,
                EasingMode = HLSNUtil.EasingMode.EASEIN2,
                AttachToBehaviour = instance,
                Vector2A = new Vector2(1.4f, 1.25f),
                Vector2B = new Vector2(1,1),
                Duration = TimeSpan.FromSeconds(.4)
            }, largeRoleIcon.rectTransform);

            instance.StartCoroutine(ShowAllText(instance, grayerBackground, sizes));
            ghostRoleIcon.gameObject.SetActive(true);
            ghostRoleIcon.enabled = true;
            Plugin.MyAnimator.AnimateGraphics(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.ChangeA,
                AttachToBehaviour = instance,
                ColorA = new Color(0, 0, 0, 1),
                ColorB = new Color(0, 0, 0, 0),
                Duration = TimeSpan.FromSeconds(1)
            }, ghostRoleIcon);
            yield return Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Rescale,
                AttachToBehaviour = instance,
                Vector2A = new Vector2(1f, 1f),
                Vector2B = new Vector2(1.5f, 1.5f),
                Duration = TimeSpan.FromSeconds(1)
            }, ghostRoleIcon.rectTransform);
            //roleFlashImage.gameObject.SetActive(false);
            //wiggle

            yield return Plugin.MyAnimator.Animate3DObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Rotate,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                Vector3A = new Vector3(0, 0, 0),
                Vector3B = new Vector3(0, 0, 10),
                Duration = TimeSpan.FromSeconds(.2)
            }, largeRoleIcon.transform);
            for (int i = 0; i < 2;i++)
            {
                yield return Plugin.MyAnimator.Animate3DObjects(new HLSNAnimationRequest
                {
                    Type = HLSNAnimator.AnimationType.Rotate,
                    AttachToBehaviour = instance,
                    EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                    Vector3A = new Vector3(0, 0, 10),
                    Vector3B = new Vector3(0, 0, -10),
                    Duration = TimeSpan.FromSeconds(.1)
                }, largeRoleIcon.transform);
                yield return Plugin.MyAnimator.Animate3DObjects(new HLSNAnimationRequest
                {
                    Type = HLSNAnimator.AnimationType.Rotate,
                    AttachToBehaviour = instance,
                    EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                    Vector3A = new Vector3(0, 0, -10),
                    Vector3B = new Vector3(0, 0, 10),
                    Duration = TimeSpan.FromSeconds(.1)
                }, largeRoleIcon.transform);
            }
            yield return Plugin.MyAnimator.Animate3DObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Rotate,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                Vector3A = new Vector3(0, 0, 10),
                Vector3B = new Vector3(0, 0, -10),
                Duration = TimeSpan.FromSeconds(.1)
            }, largeRoleIcon.transform);
            yield return Plugin.MyAnimator.Animate3DObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Rotate,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                Vector3A = new Vector3(0, 0, -10),
                Vector3B = new Vector3(0, 0, 0),
                Duration = TimeSpan.FromSeconds(.15)
            }, largeRoleIcon.transform);
            yield return new WaitForSeconds(1f);
            Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Move,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                Vector2A = new Vector2(0, roleBackground.rectTransform.localPosition.y),
                Vector2B = new Vector3(-sizes.width * 0.2f, roleBackground.rectTransform.localPosition.y),
                Duration = TimeSpan.FromSeconds(.2)
            }, roleBackground.rectTransform);
            Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Move,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                Vector2A = new Vector2(0, yourRoleIs.rectTransform.localPosition.y),
                Vector2B = new Vector3(-sizes.width * 0.2f, yourRoleIs.rectTransform.localPosition.y),
                Duration = TimeSpan.FromSeconds(.2)
            }, yourRoleIs.rectTransform);
            Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Move,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                Vector2A = new Vector2(0, instance.RoleName.rectTransform.localPosition.y),
                Vector2B = new Vector3(-sizes.width * 0.2f, instance.RoleName.rectTransform.localPosition.y),
                Duration = TimeSpan.FromSeconds(.2)
            }, instance.RoleName.rectTransform);
            Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Move,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                Vector2A = new Vector2(0, largeRoleIcon.rectTransform.localPosition.y),
                Vector2B = new Vector3(-sizes.width * 0.2f, largeRoleIcon.rectTransform.localPosition.y),
                Duration = TimeSpan.FromSeconds(.2)
            }, largeRoleIcon.rectTransform);
            //roleCardNewGO.transform.localPosition = new Vector3(Screen.width / 2f + Screen.width / 4f, Screen.height / 3f);
            Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Move,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                Vector2A = roleCardNewGO.transform.localPosition,
                Vector2B = new Vector3(sizes.width / 2f - sizes.width / 4f + 220, sizes.height / 2f - sizes.height * 0.05f),
                Duration = TimeSpan.FromSeconds(.2)
            }, (RectTransform) roleCardNewGO.transform);
            Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.MoveAnchorMin,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                Vector2A = instance.RoleName.rectTransform.anchorMin,
                Vector2B = new Vector2(0.25f, 0.3f),
                Duration = TimeSpan.FromSeconds(.2)
            }, instance.RoleName.rectTransform);
            Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.MoveAnchorMax,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                Vector2A = instance.RoleName.rectTransform.anchorMax,
                Vector2B = new Vector2(0.75f, 0.5f),
                Duration = TimeSpan.FromSeconds(.2)
            }, instance.RoleName.rectTransform);
            if(GlobalServiceLocator.UserService.Settings.ExtendedPlayerNumbersEnabled)
            {
                yield return new WaitForSeconds(0.5f);
                var newScrollCountText = TextMeshProUGUI.Instantiate(instance.WaterWheelController.SelectedRoleSlot.OddsLabel, RoleRevealPanel).GetComponent<TextMeshProUGUI>();
                newScrollCountText.gameObject.SetActive(true);
                newScrollCountText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                newScrollCountText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                newScrollCountText.rectTransform.sizeDelta = new Vector2(sizes.width * 0.09f, sizes.height / 12f);
                newScrollCountText.margin = new Vector4(0, 0, 0, 0);
                newScrollCountText.alignment = TextAlignmentOptions.Center;
                newScrollCountText.enableAutoSizing = true;
                newScrollCountText.fontSizeMin = 10;
                newScrollCountText.fontSizeMax = 150;
                newScrollCountText.text = $"You are <sprite=\"PlayerNumbers\" index={GlobalServiceLocator.GameService.ActiveGameState.Me.Position + 1}>";

                yield return Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
                {
                    Type = HLSNAnimator.AnimationType.Move,
                    AttachToBehaviour = instance,
                    EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                    Vector2A = new Vector2(-sizes.width / 2f + sizes.width * 0.075f, -sizes.height / 2f - sizes.height / 14f),
                    Vector2B = new Vector2(-sizes.width / 2f + sizes.width * 0.075f, -sizes.height / 2f + sizes.height / 14f),
                    Duration = TimeSpan.FromSeconds(1)
                }, (RectTransform)newScrollCountText.transform);
                //LogWaterWheelInfo($"{newScrollCountText.name} is active itself? {newScrollCountText.gameObject.activeSelf}. Active in hierarchy? {newScrollCountText.gameObject.activeInHierarchy}. The rectTransform local position is: {newScrollCountText.rectTransform.localPosition}, size delta is: {newScrollCountText.rectTransform.sizeDelta}, anchor min: {newScrollCountText.rectTransform.anchorMin}, anchor max: {newScrollCountText.rectTransform.anchorMax}, scale: {newScrollCountText.rectTransform.localScale}");
            }
        }
        
        static IEnumerator ShowAllText(RoleRevealController instance, Image grayerBackground, Rect sizes)
        {
            grayerBackground.gameObject.SetActive(true);
            yield return Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Move,
                EasingMode = HLSNUtil.EasingMode.EASEOUT3,
                AttachToBehaviour = instance,
                Vector2A = new Vector2(-sizes.width, grayerBackground.rectTransform.localPosition.y),
                Vector2B = new Vector2(0, grayerBackground.rectTransform.localPosition.y),
                Duration = TimeSpan.FromSeconds(.5)
            }, grayerBackground.rectTransform);
            instance.RoleName.gameObject.SetActive(true);
            yield return Plugin.MyAnimator.AnimateTMPs(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.TMPRetext,
                AttachToBehaviour = instance,
                StringA = "",
                StringB = Plugin.SelectedRole.Name,
                Duration = TimeSpan.FromSeconds(.3)
            }, instance.RoleName);
            instance.RoleDescription.gameObject.SetActive(true);
            yield return Plugin.MyAnimator.AnimateTMPs(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.TMPRetext,
                EasingMode = HLSNUtil.EasingMode.EASEOUT3,
                AttachToBehaviour = instance,
                StringA = "",
                StringB = instance.l10n(string.Format("GUI_ROLE_SELECTED_DESC_{0}", Plugin.SelectedRole.Id.ToString())),
                Duration = TimeSpan.FromSeconds(.75)
            }, instance.RoleDescription);
            instance.TipLabel.gameObject.SetActive(true);
            yield return Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Move,
                EasingMode = HLSNUtil.EasingMode.EASEOUT3,
                AttachToBehaviour = instance,
                Vector2A = new Vector2(instance.TipLabel.rectTransform.localPosition.x, -sizes.height /2f - 300f),
                Vector2B = new Vector2(instance.TipLabel.rectTransform.localPosition.x, instance.TipLabel.rectTransform.localPosition.y),
                Duration = TimeSpan.FromSeconds(1)
            }, instance.TipLabel.rectTransform);
            
        }
        
        static IEnumerator PlayScrollCounterAnimation(TextMeshProUGUI oldText, TextMeshProUGUI newText, bool ranOut, WaterWheelController instance)
        {
            double duration = 0.2;
            string soundName = "Sound/Hellession/ScrollConsumed";
            if(ranOut)
            {
                duration = 0.3;
                soundName = "Sound/Hellession/OutOfScrolls";
            }
            var oldDropAnimation = new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Move,
                AttachToBehaviour = instance,
                Vector2A = new Vector2(0, 0),
                Vector2B = new Vector2(0, -oldText.rectTransform.sizeDelta.y),
                EasingMode = HLSNUtil.EasingMode.EASEIN3,
                Duration = TimeSpan.FromSeconds(duration)
            };
            var newDropAnimation = new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Move,
                AttachToBehaviour = instance,
                Vector2A = new Vector2(0, newText.rectTransform.sizeDelta.y),
                Vector2B = new Vector2(0, 0),
                EasingMode = HLSNUtil.EasingMode.EASEIN3,
                Duration = TimeSpan.FromSeconds(duration)
            };
            var springBack1 = new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Move,
                AttachToBehaviour = instance,
                Vector2A = new Vector2(0, 0),
                Vector2B = new Vector2(0, -newText.rectTransform.sizeDelta.y * 0.2f),
                EasingMode = HLSNUtil.EasingMode.EASEOUT3,
                Duration = TimeSpan.FromSeconds(duration / 4)
            };
            var springBack2 = new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Move,
                AttachToBehaviour = instance,
                Vector2A = new Vector2(0, -newText.rectTransform.sizeDelta.y * 0.2f),
                Vector2B = new Vector2(0, 0),
                EasingMode = HLSNUtil.EasingMode.EASEIN3,
                Duration = TimeSpan.FromSeconds(duration / 4)
            };
            instance.PlaySound(soundName);
            Plugin.MyAnimator.AnimateObjects(oldDropAnimation, oldText.rectTransform);
            yield return Plugin.MyAnimator.AnimateObjects(newDropAnimation, newText.rectTransform);
            yield return Plugin.MyAnimator.AnimateObjects(springBack1, newText.rectTransform);
            yield return Plugin.MyAnimator.AnimateObjects(springBack2, newText.rectTransform);
        }
        
        public static IEnumerator PlayBurnAnimation(Image imageToPlayFor, List<Sprite> spriteList, int frames = 20)
        {
            //Every 1 / 12th of a second the fire sprite should change
            DateTime changeAt = DateTime.Now + TimeSpan.FromSeconds(1 / 12.0);
            int frame = 0;
            imageToPlayFor.sprite = spriteList[frame];
            for (;frame < frames;)
            {
                if (DateTime.Now >= changeAt)
                {
                    frame++;
                    imageToPlayFor.sprite = spriteList[frame % spriteList.Count];
                    changeAt = DateTime.Now + TimeSpan.FromSeconds(1 / 12.0);
                }
                yield return null;
            }
        }
        
        static void UpdateWWGirl(Image girl, double seconds, bool preSpin)
        {
            List<KeyValuePair<double, Sprite>> preSpinTable = new List<KeyValuePair<double, Sprite>>()
            {
                new KeyValuePair<double, Sprite>(0.56, WaterWheelGirlAtlas[0]),
                new KeyValuePair<double, Sprite>(0.895, WaterWheelGirlAtlas[1]),
                new KeyValuePair<double, Sprite>(10000, WaterWheelGirlAtlas[2])
            };
            List<KeyValuePair<double, Sprite>> beforeImmediateRespinTable = new List<KeyValuePair<double, Sprite>>()
            {
                new KeyValuePair<double, Sprite>(0.032, WaterWheelGirlAtlas[2]),
                new KeyValuePair<double, Sprite>(0.136, WaterWheelGirlAtlas[3]),
                new KeyValuePair<double, Sprite>(0.233, WaterWheelGirlAtlas[4]),
                new KeyValuePair<double, Sprite>(0.33, WaterWheelGirlAtlas[5]),
                new KeyValuePair<double, Sprite>(0.4, WaterWheelGirlAtlas[6]),
                new KeyValuePair<double, Sprite>(0.44, WaterWheelGirlAtlas[5]),
                new KeyValuePair<double, Sprite>(0.46, WaterWheelGirlAtlas[4]),
                new KeyValuePair<double, Sprite>(0.48, WaterWheelGirlAtlas[3]),
                new KeyValuePair<double, Sprite>(1000, WaterWheelGirlAtlas[2]),
            };
            List<KeyValuePair<double, Sprite>> afterImmediateRespinTable = new List<KeyValuePair<double, Sprite>>()
            {
                new KeyValuePair<double, Sprite>(0.032, WaterWheelGirlAtlas[2]),
                new KeyValuePair<double, Sprite>(0.136, WaterWheelGirlAtlas[3]),
                new KeyValuePair<double, Sprite>(0.233, WaterWheelGirlAtlas[4]),
                new KeyValuePair<double, Sprite>(0.33, WaterWheelGirlAtlas[5]),
                new KeyValuePair<double, Sprite>(0.434, WaterWheelGirlAtlas[6]),
                new KeyValuePair<double, Sprite>(0.434, WaterWheelGirlAtlas[7]),
                new KeyValuePair<double, Sprite>(0.6356, WaterWheelGirlAtlas[8]),
                new KeyValuePair<double, Sprite>(0.7328, WaterWheelGirlAtlas[9]),
                new KeyValuePair<double, Sprite>(0.83, WaterWheelGirlAtlas[10]),
                new KeyValuePair<double, Sprite>(1.0314, WaterWheelGirlAtlas[11]),
                new KeyValuePair<double, Sprite>(1.1356, WaterWheelGirlAtlas[12]),
                new KeyValuePair<double, Sprite>(1.1981, WaterWheelGirlAtlas[13]),
                new KeyValuePair<double, Sprite>(1.3022, WaterWheelGirlAtlas[14]),
                new KeyValuePair<double, Sprite>(1.3647, WaterWheelGirlAtlas[15]),
                new KeyValuePair<double, Sprite>(1.4342, WaterWheelGirlAtlas[16]),
                new KeyValuePair<double, Sprite>(1.6356, WaterWheelGirlAtlas[17]),
                new KeyValuePair<double, Sprite>(1.7328, WaterWheelGirlAtlas[18]),
                new KeyValuePair<double, Sprite>(1.8022, WaterWheelGirlAtlas[19]),
                new KeyValuePair<double, Sprite>(1.8994, WaterWheelGirlAtlas[20]),
                new KeyValuePair<double, Sprite>(1.9967, WaterWheelGirlAtlas[21]),
                new KeyValuePair<double, Sprite>(2.1008, WaterWheelGirlAtlas[22]),
                new KeyValuePair<double, Sprite>(2.1633, WaterWheelGirlAtlas[23]),
                new KeyValuePair<double, Sprite>(10000, WaterWheelGirlAtlas[24])
            };
            List<KeyValuePair<double, Sprite>> delayedRespinTable = new List<KeyValuePair<double, Sprite>>()
            {
                new KeyValuePair<double, Sprite>(0.02, WaterWheelGirlAtlas[2]),
                new KeyValuePair<double, Sprite>(0.035, WaterWheelGirlAtlas[3]),
                new KeyValuePair<double, Sprite>(0.05, WaterWheelGirlAtlas[4]),
                new KeyValuePair<double, Sprite>(0.08, WaterWheelGirlAtlas[5]),
                new KeyValuePair<double, Sprite>(1000, WaterWheelGirlAtlas[6])
            };
            if(preSpin)
            {
                ChangeSpriteBasedOnTime(girl, seconds, preSpinTable);
                return;
            }
            if (seconds < lastImmediateSpinFlatlineTime)
            {
                //ONLY IMMEDIATE SPINS ARE INVOLVED. DELAYED SPINS HAVE NOTHING TO DO WITH THIS.
                //Figure out which immediate spin function we need to use.
                int i = immediateSpinStrengthPerFrame.Count - 1;
                while (i > 0 && seconds < i * ImmediateSpinDelay)
                {
                    i--;
                    //seconds -= 0.5;
                }
                seconds -= i * 0.5;
                //at this point 'i' is the index of the function that has to be used.
                if(i < immediateSpinStrengthPerFrame.Count - 1)
                    ChangeSpriteBasedOnTime(girl, seconds, beforeImmediateRespinTable);
                else
                    ChangeSpriteBasedOnTime(girl, seconds, afterImmediateRespinTable);
                return;
            }
            if (delayedSpinStrengthPerFrame.Count > 0)
            {
                LogWaterWheelInfo($"DELAYED SPIN AFTER IMMEDIATE FLATLINE TIME!!!");
                int i = 0;
                double lastSpinCorrectedTime = seconds;
                double nextSpinTime = seconds;
                double runningTime = lastImmediateSpinFlatlineTime;
                while (i < delayedSpinStrengthPerFrame.Count)
                {
                    nextSpinTime = delayedSpinDelays[i] + runningTime;
                    var flatlineTime = GetFlatlineTime(delayedSpinFlatlines[i], delayedSpinStrengthPerFrame[i], delayedSpinBs[i], nextSpinTime + ImmediateSpinPeakTime);
                    if (nextSpinTime > seconds)
                    {
                        //it's in a flatline before a delayed spin (i is the NEXT delayed spin)
                        if (nextSpinTime - seconds <= 0.1)
                        {
                            LogWaterWheelInfo($"IN LESS THAN 0.1 SECONDS NEXT DELAYED SPIN STARTS. Nextspintime: {nextSpinTime}. Seconds: {seconds}. LastSpinCorrectedTime: {lastSpinCorrectedTime}. Flatline time: {flatlineTime}. Running time: {runningTime}");
                            ChangeSpriteBasedOnTime(girl, nextSpinTime - lastSpinCorrectedTime, delayedRespinTable);
                        }
                        else
                        {
                            LogWaterWheelInfo($"FLATLINE BEFORE DELAYED SPIN. Nextspintime: {nextSpinTime}. Seconds: {seconds}. LastSpinCorrectedTime: {lastSpinCorrectedTime}. Flatline time: {flatlineTime}. Running time: {runningTime}");
                            ChangeSpriteBasedOnTime(girl, lastSpinCorrectedTime, afterImmediateRespinTable);
                        }
                        return;
                    }
                    else if (flatlineTime > seconds)
                    {
                        lastSpinCorrectedTime -= nextSpinTime;
                        //ok so this is currently in a delayed spin (before its flatline) (i is the CURRENT delayed spin)
                        //LogWaterWheelInfo($"IN DELAYED SPIN BEFORE FLATLINE. Nextspintime: {nextSpinTime}. Seconds: {seconds}. LastSpinCorrectedTime: {lastSpinCorrectedTime}. Flatline time: {flatlineTime}. Running time: {runningTime}");
                        ChangeSpriteBasedOnTime(girl, lastSpinCorrectedTime, afterImmediateRespinTable);
                        return;
                    }
                    i++;
                    lastSpinCorrectedTime -= nextSpinTime;
                    runningTime = flatlineTime;
                }
                //it's in a flatline after the LAST delayed spin.
                //LogWaterWheelInfo($"IN LAST DELAYED SPIN! Nextspintime: {nextSpinTime}. Seconds: {seconds}. LastSpinCorrectedTime: {lastSpinCorrectedTime}. Running time: {runningTime}");
                ChangeSpriteBasedOnTime(girl, lastSpinCorrectedTime, afterImmediateRespinTable);
                return;
            }
            ChangeSpriteBasedOnTime(girl, seconds, afterImmediateRespinTable);
        }
        
        static void ChangeSpriteBasedOnTime(Image img, double seconds, List<KeyValuePair<double, Sprite>> spriteTable)
        {
            for (int i = 0; i < spriteTable.Count - 2;i++)
            {
                if(seconds < spriteTable[i].Key)
                {
                    ChangeImageSprite(img, spriteTable[i].Value);
                    return;
                }
            }
            ChangeImageSprite(img, spriteTable.Last().Value);
        }
        
        static void ChangeImageSprite(Image img, Sprite spr)
        {
            if(img.sprite != spr)
                img.sprite = spr;
        }
        
        static IEnumerator HandleSelectedRoleWiggle(WaterWheelController instance)
        {
            //the wiggle lasts 1 second.
            //Wiggle 1: counter clockwise in 5 frames
            //Wiggle 2: clockwise in 15 frames
            //Wiggle 3: counter clockwise again in 12 frames
            //Wiggle 4: finally back into its initial rotation
            yield return Plugin.MyAnimator.Animate3DObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Rotate,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                Vector3A = instance.SelectedRoleSlot.transform.localEulerAngles,
                Vector3B = new Vector3(0, 0, 6),
                Duration = TimeSpan.FromSeconds(0.083)
            }, instance.SelectedRoleSlot.transform);
            yield return Plugin.MyAnimator.Animate3DObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Rotate,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                Vector3A = instance.SelectedRoleSlot.transform.localEulerAngles,
                Vector3B = new Vector3(0, 0, -10),
                Duration = TimeSpan.FromSeconds(0.25)
            }, instance.SelectedRoleSlot.transform);
            yield return Plugin.MyAnimator.Animate3DObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Rotate,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                Vector3A = instance.SelectedRoleSlot.transform.localEulerAngles,
                Vector3B = new Vector3(0, 0, 8),
                Duration = TimeSpan.FromSeconds(0.2)
            }, instance.SelectedRoleSlot.transform);
            yield return Plugin.MyAnimator.Animate3DObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Rotate,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.EASEINOUT2,
                Vector3A = instance.SelectedRoleSlot.transform.localEulerAngles,
                Vector3B = new Vector3(0, 0, 0),
                Duration = TimeSpan.FromSeconds(0.467)
            }, instance.SelectedRoleSlot.transform);
            
        }
        
        static bool IsUniqueRole(Role role)
        {
            //All unique roles:
            //4, 6, 8, 13, 21, 23, 31, 51, 52, 53, 55, 56, 57, 58, 59, 60, 61
            switch(role.Id)
            {
                case 4:
                case 6:
                case 8:
                case 13:
                case 21:
                case 23:
                case 31:
                case 51:
                case 52:
                case 53:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                case 61:
                    return true;
            }
            return false;
        }

        static IEnumerator HandleSelectedRoleFlash(WaterWheelController instance)
        {
            //the flash lasts 2 / 6 seconds.
            //It flashes up in 1/6th of a second, then down in also 1/6th of a second.
            Image createdComponent = new GameObject("HLSN_RoleFlash", new Type[] { typeof(Image) }).GetComponent<Image>();
            createdComponent.transform.SetParent(instance.SelectedRoleSlot.transform);
            createdComponent.rectTransform.localPosition = new Vector2(0, 0);
            //just to be safe. It'd be easier to just set the anchors, but I have no fucking clue what it'd do if I did that.
            createdComponent.rectTransform.sizeDelta = new Vector2(500, Convert.ToSingle(RoleSlotHeightReal));
            createdComponent.color = new Color(1, 1, 1, 0);
            yield return Plugin.MyAnimator.AnimateGraphics(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.ChangeA,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.LINEAR,
                ColorA = new Color(1, 1, 1, 0),
                ColorB = new Color(1, 1, 1, 1),
                Duration = TimeSpan.FromSeconds(1 / 6.0)
            }, createdComponent);
            yield return Plugin.MyAnimator.AnimateGraphics(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.ChangeA,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.LINEAR,
                ColorA = new Color(1, 1, 1, 1),
                ColorB = new Color(1, 1, 1, 0),
                Duration = TimeSpan.FromSeconds(1 / 6.0)
            }, createdComponent);
        }

        static IEnumerator HandleSelectedRoleJump(WaterWheelController instance)
        {
            //the jump lasts 2 / 6 seconds.
            //It jumps up in 1/6th of a second, then down in also 1/6th of a second.
            yield return Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Rescale,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.EASEIN2,
                Vector2A = new Vector2(1, 1),
                Vector2B = new Vector2(1.2f, 1.2f),
                Duration = TimeSpan.FromSeconds(1 / 6.0)
            }, instance.SelectedRoleSlot.gameObject.GetComponent<RectTransform>());
            yield return Plugin.MyAnimator.AnimateObjects(new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Rescale,
                AttachToBehaviour = instance,
                EasingMode = HLSNUtil.EasingMode.EASEOUT2,
                Vector2A = new Vector2(1.2f, 1.2f),
                Vector2B = new Vector2(1, 1),
                Duration = TimeSpan.FromSeconds(1 / 6.0)
            }, instance.SelectedRoleSlot.gameObject.GetComponent<RectTransform>());
        }
        
        static void OnRoleChangedSmart(Role newRole, WaterWheelController instance, bool makeSound = true)
        {
            if(makeSound)
                instance.PlaySound("Sound/WaterWheel/Click1");
            instance.CurrentRoleLabel.SetText(newRole.Name);
            instance.CurrentRoleLabel.faceColor = newRole.Color;
            if(Plugin.RoleLotInfos.ContainsKey(newRole))
            {
                RoleLotInfo roleLotInfo = Plugin.RoleLotInfos[newRole];
                instance.CurrentRoleLots.SetText(roleLotInfo.Lots.ToString());
                instance.CurrentRoleTotalLots.SetText(roleLotInfo.TotalLots.ToString());
            }
            else
            {
                instance.CurrentRoleLots.SetText(HLSNUtil.GetRandomInt(1, 200).ToString());
                instance.CurrentRoleTotalLots.SetText(HLSNUtil.GetRandomInt(150, 4000).ToString());
            }
        }

        static List<WaterWheelRoleController> allSlots;

        static List<bool> slotOccupancyState;

        static int arrowheadSettleIndex;

        static List<int> GetValidGroupPositions(List<Role> roleGroup, bool containsPlayer)
        {
            //very inefficient method imo
            List<int> result = new List<int>();
            for (int i = 0; i < allSlots.Count;i++)
            {
                bool legit = true;
                for (int i2 = 0; i2 < roleGroup.Count;i2++)
                {
                    if(arrowheadSettleIndex == i + i2 && !containsPlayer)
                    {
                        legit = false;
                        //skip all the i positions, because at this point we know that any position until this i2 position will also be occupied and invalid
                        i += i2;
                        break;
                    }
                    if (slotOccupancyState.Count > i + i2 && slotOccupancyState[i + i2])
                    {
                        legit = false;
                        //skip all the i positions, because at this point we know that any position until this i2 position will also be occupied and invalid
                        i += i2;
                        break;
                    }
                }
                if(legit)
                    result.Add(i);
            }
            return result;
        }

        static List<List<Role>> RoleGroups;
        
        /// <summary>
        /// 0-Based positions of each slot that has a random blab
        /// </summary>
        static List<int> RandomBlabs;

        static List<bool> RoleGroupContainsPlayer;
        
        /// <summary>
        /// Key: Starting Position (0-based), Value: Group index in RoleGroups
        /// </summary>
        static Dictionary<int, int> RoleGroupPlacements;

        /// <summary>
        /// Attempts to use the id provided to find all the roles grouped into one.
        /// </summary>
        /// <param name="id">The role ID that is actually a group.</param>
        /// <param name="gameRules">The Game Rules of Town of Salem.</param>
        static List<Role> ScourRoleGroup(int id, GameRules gameRules)
        {
            List<Role> result = new List<Role>();
            if(gameRules.RandomRoles.ContainsKey(id))
            {
                foreach(var kv in gameRules.RandomRoles[id])
                {
                    if(gameRules.RandomRoles.ContainsKey(kv.Id) && gameRules.RandomRoles[kv.Id] != null && gameRules.RandomRoles[kv.Id].Count > 0)
                    {
                        //gonna try and be extra sure to be safe here.
                        result.AddRange(ScourRoleGroup(kv.Id, gameRules));
                    }
                    else
                        result.Add(kv);
                }
                switch(id)
                {
                    case 71:
                        result.Add(gameRules.Roles[52]);
                        result.Add(gameRules.Roles[53]);
                        break;
                    case 74:
                        result.Add(gameRules.Roles[53]);
                        break;
                }
            }
            return result;
        }

        static double GetParabolicFunctionB(double peakSpeed, double timeAtZero, double timeToPeak)
        {
            return Math.Sqrt(Math.Abs(-peakSpeed / (-Math.Pow(timeAtZero, 2) - 2 * timeAtZero * timeToPeak + Math.Pow(timeToPeak, 2))));
        }

        /// <summary>
        /// Compute parabolic function here
        /// </summary>
        /// <param name="x">time (in seconds since start)</param>
        /// <param name="a">peak speed per FRAME (SpinStrengthPerFrame)</param>
        /// <param name="b">sqrt(-a / c^2) . Get from GetParabolicFunctionB()</param>
        /// <param name="c">how many seconds it takes for the parabola to peak (NOTE - this peak should be based off the starting time)</param>
        /// <returns>The Y</returns>
        static double ComputeParabolic(double x, double a, double b, double c)
        {
            return -Math.Pow(b * (x - c), 2) + a;
        }
        
        /// <summary>
        /// Gets the X (time in sec) at which the 'flatline' starts - the slow drop off till the water wheel halts.
        /// </summary>
        static double GetFlatlineTime(double y, double a, double b, double c)
        {
            double formulaicA = -Math.Pow(b, 2);
            double formulaicB = Math.Pow(b, 2) * c * 2;
            double formulaicC = -Math.Pow(b, 2) * Math.Pow(c, 2) + a - y;
            double discriminant = Math.Pow(formulaicB, 2) - 4 * formulaicA * formulaicC;
            if(discriminant <= 0)
                throw new Exception($"Invalid values in GetFlatlineTime() or error in coding. Y = {y}, A = {a}, B = {b}, C = {c}, formulaicA = {formulaicA}, formulaicB = {formulaicB}, formulaicC = {formulaicC}, discriminant = {discriminant}");
            double sol1 = (-formulaicB + Math.Sqrt(discriminant)) / (2 * formulaicA);
            double sol2 = (-formulaicB - Math.Sqrt(discriminant)) / (2 * formulaicA);
            //LogWaterWheelInfo($"GetFlatlineTime() is computing Y = {y}, A = {a}, B = {b}, C = {c}, formulaicA = {formulaicA}, formulaicB = {formulaicB}, formulaicC = {formulaicC}, discriminant = {discriminant}, solution 1 = {sol1} and solution 2 = {sol2}");
            return Math.Max(sol1, sol2);
        }
        
        static double GetSpeedPerFrame(double seconds)
        {
            return GetSpeedPerSecond(seconds) /** lockedUpdateTime*/;
        }
        
        /// <summary>
        /// Returns the speed of the water wheel's spin AT the specified amount of seconds.
        /// Uses private static fields to calculate the speed.
        /// Returns speed PER FRAME, not frame.
        /// </summary>
        /// <remarks>The seconds argument means the seconds AFTER the actual spin is made. It does not count the period when the woman pulls the water wheel back a bit. That period should be handled by HLSNAnimator.</remarks>
        static double GetSpeedPerSecond(double seconds)
        {
            /*
              BASE SPIN PERIOD FUNCTIONS
              
              The numbers I'd have to deal with are so big, that I believe I will use smaller numbers to avoid overflows
              for that reason, the speed is now being changed to speed per FRAME, NOT SECONDS.
              ALWAYS USE DOUBLES FOR COMPUTATIONS FOR BEST PRECISION.
              
              Parabolic spin: y = -(b(x-c))^2 + a
              Where: 
              c is how many seconds it takes for the parabola to peak (NOTE - this peak should be based off the starting time)
              a is peak speed per FRAME
              b is some number we have to calculate so that at 0 (or expected X) the speed is 0!
              b = sqrt(-a / c^2)
              x is time (in seconds)
              y is speed per FRAME
              
              Slow drop off till halt: y = -0.03x + a
              Where:
              x is time (in seconds)
              y is speed per FRAME
              a is some number you need to calculate
              
              Priority for which function is used to calculate the speed
              1. Drop off after the last delayed respin
              2. Latest applicable and active delayed respin
              3. Drop off after immediate respins
              4. Latest and active immediate respin
              
              
            */

            //attempt to use a delayed respin - the latest one that's applicable.
            //only latest spin's B value is required, because everything else is tied to time
            //...no actually. That's false. We need to calculate every B of everything.
            
            if(seconds < lastImmediateSpinFlatlineTime)
            {
                //ONLY IMMEDIATE SPINS ARE INVOLVED. DELAYED SPINS HAVE NOTHING TO DO WITH THIS.
                //Figure out which immediate spin function we need to use.
                int i = immediateSpinStrengthPerFrame.Count - 1;
                while(i > 0 && seconds < i * ImmediateSpinDelay)
                    i--;
                //at this point 'i' is the index of the function that has to be used.
                return Math.Max(0, ComputeParabolic(seconds, immediateSpinStrengthPerFrame[i], immediateSpinBs[i], i * ImmediateSpinDelay + ImmediateSpinPeakTime));
            }
            //it's either a flatline or a delayedSpin here. you have to figure out which one...
            if(delayedSpinStrengthPerFrame.Count > 0)
            {
                int i = 0;
                double runningTime = lastImmediateSpinFlatlineTime;
                double lastSpeedFlatline = immediateSpinFlatline;
                while(i<delayedSpinStrengthPerFrame.Count)
                {
                    var flatlineTime = GetFlatlineTime(delayedSpinFlatlines[i], delayedSpinStrengthPerFrame[i], delayedSpinBs[i], delayedSpinDelays[i] + runningTime + ImmediateSpinPeakTime);
                    if(delayedSpinDelays[i] + runningTime > seconds)
                    {
                        //it's in a flatline before a delayed spin
                        double a = lastSpeedFlatline - -FlatlineFallOffPerFrame * runningTime;
                        return Math.Max(0, -FlatlineFallOffPerFrame * seconds + a);
                    }
                    else if(flatlineTime > seconds)
                    {
                        //ok so this is currently in a flatline spin.
                        return Math.Max(0, ComputeParabolic(seconds, delayedSpinStrengthPerFrame[i], delayedSpinBs[i], delayedSpinDelays[i] + runningTime + ImmediateSpinPeakTime));
                    }
                    i++;
                    runningTime = flatlineTime;
                }
                //it's in a flatline after the LAST delayed spin.
                double a2 = delayedSpinFlatlines.Last() - -FlatlineFallOffPerFrame * runningTime;
                return Math.Max(0, -FlatlineFallOffPerFrame * seconds + a2);
            }
            double a3 = immediateSpinFlatline - -FlatlineFallOffPerFrame * lastImmediateSpinFlatlineTime;
            return Math.Max(0, -FlatlineFallOffPerFrame * seconds + a3);
        }

        static List<double> immediateSpinBs;
        
        /// <summary>
        /// Also known as 'a'
        /// </summary>
        static List<double> immediateSpinStrengthPerFrame;
        /// <summary>
        /// uses Speed PER FRAME not second.
        /// </summary>
        static double immediateSpinFlatline;
        static double lastImmediateSpinFlatlineTime;

        static List<double> delayedSpinBs;
        static List<double> delayedSpinStrengthPerFrame;
        static List<double> delayedSpinDelays;
        /// <summary>
        /// uses Speed PER FRAME not second.
        /// </summary>
        static List<double> delayedSpinFlatlines;

        static double lockedUpdateTime;

        public static RectTransform RoleRevealPanel;
        public static Image ScreenWiper;
    }

    [HarmonyPatch(typeof(BasePreGameSceneController), "HandleServerOnFirstDayTransition")]
    class PatchHideItAll
    {
        //TODO: TRANSPILE THIS!!!
        static bool Prefix(BasePreGameSceneController __instance)
        {
            //Plugin.Log.LogInfo($"PREFIX BasePreGameSceneController.HandleServerOnFirstDayTransition() FIRED.");
            __instance.SoundControlPanelGO.SetActive(false);
            Debug.Log("Hiding RoleRevealPanel.");
            __instance.StartCoroutine(UnloadPreGame(__instance));

            return false;
        }
        
        static IEnumerator UnloadPreGame(BasePreGameSceneController __instance)
        {
            yield return new WaitForSeconds(1f);
            __instance.RoleRevealPanel.Hide();
            
            yield return new WaitForSeconds(2f);
            GlobalServiceLocator.ApplicationService.UnloadScene(Scene.PreGame);
            yield break;
        }
    }

    [HarmonyPatch(typeof(WaterWheelController), nameof(WaterWheelController.Hide))]
    class PatchOnRoleChanged
    {
        static void Postfix(WaterWheelController __instance)
        {
            RectTransform gigaParent = (RectTransform)__instance.SelectedRoleSlot.transform.parent.parent.parent;
            var springBack2 = new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Move,
                AttachToBehaviour = __instance,
                Vector2A = new Vector2(0, 0),
                Vector2B = new Vector2(0, ((RectTransform)__instance.transform).rect.height),
                EasingMode = HLSNUtil.EasingMode.EASEIN3,
                Duration = TimeSpan.FromSeconds(2f)
            };
            Plugin.MyAnimator.AnimateObjects(springBack2, (RectTransform)gigaParent.parent);
            //Plugin.Log.LogInfo($"Postfix WaterWheelController.Hide() FIRED.");
        }
    }
}