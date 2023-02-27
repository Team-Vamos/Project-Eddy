using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class PlayerObjectControler : NetworkBehaviour
{
    public bool isHost { get { return isServer; } }
    public bool isLocal { get { return isLocalPlayer; } }
    //Player data
    [SyncVar] public int ConnectionID;
    [SyncVar] public int PlayerIdNumber;
    [SyncVar] public ulong PlayerSteamID;
    [SyncVar(hook = nameof(PlayerNameUpdate))] public string PlayerName;
    [SyncVar(hook = nameof(PlayerReadyUpdate))] public bool Ready;


    private CustomNetworkManager manager;
    private CustomNetworkManager Manager{
        get{
            if(manager != null){
                return manager;
            }return manager = CustomNetworkManager.singleton as CustomNetworkManager;
        }
    }

    private void Start(){
        DontDestroyOnLoad(this.gameObject);
        NetManager.Instance.Go();
    }
    private void PlayerReadyUpdate(bool oldValue, bool newValue){
        if(isServer){
            this.Ready = newValue;
        }if(isClient){
            LobbyController.Instance.UpdatePlayerList();
        }
    }
    [Command]
    private void CmdSetPlayerReady(){
        this.PlayerReadyUpdate(this.Ready, !this.Ready);
    }
    public void ChangeReady(){
        if(isOwned){
            CmdSetPlayerReady();
        }
    }
    public override void OnStartAuthority()
    {
        CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());
        gameObject.name = "LocalGamePlayer";
        LobbyController.Instance.FindLovalPlayer();
        LobbyController.Instance.UpdateLobbyName();
    }
    public override void OnStartClient()
    {
        Manager.gamePlayers.Add(this);
        LobbyController.Instance.UpdateLobbyName();
        LobbyController.Instance.UpdatePlayerList();
    }
    public override void OnStopClient()
    {
        Manager.gamePlayers.Remove(this);
        LobbyController.Instance.UpdatePlayerList();
    }
    [Command]
    private void CmdSetPlayerName(string PlayerName){
        this.PlayerNameUpdate(this.PlayerName, PlayerName);

    }
    public void PlayerNameUpdate(string Oldvalue, string NewValue){
        if(isServer){
            this.PlayerName = NewValue;
        }
        if(isClient){
            LobbyController.Instance.UpdatePlayerList();
        }
    }


    //게임시작
    public void CanStartGame(string sceneName){
        if(isOwned){
            CmdCanStartGame(sceneName);
        }
    }
    [Command]
    public void CmdCanStartGame(string sceneName){
        manager.StartGame(sceneName);
    }
}
