using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Card;

public partial class GetAllSOEditor
{
    [MenuItem("Editor/GetAllCardSO")]
    private static void GetAllCardSO()
    {
        List<CardBaseSO> list = GetAllSO<CardBaseSO>();
        CardManager cardManager = GameObject.FindObjectOfType<CardManager>();
        if(cardManager == null)
        {
            Debug.LogError("CardManager is Null Check your hierarchy");
            return;
        }
        cardManager.AddSO(list);
        EditorUtility.SetDirty(cardManager);
    }
}