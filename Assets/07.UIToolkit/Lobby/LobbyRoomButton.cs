using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LobbyRoomButton : MonoBehaviour
{
    private LobbyController lobbyController;
    private InputAddress inputAddress;
    public VisualElement list;
    public Button readyButton;
    public Button startButton;
    public Button copyButton;
    private void Awake() {
        lobbyController = FindObjectOfType<LobbyController>();
        inputAddress = FindObjectOfType<InputAddress>();
    }
    private void OnEnable() {
        UIDocument ui = GetComponent<UIDocument>();

        VisualElement root = ui.rootVisualElement;

        list = root.Q<ScrollView>("Servers");
        Debug.Log(list);
        readyButton = root.Query<Button>("Button").AtIndex(0);
        startButton = root.Query<Button>("Button").AtIndex(1);
        copyButton = root.Query<Button>("Button").AtIndex(2);
        readyButton.RegisterCallback<ClickEvent>(OnClickReady);
        startButton.RegisterCallback<ClickEvent>(OnClickStart);
        copyButton.RegisterCallback<ClickEvent>(OnClickCopy);
    }
    
    private void OnClickReady(ClickEvent evt) {
        lobbyController.ReadyPlayer();
    }
    private void OnClickStart(ClickEvent evt) {
        lobbyController.StartGame();
    }
    private void OnClickCopy(ClickEvent evt) {
        inputAddress.CopyAddress();
    }
    
}
