using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UIElements;
using Steamworks;
using System.Linq;

public class LobbyController : MonoSingleton<LobbyController>
{
    [SerializeField]
    private VisualTreeAsset playerInfoAsset;
    private LobbyRoomButton lobbyRoomButton;
    //public Text LobbyNameText;

    //public GameObject PlayerListViewContent;
    //public GameObject PlayerListItemPrefab;
    public GameObject LocalPlayerObject;
    public ulong CurrentLobbyID;
    public bool PlayerItemCreated = false;
    private List<PlayerListItem> PlayerListItems = new List<PlayerListItem>();
    public PlayerObjectControler LocalPlayerController;
    //public Button StartGameButton;
    //public Button addressButton;
    //public Text ReadyButtonText;
    private CustomNetworkManager manager;

    private CustomNetworkManager Manager
    {
        get
        {
            if (manager != null)
            {
                return manager;
            }

            return manager = CustomNetworkManager.singleton as CustomNetworkManager;
        }
    }

    [Header("디버그용")] [SerializeField] private bool fastGameStart;

    private void Awake()
    {
        lobbyRoomButton = FindObjectOfType<LobbyRoomButton>();
        if (fastGameStart)
        {
            Invoke("ReadyPlayer", 1f);
            Invoke("StartGame", 1.2f);
        }
    }

    public void UpdateAddressButton()
    {
        if (LocalPlayerController == null) return;
        lobbyRoomButton.copyButton.style.display = (LocalPlayerController.PlayerIdNumber == 1)?DisplayStyle.Flex:DisplayStyle.None;
        //addressButton.interactable = (LocalPlayerController.PlayerIdNumber == 1);
    }

    public void ReadyPlayer()
    {
        LocalPlayerController.ChangeReady();
    }

    public void UpdateButton()
    {
        if (LocalPlayerController.Ready)
        {
            lobbyRoomButton.readyButton.text = "Unready";
            //ReadyButtonText.text = "Unready";
            
        }
        else
        {
            lobbyRoomButton.readyButton.text = "Ready";
            //ReadyButtonText.text = "Ready";
        }
    }

    public void CheckIfAllReady()
    {
        bool AllReady = false;
        foreach (PlayerObjectControler player in Manager.gamePlayers)
        {
            if (player.Ready)
            {
                AllReady = true;
            }
            else
            {
                AllReady = false;
                break;
            }
        }

        if (AllReady)
        {
            if (LocalPlayerController.PlayerIdNumber == 1)
            {
                lobbyRoomButton.startButton.style.display = DisplayStyle.Flex;
                //StartGameButton.interactable = true;
            }
            else
            {
                lobbyRoomButton.startButton.style.display = DisplayStyle.None;
                //StartGameButton.interactable = false;
            }
        }
        else
        {
            lobbyRoomButton.startButton.style.display = DisplayStyle.None;
            //StartGameButton.interactable = false;
        }
    }

    public void UpdateLobbyName()
    {
        CurrentLobbyID = Manager.GetComponent<SteamLobby>().CurrentLobbyID;
        // SteamAuth.LobbyId = CurrentLobbyID;
        string name = SteamMatchmaking.GetLobbyData(new CSteamID(CurrentLobbyID), "name");
        string[] spstring = name.Split('$');
        //LobbyNameText.text = spstring[0];
    }

    public void UpdatePlayerList()
    {
        if (!PlayerItemCreated)
        {
            CreatHostPlayerItem();
        }

        if (PlayerListItems.Count < Manager.gamePlayers.Count)
        {
            CreatClientPlayerItem();
        }

        if (PlayerListItems.Count > Manager.gamePlayers.Count)
        {
            RemovePlayerItem();
        }

        if (PlayerListItems.Count == Manager.gamePlayers.Count)
        {
            UpdatePlayerItem();
        }
    }

    public void FindLovalPlayer()
    {
        LocalPlayerObject = GameObject.Find("LocalGamePlayer");
        LocalPlayerController = LocalPlayerObject.GetComponent<PlayerObjectControler>();
        UpdateAddressButton();
    }

