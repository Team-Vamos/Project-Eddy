using EventManagers;
using Mirror;
using UnityEngine;

public class NetManager : NetworkSingleton<NetManager>
{
    public const string StartGameCallback = "StartGameCallback";
    public const string NetworkConnected = "NetworkConnected";

    public bool gameStarted;
    public bool isServerStarted;

    private void Awake()
    {
        EventManager.StopListening(StartGameCallback, StartGame);
        EventManager.StartListening(StartGameCallback, StartGame);
        (NetworkManager.singleton as CustomNetworkManager)?.GetLocalPlayer();
    }

    private void OnDestroy()
    {
        EventManager.StopListening(StartGameCallback, StartGame);
    }

    public void Go()
    {
        Debug.Log("NetManager Go");
    }

    public override void OnStartServer()
    {
        isServerStarted = true;
        EventManager.TriggerEvent(NetworkConnected);
    }


    public override void OnStartClient()
    {
        isServerStarted = true;
        EventManager.TriggerEvent(NetworkConnected);
    }

    private void StartGame()
    {
        gameStarted = true;
    }
}