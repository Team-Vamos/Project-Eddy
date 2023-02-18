using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using UnityEngine.UIElements;

public class LobbyDataEntry : MonoBehaviour
{
    public CSteamID lobbyID;
    public string lobbyName;
    public Text lobbyNameText;
    public void SetLobbyData(){
        
        if(lobbyName == ""){
            lobbyNameText.text = "Empty";
        }else{
            lobbyNameText.text = lobbyName;
        }
    }
    public void JoinLobby(){
        SteamLobby.Instance.JoinLobby(lobbyID);
    }
    public void SetLobbyLable()
    {
        LobbyButton.lobbyListView.makeItem = () => new Label();
    }
}
