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
using UnpredictableWaterWheel;

public class DummyMB : MonoBehaviour
{
    public void Start()
    {
        Plugin.Log.LogInfo($"Dummy MonoBehaviour reporting for duty!");
        DontDestroyOnLoad(gameObject);
    }

    bool _disabled = false;

    public void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.R) && Input.GetKeyDown(KeyCode.E) && !_disabled)
        {
            _disabled = true;
            GlobalServiceLocator.ApplicationService.ShowScene(Scene.Home);
            Destroy(gameObject);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.R) && Input.GetKeyDown(KeyCode.T) && !_disabled)
        {
            Screen.SetResolution(1920, 1080, FullScreenMode.Windowed, 144);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.R) && Input.GetKeyDown(KeyCode.P) && !_disabled)
        {
            Application.targetFrameRate = 60;
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.R) && Input.GetKeyDown(KeyCode.O) && !_disabled)
        {
            Application.targetFrameRate = 30;
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.R) && Input.GetKeyDown(KeyCode.I) && !_disabled)
        {
            Application.targetFrameRate = 144;
        }
    }
}