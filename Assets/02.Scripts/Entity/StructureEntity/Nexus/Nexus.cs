using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 글로벌로 동작하는 스크립트 입니다.
/// </summary>
public class Nexus : StructureEntity
{
    #region Events

    public delegate void NexusCreatedDelegate();

    public delegate void NexusDamagedDelegate(float damage);

    public delegate void NexusDestroyedDelegate();

    public delegate void NexusUpgradedDelegate();

    public static event NexusCreatedDelegate OnNexusCreated;
    public static event NexusDamagedDelegate OnNexusDamaged;
    public static event NexusDestroyedDelegate OnNexusDestroyed;

    public static event NexusUpgradedDelegate OnNexusUpgraded;

    #endregion

    public static Nexus Instance { get; private set; }
    
    [SerializeField] private ResourceStorage resourceStorage;
    public float interactRange = 3f;
    public ResourceStorage ResourceStorage => resourceStorage;

    public Nexus() : base(1000)
    {
        OnDestroyed += () => OnNexusDestroyed?.SafeInvoke();
    }

    private void Awake()
    {
        OnNexusCreated?.SafeInvoke();
        Instance = this;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        OnNexusDamaged?.SafeInvoke(damage);
    }

    public void Upgrade()
    {
        // TODO: Upgrade the nexus
        Debug.Log("업그레이드 시도 ( 아직 기능 미완 )");
        OnNexusUpgraded?.SafeInvoke();
    }

    public bool CanUpgrade => true;
}