using System.Collections;
using System.Collections.Generic;
using EventManagers;
using UnityEngine;

public class StoreNPC : BaseNPC
{
    [SerializeField]
    private StoreUIDocument _storeUIDocument;
    private bool _isCollide = false;

    public override void Hit()
    {
        if(_isCollide)return;
        _isCollide = true;
        StopNPC();
        _storeUIDocument.ShowStore();
    }

    // TODO: 나중에 풀링 해둘 것
    private void OnEnable() {
        EventManager.StartListening(StoreManager.StoreEndCallback, RegisterEndCallback);
    }

    private void RegisterEndCallback()
    {
        EventManager.StopListening(StoreManager.StoreEndCallback, RegisterEndCallback);

        EndActionNPC();
    }
}