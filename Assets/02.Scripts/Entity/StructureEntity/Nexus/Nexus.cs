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
    
    public Nexus() : base(1000)
    {
        OnDestroyed += () => OnNexusDestroyed?.SafeInvoke();
    }
    
    private void Awake()
    {
        OnNexusCreated?.SafeInvoke();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        
        OnNexusDamaged?.SafeInvoke(damage);
    }
    
    public void Upgrade()
    {
        // TODO: Upgrade the nexus
        OnNexusUpgraded?.SafeInvoke();
    }
}