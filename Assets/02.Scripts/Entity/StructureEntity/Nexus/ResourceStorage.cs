using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    [SerializeField] private int maxResource;
    [SerializeField] private int currentResource;
    
    public int MaxResource => maxResource;
    public int CurrentResource => currentResource;
    public bool IsFull => currentResource == maxResource;
    
    public void AddResource(int amount)
    {
        currentResource = Mathf.Clamp(currentResource + amount, 0, maxResource);
    }
    
    public void RemoveResource(int amount)
    {
        currentResource = Mathf.Clamp(currentResource - amount, 0, maxResource);
    }
}