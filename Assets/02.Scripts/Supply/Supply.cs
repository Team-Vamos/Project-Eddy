using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Supply : MonoBehaviour
{
    // TODO: 아이템을 가지고 있어야함, SetItem필요
    // TODO: 풀링 필요
    [SerializeField]
    private SupplySpawner _spawner;
    
    private float _boxHeight;

    private SpriteRenderer _shadow;
    private Vector3 _resetScale;

    private Color _resetColor;
    private Transform _supplyBox;

    private void Awake() {
        _supplyBox = transform.Find("SupplyBox");
        _shadow = transform.Find("Shadow").GetComponent<SpriteRenderer>();
        _resetScale = _shadow.transform.localScale;
        _shadow.color = _resetColor;

    }

    private void OnDisable() {
        Vector3 pos = _supplyBox.transform.position;
        pos.y = _boxHeight;
        _supplyBox.transform.position = pos;

        _shadow.color = _resetColor;

        _shadow.transform.localScale = _resetScale;
    }

    public void Init(float dropDuration,float startAlpha ,float endAlpha, float boxDropDuration, float boxHeight,Vector3 shadowStartScale ,Vector3 shadowEndScale)
    {
        _boxHeight = boxHeight;
        Color c = _shadow.color;
        c.a = startAlpha;
        _shadow.color = c;

        _shadow.transform.localScale = shadowStartScale;

        _shadow.DOFade(endAlpha, dropDuration);//.OnComplete(()=>StartCoroutine(ActiveFalse()));
        _shadow.transform.DOScale(shadowEndScale, dropDuration);
        _supplyBox.DOLocalMoveY(0f, boxDropDuration).SetDelay(dropDuration - boxDropDuration);//.SetEase(Ease.InQuart);
    }

    private IEnumerator ActiveFalse()
    {
        yield return null;
        gameObject.SetActive(false);
    }

}
