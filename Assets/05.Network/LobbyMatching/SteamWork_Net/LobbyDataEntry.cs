using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.UIElements;

public class LobbyDataEntry : MonoBehaviour
{
    public CSteamID lobbyID;
    public string lobbyName;
    public void SetLobbyData(){
        
        if(lobbyName == ""){
            lobbyName = "Empty";
        }
        SetLobbyLable();
    }
    public void JoinLobby(){
        SteamLobby.Instance.JoinLobby(lobbyID);
    }
    public void SetLobbyLable()
    {
        var button = new Button(JoinLobby);
        LobbyButton.lobbyScrollView.Add(button);
    }
}
