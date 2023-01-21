using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEUtils;
using NetProtocol;
using System.ComponentModel.Design;

public class GameRoot : MonoBehaviour
{
    public static GameRoot instance;
    public Transform UIRoot;
    public AlertWindow alertWindow;
    private void Start()
    {
        instance = this;
        LogConfig logConfig = new LogConfig
        {
            enableLog = true,
            logPrefix = "",
            enableTime = true,
            logSeparate = ">",
            enableThreadID = true,
            enableTrace = true,
            enableSave = true,
            enableCover = true,
            saveName = "HOKClientLog.txt",
            loggerType = LoggerType.Unity
        };
        PELog.InitSettings(logConfig);
        PELog.ColorLog(LogColor.Green, "Initialize logger completed.");
        DontDestroyOnLoad(this);
        InitializeRoot();
        PELog.Log("Intialize Root");
        InitializeServices();
        PELog.Log("Intialize Completed");
        audioService.PlayUIAudio("Login_00");

    }
    private void InitializeRoot()
    {
        for (int i = 0; i < UIRoot.childCount; i++)
        {
            Transform trans = UIRoot.GetChild(i);
            trans.gameObject.SetActive(false);
        }
        //alertWindow.SetWindowState();
        //alertWindow.ShowAlert("Initialize Success from alertWindow");
    }
    private void Update()
    {
      
    }
    private NetService netService;
    private AudioService audioService;
    private ResourceService resourceService;
    private void InitializeServices()
    {
        netService = GetComponent<NetService>();
         netService.InitializeService();
        audioService = GetComponent<AudioService>();
        audioService.InitializeService();
        resourceService = GetComponent<ResourceService>();
        resourceService.InitializeService();
        LoginSystem loginSystem = GetComponent<LoginSystem>();
        loginSystem.InitializeSystem();
        loginSystem.EnterLogin();
        PELog.Log("Enter Login User Interface");
    }
    public void ShowAlert(string msg)
    {
        alertWindow.ShowAlert(msg);
    }
}