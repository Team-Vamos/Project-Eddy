using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerToolInfo
{
    private VisualElement _image;
    private Label _levelText;

    public PlayerToolInfo(VisualElement image)
    {
        _image = image;
    }

    public void SetImage(Sprite image)
    {
        _image.style.backgroundImage = new StyleBackground(image);
    }

    public void SetLevelText(string text)
    {
        _levelText.text = text;
    }
}
