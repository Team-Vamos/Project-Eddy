using System;
using UnityEngine;
using UnityEngine.Serialization;

public class NexusInteract : MonoBehaviour
{
    private ItemHolder _itemHolder;

    private void Awake()
    {
        _itemHolder = GetComponent<ItemHolder>();
        _itemHolder.OnItemRemoved += OnItemRemoved;
    }

    private void OnItemRemoved(Item item)
    {
        switch (item)
        {
            case Resource resource:
                Nexus.Instance.ResourceStorage.AddResource(resource.amount);
                PoolManager.Destroy(resource.gameObject);
                break;
            case Chest chest:
                // TODO: chest open implementation
                PoolManager.Destroy(chest.gameObject);
                break;
        }
    }
}

internal class Chest : Item
{
}

internal class Resource : Item
{
    public int amount = 1;
}