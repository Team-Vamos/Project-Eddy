using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class OreGaugeBar : MonoBehaviour
{
    [SerializeField]
    private OreSpawner _spawner;

    private UIDocument _document;
    private void Awake() {
        _document = GetComponent<UIDocument>();
    }
    private void OnEnable() {
        // _spawner.SetRootVisualElement(_document.rootVisualElement.Q<VisualElement>("Canvas"));
    }
}