using System;
using UnityEngine;

public class NexusUI : MonoBehaviour
{
    public float range = 5f;
    
    private Nexus _nexus;
    
    [SerializeField] private NexusHpUI hpBar;
    [SerializeField] private NexusUpgradeUI upgradeUI;
    [SerializeField] private GlobalNexusHpUI globalHpBar;
    
    [SerializeField] private float uiRange;

    private void Awake()
    {
        _nexus = GetComponentInParent<Nexus>();
        
        hpBar.SetTarget(_nexus);
        upgradeUI.SetTarget(_nexus);
        globalHpBar.SetTarget(_nexus);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}