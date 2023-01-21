using NetProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PENet;
using PEUtils;
using System.Threading.Tasks;
using System;

public class NetService : MonoBehaviour
{
    public static NetService instance;
    public static readonly string packQue_lock = "pkgque_lock";
    private KCPNet<ClientSession, NetMessage> client = null;
    private Queue<NetMessage> messagePackQue = null;
    private Task<bool> checkTask = null;

    public void InitializeService()
    {
        instance = this;
        client = new KCPNet<ClientSession, NetMessage>();
        messagePackQue = new Queue<NetMessage>();

        messagePackQue.Clear();
        KCPTool.LogFunc = this.Log;
        KCPTool.WarnFunc = this.Warn;
        KCPTool.ErrorFunc = this.Error;
        KCPTool.ColorLogFunc = (color, msg) =>
        {
            this.ColorLog((LogColor)color, msg);
        };
        string ipAddress = ServerConfiguration.localDevInnerIP;
        int port = ServerConfiguration.udpPort;
        //LoginSystem login = LoginSystem.instance;

        //if (login != null)
        //{
        //    if (login.loginWindow.togSrv.isOn)
        //    {
            
        //    }
        //}
        client.StartAsClient(ipAddress, port);
        checkTask = client.ConnectServer(100);

        this.Log("Initialize Net Service Completed");
    }

    public void AddMessageQueue(NetMessage netMessage)
    {
        lock (packQue_lock)
        {
            messagePackQue.Enqueue(netMessage);
        }
    }

    private uint checkConnectCount = 0;
    public void Update()
    {
        if (checkTask != null && checkTask.IsCompleted)
        {
            if (checkTask.Result)
            {
                //GameRoot.instance.ShowAlert("Connected Success");
                this.ColorLog(LogColor.Green, "Connect To Server Success");
                checkTask = null;
            }
            else
            {
                ++checkConnectCount;
                if (checkConnectCount > 4)
                {
                    this.Error("Connect Failed {0} Time(s). Please Check Your Internet Connection.", checkConnectCount);
                    GameRoot.instance.ShowAlert("Failed To Connect. Please Check Your Internet Connection");

                }
                else
                {
                    this.Warn("Connect Failed {0} Time(s), Retry...", checkConnectCount);
                    checkTask = client.ConnectServer(100);
                }
            }
        }

        if (client != null && client.clientSession != null)
        {
            if (messagePackQue.Count > 0)
            {
                lock (packQue_lock)
                {
                    NetMessage netMessage = messagePackQue.Dequeue();
                    ProcessMessage(netMessage);
                }
            }
            return;
        }


    }

    public void SendMessage(NetMessage msg, Action<bool> callback = null)
    {
     
        if (client.clientSession != null && client.clientSession.IsConnected())
        {
            client.clientSession.SendMsg(msg);
            callback?.Invoke(true);
        }
        else
        {
            GameRoot.instance.ShowAlert("No Connection");
            this.Error("No Connection");
            callback?.Invoke(false);
        }
    }

    private void ProcessMessage(NetMessage msg)
    {
        if (msg.errorCode != ErrorCode.None)
        {
            switch (msg.errorCode)
            {
                case ErrorCode.AccountIsOnline:
                    GameRoot.instance.ShowAlert("Current Account Already Online");
                    break;
                default:
                    break;
            }
            return;
        }

        //switch (msg.cmd)
        //{
        
        //}

    }
}