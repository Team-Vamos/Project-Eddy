using Mirror;
using UnityEngine;

public partial class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private GameObject nexusPrefab;

    private void Awake()
    {
        // SpawnNexus();
    }

    private void SpawnNexus()
    {
        var nexusObject = Instantiate(nexusPrefab);
        NetworkServer.Spawn(nexusObject);
    }
}