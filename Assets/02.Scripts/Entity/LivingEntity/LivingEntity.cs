using DG.Tweening;
using Mirror;
using UnityEngine;

public class LivingEntity : Entity, IDamageTaker
{
    public delegate void OnDamageTakenDelegate(float damage);

    public delegate void OnDeathDelegate();

    public OnDamageTakenDelegate OnDamageTaken;
    public OnDeathDelegate OnDeath;

    [SerializeField] private Renderer[] renderers;
    public Renderer[] Renderers => renderers;

    [field: SyncVar] public float Health { get; protected set; }

    public virtual void TakeDamage(float damage)
    {
        foreach (var bodyRenderer in renderers)
        {
            bodyRenderer.material.DOColor(Color.red, 0.1f);
            bodyRenderer.material.DOColor(Color.white, 0.1f).SetDelay(0.1f);
        }

        Health -= damage;
        OnDamageTaken?.SafeInvoke(damage);

        if (Health <= 0) OnDeath?.SafeInvoke();
    }

    protected LivingEntity(EntityType entityType) : base(entityType)
    {
    }
}