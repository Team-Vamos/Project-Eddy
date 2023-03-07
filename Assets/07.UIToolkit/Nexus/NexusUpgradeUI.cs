using System;
using UnityEngine;
using UnityEngine.UI;

public class NexusUpgradeUI : UIFor<Nexus>
{
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Text upgradeCostText;

    private void Awake()
    {
        upgradeUI.SetActive(false);
    }

    private void ShowUpgradeUI()
    {
        upgradeCostText.text = $"{Target.ResourceStorage.CurrentResource} / {Target.ResourceStorage.MaxResource}";
        upgradeUI.SetActive(true);
    }

    private void HideUpgradeUI()
    {
        upgradeUI.SetActive(false);
    }

    private void Update()
    {
        if (LocalPlayer.Instance == null)
            return;
        
        var localPlayerPos = LocalPlayer.Instance.transform.position;
        if (Vector2.Distance(localPlayerPos, Target.transform.position) <= Target.interactRange)
        {
            ShowUpgradeUI();

            if (Input.GetKeyDown(KeyCode.R))
                Target.Upgrade();
        }
        else
        {
            HideUpgradeUI();
        }
    }
}