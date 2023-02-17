using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using System.Linq;

public class LobbyController : MonoSingleton<LobbyController>
{
    public Text lobbyNameText;

    
    public GameObject PlayerListViewContent=>playerListViewContent;
    [SerializeField]
    private GameObject playerListViewContent;
    
    public GameObject PlayerListItemPrefab=>playerListItemPrefab;
    [SerializeField]
    private GameObject playerListItemPrefab;
    
    public GameObject LocalPlayerObject=>localPlayerObject;
    [SerializeField]
    private GameObject localPlayerObject;
    

    public ulong CurrentLobbyID;
    public bool PlayerItemCreated = false;
    private List<PlayerListItem> PlayerListItems = new List<PlayerListItem>();
    public PlayerObjectControler LocalPlayerController;
    public Button StartGameButton;
    public Button addressButton;
    public Text ReadyButtonText;
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

    private void Start()
    {
        if (fastGameStart)
        {
            Invoke("ReadyPlayer", 1f);
            Invoke("StartGame", 1.2f);
        }
    }

    public void UpdateAddressButton()
    {
        if (LocalPlayerController == null) return;
        addressButton.interactable = (LocalPlayerController.PlayerIdNumber == 1);
    }

    public void ReadyPlayer()
    {
        LocalPlayerController.ChangeReady();
    }

    public void UpdateButton()
    {
        if (LocalPlayerController.Ready)
        {
            ReadyButtonText.text = "Unready";
        }
        else
        {
            ReadyButtonText.text = "Ready";
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
                StartGameButton.interactable = true;
            }
            else
            {
                StartGameButton.interactable = false;
            }
        }
        else
        {
            StartGameButton.interactable = false;
        }
    }

    public void UpdateLobbyName()
    {
        CurrentLobbyID = Manager.GetComponent<SteamLobby>().CurrentLobbyID;
        // SteamAuth.LobbyId = CurrentLobbyID;
        string name = SteamMatchmaking.GetLobbyData(new CSteamID(CurrentLobbyID), "name");
        string[] spstring = name.Split('$');
        lobbyNameText.text = spstring[1];
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
        localPlayerObject = GameObject.Find("LocalGamePlayer");
        LocalPlayerController = localPlayerObject.GetComponent<PlayerObjectControler>();
        UpdateAddressButton();
    }

    public void CreatHostPlayerItem()
    {
        foreach (PlayerObjectControler player in Manager.gamePlayers)
        {
            GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
            PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();

            NewPlayerItemScript.PlayerName = player.PlayerName;
            NewPlayerItemScript.ConnectionID = player.ConnectionID;
            NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
            NewPlayerItemScript.Ready = player.Ready;
            NewPlayerItemScript.SetPlayerValues();

            NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
            NewPlayerItem.transform.localScale = Vector3.one;

            PlayerListItems.Add(NewPlayerItemScript);
        }

        PlayerItemCreated = true;
    }

    public void CreatClientPlayerItem()
    {
        foreach (PlayerObjectControler player in Manager.gamePlayers)
        {
            if (!PlayerListItems.Any(b => b.ConnectionID == player.ConnectionID))
            {
                GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
                PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();

                NewPlayerItemScript.PlayerName = player.PlayerName;
                NewPlayerItemScript.ConnectionID = player.ConnectionID;
                NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
                NewPlayerItemScript.Ready = player.Ready;
                NewPlayerItemScript.SetPlayerValues();

                NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
                NewPlayerItem.transform.localScale = Vector3.one;

                PlayerListItems.Add(NewPlayerItemScript);
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