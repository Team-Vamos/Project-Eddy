using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card;
using System;

public class StoreManager : MonoBehaviour
{
    [field: SerializeField]
    public int CardCount { get; set; } = 6;

    public event Action<int> CashUpdateAction;
    public int Cash{
        get => _cash;
        set
        {
            _cash = value;
            CashUpdateAction.SafeInvoke<int>(_cash);
        }
    }
    private int _cash;

    [SerializeField]
    private StoreUIDocument _uiDocument;
    [SerializeField]
    private CardManager _cardManager;

    [field:SerializeField]
    public BabyBottleSpawner BottleSpawner{ get; set; }

    private void Start() {
        CashUpdateAction?.SafeInvoke<int>(_cash);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            _uiDocument.ShowStore();
        }
    }

    public int GetPrice(RankType type)
    {
        CardGradeSO so = _cardManager.GetGrade(type);
        return so.price;
    }
}