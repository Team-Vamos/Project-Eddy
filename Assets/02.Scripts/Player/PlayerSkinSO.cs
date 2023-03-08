using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerSkin", menuName = "PlayerSkin", order = 0)]
[Serializable]
public class PlayerSkinSO : ScriptableObject
{
    public Sprite playerColorSprite;
    public Sprite playerSkinSprite;
}
