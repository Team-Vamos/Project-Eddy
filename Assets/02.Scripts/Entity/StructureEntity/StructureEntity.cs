using DG.Tweening;
using UnityEngine;

public class StructureEntity : Entity, IDamageTaker
{
    
    public delegate void OnDamageTakenDelegate(float damage);
    public delegate void OnDestroyedDelegate();
    
    public OnDamageTakenDelegate OnDamageTaken;
    public OnDestroyedDelegate OnDestroyed;
    
    [SerializeField] private Renderer[] _renderers;
    
    public float Health { get; protected set; }
    
    protected StructureEntity(float health)
    {
        Health = health;
    }

    public virtual void TakeDamage(float damage)
    {
        foreach (var bodyRenderer in _renderers)
        {
            bodyRenderer.material.DOColor(Color.red, 0.1f);
            bodyRenderer.material.DOColor(Color.white, 0.1f).SetDelay(0.1f);
        }
        
        Health -= damage;
        OnDamageTaken?.SafeInvoke(damage);
        
        if (Health <= 0)
        {
            OnDestroyed?.SafeInvoke();
        }
    }
}