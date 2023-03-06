using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using EventManagers;

public class NetManager : NetworkSingleton<NetManager>
{
    public static string StartNetworkCallback = "StartNetworkCallback";
    public bool networkStarted = false;
    public void Go()
    {
        Debug.Log("NetManager Go");
    }
    private void Awake() {
        EventManager.StartListening(StartNetworkCallback, SetNetwork);
    }
    private void OnDestroy() {
        EventManager.StopListening(StartNetworkCallback, SetNetwork);
    }
    private void Start() {
        StartCoroutine(WaitForNetwork());
    }
    private void SetNetwork()
    {
        
    }
    private IEnumerator WaitForNetwork()
    {
        while(!NetworkServer.active){
            yield return new WaitForEndOfFrame();
        }
        StartNetwork();
    }
    private void StartNetwork()
    {
        networkStarted = true;
        EventManager.TriggerEvent(StartNetworkCallback);
    }


    
}
