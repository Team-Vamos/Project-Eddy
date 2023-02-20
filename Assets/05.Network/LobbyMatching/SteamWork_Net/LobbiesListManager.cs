using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.SceneManagement;

public class LobbiesListManager : MonoSingleton<LobbiesListManager>
{
    public GameObject lobbyDateItemPrefab;
    public GameObject lobbyListContent;

    public List<GameObject> listOfLobbys = new List<GameObject>();
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "MainMenu"&&scene.name != "Lobby"){
            Destroy(this.gameObject);
        }
    }
    public void GetListOfLobbys(){

        
        SteamLobby.Instance.GetLobbiesList();
    }
    private void Start() {
        lobbyListContent = gameObject;
    }
    public void DisaplayLobbies(List<CSteamID> lobbyIDs, LobbyDataUpdate_t result){
        for(int i=0;i<lobbyIDs.Count;i++){
            if(lobbyIDs[i].m_SteamID == result.m_ulSteamIDLobby){
                GameObject createdItem = Instantiate(lobbyDateItemPrefab);
                LobbyDataEntry lobbyDataEntry = createdItem.GetComponent<LobbyDataEntry>();
                lobbyDataEntry.lobbyID = (CSteamID)lobbyIDs[i].m_SteamID;
                string name = SteamMatchmaking.GetLobbyData((CSteamID)lobbyIDs[i].m_SteamID, "name");
                string[] spstring = name.Split('$');
                lobbyDataEntry.lobbyName 
                    = spstring[0];

                lobbyDataEntry.SetLobbyData();

                createdItem.transform.SetParent(lobbyListContent.transform);
                createdItem.transform.localScale = Vector3.one;

                listOfLobbys.Add(createdItem);
            }
        }
    }
    public void DestroyLobbies(){
        LobbyButton.lobbyScrollView.Clear();
        foreach(GameObject lobbyItem in listOfLobbys){
            Destroy(lobbyItem);
        }
        listOfLobbys.Clear();
    }
    public void ReFreshLobbies(){
        DestroyLobbies();
        GetListOfLobbys();
    }
}
