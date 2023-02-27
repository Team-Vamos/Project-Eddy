using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using EventManagers;

public class NetManager : NetworkSingleton<NetManager>
{
    public static string StartNetworkCallback = "StartNetworkCallback";

    private bool isHost = false;
    public void Go()
    {
        Debug.Log("NetManager Go");
    }
    private void Awake() {
        EventManager.StartListening(StartNetworkCallback, SetNetwork);
    }
    private void SetNetwork()
    {
        isHost = isServer;
    }
    public override void OnStartServer()
    {
        EventManager.TriggerEvent(StartNetworkCallback);
    }
    public override void OnStartClient()
    {
        EventManager.TriggerEvent(StartNetworkCallback);
    }


    
}
