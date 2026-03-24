using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    public Transform Find(Vector2 origin, float radius, LayerMask mask)
    {
        Collider2D collider = Physics2D.OverlapCircle(origin, radius, mask);
        return collider != null ? collider.transform : null;
    }
}
