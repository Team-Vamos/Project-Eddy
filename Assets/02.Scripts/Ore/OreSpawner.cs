using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static Yields;
using UnityEngine.UIElements;

#if UNITY_EDITOR

[System.Serializable]
public class CheckOre
{
    public string description = "이 클래스는 디버깅용입니다";
    public Ore rangeCheckOre;
    public float rangeCheckDuration;

    public bool isChecking;
}
#endif


public class OreSpawner : MonoBehaviour
{
    [Header("OreSprites")]
    [SerializeField]
    private List<Sprite> _oreSprites;
    public IReadOnlyList<Sprite> OreSprites => _oreSprites;

    [SerializeField]
    private GameObject _holdOrePrefab;

    [SerializeField]
    private float _holdOreSpawnDuration = 0.7f;
    [SerializeField]
    private float _holdOreSpawnInnerRange = 1;
    [SerializeField]
    private float _holdOreSpawnOuterRange = 1.5f;
    [SerializeField]
    private Vector3 _holdOreSpawnScale = Vector3.one;

    [Header("Ore")]
    [SerializeField]
    private List<Bound> _bounds;

    [SerializeField]
    private Ore _orePrefab;
    [SerializeField]
    private int _oreSpawnCount = 3;
    [SerializeField]
    private float _oreSpawnDuration = 0.7f;
    [SerializeField]
    private Vector3 _oreSpawnScale = Vector3.one;
    [SerializeField]
    private VisualTreeAsset _oreGaugeBarPrefab;

    public VisualElement Root { get; private set; }

    private List<Ore> _oreList = new List<Ore>();


#if UNITY_EDITOR
    [Space]

    [SerializeField]
    private CheckOre _debugOre;
    [SerializeField]
    private float _testDamage = 30f;

    private Coroutine _checkOreCoroutine;
#endif

    public void SetRootVisualElement(VisualElement root)
    {
        Root = root;
    }

    public void SpawnOre()
    {
        for (int i = 0; i < _oreSpawnCount; ++i)
        {
            Ore g = Instantiate(_orePrefab, Define.GetRandomBound(_bounds.ToArray()).GetRandomPos(), Quaternion.identity);
            g.gameObject.SetActive(true);

            g.transform.localScale = Vector3.zero;

            g.transform.DOScale(_oreSpawnScale, _oreSpawnDuration).SetEase(Ease.InOutElastic);

            VisualElement gaugeBar = _oreGaugeBarPrefab.Instantiate().Q<VisualElement>("GaugeBar");
            
            
            Root.Add(gaugeBar);

            g.SetGaugeBar(gaugeBar);

            _oreList.Add(g);

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            RemoveOres();
            SpawnOre();
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            foreach(var ore in _oreList)
            {
                Debug.Log("데미지 줌");
                #if Editor
                ore.TakeDamage(_testDamage);
                #endif
            }
        }

    }


    public void RemoveOres()
    {
        _oreList.ForEach(x => x.gameObject.SetActive(false));
        _oreList.Clear();
    }

    public void SpawnHoldOre(Vector2 pos)
    {
        GameObject holdOre = Instantiate(_holdOrePrefab);

        Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        float distance = Random.Range(0f, _holdOreSpawnOuterRange) + _holdOreSpawnInnerRange;

        holdOre.transform.position = (Vector2)pos + dir * distance;

        holdOre.SetActive(true);
        holdOre.transform.DOScale(_holdOreSpawnScale, _holdOreSpawnDuration).SetEase(Ease.InOutElastic);

    }

#if UNITY_EDITOR

    [ContextMenu("광석 드롭 범위 확인")]
    private void CheckSpawnRange()
    {
        if (_debugOre.rangeCheckOre == null)
        {
            Debug.LogError("범위를 확인할 광석을 넣지 않았습니다. 범위를 확인할 오브젝트를 매니저에 넣어주세요");
            return;
        }

        _checkOreCoroutine = StartCoroutine(CheckOreRange());
    }

    [ContextMenu("범위 확인 강제 종료")]
    private void StopDebugingCheckOre()
    {
        if (_checkOreCoroutine != null)
        {
            StopCoroutine(_checkOreCoroutine);
        }
        _debugOre.isChecking = false;
    }

    private IEnumerator CheckOreRange()
    {
        _debugOre.isChecking = true;
        yield return WaitForSeconds(_debugOre.rangeCheckDuration);
        _debugOre.isChecking = false;
    }

    private void OnDrawGizmos()
    {
        if (_debugOre.isChecking)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(_debugOre.rangeCheckOre.transform.position, _holdOreSpawnOuterRange + _holdOreSpawnInnerRange);
            Gizmos.DrawWireSphere(_debugOre.rangeCheckOre.transform.position, _holdOreSpawnInnerRange);

            Gizmos.color = Color.cyan;
            foreach (var bound in _bounds)
            {
                Gizmos.DrawWireCube(bound.Center, bound.Size);
            }
        }
    }

#endif


}
