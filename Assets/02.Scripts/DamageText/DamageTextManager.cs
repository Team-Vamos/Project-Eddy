using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageTextManager : MonoBehaviour
{
    [SerializeField]
    private DamageText _textPrefab;

    [SerializeField]
    private float _textHeight = 1f;

    [SerializeField]
    private float _animationDuration = 0.8f;
    [SerializeField]
    private Color _criticalColor;
    [SerializeField]
    private Color _defaultColor;


    public void ShowDamageText(Vector3 position, bool isCritical = false)
    {
        TMP_Text text = Instantiate(_textPrefab, position, Quaternion.identity).Text;

        if(isCritical)
            text.color = _criticalColor;
        else
            text.color = _defaultColor;

        text.DOFade(0f, _animationDuration);

        text.transform.DOLocalMoveY(_textHeight, _animationDuration).OnComplete(()=>{
            text.transform.position = Vector3.zero;
        });
    }

}
