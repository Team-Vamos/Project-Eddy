using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

// TODO: Monobehaviour -> Entity
public class Ore : MonoBehaviour, IDamageTaker
{
    [SerializeField]
    private float _oreGauge = 100;

    [SerializeField]
    private int _oreHp = 5;

    [SerializeField]
    private OreSpawner _oreSpawner;

    [SerializeField]
    private Vector2 _uiOffset = new Vector2(0f, -100f);

    private VisualElement _gaugeBar;
    private VisualElement _bar;

    private SpriteRenderer _spriteRenderer;
    private float _currentOreGauge;
    private int _currentOreHp;


    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (_spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is Null");
        }
    }

    private void Start()
    {
        _currentOreGauge = _oreGauge;
        _currentOreHp = _oreHp;
    }

    private void OnDisable()
    {
        _oreSpawner.Root.Remove(_gaugeBar);
    }

    private void LateUpdate()
    {
        Vector3 uiPos = RuntimePanelUtils.CameraTransformWorldToPanel(_oreSpawner.Root.panel, transform.position, Define.MainCam);

        _gaugeBar.style.left = uiPos.x - _gaugeBar.layout.width * 0.5f + _uiOffset.x;
        _gaugeBar.style.top = uiPos.y + _gaugeBar.layout.height + _uiOffset.y;
    }

    public void SetGaugeBar(VisualElement gaugeBar)
    {
        _gaugeBar = gaugeBar;
        _bar = gaugeBar.Q<VisualElement>("Bar");
        Debug.Log(gaugeBar.style.transitionDuration);
        Debug.Log(_bar.style.transitionDuration);
    }

    public void TakeDamage(float damage)
    {
        _currentOreGauge -= damage;
        if (_currentOreGauge <= 0)
        {
            _currentOreHp--;

            _oreSpawner.SpawnHoldOre(transform.position);
            // 원석 소환
            if (_currentOreHp <= 0)
            {
                // TODO: 풀링
                gameObject.SetActive(false);
            }
            else
            {
                // 스프라이트 변경
                _spriteRenderer.sprite = _oreSpawner.OreSprites[_currentOreHp - 1];
                _currentOreGauge = _oreGauge;
            }
        }
        _bar.style.width = new Length(_currentOreGauge / _oreGauge * 100f, LengthUnit.Percent);
    }

    // private void ChangeSprite()
    // {
    //     _spriteRenderer.sprite = OreManager.Instance.OreSprites[_currentOreHp - 1];

    // }

    // public void SetHealth(Attack attack)
    // {
    // }

    // [SerializeField]
    // private float _oreGauge;
}