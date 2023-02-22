using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Steamworks;
public class PlayerListItem : MonoBehaviour
{
    public string PlayerName;
    public int ConnectionID;
    public ulong PlayerSteamID;
    private bool AvatarReceived;

    public Label PlayerNameText;
    public VisualElement PlayerIcon;
    public Label PlayerReadyText;
    public bool Ready;
    protected Callback<AvatarImageLoaded_t> ImageLoad;

    public void ChangeReadyStatus(){
        if(Ready){
            PlayerReadyText.text = "Ready";
            PlayerReadyText.style.color = Color.green;
        }else{
            PlayerReadyText.text = "Unready";
            PlayerReadyText.style.color = Color.red;
        }
    }
    private void Start(){
        ImageLoad = Callback<AvatarImageLoaded_t>.Create(OnImageLoaded);
    }
    public void SetPlayerValues(){
        PlayerNameText.text = PlayerName;
        ChangeReadyStatus();
        if(!AvatarReceived){GetPlayerIcon();}
    }
    void GetPlayerIcon(){
        int ImageID = SteamFriends.GetLargeFriendAvatar((CSteamID)PlayerSteamID);
        if(ImageID == -1)return;
        PlayerIcon.style.backgroundImage = GetSteamImageAsTexture(ImageID);
    }
    private void OnImageLoaded(AvatarImageLoaded_t callback){
        if(callback.m_steamID.m_SteamID == PlayerSteamID){
            PlayerIcon.style.backgroundImage = GetSteamImageAsTexture(callback.m_iImage);
        }else{
            return;
        }
    }
    private Texture2D GetSteamImageAsTexture(int iImage)
    {
        Texture2D texture = null;

        bool isValid = SteamUtils.GetImageSize(iImage, out uint width, out uint height);
        if (isValid)
        {
            byte[] image = new byte[width * height * 4];

            isValid = SteamUtils.GetImageRGBA(iImage, image, (int)(width * height * 4));

            if (isValid)
            {
                texture = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false, true);
                texture.LoadRawTextureData(image);
                texture.Apply();
            }
        }
        AvatarReceived = true;
        return texture;
    }
}
