using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplySpawner : MonoBehaviour
{
    [Header("보급 떨어지는 시간")]
    [SerializeField]
    private float _supplyMinTime;
    [SerializeField]
    private float _supplyMaxTime;

    [Header("총 몇초동안 떨어질 것인가")]
    [SerializeField]
    private float _dropDuration = 4f;

    [Header("그 중 박스는 몇초만에 떨어질 것인가")]
    [SerializeField]
    private float _boxDropDuration = 0.2f;

    [Header("그림자 시작 알파")]
    [SerializeField]
    private float _shadowStartAlpha = 0.2f;

    [Header("그림자 도착 알파")]
    [SerializeField]
    private float _shadowEndAlpha = 0.9f;

    [Header("상자 높이")]
    [SerializeField]
    private float _supplyBoxHeight;

    [Header("그림자 시작 스케일")]
    [SerializeField]
    private Vector3 _shadowStartScale = new Vector3(0.56f, 0.0765625f, 0.56f);

    [Header("그림자 최종 스케일")]
    [SerializeField]
    private Vector3 _shadowEndScale = new Vector3(2.56f, 0.35f, 2.56f);

    [SerializeField]
    private Supply _testObject;

    [SerializeField]
    private List<Bound> _bounds;

    private Coroutine _supplyCoroutine;

    private void Start()
    {
        StartSupply();
    }

    public void StopSupply()
    {
        StopCoroutine(_supplyCoroutine);
    }

    public void StartSupply()
    {
        _supplyCoroutine = StartCoroutine(SpawnSupply());
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        foreach (var bound in _bounds)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(bound.Center, bound.Size);
        }
#endif
    }

    /// <summary>
    /// 보급 소환하는 함수
    /// </summary>
    private IEnumerator SpawnSupply()
    {
        // TODO: 풀링
        while (true)
        {
            float random = Random.Range(_supplyMinTime, _supplyMaxTime);

            Supply supply = Instantiate(_testObject);
            // _testObject.gameObject.SetActive(true);
            supply.gameObject.SetActive(true);
            supply.transform.position = Define.GetRandomBound().GetRandomPos();
            supply.Init(_dropDuration, _shadowStartAlpha, _shadowEndAlpha, _boxDropDuration, _supplyBoxHeight, _shadowStartScale, _shadowEndScale);
            yield return Yields.WaitForSeconds(_dropDuration);
            yield return Yields.WaitForSeconds(random);
            // supply.gameObject.SetActive(false);

            // TODO: 나중에 아이템을 먹으면 다시 초가 가도록 설정

        }
    }

    
}
