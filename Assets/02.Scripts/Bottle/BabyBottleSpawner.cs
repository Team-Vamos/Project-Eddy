using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card;
using DG.Tweening;

public class BabyBottleSpawner : MonoBehaviour
{
    [Header("안 쪽 원의 반지름")]
    [SerializeField]
    private float _babyBottleInnerRadius;

    [Header("안 쪽 원과 바깥쪽 원 사이의 거리")]
    [SerializeField]
    private float _babyBottleOutterRadius;

    [Header("원의 중심 위치")]
    [SerializeField]
    private Vector2 _babyBottleOffset;
    [SerializeField]
    private float _babyBottleScaleDuration = 0.7f;

    [SerializeField]
    private Vector3 _babyBottleSpawnScale = Vector3.one;

    [SerializeField]
    private BabyBottle _bottlePrefab;
    public BabyBottle BottlePrefab => _bottlePrefab;

#if UNITY_EDITOR

    [SerializeField]
    private bool _isDebug = true;

    private void OnDrawGizmos()
    {
        if (!_isDebug) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere((Vector2)transform.position + _babyBottleOffset, _babyBottleInnerRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + _babyBottleOffset, _babyBottleInnerRadius + _babyBottleOutterRadius);
    }

#endif

    public void SpawnBabyBottle(CardController controller)
    {
        BabyBottle babyBottle = Instantiate(BottlePrefab);

        Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        float distance = Random.Range(0f, _babyBottleOutterRadius) + _babyBottleInnerRadius;

        babyBottle.transform.position = _babyBottleOffset + dir * distance;
        babyBottle.gameObject.SetActive(true);
        babyBottle.transform.DOScale(_babyBottleSpawnScale, _babyBottleScaleDuration).SetEase(Ease.InOutElastic);

        babyBottle.SetValue(controller);
    }
    
}
