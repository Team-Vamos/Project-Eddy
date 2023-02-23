using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Card;

public partial class GetAllSOEditor
{
    [MenuItem("Editor/GetAllCardGradeSO")]
    private static void GetAllCardGradeSO()
    {
        List<CardGradeSO> list = GetAllSO<CardGradeSO>();
        CardManager cardManager = GameObject.FindObjectOfType<CardManager>();
        if(cardManager == null)
        {
            Debug.LogError("CardManager is Null Check your hierarchy");
            return;
        }
        cardManager.SetCardGradeSOList(list);
        EditorUtility.SetDirty(cardManager);
    }
}