using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using EventManagers;

public class NetManager : NetworkSingleton<NetManager>
{
    public static string StartNetworkCallback = "StartNetworkCallback";

    private bool isHost = false;
    public bool networkStarted = false;
    public void Go()
    {
        Debug.Log("NetManager Go");
    }
    private void Awake() {
        EventManager.StartListening(StartNetworkCallback, SetNetwork);
    }
    private void Start() {
        if(networkStarted)
            StartNetwork();
    }
    private void SetNetwork()
    {
        isHost = isServer;
        networkStarted = true;
    }
    public override void OnStartServer()
    {
        StartNetwork();
    }
    public override void OnStartClient()
    {
        StartNetwork();
    }
    private void StartNetwork()
    {
        networkStarted = true;
        EventManager.StopListening(StartNetworkCallback, SetNetwork);
        EventManager.StartListening(StartNetworkCallback, SetNetwork);
        EventManager.TriggerEvent(StartNetworkCallback);
        SetNetwork();
    }


    
}
