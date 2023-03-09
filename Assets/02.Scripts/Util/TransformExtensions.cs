using UnityEngine;

public static class TransformExtensions
{
    public static void LookAt2D(this Transform transform, Vector2 target)
    {
        var dir = target - (Vector2) transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}