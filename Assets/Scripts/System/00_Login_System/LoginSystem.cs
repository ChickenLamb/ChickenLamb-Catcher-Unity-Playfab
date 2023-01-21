using NetProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSystem : SystemRoot
{
    public static LoginSystem instance;
    public LoginWindow loginWindow;
    //public StartWindow startWindow;

    public override void InitializeSystem()
    {
        base.InitializeSystem();
        instance = this;
        this.Log("Initialize Login System Completed");
    }

    public void EnterLogin()
    {
        loginWindow.SetWindowState();
        audioService.PlayBackgroundMusic(AudioName.MainBGMusic);
    }

    public void ResponseLogin(NetMessage msg)
    {
        root.ShowAlert("Login Success");
        //root.userData = msg.responseLogin.userData;

        //startWindow.SetWindowState();
        loginWindow.SetWindowState(false);
    }
    public void EnterLobby()
    {
        //startWindow.SetWindowState(false);
        //LobbySystem.instance.EnterLobby();
    }
}
