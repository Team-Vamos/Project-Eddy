using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LobbyButton : MonoBehaviour
{
    [SerializeField]
    private SteamLobby steamLobby;
    [SerializeField]
    private LobbiesListManager lobbiesListManager;


    private VisualElement list;
    public static ScrollView lobbyScrollView;

    private void OnEnable() {
        steamLobby = FindObjectOfType<SteamLobby>();
        lobbiesListManager = FindObjectOfType<LobbiesListManager>();




        UIDocument ui = GetComponent<UIDocument>();

        VisualElement root = ui.rootVisualElement;

        VisualElement back = root.Q<VisualElement>("Back");
        GroupBox groupBox = back.Q<GroupBox>("GroupBox");
        Button host = back.Q<Button>("HostBtn");
        Button client = back.Q<Button>("JoinBtn");
        host.RegisterCallback<ClickEvent>(OnClickHost);
        client.RegisterCallback<ClickEvent>(OnClickClient);



        list = root.Q<VisualElement>("List");
        Button menu = list.Query<Button>("Button").AtIndex(0);
        Button refresh = list.Query<Button>("Button").AtIndex(1);
        Button join = list.Query<Button>("Button").AtIndex(2);
        refresh.RegisterCallback<ClickEvent>(OnClickRefresh);
        menu.RegisterCallback<ClickEvent>(OnClickMenu);
        join.RegisterCallback<ClickEvent>(OnClickJoin);


        lobbyScrollView = list.Q<ScrollView>("Servers");
        
    }
    private void OnClickHost(ClickEvent evt) {
        steamLobby.HostLobby();
    }
    private void OnClickClient(ClickEvent evt) {
        list.style.display = DisplayStyle.Flex;
        lobbiesListManager.GetListOfLobbys();
    }
    private void OnClickRefresh(ClickEvent evt) {
        lobbiesListManager.ReFreshLobbies();
    }
    private void OnClickMenu(ClickEvent evt) {
        list.style.display = DisplayStyle.None;
    }
    private void OnClickJoin(ClickEvent evt) {

    }
}
