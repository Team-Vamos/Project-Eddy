using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager
{
    public static CustomNetworkManager Instance{get{return CustomNetworkManager.singleton as CustomNetworkManager;}}
    [SerializeField]
    private PlayerObjectControler _gamePlayerPrefab;
    
    public List<PlayerObjectControler> gamePlayers;
    public string nextSceneName;
    public override void OnStopServer()
    {
        foreach (var player in gamePlayers)
        {
            player.connectionToClient.Disconnect();
        }
    }
    public PlayerObjectControler GetLocalPlayer()
    {
        foreach (var player in gamePlayers)
        {
            if (player.isOwned)
            {
                return player;
            }
        }
        return null;
    }
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        if(SceneManager.GetActiveScene().name == "Lobby")
        {
            PlayerObjectControler GamePlayerInstance = Instantiate(_gamePlayerPrefab);
            GamePlayerInstance.ConnectionID = conn.connectionId;
            GamePlayerInstance.PlayerIdNumber = gamePlayers.Count + 1;
            GamePlayerInstance.PlayerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)SteamLobby.Instance.CurrentLobbyID, gamePlayers.Count);
            NetworkServer.AddPlayerForConnection(conn, GamePlayerInstance.gameObject);
            
            Debug.Log("사람 추가함");
            //AnimationSync animationSync= GamePlayerInstance.GetComponent<AnimationSync>();
            //animationSync.SetPlayerByID(0, GamePlayers.Count);
            //animationSync.PlayerID = GamePlayers.Count;


            //FindObjectOfType<LobbyController>().UpdateAddressButton();
        }
    }
    

    public void StartGame(string SceneName){
        ServerChangeScene(SceneName);
    }
}