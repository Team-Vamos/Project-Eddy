using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
[DisallowMultipleComponent]
public class ResourceInfoUIDocument : MonoBehaviour
{
    [SerializeField]
    private StoreManager _storeManager;
    [SerializeField]
    private ChestManager _chestManager;

    private UIDocument _document;

    private ResourceInfo _cashInfo;
    private ResourceInfo _chestInfo;

    private void Awake() {
        _document = GetComponent<UIDocument>();
    }

    private void OnEnable() {
        VisualElement root = _document.rootVisualElement;
        
        _cashInfo = new ResourceInfo(root.Q<VisualElement>("Cash"));
        _chestInfo = new ResourceInfo(root.Q<VisualElement>("Chest"));

        _chestManager.ChestCountUpdateAction += _chestInfo.ChangeString;
        _storeManager.CashUpdateAction += _cashInfo.ChangeString;
    }

    private void OnDisable() {
        _chestManager.ChestCountUpdateAction -= _chestInfo.ChangeString;
        _storeManager.CashUpdateAction -= _cashInfo.ChangeString;
    }
}