    public void CreatHostPlayerItem()
    {
        foreach (PlayerObjectControler player in Manager.gamePlayers)
        {
            GameObject NewPlayerItemObject = new GameObject("PlayerListItem");
            PlayerListItem NewPlayerItem = NewPlayerItemObject.AddComponent<PlayerListItem>();
            AddPlayerInfo(NewPlayerItem);
            NewPlayerItem.PlayerName = player.PlayerName;
            NewPlayerItem.ConnectionID = player.ConnectionID;
            NewPlayerItem.PlayerSteamID = player.PlayerSteamID;
            NewPlayerItem.Ready = player.Ready;
            NewPlayerItem.SetPlayerValues();
            NewPlayerItem.transform.SetParent(transform);
            NewPlayerItem.transform.localScale = Vector3.one;
            PlayerListItems.Add(NewPlayerItem);
        }

         PlayerItemCreated = true;
    }

    public void AddPlayerInfo(PlayerListItem NewPlayerItem) {
        VisualElement playerInfo = playerInfoAsset.Instantiate();
        NewPlayerItem.PlayerNameText = playerInfo.Q<Label>("UserName");
        NewPlayerItem.PlayerReadyText = playerInfo.Q<Label>("UserReady");
        NewPlayerItem.PlayerIcon = playerInfo.Q<VisualElement>("UserImage");
        
        lobbyRoomButton.list.Add(playerInfo);

    }

    public void CreatClientPlayerItem()
    {
        foreach (PlayerObjectControler player in Manager.gamePlayers)
        {
            if (!PlayerListItems.Any(b => b.ConnectionID == player.ConnectionID))
            {
                GameObject NewPlayerItemObject = new GameObject("PlayerListItem");
                PlayerListItem NewPlayerItem = NewPlayerItemObject.AddComponent<PlayerListItem>();
                AddPlayerInfo(NewPlayerItem);
                NewPlayerItem.PlayerName = player.PlayerName;
                NewPlayerItem.ConnectionID = player.ConnectionID;
                NewPlayerItem.PlayerSteamID = player.PlayerSteamID;
                NewPlayerItem.Ready = player.Ready;
                NewPlayerItem.SetPlayerValues();

                NewPlayerItem.transform.SetParent(transform);
                NewPlayerItem.transform.localScale = Vector3.one;

                PlayerListItems.Add(NewPlayerItem);
            }
        }
    }

    public void UpdatePlayerItem()
    {
        foreach (PlayerObjectControler player in Manager.gamePlayers)
        {
            foreach (PlayerListItem PlayerListItemScript in PlayerListItems)
            {
                if (PlayerListItemScript.ConnectionID == player.ConnectionID)
                {
                    PlayerListItemScript.PlayerName = player.PlayerName;
                    PlayerListItemScript.Ready = player.Ready;
                    PlayerListItemScript.SetPlayerValues();
                    if (player == LocalPlayerController)
                    {
                        UpdateButton();
                    }
                }
            }
        }

        CheckIfAllReady();
    }

    public void RemovePlayerItem()
    {
        List<PlayerListItem> playerListItemToRemove = new List<PlayerListItem>();

        foreach (PlayerListItem playerListItem in PlayerListItems)
        {
            if (!Manager.gamePlayers.Any(b => b.ConnectionID == playerListItem.ConnectionID))
            {
                playerListItemToRemove.Add(playerListItem);
            }
        }

        if (playerListItemToRemove.Count > 0)
        {
            foreach (PlayerListItem playerlistItemToRemove in playerListItemToRemove)
            {
                GameObject ObjectToRemove = playerlistItemToRemove.gameObject;
                PlayerListItems.Remove(playerlistItemToRemove);
                Destroy(ObjectToRemove);
                ObjectToRemove = null;
            }
        }
    }

    public void StartGame()
    {
        LocalPlayerController.CanStartGame(manager.nextSceneName);
    }
}