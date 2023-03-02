using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    #region Events

    public delegate void OnItemAddedDelegate(Item item);

    public delegate void OnItemRemovedDelegate(Item item);

    public event OnItemAddedDelegate OnItemAdded;
    public event OnItemRemovedDelegate OnItemRemoved;

    #endregion

    [SerializeField] private Transform holdingPoint;
    [SerializeField] private float heightPerItem = 0.5f;
    [SerializeField] private float pickupRange = 1f;
    [SerializeField] private LayerMask pickupLayerMask;
    [SerializeField] private float dropHoldTime = 1f;

    private readonly Stack<Item> _itemStack = new();

    private void AddItem(Item item)
    {
        // TODO: Add pickup animation
        _itemStack.Push(item);
        item.transform.position = holdingPoint.position + Vector3.up * (heightPerItem * _itemStack.Count);
        item.transform.SetParent(holdingPoint);
        item.SetHolder(this);
        OnItemAdded?.SafeInvoke(item);
    }

    private Item RemoveItem()
    {
        // TODO: Add drop animation
        if (_itemStack.Count == 0)
        {
            return null;
        }

        var item = _itemStack.Pop();
        item.transform.SetParent(null);
        item.ClearHolder();
        OnItemRemoved?.SafeInvoke(item);

        return item;
    }

    private bool HasItem()
    {
        return _itemStack.Count > 0;
    }

    private float _lastTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _lastTime = Time.time;
        }

        if (Time.time - _lastTime < dropHoldTime)
        {
            if (!Input.GetKeyUp(KeyCode.F)) return;
            _lastTime = 0f;

            PickupItem();
        }
        else if (_lastTime != 0f)
        {
            DropItem();
        }
    }

    private readonly Collider2D[] _results = new Collider2D[10];

    private void PickupItem()
    {
        // TODO: GC를 줄이기 위해 재사용할 수 있는 배열을 만들어서 사용
        var size = Physics2D.OverlapCircleNonAlloc(transform.position, pickupRange, _results, pickupLayerMask);
        if (size == 0)
        {
            return;
        }

        var closestItem = _results[0];
        var closestDistance = Vector3.Distance(transform.position, closestItem.transform.position);
        for (var i = 1; i < size; i++)
        {
            var distance = Vector3.Distance(transform.position, _results[i].transform.position);
            if (distance < closestDistance && !_results[i].GetComponent<Item>().IsHolding())
            {
                closestItem = _results[i];
                closestDistance = distance;
            }
        }

        var item = closestItem.GetComponent<Item>();
        if (item == null || item.IsHolding())
        {
            return;
        }

        AddItem(item);
    }

    private void DropItem()
    {
        if (!HasItem())
        {
            return;
        }

        var item = RemoveItem();
        item.transform.position = transform.position + Vector3.up;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}