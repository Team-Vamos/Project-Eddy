using System;
using UnityEngine;

public class NexusUI : MonoBehaviour
{
    private Nexus _nexus;

    [SerializeField] private NexusHpUI hpBar;
    [SerializeField] private NexusUpgradeUI upgradeUI;
    [SerializeField] private GlobalNexusHpUI globalHpBar;

    private void Awake()
    {
        _nexus = GetComponentInParent<Nexus>();

        hpBar.SetTarget(_nexus);
        upgradeUI.SetTarget(_nexus);
        globalHpBar.SetTarget(_nexus);
    }

    private void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        _nexus ??= GetComponentInParent<Nexus>();
#endif

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _nexus.interactRange);
    }
}