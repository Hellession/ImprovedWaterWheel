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
using UnityEngine.U2D;
using UnityEngine.UI;
using Hellession;
using System.IO;

namespace UnpredictableWaterWheel
{
    [Serializable]
    public class JsonWWSettings
    {
        public OddsMode oddsMode;
        public bool disabled;
        public bool debugMode = false;
        public bool logMode = true;
        public bool enableMockGameDebug = false;
        public RoleAppearanceMode roleApparanceMode = RoleAppearanceMode.AffectedByScrolls;

        public double immediateRespinChance = 0.05;
        public double delayedRespinChance = 0.002;
        public int maxImmediateRespins = 20;
        public int maxDelayedRespins = 10;

        public double minImmediateSpinStrength = 4000;
        public double maxImmediateSpinStrength = 6500;
        public double minDelayedSpinStrength = 3800;
        public double maxDelayedSpinStrength = 6000;
        public double minImmediateFlatlineFactor = 0.8;
        public double maxImmediateFlatlineFactor = 1.2;
        public double minDelayedFlatlineFactor = 0.7;
        public double maxDelayedFlatlineFactor = 1.2;

        public float customRoleGroupWeight = 0;
        public float investResultWeight = 0.7f;
        public float subalignmentWeight = 0.25f;
        public float factionWeight = 0.05f;
        public double roleGroupSpawnChance = 0.5;
        public int maxRoleGroups = 20;


        public double blabSpawnChance = 0.001;


        public double maxFlatlineFalloffSpeed = 5.9;
        public double minFlatlineFalloffSpeed = 3.7;
        public double maxArrowheadSlotCoverage = 0.4;
        public double minArrowheadSlotCoverage = 0.15;
    }
    
    public enum RoleAppearanceMode
    {
        AllEqual,
        AffectedByScrolls,
        AffectedByOddsMode
    }
    
    public enum OddsMode
    {
        Dumb,
        DumbAlt,
        Smart,
        Gigabrain
    }
}