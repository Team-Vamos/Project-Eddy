using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkinList", menuName = "SO/PlayerSkinList", order = 1)]
public class PlayerSkinListSO : ScriptableObject
{
    public List<PlayerSkinSO> playerSkinList = new List<PlayerSkinSO>();
}
