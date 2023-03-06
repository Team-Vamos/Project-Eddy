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
        StartCoroutine(WaitForNetwork());
    }
    private void SetNetwork()
    {
        isHost = isServer;
    }
    private IEnumerator WaitForNetwork()
    {
        while(!NetworkServer.active || !NetworkClient.isConnected)
            yield return null;
        StartNetwork();
    }
    private void StartNetwork()
    {
        Debug.Log("NetManager StartNetwork");
        networkStarted = true;
        EventManager.TriggerEvent(StartNetworkCallback);
    }


    
}
