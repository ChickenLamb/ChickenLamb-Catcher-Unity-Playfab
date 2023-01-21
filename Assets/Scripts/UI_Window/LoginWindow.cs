using NetProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using PEUtils;
using PlayFab;
using PlayFab.ClientModels;
using System.Net.Mail;
using UnityEditor.PackageManager;

public class LoginWindow : WindowRoot
{

    public TMP_Text inputEmail;
    public TMP_Text inputUsername;
    public TMP_Text inputPassword;
    public Toggle togSrv;

    protected override void InitializeWindow()
    {
        base.InitializeWindow();
        System.Random rd = new System.Random();
        inputEmail.text = rd.Next(100, 999).ToString();
        inputPassword.text = rd.Next(100, 999).ToString();
    }
    public void CLickRegisterBtn()
    {
        if(inputPassword.text.Length <= 4)
        {
            PELog.ColorLog(LogColor.Green, "Password Length too short");
            root.ShowAlert("Password Length too short");
        }
        else {
            PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest()
            {
                TitleId = PlayFabSettings.TitleId,
                Email = inputEmail.text,
                //Username = inputUsername.text,
                Password = inputPassword.text,
                RequireBothUsernameAndEmail = false

            }, reponse =>
            {
                PELog.ColorLog(LogColor.Green, $"Successful Account Creation : {inputUsername.text}, {inputEmail.text}");

                //SignIn(inputUsername.text, inputPassword.text);
            }, error =>
            {
                PELog.ColorLog(LogColor.Green, $"Unsuccessful Account Creation : {inputUsername.text}, {inputEmail.text}, {error.ErrorMessage}");
                root.ShowAlert($"Unsuccessful Account Creation : {inputUsername.text}, {inputEmail.text}, {error.ErrorMessage}");
                //OnCreateAccountFailed.Invoke(error.ErrorMessage);
            });
        }  
       
    }
    public void ClickLoginBtn()
    {
        PELog.ColorLog(LogColor.Green, inputEmail.text);
        PELog.ColorLog(LogColor.Green, inputPassword.text);
        //audioService.PlayUIAudio(AudioName.LoginBtnClick);
        PlayFabClientAPI.LoginWithEmailAddress(new LoginWithEmailAddressRequest()
        {

            Password = inputPassword.text,
            Email = inputEmail.text,


        }, response => { OnPlayFabLoginSuccess(response,  inputEmail.text, inputPassword.text); }, error => { OnLoginError(error, inputEmail.text); });
    }
    private void OnPlayFabLoginSuccess(LoginResult loginResult, string email = "", string password = "")
    {


        PELog.ColorLog(LogColor.Green, $"Successful Account Login : {email}");
        //OnLoginSuccess.Invoke();
        //SceneController.LoadNextScene();
        //PlayerPrefsUtility.setInt("LoginType", (int)loginType);
        //PlayerPrefsUtility.setString("userId", email);
        //PlayerPrefsUtility.setString("password", password);
        PELog.ColorLog(LogColor.Green, "Playfab ID : " + loginResult.PlayFabId);
        //entityKey = loginResult.EntityToken.Entity;
        //sessionTicket = loginResult.SessionTicket;
        //playfabID = loginResult.PlayFabId;
        PELog.ColorLog(LogColor.Green, "ClientLogin" + loginResult.AuthenticationContext.IsClientLoggedIn());

        //Debug.Log("Player information saved!");
    }
private void OnLoginError(PlayFabError error, string email = "")
    {
        PELog.ColorLog(LogColor.Green, $"Failure to login : {email} , {error.ErrorMessage}");
        root.ShowAlert($"Failure to login : {email} , {error.ErrorMessage}");

    }

}
