using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
     [SerializeField]
    private List<Bound> _spawnBounds;

    [SerializeField]
    private List<BaseNPC> _npcPrefabs;

#if UNITY_EDITOR
    [SerializeField]
    private bool _isDebug = true;

    private List<Vector3> _gizmos = new List<Vector3>();
#endif

    private List<Bound> _tempBounds;


    private void Start() {
        SpawnNPC();
    }

    [ContextMenu("렌덤 이동")]
    public void SpawnNPC()
    {
        _tempBounds = new List<Bound>(_spawnBounds);
        Bound randomBound = Define.GetRandomBound(_spawnBounds.ToArray());
        Bound oppositeBound = GetOppositeBound(randomBound);

        Vector3 startPos = randomBound.GetRandomPos();
        Vector3 destination = oppositeBound.GetRandomPos();

        BaseNPC npc = Instantiate(_npcPrefabs[Random.Range(0, _npcPrefabs.Count)], startPos, Quaternion.identity);
        npc.gameObject.SetActive(true);
        npc.Move(startPos);
        npc.SetDestination(destination);

#if UNITY_EDITOR
        _gizmos.Clear();
        _gizmos.Add(startPos);
        _gizmos.Add(destination);

#endif

    }

    private Bound GetOppositeBound(Bound bound)
    {
        // 가까운 순서로 정렬
        _tempBounds.Sort((b1, b2) =>
        (bound.Center - b1.Center).sqrMagnitude.CompareTo((bound.Center - b2.Center).sqrMagnitude));

        return _tempBounds[_tempBounds.Count - 1];
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (!_isDebug) return;
        Gizmos.color = Color.gray;

        foreach (var gizmo in _gizmos)
        {
            Gizmos.DrawSphere(gizmo, 0.3f);
        }

        Gizmos.color = Color.yellow;
        foreach (var bound in _spawnBounds)
        {
            Gizmos.DrawWireCube(bound.Center, bound.Size);
        }
#endif
    }
}