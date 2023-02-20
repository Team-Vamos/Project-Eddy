using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class ChestUIDocument : MonoBehaviour
{
    [SerializeField]
    private VisualTreeAsset _card;


    private UIDocument _document;

    private void Awake() {
        _document = GetComponent<UIDocument>();
    }

    private void OnEnable() {
        VisualElement root = _document.rootVisualElement;
        
    }
}
