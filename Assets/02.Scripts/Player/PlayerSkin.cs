using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    private PlayerObjectControler _playerObjectControler;
    [SerializeField]
    private PlayerSkinListSO playerSkinListSO;
    private PlayerSkinSO playerSkinSO;
    [SerializeField]
    private PlayerSkinSO defaultSkin;
    
    
    void Awake()
    {
        _playerObjectControler = GetComponent<PlayerObjectControler>();
        playerSkinSO = defaultSkin;
        SetPlayerSkin(playerSkinSO);
    }
    public PlayerSkinSO GetPlayerSkin(){
        return playerSkinSO;
    }
    private void SetPlayerSkin(PlayerSkinSO skin){
        playerSkinSO = skin;
        _playerObjectControler.playerColorSpriteRenderer.sprite = playerSkinSO.playerColorSprite;
        _playerObjectControler.playerSkinSpriteRenderer.sprite = playerSkinSO.playerSkinSprite;
    }
}
