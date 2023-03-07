using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using EventManagers;

public class NetManager : NetworkSingleton<NetManager>
{
    public static string StartGameCallback = "StartGameCallback";
    public bool gameStarted = false;
    public bool isServerStarted = false;
    public void Go()
    {
        Debug.Log("NetManager Go");
    }
    public override void OnStartServer()
    {
        isServerStarted = true;
    }
    public override void OnStartClient()
    {
        isServerStarted = true;
    }

    private void StartGame()
    {
        gameStarted = true;
        EventManager.TriggerEvent(StartGameCallback);
    }


    
}