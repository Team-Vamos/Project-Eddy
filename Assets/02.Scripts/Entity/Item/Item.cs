using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Renderer[] renderers;

    private ItemHolder _holder;

    public void SetHolder(ItemHolder holder)
    {
        _holder = holder;
    }

    public void ClearHolder()
    {
        _holder = null;
    }

    public bool IsHolding()
    {
        return _holder != null;
    }
}