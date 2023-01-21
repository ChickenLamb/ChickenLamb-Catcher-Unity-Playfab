using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProtocol;
using PENet;

public class ClientSession : KCPSession<NetMessage>
{
    protected override void OnConnected()
    {
        GameRoot.instance.ShowAlert("Connected Success");
    }

    protected override void OnDisConnected()
    {
        GameRoot.instance.ShowAlert("Disconnected Success");
    }

    protected override void OnReciveMsg(NetMessage msg)
    {
        NetService.instance.AddMessageQueue(msg);
    }

    protected override void OnUpdate(DateTime now)
    {

    }
}
