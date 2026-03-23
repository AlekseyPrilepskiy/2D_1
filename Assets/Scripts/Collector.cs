using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            coin.Collect();
        }

        if (other.TryGetComponent(out HealthPack pack) && TryGetComponent(out Health Health))
        {
            pack.Collect(Health);
        }
    }
}
