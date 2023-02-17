using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LobbyButton : MonoBehaviour
{
    private void Awake() {
        UIDocument ui = GetComponent<UIDocument>();
        VisualElement root = ui.rootVisualElement;

        Button button = root.Q<Button>("host");
        button.RegisterCallback<ClickEvent>(OnClick);
    }
    private void OnClick(ClickEvent evt) {
        Debug.Log("Button Clicked");
    }
}
