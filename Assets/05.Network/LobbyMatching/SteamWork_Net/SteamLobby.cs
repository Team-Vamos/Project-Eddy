using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using Mirror;

public class SteamLobby : MonoSingleton<SteamLobby>
{
    //Callbacks
    protected Callback<LobbyCreated_t> LobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> JoinRequest;
    protected Callback<LobbyEnter_t> LobbyEnterd;


    protected Callback<LobbyMatchList_t> LobbyList;
    protected Callback<LobbyDataUpdate_t> LobbyDataUpdated;

    public List<CSteamID> lobbyIDs = new List<CSteamID>();

    //Variables
    public ulong CurrentLobbyID;

    private const string HostAddresskey = "Eddy";

    private CustomNetworkManager manager;

    private bool JoinWithAddress = false;
    private string joinAddress;
    private bool isLobby = false;

    public void Input_SurverAddress(string address)
    {
        LobbiesListManager.Instance.ReFreshLobbies();
        JoinWithAddress = true;
        joinAddress = address;
    }

    private void LetsGoAddress()
    {
        if (!JoinWithAddress) return;
        JoinWithAddress = false;
        Debug.Log(lobbyIDs.Count);
        foreach (var lobbyID in lobbyIDs)
        {
            string name = SteamMatchmaking.GetLobbyData((CSteamID)lobbyID.m_SteamID, "name");
            string[] spstring = name.Split('$');
            if (spstring[1] == joinAddress)
            {
                SteamLobby.Instance.JoinLobby(lobbyID);
            }
        }
    }

    public string GetAddress()
    {
        return StringConverter.ConvertToSimpleEncoding(SteamFriends.GetPersonaName().ToString());
    }

    private void Start()
    {
        if (!SteamManager.Initialized) return;
        manager = GetComponent<CustomNetworkManager>();
        LobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        JoinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
        LobbyEnterd = Callback<LobbyEnter_t>.Create(OnLobbyEntered);

        LobbyList = Callback<LobbyMatchList_t>.Create(OnGetLobbyList);
        LobbyDataUpdated = Callback<LobbyDataUpdate_t>.Create(OnGetLobbyData);
    }

    public void HostLobby()
    {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic,
            manager.maxConnections); //k_ELobbyTypePublic, k_ELobbyTypeFriendsOnly
    }

    public void OnLobbyCreated(LobbyCreated_t callback)
    {
        isLobby = true;
        if (callback.m_eResult != EResult.k_EResultOK) return;

        manager.StartHost();

        string inviteCode = StringConverter.ConvertToSimpleEncoding(SteamFriends.GetPersonaName().ToString());
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddresskey,
            SteamUser.GetSteamID().ToString());
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name",
            SteamFriends.GetPersonaName().ToString() + "$" + inviteCode);

        Debug.Log("로비가 성공적으로 만들어졌습니다.");
        Debug.Log(SteamFriends.GetPersonaName().ToString() + "$" + inviteCode);
    }

    private void OnJoinRequest(GameLobbyJoinRequested_t callback)
    {
        Debug.Log("Request To Join Lobby");
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }


    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        isLobby = true;
        CurrentLobbyID = callback.m_ulSteamIDLobby;
        if (NetworkServer.active) return;

        manager.networkAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddresskey);
        Debug.Log(manager.networkAddress);
        manager.StartClient();
    }

    public void JoinLobby(CSteamID lobbyID)
    {
        SteamMatchmaking.JoinLobby(lobbyID);
        LobbiesListManager.Instance.ReFreshLobbies();
        isLobby = true;
    }

    public void GetLobbiesList()
    {
        if (lobbyIDs.Count > 0)
        {
            lobbyIDs.Clear();
        }

        SteamMatchmaking.AddRequestLobbyListResultCountFilter(60);
        SteamMatchmaking.RequestLobbyList();
    }

    public void OnGetLobbyList(LobbyMatchList_t result)
    {
        if (LobbiesListManager.Instance.listOfLobbys.Count > 0)
        {
            LobbiesListManager.Instance.DestroyLobbies();
        }

        Debug.Log("현재 받아온 로비 수" + result.m_nLobbiesMatching);
        for (int i = 0; i < result.m_nLobbiesMatching; i++)
        {
            CSteamID lobbyID = SteamMatchmaking.GetLobbyByIndex(i);
            
            if (SteamMatchmaking.GetLobbyData((CSteamID)lobbyID.m_SteamID, "name") != "")
            {
                Debug.Log(SteamMatchmaking.GetLobbyData((CSteamID)lobbyID.m_SteamID, "name"));
                lobbyIDs.Add(lobbyID);
                SteamMatchmaking.RequestLobbyData(lobbyID);
            }
        }

        LetsGoAddress();
    }

    void OnGetLobbyData(LobbyDataUpdate_t result)
    {
        if(isLobby)return;
        LobbiesListManager.Instance.DisaplayLobbies(lobbyIDs, result);
    }
}