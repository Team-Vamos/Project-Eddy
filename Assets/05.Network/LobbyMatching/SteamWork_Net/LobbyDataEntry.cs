
using UnityEngine;
using Steamworks;

public class LobbyDataEntry : MonoBehaviour
{
    public CSteamID lobbyID;
    public string lobbyName;

    public void JoinLobby(){
        SteamLobby.Instance.JoinLobby(lobbyID);
    }

}
