using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkinList", menuName = "SO/PlayerSkinList", order = 1)]
public class PlayerSkinListSO : ScriptableObject
{
    private void OnValidate()
    {
        foreach (var skin in playerSkinList)
        {
            skin.skinIndex = playerSkinList.IndexOf(skin);
        }
    }
    public List<PlayerSkinSO> playerSkinList = new List<PlayerSkinSO>();
}
