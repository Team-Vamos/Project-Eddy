using System.Collections;
using System.Collections.Generic;
using EventManagers;
using UnityEngine;

public class StoreNPC : BaseNPC
{
    [SerializeField]
    private StoreUIDocument _storeUIDocument;
    private bool _isCollide = false;

    private const string shopEndCallback = "ShopEndCallback";
    public override void Hit()
    {
        if(_isCollide)return;
        _isCollide = true;
        StopNPC();
        _storeUIDocument.ShowStore();
        // TODO: 상점 카드 보여주기
        Debug.Log("상점 카드 보여줌");
    }

    private void OnEnable() {
        EventManager.StartListening(shopEndCallback, RegisterEndCallback);
    }

    private void RegisterEndCallback()
    {
        EventManager.StopListening(shopEndCallback, RegisterEndCallback);

        EndActionNPC();
    }
}