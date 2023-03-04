using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card;

public class StoreManager : MonoBehaviour
{
    [field: SerializeField]
    public int CardCount { get; set; } = 6;

    public int Cash{ get; set; } = 100000;

    [SerializeField]
    private StoreUIDocument _uiDocument;
    [SerializeField]
    private CardManager _cardManager;

    [field:SerializeField]
    public BabyBottleSpawner BottleSpawner{ get; set; }

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