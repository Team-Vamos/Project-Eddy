using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LobbyRoomButton : MonoBehaviour
{
    private VisualElement list;
 

    public Button readyButton;
    public Button startButton;
    public Button copyButton;
    private void Awake() {
        UIDocument ui = GetComponent<UIDocument>();

        VisualElement root = ui.rootVisualElement;

        list = root.Q<ListView>("Servers");
        Button ready = root.Query<Button>("Button").AtIndex(0);
        Button start = root.Query<Button>("Button").AtIndex(1);
        Button copy = root.Query<Button>("Button").AtIndex(2);
        ready.RegisterCallback<ClickEvent>(OnClickReady);
        start.RegisterCallback<ClickEvent>(OnClickStart);
        copy.RegisterCallback<ClickEvent>(OnClickCopy);
    }
    
    private void OnClickReady(ClickEvent evt) {
        
    }
    private void OnClickStart(ClickEvent evt) {

    }
    private void OnClickCopy(ClickEvent evt) {
        
    }
    
}
