using UnityEngine;

public class UIFor<T> : MonoBehaviour where T : MonoBehaviour
{
    protected T Target { get; private set; }

    public void SetTarget(T target)
    {
        Target = target;
    }
}