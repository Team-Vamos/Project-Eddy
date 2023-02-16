using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager
{
    [SerializeField]
    private PlayerObjectControler GamePlayerPrefab;
    
    public List<PlayerObjectControler> GamePlayers;
    public string nextSceneName;
    public override void OnStopServer()
    {
        foreach (var player in GamePlayers)
        {
            player.connectionToClient.Disconnect();
        }
    }
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        if(SceneManager.GetActiveScene().name == "Lobby" || SceneManager.GetActiveScene().name == "Main")
        {
            PlayerObjectControler GamePlayerInstance = Instantiate(GamePlayerPrefab);
            GamePlayerInstance.ConnectionID = conn.connectionId;
            GamePlayerInstance.PlayerIdNumber = GamePlayers.Count + 1;
            GamePlayerInstance.PlayerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)SteamLobby.Instance.CurrentLobbyID, GamePlayers.Count);
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
